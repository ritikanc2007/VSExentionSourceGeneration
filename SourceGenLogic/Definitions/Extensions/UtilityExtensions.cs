using Microsoft.CodeAnalysis.CSharp.Syntax;
using Restarted.Generators.Generators.BaseModels;
using SourceGeneratorParser.Models.Metadata;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Xml.Linq;

namespace Restarted.Generators.Definitions.Extensions
{
    public static class UtilityExtensions
    {
        private const string ConstPluralName = "PluralName";
        private const string ConstPluralArgumentPluralEntityName = "pluralEntity";


        #region PluralNameExtensions
        public static string GetPluralName(this List<AttributeItemInfo> attributes)
        {
            var plurAttr= attributes.ContainsName(ConstPluralName).FirstOrDefault();
            if (plurAttr != null)
            {
                var args = plurAttr.ToParentNameValues();
                if (args != null)
                {
                    var pluralName = args.NameValues[ConstPluralArgumentPluralEntityName];
                    if (!string.IsNullOrEmpty(pluralName) )
                        return pluralName;
                }
            }
            return string.Empty;
        }
       
        public static string GetPluralName(this MethodItemInfo methodDef)
        {
            // search for a method level PluralName attribute
            var attrGenerator = methodDef.Attributes.Where(o => o.Equals(ConstPluralName)).FirstOrDefault(); ;
            if (attrGenerator!= null)
            {
                var args = attrGenerator.ToParentNameValues();
                if (args != null)
                {
                    var pluralName = args.NameValues[ConstPluralArgumentPluralEntityName];
                    if (!string.IsNullOrEmpty(pluralName))
                        return pluralName;
                }
            }

            return string.Empty;
        }
        #endregion

        public static List<AttributeItemInfo> ContainsName(this List<AttributeItemInfo> attributes, string name)
        {
            return attributes.Where(o => o.Name.Contains(name)).ToList();
        }

        public static List<NameType> ToNameTypeList(this List<MemberItemInfo> members)
        {
            return (from m in members select new NameType(m.Name, m.Type.CleanChars())).ToList();
        }

        public static bool Disabled(this AttributeItemInfo attributeInfo)
        {
            if (attributeInfo != null)
            {
                var args = attributeInfo.ToParentNameValues();
                if (args != null && args.NameValues.Count >0)
                {
                    string disabled = args.NameValues["Disabled"].CleanChars();
                    if (!string.IsNullOrEmpty(disabled) && bool.TryParse(disabled, out bool isDisabled))
                        return isDisabled;
                }

            }
            return false;
        }
        public static bool Disabled(this MethodItemInfo methodDef, string generatorAttributeName)
        {
            var attrGenerator = methodDef.Attributes.Where(o => o.Equals(generatorAttributeName)).FirstOrDefault(); ;
            if (attrGenerator!= null)
            {
                var args = attrGenerator.ToParentNameValues();
                if (args != null)
                {
                    var disabled = args.NameValues["Disabled"];
                    if (!string.IsNullOrEmpty(disabled) && bool.TryParse(disabled, out bool isDisabled))
                        return isDisabled;
                }
            }

            return false;
        }

        /// <summary>
        /// Format
        ///     {Name= "AttributeName
        ///      Args= Dictionary<string,string>();  
        ///     }
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public static ParentNameValues ToParentNameValues(this AttributeItemInfo attributeInfo)
        {


            NameValueCollection collection = new NameValueCollection();
            foreach (ArgumentItemInfo item in attributeInfo.Arguments)
            {
                
                collection.Add(item.Name, item.Type);
            }

            return new ParentNameValues() { ParentName=attributeInfo.Name, NameValues=collection };
        }

     
        public static string Alternate(this string value, string alternateValue)
        {

            return string.IsNullOrEmpty(value) ? alternateValue : value;

        }

        public static string CleanChars(this string value)
        {
            if (!string.IsNullOrEmpty(value))
                return value.Replace("\"", "");
            else
                return value;
        }
    }
}
