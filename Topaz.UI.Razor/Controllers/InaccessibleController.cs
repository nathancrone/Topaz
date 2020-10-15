using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.FileIO;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Topaz.Common;
using Topaz.Common.Enums;
using Topaz.Common.Models;
using Topaz.Data;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InaccessibleController : ControllerBase
    {
        private static readonly ContactActivityTypeEnum[] phoneActivity = {
            ContactActivityTypeEnum.PhoneWithoutVoicemail,
            ContactActivityTypeEnum.PhoneWithVoicemail
        };

        private static readonly PhoneReponseTypeEnum[] phoneCheck = {
            PhoneReponseTypeEnum.VoicemailBusiness,
            PhoneReponseTypeEnum.AnsweredRespondedFavorably,
            PhoneReponseTypeEnum.AnsweredHangUpAfterListening,
            PhoneReponseTypeEnum.AnsweredNotInterested,
            PhoneReponseTypeEnum.AnsweredDoNotContact,
            PhoneReponseTypeEnum.AnsweredProfanityOrThreatening,
            PhoneReponseTypeEnum.AnsweredNoEnglish,
            PhoneReponseTypeEnum.AnsweredBusiness
        };

        private static readonly PhoneReponseTypeEnum[] voicemailCheck = {
            PhoneReponseTypeEnum.VoicemailNoName,
            PhoneReponseTypeEnum.VoicemaiNameMatches,
            PhoneReponseTypeEnum.VoicemailDifferentName,
            PhoneReponseTypeEnum.VoicemailBusiness,
            PhoneReponseTypeEnum.AnsweredRespondedFavorably,
            PhoneReponseTypeEnum.AnsweredNotGoodTime,
            PhoneReponseTypeEnum.AnsweredHangUpAfterListening,
            PhoneReponseTypeEnum.AnsweredNotInterested,
            PhoneReponseTypeEnum.AnsweredDoNotContact,
            PhoneReponseTypeEnum.AnsweredProfanityOrThreatening,
            PhoneReponseTypeEnum.AnsweredNoEnglish,
            PhoneReponseTypeEnum.AnsweredBusiness
        };

        private static readonly PhoneReponseTypeEnum[] doNotContact = {
            PhoneReponseTypeEnum.AnsweredDoNotContact,
            PhoneReponseTypeEnum.AnsweredProfanityOrThreatening
        };

        private static readonly Regex regexPhoneNumber = new Regex(@"\D");
        private static readonly Regex regexMailingAddress1 = new Regex(@"^(\w+)\s(.*)");
        private static readonly Regex regexMailingAddress2 = new Regex(@"\w+$");


        private readonly Topaz.Data.TopazDbContext _context;
        private readonly ILogger<InaccessibleController> _logger;

        public InaccessibleController(Topaz.Data.TopazDbContext context, ILogger<InaccessibleController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        public IEnumerable<Object> GetCurrentTerritory()
        {
            var Claims = (ClaimsIdentity)this.User.Identity;
            var PublisherId = int.Parse(Claims.FindFirst("PublisherId").Value);

            return _context.TerritoryActivities
                .Where(x => x.PublisherId == PublisherId && x.CheckInDate == null && x.InaccessibleTerritory != null)
                .Select(x => new
                {
                    x.TerritoryActivityId,
                    x.InaccessibleTerritory.TerritoryId,
                    x.InaccessibleTerritory.TerritoryCode,
                    StreetTerritoryId = x.InaccessibleTerritory.StreetTerritory.TerritoryId,
                    StreetTerritoryCode = x.InaccessibleTerritory.StreetTerritory.TerritoryCode,
                    x.CheckOutDate
                })
                .AsNoTracking()
                .ToList();
        }

        [HttpGet]
        [Route("[action]/{take?}")]
        public IEnumerable<Object> GetAvailableTerritory(int? take = null)
        {
            var LinqResult = _context.InaccessibleTerritories.Where(x => !x.InActive && !x.Activity.Any());

            LinqResult = LinqResult.Union(_context.InaccessibleTerritories.Where(x => !x.InActive && !x.Activity.Any(y => y.CheckInDate == null)));

            return LinqResult
                .Select(x => new
                {
                    x.TerritoryId,
                    x.TerritoryCode,
                    StreetTerritoryId = x.StreetTerritory.TerritoryId,
                    StreetTerritoryCode = x.StreetTerritory.TerritoryCode,
                    CheckInDate = x.Activity.Max(y => y.CheckInDate)
                })
                .OrderBy(x => x.CheckInDate)
                .Take(take ?? 3)
                .AsNoTracking()
                .ToList();
        }

        [HttpGet]
        [Route("[action]/{id}/{type}")]
        public IEnumerable<Object> GetAvailableAssignments(int id, string type)
        {
            var Assignments = _context.InaccessibleProperties
                .Include(x => x.ContactLists)
                .ThenInclude(x => x.Contacts)
                .Include(x => x.ContactLists)
                .ThenInclude(x => x.Contacts)
                .ThenInclude(x => x.AssignPublisher)
                .Include(x => x.ContactLists)
                .ThenInclude(x => x.Contacts)
                .ThenInclude(x => x.PhoneType)
                .Where(x => x.TerritoryId == id)
                .SelectMany(x => x.ContactLists.Where(y => y.InaccessibleContactListId == x.CurrentContactListId).SelectMany(y => y.Contacts))
                .AsNoTracking();

            // list of phone numbers
            var territoryPhoneNumbers = Assignments
                .Where(x => !string.IsNullOrEmpty(x.PhoneNumber)).AsNoTracking().ToList()
                .Select(x => new { x.InaccessibleContactId, PhoneNumber = regexPhoneNumber.Replace(x.PhoneNumber, "") });

            // compare phone numbers to the do not contact phone list
            var territoryDoNotContactPhoneNumbers = _context.DoNotContactPhones.Where(x => territoryPhoneNumbers.Select(y => y.PhoneNumber).Contains(x.PhoneNumber)).Select(x => x.PhoneNumber).ToList();
            var territoryDoNotContactPhoneContactIds = territoryPhoneNumbers.Where(x => territoryDoNotContactPhoneNumbers.Contains(x.PhoneNumber)).Select(x => x.InaccessibleContactId);

            // get this territory's associated street territory
            var streetTerritoryId = _context.InaccessibleProperties.Where(x => x.TerritoryId == id).Select(x => x.Territory.StreetTerritoryId).FirstOrDefault();

            // list of this territory's mailing addresses
            var territoryMailingAddresses = Assignments
                .Where(x => !string.IsNullOrEmpty(x.MailingAddress1))
                .Select(x => new { x.InaccessibleContactId, x.MailingAddress1, x.MailingAddress2 }).AsNoTracking().ToList();

            // list of 'do not contact' addresses for this territories associated street territory
            var territoryDoNotContactMailingAddresses = _context.DoNotContactLetters.Where(x => x.TerritoryId == streetTerritoryId).Select(x => new { x.MailingAddress1, x.MailingAddress2 }).AsNoTracking().ToList();

            // compare the mailing addresses to the list of 'do not contact' addresses
            var territoryDoNotContactLetterContactIds = territoryMailingAddresses.Where((x) =>
            {
                if (!regexMailingAddress1.IsMatch(x.MailingAddress1))
                    return false;

                if (!string.IsNullOrEmpty(x.MailingAddress2) && !regexMailingAddress2.IsMatch(x.MailingAddress2))
                    return false;

                return territoryDoNotContactMailingAddresses.Any((y) =>
                {
                    if (!regexMailingAddress1.IsMatch(y.MailingAddress1))
                        return false;

                    var yMailingStreetNumber = regexMailingAddress1.Match(y.MailingAddress1).Groups[1].Value.ToLower();
                    var xMailingStreetNumber = regexMailingAddress1.Match(x.MailingAddress1).Groups[1].Value.ToLower();

                    var yMailingStreetName = Regex.Replace(regexMailingAddress1.Match(y.MailingAddress1).Groups[2].Value, @"\W", "").ToLower();
                    var xMailingStreetName = Regex.Replace(regexMailingAddress1.Match(x.MailingAddress1).Groups[2].Value, @"\W", "").ToLower();

                    var similarityScore = (xMailingStreetName.Length >= yMailingStreetName.Length) ?
                        (double)yMailingStreetName.DamerauLevenshteinDistanceTo(xMailingStreetName) / xMailingStreetName.Length :
                        (double)xMailingStreetName.DamerauLevenshteinDistanceTo(yMailingStreetName) / yMailingStreetName.Length;

                    if (string.IsNullOrEmpty(x.MailingAddress2) && string.IsNullOrEmpty(y.MailingAddress2))
                        return similarityScore <= .35 && yMailingStreetNumber == xMailingStreetNumber;

                    if (!regexMailingAddress2.IsMatch(y.MailingAddress2))
                        return false;

                    var yMailingAddress2 = regexMailingAddress2.Match(y.MailingAddress2).Value.ToLower();
                    var xMailingAddress2 = regexMailingAddress2.Match(x.MailingAddress2).Value.ToLower();

                    return similarityScore <= .35 && yMailingStreetNumber == xMailingStreetNumber && yMailingAddress2 == xMailingAddress2;
                });
            }).Select(x => x.InaccessibleContactId);

            var FilteredAssignments = Assignments;
            if (type == "phone")
            {
                FilteredAssignments = FilteredAssignments.Where(x =>
                    // is not a 'do not contact' phone
                    !territoryDoNotContactPhoneContactIds.Contains(x.InaccessibleContactId) &&
                    // has phone number
                    !string.IsNullOrEmpty(x.PhoneNumber) &&
                    // phone has not been attempted
                    x.ContactActivity.All(y => !phoneActivity.Contains((ContactActivityTypeEnum)y.ContactActivityTypeId))
                );
            }
            else if (type == "vm")
            {
                FilteredAssignments = FilteredAssignments.Where(x =>
                    // is not a 'do not contact' phone
                    !territoryDoNotContactPhoneContactIds.Contains(x.InaccessibleContactId) &&
                    // has phone number
                    !string.IsNullOrEmpty(x.PhoneNumber) &&
                    // phone has been attempted
                    x.ContactActivity.Any(y => phoneActivity.Contains((ContactActivityTypeEnum)y.ContactActivityTypeId)) &&
                    // there has been no answer
                    x.ContactActivity.Where(y => phoneActivity.Contains((ContactActivityTypeEnum)y.ContactActivityTypeId)).All(y => !phoneCheck.Contains((PhoneReponseTypeEnum)y.PhoneResponseTypeId)) &&
                    // phone with voicemail has not been attempted
                    x.ContactActivity.All(y => y.ContactActivityTypeId != (int)ContactActivityTypeEnum.PhoneWithVoicemail)
                );
            }
            else if (type == "letter")
            {
                FilteredAssignments = FilteredAssignments.Where(x =>
                    // has mailing address
                    !string.IsNullOrEmpty(x.MailingAddress1) &&
                    (
                        // is a 'do not contact' phone, not a 'do not contact' letter, and letter has NOT been sent
                        (
                            territoryDoNotContactPhoneContactIds.Contains(x.InaccessibleContactId) &&
                            !territoryDoNotContactLetterContactIds.Contains(x.InaccessibleContactId)) &&
                            !x.ContactActivity.Any(y => y.ContactActivityTypeId == (int)ContactActivityTypeEnum.Letter
                        ) ||
                        // has no phone number, not a 'do not contact' letter, and letter has NOT been sent
                        (
                            string.IsNullOrEmpty(x.PhoneNumber) &&
                            !territoryDoNotContactLetterContactIds.Contains(x.InaccessibleContactId) &&
                            !x.ContactActivity.Any(y => y.ContactActivityTypeId == (int)ContactActivityTypeEnum.Letter)
                        ) ||
                        (
                            // has phone number
                            !string.IsNullOrEmpty(x.PhoneNumber) &&
                            // phone has been attempted
                            x.ContactActivity.Any(y => phoneActivity.Contains((ContactActivityTypeEnum)y.ContactActivityTypeId)) &&
                            // there has been no answer
                            x.ContactActivity.Where(y => phoneActivity.Contains((ContactActivityTypeEnum)y.ContactActivityTypeId)).All(y => !phoneCheck.Contains((PhoneReponseTypeEnum)y.PhoneResponseTypeId)) &&
                            // phone with voicemail has been attempted
                            x.ContactActivity.Any(y => y.ContactActivityTypeId == (int)ContactActivityTypeEnum.PhoneWithVoicemail) &&
                            // no voicemail left and no answer
                            x.ContactActivity.Where(y => y.ContactActivityTypeId == (int)ContactActivityTypeEnum.PhoneWithVoicemail).All(y => !voicemailCheck.Contains((PhoneReponseTypeEnum)y.PhoneResponseTypeId)) &&
                            // not a 'do not contact' letter
                            !territoryDoNotContactLetterContactIds.Contains(x.InaccessibleContactId) &&
                            // a letter has NOT been sent
                            !x.ContactActivity.Any(y => y.ContactActivityTypeId == (int)ContactActivityTypeEnum.Letter)
                        )
                    )
                );
            }
            else
            {
                FilteredAssignments = FilteredAssignments.Where(x =>
                    // is a 'do not contact' phone and a 'do not contact' letter
                    (territoryDoNotContactPhoneContactIds.Contains(x.InaccessibleContactId) && territoryDoNotContactLetterContactIds.Contains(x.InaccessibleContactId)) ||
                    (
                        // has phone number
                        !string.IsNullOrEmpty(x.PhoneNumber) &&
                        // phone has been attempted
                        x.ContactActivity.Any(y => phoneActivity.Contains((ContactActivityTypeEnum)y.ContactActivityTypeId)) &&
                        // the phone was answered
                        x.ContactActivity.Where(y => phoneActivity.Contains((ContactActivityTypeEnum)y.ContactActivityTypeId)).Any(y => phoneCheck.Contains((PhoneReponseTypeEnum)y.PhoneResponseTypeId))
                    ) ||
                    (
                        // has phone number
                        !string.IsNullOrEmpty(x.PhoneNumber) &&
                        // phone with voicemail has been attempted
                        x.ContactActivity.Any(y => y.ContactActivityTypeId == (int)ContactActivityTypeEnum.PhoneWithVoicemail) &&
                        // either voicemail left or there was an answer
                        x.ContactActivity.Where(y => y.ContactActivityTypeId == (int)ContactActivityTypeEnum.PhoneWithVoicemail).Any(y => voicemailCheck.Contains((PhoneReponseTypeEnum)y.PhoneResponseTypeId))
                    ) ||
                    (
                        // does not have phone number
                        string.IsNullOrEmpty(x.PhoneNumber) &&
                        // is 'do not contact' letter
                        territoryDoNotContactLetterContactIds.Contains(x.InaccessibleContactId)
                    ) ||
                    (
                        // letter has been sent
                        x.ContactActivity.Any(y => y.ContactActivityTypeId == (int)ContactActivityTypeEnum.Letter)
                    )
                );
            }

            var results = FilteredAssignments.ToList();

            // set the do not contact phone flag to true
            results.ForEach((x) =>
            {
                x.DoNotContactPhone = territoryDoNotContactPhoneContactIds.Contains(x.InaccessibleContactId);
                x.DoNotContactLetter = territoryDoNotContactLetterContactIds.Contains(x.InaccessibleContactId);
            });

            return results
                .OrderBy(x => x.MailingAddress1)
                .ThenBy(x => x.MailingAddress2)
                .ThenBy(x => x.LastName);
        }

        [HttpGet]
        [Route("[action]/{id:int}")]
        public IEnumerable<Object> GetContactActivity(int id)
        {
            return _context.InaccessibleContactActivities
                .Where(x => x.InaccessibleContactId == id)
                .Include(x => x.Publisher)
                .Include(x => x.PhoneResponseType)
                .Include(x => x.ContactActivityType)
                .AsNoTracking();
        }

        [HttpGet]
        [Route("[action]")]
        public Object CurrentUserAssignments()
        {
            var Claims = (ClaimsIdentity)this.User.Identity;
            var PublisherId = int.Parse(Claims.FindFirst("PublisherId").Value);

            var Assignments = _context.InaccessibleProperties
                .Include(x => x.ContactLists)
                .ThenInclude(x => x.Contacts)
                .Include(x => x.ContactLists)
                .ThenInclude(x => x.Contacts)
                .ThenInclude(x => x.AssignPublisher)
                .Include(x => x.ContactLists)
                .ThenInclude(x => x.Contacts)
                .ThenInclude(x => x.PhoneType)
                .SelectMany(x => x.ContactLists.Where(y => y.InaccessibleContactListId == x.CurrentContactListId).SelectMany(y => y.Contacts.Where(z => z.AssignPublisherId == PublisherId)))
                .AsNoTracking();

            // list of phone numbers
            var territoryPhoneNumbers = Assignments
                .Where(x => !string.IsNullOrEmpty(x.PhoneNumber)).AsNoTracking().ToList()
                .Select(x => new { x.InaccessibleContactId, PhoneNumber = regexPhoneNumber.Replace(x.PhoneNumber, "") });

            // compare phone numbers to the do not contact phone list
            var territoryDoNotContactPhoneNumbers = _context.DoNotContactPhones.Where(x => territoryPhoneNumbers.Select(y => y.PhoneNumber).Contains(x.PhoneNumber)).Select(x => x.PhoneNumber).ToList();
            var territoryDoNotContactPhoneContactIds = territoryPhoneNumbers.Where(x => territoryDoNotContactPhoneNumbers.Contains(x.PhoneNumber)).Select(x => x.InaccessibleContactId);

            return new
            {
                PhoneWithoutVoicemail = Assignments.Where(x =>
                    // is not a 'do not contact' phone
                    !territoryDoNotContactPhoneContactIds.Contains(x.InaccessibleContactId) &&
                    // has phone number
                    !string.IsNullOrEmpty(x.PhoneNumber) &&
                    // phone has not been attempted
                    x.ContactActivity.All(y => !phoneActivity.Contains((ContactActivityTypeEnum)y.ContactActivityTypeId))
                ).OrderBy(x => x.MailingAddress1).ThenBy(x => x.MailingAddress2).ThenBy(x => x.LastName),
                PhoneWithVoicemail = Assignments.Where(x =>
                    // is not a 'do not contact' phone
                    !territoryDoNotContactPhoneContactIds.Contains(x.InaccessibleContactId) &&
                    // has phone number
                    !string.IsNullOrEmpty(x.PhoneNumber) &&
                    // phone has been attempted
                    x.ContactActivity.Any(y => phoneActivity.Contains((ContactActivityTypeEnum)y.ContactActivityTypeId)) &&
                    // there has been no answer
                    x.ContactActivity.Where(y => phoneActivity.Contains((ContactActivityTypeEnum)y.ContactActivityTypeId)).All(y => !phoneCheck.Contains((PhoneReponseTypeEnum)y.PhoneResponseTypeId)) &&
                    // phone with voicemail has not been attempted
                    x.ContactActivity.All(y => y.ContactActivityTypeId != (int)ContactActivityTypeEnum.PhoneWithVoicemail)
                ).OrderBy(x => x.MailingAddress1).ThenBy(x => x.MailingAddress2).ThenBy(x => x.LastName),
                Letter = Assignments.Where(x =>
                    // has mailing address
                    !string.IsNullOrEmpty(x.MailingAddress1) &&
                    (
                        // is a 'do not contact' phone and letter has NOT been sent
                        (
                            territoryDoNotContactPhoneContactIds.Contains(x.InaccessibleContactId) &&
                            !x.ContactActivity.Any(y => y.ContactActivityTypeId == (int)ContactActivityTypeEnum.Letter)
                        ) ||
                        // has no phone number and letter has NOT been sent
                        (
                            string.IsNullOrEmpty(x.PhoneNumber) &&
                            !x.ContactActivity.Any(y => y.ContactActivityTypeId == (int)ContactActivityTypeEnum.Letter)
                        ) ||
                        (
                            // has phone number
                            !string.IsNullOrEmpty(x.PhoneNumber) &&
                            // phone has been attempted
                            x.ContactActivity.Any(y => phoneActivity.Contains((ContactActivityTypeEnum)y.ContactActivityTypeId)) &&
                            // there has been no answer
                            x.ContactActivity.Where(y => phoneActivity.Contains((ContactActivityTypeEnum)y.ContactActivityTypeId)).All(y => !phoneCheck.Contains((PhoneReponseTypeEnum)y.PhoneResponseTypeId)) &&
                            // phone with voicemail has been attempted
                            x.ContactActivity.Any(y => y.ContactActivityTypeId == (int)ContactActivityTypeEnum.PhoneWithVoicemail) &&
                            // no voicemail left and no answer
                            x.ContactActivity.Where(y => y.ContactActivityTypeId == (int)ContactActivityTypeEnum.PhoneWithVoicemail).All(y => !voicemailCheck.Contains((PhoneReponseTypeEnum)y.PhoneResponseTypeId)) &&
                            // a letter has NOT been sent
                            !x.ContactActivity.Any(y => y.ContactActivityTypeId == (int)ContactActivityTypeEnum.Letter)
                        )
                    )
                ).OrderBy(x => x.MailingAddress1).ThenBy(x => x.MailingAddress2).ThenBy(x => x.LastName)
            };
        }

        [HttpGet]
        [Route("[action]")]
        public IEnumerable<Object> GetPhoneResponseTypes()
        {
            return _context.PhoneResponseTypes.OrderBy(x => x.Name);
        }

        [HttpPost]
        [Route("[action]/{assignee:int}")]
        public int Assign(int assignee, [FromBody] int[] assignments)
        {
            var contacts = _context.InaccessibleContacts.Where(x => assignments.Contains(x.InaccessibleContactId));
            contacts.ToList().ForEach(x =>
            {
                x.AssignPublisherId = assignee;
                x.AssignDate = DateTime.UtcNow;
            });
            return _context.SaveChanges();
        }

        [HttpPost]
        [Route("[action]")]
        public int Unassign([FromBody] int[] assignments)
        {
            var contacts = _context.InaccessibleContacts.Where(x => assignments.Contains(x.InaccessibleContactId));
            contacts.ToList().ForEach(x =>
            {
                x.AssignPublisherId = null;
                x.AssignDate = null;
            });
            return _context.SaveChanges();
        }

        [HttpPost]
        [Route("/[controller]/ResponseType/{responseTypeId:int}/{activityTypeId:int}/PhoneActivities")]
        public int SavePhoneActivities(int responseTypeId, int activityTypeId, [FromBody] int[] assignments)
        {
            var contacts = _context.InaccessibleContacts.Include(x => x.ContactActivity).Where(x => assignments.Contains(x.InaccessibleContactId));
            contacts.ToList().ForEach(x =>
            {
                if (x.AssignPublisherId.HasValue)
                {
                    x.ContactActivity.Add(new InaccessibleContactActivity()
                    {
                        PublisherId = x.AssignPublisherId.Value,
                        ActivityDate = DateTime.UtcNow,
                        ContactActivityTypeId = activityTypeId,
                        PhoneResponseTypeId = responseTypeId
                    });

                    // if contact has a phone number and the response is do not contact
                    if (doNotContact.Contains((PhoneReponseTypeEnum)responseTypeId) && !string.IsNullOrEmpty(x.PhoneNumber))
                    {
                        _context.DoNotContactPhones.Add(new DoNotContactPhone()
                        {
                            PublisherId = x.AssignPublisherId.Value,
                            ReportedDate = DateTime.UtcNow,
                            PhoneNumber = regexPhoneNumber.Replace(x.PhoneNumber, ""),
                            Notes = _context.PhoneResponseTypes.Find(responseTypeId).Name
                        });
                    }

                    x.AssignPublisherId = null;
                }
            });
            return _context.SaveChanges();
        }

        [HttpPost]
        [Route("/[controller]/LetterActivities")]
        public int SaveLetterActivities([FromBody] int[] assignments)
        {
            var contacts = _context.InaccessibleContacts.Include(x => x.ContactActivity).Where(x => assignments.Contains(x.InaccessibleContactId));
            contacts.ToList().ForEach(x =>
            {
                if (x.AssignPublisherId.HasValue)
                {
                    x.ContactActivity.Add(new InaccessibleContactActivity()
                    {
                        PublisherId = x.AssignPublisherId.Value,
                        ActivityDate = DateTime.UtcNow,
                        ContactActivityTypeId = (int)ContactActivityTypeEnum.Letter
                    });
                    x.AssignPublisherId = null;
                }
            });
            return _context.SaveChanges();
        }

        [HttpPost]
        [Route("/[controller]/Contact/{id:int}/PhoneActivity")]
        public int SavePhoneActivity(int id, [FromBody] SavePhoneActivityDto dto)
        {
            var contact = _context.InaccessibleContacts.Find(id);

            if (contact != null && contact.AssignPublisherId.HasValue)
            {
                _context.InaccessibleContactActivities.Add(new InaccessibleContactActivity()
                {
                    InaccessibleContactId = id,
                    PublisherId = contact.AssignPublisherId.Value,
                    ActivityDate = System.DateTime.UtcNow,
                    ContactActivityTypeId = dto.contactActivityTypeId,
                    PhoneResponseTypeId = dto.phoneResponseTypeId,
                    Notes = dto.notes
                });

                // if contact has a phone number and the response is do not contact
                if (doNotContact.Contains((PhoneReponseTypeEnum)dto.phoneResponseTypeId) && !string.IsNullOrEmpty(contact.PhoneNumber))
                {
                    var notesValue = new List<string>() { _context.PhoneResponseTypes.Find(dto.phoneResponseTypeId).Name };
                    if (!string.IsNullOrEmpty(dto.notes))
                        notesValue.Add(dto.notes);

                    _context.DoNotContactPhones.Add(new DoNotContactPhone()
                    {
                        PublisherId = contact.AssignPublisherId.Value,
                        ReportedDate = DateTime.UtcNow,
                        PhoneNumber = Regex.Replace(contact.PhoneNumber, @"\D", ""),
                        Notes = String.Join(": ", notesValue.ToArray())
                    });
                }

                contact.AssignPublisherId = null;
            }

            return _context.SaveChanges();
        }
        public class SavePhoneActivityDto
        {
            public int contactActivityTypeId { get; set; }
            public string notes { get; set; }
            public int phoneResponseTypeId { get; set; }
        }

        [HttpPost]
        [Route("/[controller]/Contact/{id:int}/LetterActivity")]
        public int SaveLetterActivity(int id, [FromBody] SaveLetterActivityDto dto)
        {
            var contact = _context.InaccessibleContacts.Find(id);

            if (contact != null && contact.AssignPublisherId.HasValue)
            {
                _context.InaccessibleContactActivities.Add(new InaccessibleContactActivity()
                {
                    InaccessibleContactId = id,
                    PublisherId = contact.AssignPublisherId.Value,
                    ActivityDate = System.DateTime.UtcNow,
                    ContactActivityTypeId = (int)ContactActivityTypeEnum.Letter,
                    Notes = dto.notes
                });
            }

            contact.AssignPublisherId = null;

            return _context.SaveChanges();
        }

        public class SaveLetterActivityDto
        {
            public string notes { get; set; }
        }

        [HttpPost]
        [Route("[action]")]
        public IEnumerable<Object> ConvertPropertyContactListCsv(int id, [FromBody] IFormFile csvFile)
        {

            JArray resultArray = new JArray();

            using (var parser = new TextFieldParser(csvFile.OpenReadStream()))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.Delimiters = new string[] { "," };

                var rowIndex = 0;
                string[] columns;
                string[] currentRow;
                while (!parser.EndOfData)
                {
                    try
                    {
                        if (rowIndex == 0)
                        {
                            columns = parser.ReadFields();
                        }
                        else
                        {
                            currentRow = parser.ReadFields();
                            var columnIndex = 0;
                            JObject resultObject = new JObject();
                            foreach (var currentColumn in currentRow)
                            {
                                resultObject.Add(new JProperty("Halo", 9));
                                columnIndex++;
                            }
                            resultArray.Add(resultObject);
                        }
                    }
                    catch (MalformedLineException ex)
                    {

                    }
                    rowIndex++;
                }
            }

            return new List<string>() { };
        }
    }
}
