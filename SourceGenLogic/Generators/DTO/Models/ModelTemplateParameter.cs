using Restarted.Generators.Definitions.Extensions;
using Restarted.Generators.Generators.BaseModels;
using Restarted.Generators.Processor.Interfaces;
using SourceGeneratorParser.Models.Types;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Restarted.Generators.Generators.DTO.Template
{
    public class ModelTemplateParameter: ITemplateParameter
    {

       
        public ModelTemplateParameter(TypeDefinitionInfo typeDefinitionInfo,string nameSpace, string sourceFileName,string commaSeperatedMembers,bool allMembersNullable)
        {
            SourceFileName = sourceFileName;
            PreferredNameSpace = nameSpace;

            TypeDefinitionInfo = typeDefinitionInfo;
            Members = MapMembers( commaSeperatedMembers, typeDefinitionInfo.Members.ToNameTypeList(),  allMembersNullable);
        }
        public string SourceFileName { get; set; }
        public string PreferredNameSpace { get; set; }
        public  List<NameType> Members { get;  }
        public TypeDefinitionInfo TypeDefinitionInfo { get; set; }

        private  List<NameType> MapMembers(string commaSeperatedMembers, List<NameType> classMembers, bool allMembersNullable)
        {
            Dictionary<string, string> membersAndTypes = new Dictionary<string, string>();
            // e.g.UserListDTO Address, Email, Phone properties
            // definition
            // Address.Line1 as Address:string,
            // Contact.PrimaryEmail as Email:string,
            // Contact.PrimaryPhone as Phone:string
            // Based on definition GENERATE Mapper profile

            // "Id:int?,Name:string?,UserName:string?, Address.Line1 as AddressDTO:string, Contact.PrimaryEmail as Email:string, Roles:List<RoleDTO>"
            // "Id:int?,Name:string?,UserName:string?, Address as Address, Contact:ContactDTO, Roles:List<RoleDTO>"
            if (!string.IsNullOrEmpty(commaSeperatedMembers))
            {
                string fieldNameToAdd = null;
                string fieldDataTypeToAdd = null;


                var fields = commaSeperatedMembers.Trim().Split(',');
                
                
                foreach (var field in fields)
                {
                    var fieldValue = field.Split(':');
                    if (fieldValue.Length > 1)
                    {
                        fieldNameToAdd = fieldValue[0].Trim();
                        fieldDataTypeToAdd = fieldValue[1].Trim();

                    }
                    else
                    {
                        fieldNameToAdd = fieldValue[0].Trim();
                        var memberInfo = classMembers.Where(o => o.Name.Equals(fieldNameToAdd, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                        if (memberInfo != null)
                            fieldDataTypeToAdd = memberInfo.Type ??= "string";
                        else
                            fieldDataTypeToAdd = "string";
                    }

                    //// Check "as" alias added in definition
                    //string asDefinition = " as ";
                    //if (fieldNameToAdd.Trim().Contains(asDefinition))
                    //{
                    //    string[] asSplit = fieldNameToAdd.Split(new[] { asDefinition }, StringSplitOptions.None);
                    //    string asPart = asSplit[0];
                    //    fieldNameToAdd = asSplit[1];

                    //}


                    membersAndTypes.Add(fieldNameToAdd, fieldDataTypeToAdd);
                }

            }
            else
            { //if members is not defined, add all
                foreach (var member in classMembers)
                {
                    membersAndTypes.Add(member.Name, member.Type);
                }
            }

            if (allMembersNullable)
            {
                Dictionary<string, string> membersAndTypesUpdated = new Dictionary<string, string>();
                var toUpdate = membersAndTypes
                .Select(c => new KeyValuePair<string, string>(c.Key, c.Value));
                foreach (var kv in toUpdate)
                    membersAndTypesUpdated[kv.Key] = (!kv.Value.Contains('?')) ? $"{kv.Value}?" : kv.Value;

                membersAndTypes = membersAndTypesUpdated;

            }

            List<NameType> templateMembers = new List<NameType>();
            foreach (var memberName in membersAndTypes.Keys)
            {
                // taking member type from the Entity's attribute definition

                templateMembers.Add(new NameType(memberName, membersAndTypes[memberName]));
            }

            return templateMembers;
        }

    }


}
