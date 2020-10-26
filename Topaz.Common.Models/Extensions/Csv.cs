using System;
using System.Linq;
using Topaz.Common.Models;
using Topaz.Common.Extensions;

namespace Topaz.Common.Models.Extensions
{
    public static class Csv
    {
        public static string ToCsv(this InaccessibleContact c)
        {
            var line = new string[] {
                c.FirstName,
                c.LastName,
                c.MiddleInitial,
                c.Age.ToString(),
                c.PhoneNumber,
                c.MailingAddress1,
                c.MailingAddress2,
                c.PostalCode
            };
            return string.Join(",", line.Select(x => x.CsvEscape()));
        }
    }
}