using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.FileIO;
using Microsoft.AspNetCore.Authorization;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

using Topaz.UI.ReportShared;
using Topaz.Common.Extensions;
using Topaz.Common.Enums;
using Topaz.Common.Models;
using Topaz.Common.Models.Extensions;
using Topaz.Data;


namespace Topaz.UI.Razor.Controllers
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

        private static readonly Regex regexNonDigit = new Regex(@"\D");
        private static readonly Regex regexMailingAddress1 = new Regex(@"^(\w+)\s(.*)");
        private static readonly Regex regexMailingAddress2 = new Regex(@"\w+$");
        private (string name, int? columnIndex, bool columnrequired)[] columnInformation = new (string name, int? columnIndex, bool columnrequired)[] {
            ("FirstName", null, true),
            ("LastName", null, true),
            ("MiddleInitial", null, false),
            ("Age", null, false),
            ("PhoneNumber", null, true),
            ("PhoneType", null, true),
            ("MailingAddress1", null, true),
            ("MailingAddress2", null, false),
            ("PostalCode", null, true)
        };

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
            var sql = @"
                SELECT t2.TerritoryId 
                FROM Territories t2 
                    INNER JOIN TerritoryActivities a2 ON t2.TerritoryId = a2.TerritoryId
                    INNER JOIN (
                        SELECT t2_.TerritoryId,
                            MIN(l.CreateDate) CreateDate
                        FROM Territories t2_
                            INNER JOIN InaccessibleProperties p ON t2_.TerritoryId = p.TerritoryId
                            INNER JOIN InaccessibleContactLists l ON p.CurrentContactListId = l.InaccessibleContactListId
                        WHERE t2_.Discriminator = 'InaccessibleTerritory'
                        GROUP BY t2_.TerritoryId
                    ) cd ON t2.TerritoryId = cd.TerritoryId
                WHERE t2.InActive = 0 AND t2.Discriminator = 'InaccessibleTerritory' AND 
                    NOT EXISTS(
                        SELECT NULL
                        FROM TerritoryActivities a2
                        WHERE t2.TerritoryId = a2.TerritoryId
                            AND a2.CheckInDate IS NULL
                    )
                    AND EXISTS(
                        SELECT NULL
                        FROM InaccessibleProperties p
                        WHERE t2.TerritoryId = p.TerritoryId
                    )
                    AND NOT EXISTS(
                        SELECT NULL
                        FROM InaccessibleProperties p
                        WHERE t2.TerritoryId = p.TerritoryId
                            AND p.CurrentContactListId IS NULL
                    )
                GROUP BY t2.TerritoryId
                HAVING MAX(a2.CheckInDate) < cd.CreateDate";

            var availableTerritoryIds = new List<long>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sql;
                _context.Database.OpenConnection();
                using (var results = command.ExecuteReader())
                {
                    while (results.Read())
                    {
                        availableTerritoryIds.Add((long)results["TerritoryId"]);
                    }
                }
            }

            var LinqResult = _context.InaccessibleTerritories.Where(x => !x.InActive && !x.Activity.Any());

            LinqResult = LinqResult.Union(_context.InaccessibleTerritories.Where(x => availableTerritoryIds.Contains(x.TerritoryId) && !x.InActive && !x.Activity.Any(y => y.CheckInDate == null)));

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
                .Select(x => new { x.InaccessibleContactId, PhoneNumber = regexNonDigit.Replace(x.PhoneNumber, "") });

            // compare phone numbers to the do not contact phone list
            var territoryDoNotContactPhoneNumbers = _context.DoNotContactPhones.Where(x => territoryPhoneNumbers.Select(y => y.PhoneNumber).Contains(x.PhoneNumber)).Select(x => x.PhoneNumber).ToList();
            var territoryDoNotContactPhoneContactIds = territoryPhoneNumbers.Where(x => territoryDoNotContactPhoneNumbers.Contains(x.PhoneNumber)).Select(x => x.InaccessibleContactId);

            // get this territory's associated street territory
            var streetTerritoryId = _context.InaccessibleProperties.Where(x => x.TerritoryId == id).Select(x => x.Territory.StreetTerritoryId).FirstOrDefault();

            // list of this territory's mailing addresses
            var territoryMailingAddresses = Assignments
                .Where(x => !string.IsNullOrEmpty(x.MailingAddress1))
                .Select(x => new { x.InaccessibleContactId, x.MailingAddress1, x.MailingAddress2 }).AsNoTracking().ToList();

            // list of 'do not contact' addresses for this territory's associated street territory
            var territoryDoNotContactMailingAddresses = _context.DoNotContactLetters.Where(x => x.TerritoryId == streetTerritoryId).Select(x => new { x.MailingAddress1, x.MailingAddress2 }).AsNoTracking().ToList();

            // compare the mailing addresses to the list of 'do not contact' addresses
            var territoryDoNotContactLetterContactIds = territoryMailingAddresses.Where((x) =>
            {
                // validate contact mailing address 1 pattern
                if (!regexMailingAddress1.IsMatch(x.MailingAddress1))
                    return false;

                // validate contact mailing address 2 pattern if specified
                if (!string.IsNullOrEmpty(x.MailingAddress2) && !regexMailingAddress2.IsMatch(x.MailingAddress2))
                    return false;

                return territoryDoNotContactMailingAddresses.Any((y) =>
                {
                    // validate do not contact mailing address 1 pattern
                    if (!regexMailingAddress1.IsMatch(y.MailingAddress1))
                        return false;

                    // do not contact street name
                    var yMailingStreetName = Regex.Replace(regexMailingAddress1.Match(y.MailingAddress1).Groups[2].Value, @"\W", "").ToLower();

                    // contact street name
                    var xMailingStreetName = Regex.Replace(regexMailingAddress1.Match(x.MailingAddress1).Groups[2].Value, @"\W", "").ToLower();

                    // compare similarity of street names (the lower the number the more similar they are)
                    var streetNameSimilarityScore = (xMailingStreetName.Length >= yMailingStreetName.Length) ?
                        (double)yMailingStreetName.DamerauLevenshteinDistanceTo(xMailingStreetName) / xMailingStreetName.Length :
                        (double)xMailingStreetName.DamerauLevenshteinDistanceTo(yMailingStreetName) / yMailingStreetName.Length;

                    // street name isn't similar enough between the two records
                    if (streetNameSimilarityScore > .5)
                        return false;

                    // do not contact street number
                    var yMailingStreetNumber = regexMailingAddress1.Match(y.MailingAddress1).Groups[1].Value.ToLower();
                    int yMailingStreetNumberNumeric;

                    // contact street number
                    var xMailingStreetNumber = regexMailingAddress1.Match(x.MailingAddress1).Groups[1].Value.ToLower();
                    int xMailingStreetNumberNumeric;

                    // check if street numbers match as string
                    var streetNumbersMatch = yMailingStreetNumber == xMailingStreetNumber;

                    // if street numbers don't match as string and both are numeric then compare as numeric
                    if (!streetNumbersMatch)
                        streetNumbersMatch =
                            int.TryParse(yMailingStreetNumber, out yMailingStreetNumberNumeric) &&
                            int.TryParse(xMailingStreetNumber, out xMailingStreetNumberNumeric) &&
                            yMailingStreetNumberNumeric == xMailingStreetNumberNumeric;

                    // if street numbers still don't match
                    if (!streetNumbersMatch)
                        return false;

                    // if mailing address 2 specified
                    if (!string.IsNullOrEmpty(x.MailingAddress2) && string.IsNullOrEmpty(x.MailingAddress2) == string.IsNullOrEmpty(y.MailingAddress2))
                    {
                        // validate do not contact mailing address 2 pattern
                        if (!regexMailingAddress2.IsMatch(y.MailingAddress2))
                            return false;

                        var yMailingAddress2 = regexMailingAddress2.Match(y.MailingAddress2).Value.ToLower();
                        int yMailingAddress2Numeric;

                        var xMailingAddress2 = regexMailingAddress2.Match(x.MailingAddress2).Value.ToLower();
                        int xMailingAddress2Numeric;

                        // check if mailing address 2 match as string
                        var address2Match = yMailingAddress2 == xMailingAddress2;

                        // if mailing address 2 doesn't match as string and both are numeric then compare as numeric
                        if (!address2Match)
                            address2Match =
                                int.TryParse(yMailingAddress2, out yMailingAddress2Numeric) &&
                                int.TryParse(xMailingAddress2, out xMailingAddress2Numeric) &&
                                yMailingAddress2Numeric == xMailingAddress2Numeric;

                        // if mailing address 2 still doesn't match
                        if (!address2Match)
                            return false;
                    }

                    return true;
                });
            }).Select(x => x.InaccessibleContactId);

            var FilteredAssignments = Assignments;
            if (type == "phone")
            {
                FilteredAssignments = FilteredAssignments.Where(x =>
                    // has not been exported
                    !x.ContactActivity.Any(y => y.ContactActivityTypeId == (int)ContactActivityTypeEnum.Export) &&
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
                    // has not been exported
                    !x.ContactActivity.Any(y => y.ContactActivityTypeId == (int)ContactActivityTypeEnum.Export) &&
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
                    // has not been exported
                    !x.ContactActivity.Any(y => y.ContactActivityTypeId == (int)ContactActivityTypeEnum.Export) &&
                    // has mailing address
                    !string.IsNullOrEmpty(x.MailingAddress1) &&
                    (
                        // is a 'do not contact' phone, not a 'do not contact' letter, and there is no activity
                        (
                            territoryDoNotContactPhoneContactIds.Contains(x.InaccessibleContactId) &&
                            !territoryDoNotContactLetterContactIds.Contains(x.InaccessibleContactId) &&
                            !x.ContactActivity.Any()
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
            else // if done
            {
                FilteredAssignments = FilteredAssignments.Where(x =>
                    // has been exported
                    x.ContactActivity.Any(y => y.ContactActivityTypeId == (int)ContactActivityTypeEnum.Export) ||
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

            var results = FilteredAssignments.Select(x => new { Contact = x, ActivityDate = x.ContactActivity.Max(y => y.ActivityDate) }).ToList();

            // set the do not contact phone flag to true
            results.ForEach((x) =>
            {
                x.Contact.DoNotContactPhone = territoryDoNotContactPhoneContactIds.Contains(x.Contact.InaccessibleContactId);
                x.Contact.DoNotContactLetter = territoryDoNotContactLetterContactIds.Contains(x.Contact.InaccessibleContactId);
                if (x.Contact.AssignPublisherId.HasValue && x.Contact.AssignDate.HasValue)
                {
                    x.Contact.AssignedDays = (DateTime.UtcNow - x.Contact.AssignDate.Value).Days;
                }
                if (x.ActivityDate.HasValue)
                {
                    x.Contact.ActivityDays = (DateTime.UtcNow - x.ActivityDate.Value).Days;
                }
            });

            return results.Select(x => x.Contact)
                .OrderBy(x => x.MailingAddress1)
                .ThenBy(x => x.MailingAddress2)
                .ThenBy(x => x.LastName);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public IEnumerable<Object> GetTerritoryExports(int id)
        {
            DateTime? checkOutDate = null;
            if (_context.TerritoryActivities.Any(x => x.TerritoryId == id))
            {
                checkOutDate = _context.TerritoryActivities.Where(x => x.TerritoryId == id).Max(x => x.CheckOutDate);
            }

            return _context.InaccessibleTerritoryExports.Where(x => x.TerritoryId == id && (checkOutDate == null || x.ExportDate >= checkOutDate.Value)).Select(x =>
                    new
                    {
                        x.InaccessibleTerritoryExportId,
                        x.Publisher.FirstName,
                        x.Publisher.LastName,
                        x.ExportDate,
                        ExportItemCount = x.Items.Count()
                    }
                ).OrderBy(x => x.ExportDate).ThenBy(x => x.LastName).ThenBy(x => x.FirstName);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public IActionResult GetTerritoryExportContacts(int id)
        {
            var export = _context.InaccessibleTerritoryExports
                .Include(x => x.Territory)
                .Include(x => x.Publisher)
                .Include(x => x.Items)
                .ThenInclude(x => x.Contact)
                .ThenInclude(x => x.PhoneType)
                .FirstOrDefault(x => x.InaccessibleTerritoryExportId == id);

            using (MemoryStream memoryStream1 = new MemoryStream())
            using (StreamWriter streamWriter1 = new StreamWriter(memoryStream1))
            {
                try
                {
                    streamWriter1.WriteLine(string.Join(",", columnInformation.Select(x => x.name).ToArray()));
                    foreach (var contact in export.Items.Select(x => x.Contact))
                    {
                        streamWriter1.WriteLine(contact.ToCsv());
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    streamWriter1.Flush();
                    streamWriter1.Close();
                }

                var territory = Regex.Replace(export.Territory.TerritoryCode, @"\W", "").ToLower();
                var firstName = Regex.Replace(export.Publisher.FirstName, @"\W", "").ToLower();
                var lastName = Regex.Replace(export.Publisher.LastName, @"\W", "").ToLower();

                var fileName = $"{export.InaccessibleTerritoryExportId}-{territory}-{firstName}{lastName}";

                Response.Headers.Add("Content-Disposition", $"inline; filename={fileName}.csv");
                return File(memoryStream1.ToArray(), "text/csv", $"{fileName}.csv");
            }
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
                .Select(x => new { x.InaccessibleContactId, PhoneNumber = regexNonDigit.Replace(x.PhoneNumber, "") });

            // compare phone numbers to the do not contact phone list
            var territoryDoNotContactPhoneNumbers = _context.DoNotContactPhones.Where(x => territoryPhoneNumbers.Select(y => y.PhoneNumber).Contains(x.PhoneNumber)).Select(x => x.PhoneNumber).ToList();
            var territoryDoNotContactPhoneContactIds = territoryPhoneNumbers.Where(x => territoryDoNotContactPhoneNumbers.Contains(x.PhoneNumber)).Select(x => x.InaccessibleContactId);

            return new
            {
                PhoneWithoutVoicemail = Assignments.Where(x =>
                    // has not been exported
                    !x.ContactActivity.Any(y => y.ContactActivityTypeId == (int)ContactActivityTypeEnum.Export) &&
                    // is not a 'do not contact' phone
                    !territoryDoNotContactPhoneContactIds.Contains(x.InaccessibleContactId) &&
                    // has phone number
                    !string.IsNullOrEmpty(x.PhoneNumber) &&
                    // phone has not been attempted
                    x.ContactActivity.All(y => !phoneActivity.Contains((ContactActivityTypeEnum)y.ContactActivityTypeId))
                ).OrderBy(x => x.MailingAddress1).ThenBy(x => x.MailingAddress2).ThenBy(x => x.LastName),
                PhoneWithVoicemail = Assignments.Where(x =>
                    // has not been exported
                    !x.ContactActivity.Any(y => y.ContactActivityTypeId == (int)ContactActivityTypeEnum.Export) &&
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
                    // has not been exported
                    !x.ContactActivity.Any(y => y.ContactActivityTypeId == (int)ContactActivityTypeEnum.Export) &&
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
        
        [HttpPost]
        [Route("[action]/{type}")]
        public Object CurrentUserAssignAvailable(string type)
        {
            var Claims = (ClaimsIdentity)this.User.Identity;
            var PublisherId = int.Parse(Claims.FindFirst("PublisherId").Value);

            var territories = _context.InaccessibleTerritories.Where(x => 
                    x.Activity.Any(y => 
                        y.CheckOutDate.HasValue && 
                        !y.CheckInDate.HasValue
                    ) && 
                    x.InaccessibleProperties.Any(y => 
                        y.CurrentContactListId.HasValue && 
                        y.ContactLists.FirstOrDefault(z => z.InaccessibleContactListId == y.CurrentContactListId).Contacts.Any(a => a.IsAvailable)
                    )
                ).OrderBy(x => x.Activity.Where(y => y.CheckOutDate.HasValue && !y.CheckInDate.HasValue).Max(y => y.CheckOutDate)).Select(x => x.TerritoryId);
            
            var contactIds = new List<int>();
            foreach (var territoryId in territories)
            {
                var available = _context.InaccessibleProperties.Where(x => x.TerritoryId == territoryId && x.CurrentContactListId.HasValue)
                    .Include(x => x.ContactLists)
                    .ThenInclude(x => x.Contacts)
                    .SelectMany(x => x.ContactLists.Where(y => y.InaccessibleContactListId == x.CurrentContactListId).SelectMany(y => y.Contacts.Where(z => z.IsAvailable)))
                    .AsNoTracking();
                       
                if (type == "phone")
                {
                    contactIds.AddRange(available.Where(x =>
                        // has not been exported
                        !x.ContactActivity.Any(y => y.ContactActivityTypeId == (int)ContactActivityTypeEnum.Export) &&
                        // has phone number
                        !string.IsNullOrEmpty(x.PhoneNumber) &&
                        // phone has not been attempted
                        x.ContactActivity.All(y => !phoneActivity.Contains((ContactActivityTypeEnum)y.ContactActivityTypeId))
                    ).OrderBy(x => x.MailingAddress1).ThenBy(x => x.MailingAddress2).ThenBy(x => x.LastName).Select(x => x.InaccessibleContactId).Take(3 - contactIds.Count()));
                }
                else if (type == "vm")
                {
                    contactIds.AddRange(available.Where(x =>
                        // has not been exported
                        !x.ContactActivity.Any(y => y.ContactActivityTypeId == (int)ContactActivityTypeEnum.Export) &&
                        // has phone number
                        !string.IsNullOrEmpty(x.PhoneNumber) &&
                        // phone has been attempted
                        x.ContactActivity.Any(y => phoneActivity.Contains((ContactActivityTypeEnum)y.ContactActivityTypeId)) &&
                        // there has been no answer
                        x.ContactActivity.Where(y => phoneActivity.Contains((ContactActivityTypeEnum)y.ContactActivityTypeId)).All(y => !phoneCheck.Contains((PhoneReponseTypeEnum)y.PhoneResponseTypeId)) &&
                        // phone with voicemail has not been attempted
                        x.ContactActivity.All(y => y.ContactActivityTypeId != (int)ContactActivityTypeEnum.PhoneWithVoicemail)
                    ).OrderBy(x => x.MailingAddress1).ThenBy(x => x.MailingAddress2).ThenBy(x => x.LastName).Select(x => x.InaccessibleContactId).Take(3 - contactIds.Count()));
                }
                else if (type == "letter")
                {
                    contactIds.AddRange(available.Where(x =>
                        // has not been exported
                        !x.ContactActivity.Any(y => y.ContactActivityTypeId == (int)ContactActivityTypeEnum.Export) &&
                        // has mailing address
                        !string.IsNullOrEmpty(x.MailingAddress1) &&
                        (
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
                    ).OrderBy(x => x.MailingAddress1).ThenBy(x => x.MailingAddress2).ThenBy(x => x.LastName).Select(x => x.InaccessibleContactId).Take(3 - contactIds.Count()));
                }
                    
                if (contactIds.Count() >= 3)
                {
                    break;
                }
            }

            var assign = _context.InaccessibleContacts.Where(x => contactIds.Any(y => y == x.InaccessibleContactId));

            foreach (var x in assign)
            {
                x.IsAvailable = false;
                x.AssignDate = DateTime.UtcNow;
                x.AssignPublisherId = PublisherId;
            }

            return _context.SaveChanges();
        }
        
        [HttpGet]
        [Route("[action]")]
        public IEnumerable<Object> GetPhoneResponseTypes()
        {
            return _context.PhoneResponseTypes.OrderBy(x => x.Name);
        }

        [HttpGet]
        [Route("[action]/{id:int}")]
        public IEnumerable<JObject> GetTerritoryProperties(int id)
        {
            return _context.InaccessibleProperties
                .Include(x => x.ContactLists)
                .ThenInclude(x => x.Contacts)
                .ThenInclude(x => x.PhoneType)
                .Where(x => x.TerritoryId == id)
                .AsNoTracking()
                .ToList()
                .Select((x) =>
                {
                    var p = JObject.FromObject(x, new JsonSerializer() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, ContractResolver = new CamelCasePropertyNamesContractResolver() });

                    if (x.CurrentContactListId.HasValue)
                        p.Add(new JProperty("contacts", JArray.FromObject(x.ContactLists.Where(y => y.InaccessibleContactListId == x.CurrentContactListId).FirstOrDefault().Contacts, new JsonSerializer() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, ContractResolver = new CamelCasePropertyNamesContractResolver() })));
                    else
                        p.Add(new JProperty("contacts", null));

                    p.Remove("contactLists");
                    return p;
                });
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
                x.IsAvailable = false;
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
        [Route("[action]/{isAvailable:bool}")]
        public int FlagAvailability(bool isAvailable, [FromBody] int[] assignments)
        {
            var contacts = _context.InaccessibleContacts.Where(x => assignments.Contains(x.InaccessibleContactId));
            contacts.ToList().ForEach(x =>
            {
                x.AssignPublisherId = null;
                x.AssignDate = null;
                x.IsAvailable = isAvailable;
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
                        var phoneNumber = regexNonDigit.Replace(x.PhoneNumber, "");
                        if (!_context.DoNotContactPhones.Any(y => y.PhoneNumber == phoneNumber))
                        {
                            _context.DoNotContactPhones.Add(new DoNotContactPhone()
                            {
                                PublisherId = x.AssignPublisherId.Value,
                                ReportedDate = DateTime.UtcNow,
                                PhoneNumber = phoneNumber,
                                Notes = _context.PhoneResponseTypes.Find(responseTypeId).Name
                            });
                        }
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
        [Route("/[controller]/ExportActivities/{assignee:int}")]
        public int SaveExportActivities(int assignee, [FromBody] int[] assignments)
        {
            var contacts = _context.InaccessibleContacts.Include(x => x.ContactActivity).Where(x => assignments.Contains(x.InaccessibleContactId));

            // if there are any contacts to be exported and they are all unassigned
            if (contacts.Any() && contacts.All(x => !x.AssignPublisherId.HasValue))
            {
                var contactsAll = contacts.ToList();

                var contactFirst = contactsAll.Select(x => x.InaccessibleContactId).FirstOrDefault();
                var territoryId = _context.InaccessibleContacts.Where(x => x.InaccessibleContactId == contactFirst).Select(x => x.ContactList.Property.TerritoryId).FirstOrDefault();

                var export = new InaccessibleTerritoryExport()
                {
                    TerritoryId = territoryId,
                    PublisherId = assignee,
                    ExportDate = DateTime.UtcNow
                };

                _context.InaccessibleTerritoryExports.Add(export);

                // save the export (no items yet)
                _context.SaveChanges();

                contactsAll.ForEach(x =>
                {
                    x.IsAvailable = false;
                    // record an export activity for the contact
                    var activity = new InaccessibleContactActivity()
                    {
                        PublisherId = assignee,
                        ActivityDate = DateTime.UtcNow,
                        ContactActivityTypeId = (int)ContactActivityTypeEnum.Export,
                        InaccessibleTerritoryExportId = export.InaccessibleTerritoryExportId
                    };
                    x.ContactActivity.Add(activity);

                    // create an export item for the contact
                    var exportItem = new InaccessibleTerritoryExportItem()
                    {
                        InaccessibleTerritoryExportId = export.InaccessibleTerritoryExportId,
                        InaccessibleContactId = x.InaccessibleContactId
                    };

                    // add the export item to the export
                    _context.InaccessibleTerritoryExportItems.Add(exportItem);
                });

                // save the export items
                _context.SaveChanges();

                // associate the export items with contacts
                _context.Database.ExecuteSqlRaw($@"
                    UPDATE 
                    InaccessibleContacts 
                    SET 
                    InaccessibleTerritoryExportItemId = (
                        SELECT 
                        InaccessibleTerritoryExportItemId 
                        FROM 
                        InaccessibleTerritoryExportItems 
                        WHERE 
                        InaccessibleTerritoryExportId = {export.InaccessibleTerritoryExportId} AND 
                        InaccessibleContactId = InaccessibleContacts.InaccessibleContactId 
                    ) 
                    WHERE 
                    EXISTS (
                        SELECT 
                        InaccessibleTerritoryExportItemId 
                        FROM 
                        InaccessibleTerritoryExportItems 
                        WHERE 
                        InaccessibleTerritoryExportId = {export.InaccessibleTerritoryExportId} AND 
                        InaccessibleContactId = InaccessibleContacts.InaccessibleContactId 
                    )"
                );
            }

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
                        Notes = string.Join(": ", notesValue.ToArray())
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
        [Route("[action]/{id}")]
        public int RemovePropertyContactList(int id)
        {
            var property = new InaccessibleProperty() { InaccessiblePropertyId = id, CurrentContactListId = null };
            _context.InaccessibleProperties.Attach(property);
            _context.Entry(property).State = EntityState.Unchanged;
            _context.Entry(property).Property(X => X.CurrentContactListId).IsModified = true;
            return _context.SaveChanges();
        }

        [HttpPost]
        [Route("[action]")]
        public Object UploadContactsCsv([FromForm] IFormFile csvFile)
        {
            JObject resultObject = new JObject();

            JArray messagesError = new JArray();
            JArray messagesWarning = new JArray();

            JArray rowArray = new JArray();

            int rowErrors = 0;
            int rowWarnings = 0;
            using (var parser = new TextFieldParser(csvFile.OpenReadStream()))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.Delimiters = new string[] { "," };

                var rowIndex = 0;
                var columns = new List<string>();
                string[] currentRow;
                while (!parser.EndOfData)
                {
                    try
                    {
                        // on the first row
                        if (rowIndex == 0)
                        {
                            // add the column names
                            columns.AddRange(parser.ReadFields().Select(x => x.Trim()));

                            // calculate the index of each column
                            var lower = columns.Select(x => x.ToLower()).ToArray();
                            for (var i = 0; i < columnInformation.Length; i++)
                                columnInformation[i].columnIndex = Array.IndexOf(lower, columnInformation[i].name.ToLower());

                            // if there are columns without name specified
                            if (columns.Where(x => string.IsNullOrEmpty(x)).Count() != 0)
                                messagesError.Add("This file has one or more empty column headers. This is not allowed.");

                            // if there are duplicate columns
                            if (columns.Distinct().Count() != columns.Count())
                                messagesError.Add("There are multiple columns with the same name. This is not allowed.");

                            // check the columns against the required columns
                            var missingRequiredColumns = columnInformation.Where(x => x.columnrequired && x.columnIndex == -1).Select(x => x.name);

                            // if there are any missing required columns
                            if (missingRequiredColumns.Any())
                            {
                                if (missingRequiredColumns.Count() == 1)
                                    messagesError.Add($"The following required column is missing: {string.Join(", ", missingRequiredColumns)}");
                                else
                                    messagesError.Add($"The following {missingRequiredColumns.Count()} required columns are missing: {string.Join(", ", missingRequiredColumns)}");
                            }

                            // exit if there are any column errors...
                            if (messagesError.Any())
                                break;

                            // check the columns against the optional columns
                            var missingOptionalColumns = columnInformation.Where(x => !x.columnrequired && x.columnIndex == -1).Select(x => x.name); ;

                            // if there are any missing optional columns
                            if (missingOptionalColumns.Any())
                            {
                                if (missingOptionalColumns.Count() == 1)
                                    messagesWarning.Add($"The following optional column is missing: {string.Join(", ", missingOptionalColumns)}");
                                else
                                    messagesWarning.Add($"The following {missingOptionalColumns.Count()} optional columns are missing: {string.Join(", ", missingOptionalColumns)}");
                            }

                            // if there are any columns that will be ignored
                            var ignoredColumns = columns.Where(x => !columnInformation.Any(y => string.Compare(x, y.name, true) == 0));
                            if (ignoredColumns.Any())
                            {
                                if (ignoredColumns.Count() == 1)
                                    messagesWarning.Add($"The following column will be ignored: {string.Join(", ", ignoredColumns)}");
                                else
                                    messagesWarning.Add($"The following {ignoredColumns.Count()} columns will be ignored: {string.Join(", ", ignoredColumns)}");
                            }
                        }
                        else
                        {
                            currentRow = parser.ReadFields().Select(x => x.Trim()).ToArray();

                            JObject objContact = new JObject();

                            var columnIndex = 0;
                            foreach (var currentColumn in currentRow)
                            {
                                if (columnInformation.Any(x => x.columnIndex == columnIndex))
                                {
                                    objContact.Add(new JProperty(columnInformation.FirstOrDefault(x => x.columnIndex == columnIndex).name, currentRow[columnIndex]));
                                }
                                columnIndex++;
                            }

                            var rowContact = objContact.ToObject<PropertyContactDto>();

                            rowContact.Validate();

                            rowErrors += rowContact.Errors.Count();
                            rowWarnings += rowContact.Warnings.Count();

                            rowArray.Add(JObject.FromObject(rowContact, new JsonSerializer() { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
                        }
                    }
                    catch (MalformedLineException ex)
                    {
                        messagesError.Add($"Unable to import the file. There is something wrong with the file format. {ex.Message}");
                        break;
                    }
                    rowIndex++;
                }
            }

            resultObject.Add(new JProperty("errors", messagesError));
            resultObject.Add(new JProperty("warnings", messagesWarning));
            resultObject.Add(new JProperty("rowErrors", rowErrors));
            resultObject.Add(new JProperty("rowWarnings", rowWarnings));
            resultObject.Add(new JProperty("rows", rowArray));

            return resultObject;
        }

        [HttpPost]
        [Route("[action]/{id}")]
        public List<InaccessibleContact> UploadContacts(int id, [FromBody] List<PropertyContactDto> contactDtos)
        {
            var list = new InaccessibleContactList()
            {
                InaccessiblePropertyId = id,
                CreateDate = DateTime.UtcNow
            };

            foreach (var contactDto in contactDtos)
            {
                contactDto.Validate();

                if (contactDto.Errors.Count == 0)
                {
                    PhoneTypeEnum phoneType;

                    switch (contactDto.PhoneType)
                    {
                        case "C":
                            phoneType = PhoneTypeEnum.Mobile;
                            break;
                        case "M":
                            phoneType = PhoneTypeEnum.Mobile;
                            break;
                        case "L":
                            phoneType = PhoneTypeEnum.Landline;
                            break;
                        case "V":
                            phoneType = PhoneTypeEnum.Voip;
                            break;
                        default:
                            phoneType = PhoneTypeEnum.Landline;
                            break;
                    }

                    contactDto.Age = regexNonDigit.Replace(contactDto.Age, "");
                    int age;

                    list.Contacts.Add(
                        new InaccessibleContact()
                        {
                            FirstName = contactDto.FirstName,
                            LastName = contactDto.LastName,
                            MiddleInitial = contactDto.MiddleInitial,
                            Age = (int.TryParse(contactDto.Age, out age)) ? (int?)age : null,
                            MailingAddress1 = contactDto.MailingAddress1,
                            MailingAddress2 = contactDto.MailingAddress2,
                            PostalCode = contactDto.PostalCode,
                            PhoneNumber = contactDto.PhoneNumber,
                            PhoneTypeId = (!string.IsNullOrEmpty(contactDto.PhoneNumber)) ? (int?)phoneType : null
                        }
                    );
                }
            }

            _context.InaccessibleContactLists.Add(list);
            _context.SaveChanges();

            var property = new InaccessibleProperty() { InaccessiblePropertyId = id, CurrentContactListId = list.InaccessibleContactListId };
            _context.InaccessibleProperties.Attach(property);
            _context.Entry(property).State = EntityState.Unchanged;
            _context.Entry(property).Property(X => X.CurrentContactListId).IsModified = true;
            _context.SaveChanges();

            return _context.InaccessibleContacts
                    .Include(x => x.PhoneType)
                    .Where(x => x.InaccessibleContactListId == list.InaccessibleContactListId).AsNoTracking().ToList();
        }
        public class PropertyContactDto
        {
            public PropertyContactDto()
            {
                Errors = new List<string>();
                Warnings = new List<string>();
            }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string MiddleInitial { get; set; }
            public string Age { get; set; }
            public string MailingAddress1 { get; set; }
            public string MailingAddress2 { get; set; }
            public string PostalCode { get; set; }
            public string PhoneNumber { get; set; }
            public string PhoneType { get; set; }
            public List<string> Errors { get; set; }
            public List<string> Warnings { get; set; }
        }

        [HttpGet]
        [Route("[action]")]
        public IEnumerable<Object> GetTerritory()
        {
            var result = _context.InaccessibleTerritories.Select(x =>
                    new
                    {
                        x.TerritoryId,
                        x.TerritoryCode,
                        StreetTerritoryId = x.StreetTerritory.TerritoryId,
                        StreetTerritoryCode = x.StreetTerritory.TerritoryCode,
                        x.InActive,
                        Activity = x.Activity.OrderByDescending(y => y.CheckOutDate).FirstOrDefault()
                    }
                ).ToList();

            var publisherIds = result.Where(y => y.Activity != null).Select(y => y.Activity.PublisherId).ToList();

            var publishers = _context.Publishers.Where(x => publisherIds.Contains(x.PublisherId)).Select(x => new { x.PublisherId, x.FirstName, x.LastName }).ToList();

            return result.Select(x => new
            {
                x.TerritoryId,
                x.TerritoryCode,
                x.StreetTerritoryId,
                x.StreetTerritoryCode,
                x.InActive,
                x.Activity,
                Publisher = x.Activity != null ? publishers.FirstOrDefault(y => y.PublisherId == x.Activity.PublisherId) : null
            });
        }

        [HttpGet]
        [Route("[action]")]
        public IEnumerable<Object> GetTerritoryOut()
        {
            var result = _context.InaccessibleTerritories.Where(x =>
                x.Activity.Any(y =>
                    !y.CheckOutDate.HasValue && y.CheckInDate.HasValue
                )).Select(x =>
                    new
                    {
                        x.TerritoryId,
                        x.TerritoryCode,
                        StreetTerritoryId = x.StreetTerritory.TerritoryId,
                        StreetTerritoryCode = x.StreetTerritory.TerritoryCode,
                        x.InActive,
                        Activity = x.Activity.OrderByDescending(y => y.CheckOutDate).FirstOrDefault()
                    }
                ).ToList();

            var publisherIds = result.Where(y => y.Activity != null).Select(y => y.Activity.PublisherId).ToList();

            var publishers = _context.Publishers.Where(x => publisherIds.Contains(x.PublisherId)).Select(x => new { x.PublisherId, x.FirstName, x.LastName }).ToList();

            return result.Select(x => new
            {
                x.TerritoryId,
                x.TerritoryCode,
                x.StreetTerritoryId,
                x.StreetTerritoryCode,
                x.InActive,
                x.Activity,
                Publisher = x.Activity != null ? publishers.FirstOrDefault(y => y.PublisherId == x.Activity.PublisherId) : null
            });
        }

        [HttpGet]
        [Route("[action]")]
        public IEnumerable<Object> GetTerritoryIn()
        {
            var result = _context.InaccessibleTerritories.Where(x =>
                !x.Activity.Any() || x.Activity.All(y => y.CheckOutDate.HasValue && y.CheckInDate.HasValue)).Select(x =>
                    new
                    {
                        x.TerritoryId,
                        x.TerritoryCode,
                        StreetTerritoryId = x.StreetTerritory.TerritoryId,
                        StreetTerritoryCode = x.StreetTerritory.TerritoryCode,
                        x.InActive,
                        Activity = x.Activity.OrderByDescending(y => y.CheckOutDate).FirstOrDefault()
                    }
                ).ToList();

            var publisherIds = result.Where(y => y.Activity != null).Select(y => y.Activity.PublisherId).ToList();

            var publishers = _context.Publishers.Where(x => publisherIds.Contains(x.PublisherId)).Select(x => new { x.PublisherId, x.FirstName, x.LastName }).ToList();

            return result.Select(x => new
            {
                x.TerritoryId,
                x.TerritoryCode,
                x.StreetTerritoryId,
                x.StreetTerritoryCode,
                x.InActive,
                x.Activity,
                Publisher = x.Activity != null ? publishers.FirstOrDefault(y => y.PublisherId == x.Activity.PublisherId) : null
            });
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        [Route("[action]")]
        public IEnumerable<Object> GetReport()
        {
            return _context.InaccessibleTerritories.Where(x => !x.InActive).Select(x => new ReportTerritory() {
                TerritoryId = x.TerritoryId, 
                TerritoryCode = x.StreetTerritory.TerritoryCode + " / " + x.TerritoryCode, 
                Activity = x.Activity.Select(y => new ReportActivity() {
                    TerritoryActivityId = y.TerritoryActivityId, 
                    TerritoryId = y.TerritoryId, 
                    PublisherId = y.PublisherId, 
                    FirstName = y.Publisher.FirstName, 
                    LastName = y.Publisher.LastName, 
                    CheckOutDate = y.CheckOutDate.Value, 
                    CheckInDate = y.CheckInDate
                }).OrderBy(y => y.CheckOutDate)
            }).OrderBy(x => x.TerritoryCode);
        }
    }

    public static class ExtensionMethods
    {
        public static void Validate(this Topaz.UI.Razor.Controllers.InaccessibleController.PropertyContactDto contact)
        {
            if (string.IsNullOrEmpty(contact.FirstName) || string.IsNullOrEmpty(contact.LastName))
            {
                contact.Errors.Add("Contacts must have a first and last name. This record will not be imported.");
            }

            if (string.IsNullOrEmpty(contact.MailingAddress1) && string.IsNullOrEmpty(contact.PhoneNumber))
            {
                contact.Errors.Add("Contacts must have either an address or a phone number. This record will not be imported.");
            }

            var isMailingError = (
                (!string.IsNullOrEmpty(contact.MailingAddress1) && string.IsNullOrEmpty(contact.PostalCode)) ||
                (string.IsNullOrEmpty(contact.MailingAddress1) && !string.IsNullOrEmpty(contact.PostalCode))
            );
            if (isMailingError)
            {
                contact.Errors.Add("An address must have a mailing address and postal code. This record will not be imported.");
            }

            var isPhoneError = (
                (!string.IsNullOrEmpty(contact.PhoneNumber) && string.IsNullOrEmpty(contact.PhoneType)) ||
                (string.IsNullOrEmpty(contact.PhoneNumber) && !string.IsNullOrEmpty(contact.PhoneType))
            );
            if (isPhoneError)
            {
                contact.Errors.Add("A phone number must include the number and the phone type. This record will not be imported.");
            }

            var phoneTypes = new string[] { "C", "L", "V" };
            if (contact.Errors.Count() == 0 && !string.IsNullOrEmpty(contact.PhoneType) && !phoneTypes.Any(x => string.Compare(contact.PhoneType, x, true) == 0))
            {
                if (!isMailingError)
                {
                    contact.Warnings.Add("This contact has an invalid phone type. The phone number will be ignored.");
                }
                else
                {
                    contact.Errors.Add("This contact has an invalid phone type. This record will not be imported.");
                }
            }

            int age;
            if (contact.Errors.Count() == 0 && !string.IsNullOrEmpty(contact.Age) && !int.TryParse(contact.Age, out age))
            {
                contact.Warnings.Add("This contact has an invalid age. The age will be ignored.");
            }
        }
    }
}
