using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolWindow.Utility
{
    internal  static class CommonHelper
    {
        public static string GetPluralName(string word)
        {
            var pluralService = System.Data.Entity.Design.PluralizationServices.PluralizationService.CreateService(new System.Globalization.CultureInfo("en-US"));

            string pluralEntityName = pluralService.Pluralize(word);
            return pluralEntityName;
        }

    }
}
