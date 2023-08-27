using Restarted.Generators.Generators.Controllers.Models;
using Restarted.Generators.Generators.DTO.Template;
using SourceGeneratorParser.Models.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Restarted.Generators.FeatureProcessors.Process
{
    internal static class ParameterHelper
    {

        #region DTO Helper methods
        public static KeyValuePair<string, string> ConstructorParamString(ModelTemplateParameter parameter)
        {
            StringBuilder blder = new StringBuilder();
            StringBuilder blderMap = new StringBuilder();
            int idx = 0;
            foreach (var nameType in parameter.Members)
            {
                if (string.IsNullOrEmpty(nameType.Name)) continue;
                idx +=1;
                string comma = (idx == parameter.Members.Count) ? "" : ",";
                string name = LowerFirstChar(nameType.Name);
                blder.AppendLine($"{nameType.Type} {name}{comma}");

                blderMap.AppendLine($" {nameType.Name} = {name} " +";");
            }
            return new KeyValuePair<string, string>(blder.ToString(), blderMap.ToString());
        }

        public static string PropertiesString(ModelTemplateParameter parameter)
        {
            StringBuilder blderCtor = new StringBuilder();


            int idx = 0;

            string conststructorParam = string.Empty;
            string memberMapping = string.Empty;
            foreach (var nameType in parameter.Members)
            {
                idx +=1;
                string comma = (idx == parameter.Members.Count) ? "" : ",";


                blderCtor.AppendLine($"public {nameType.Type} {nameType.Name} " +" { get; set; }");



            }
            return blderCtor.ToString();
        }
       

        internal static string LowerFirstChar(string value)
        { return value.Substring(0, 1).ToLower()+ value.Substring(1); }
        #endregion

        #region ControllerWithCQRS
        internal static string ActionMethodsDeleteBodies(ApiTemplateParameter parameters)
        {
            StringBuilder bldr = new StringBuilder();
            foreach (var param in parameters.Actions.Where(o=> o.HttpAction =="DELETE").ToList())
            {
                string DeleteTemplate = @$"                        
                        [HttpDelete(""{param.Route}"")]
                        public async Task<IActionResult> {param.MethodName}({param.MethodParameterDefinition})
                        {{

                            var result = await this.mediator.Send(new {param.CQRSRequestName}({param.MethodParametersString})).Result;
                            return Ok(result);
                        }}";
                bldr.AppendLine(DeleteTemplate);
            }
            return bldr.ToString();
        }

        internal static string ActionMethodsGetBodies(ApiTemplateParameter parameters)
        {
            StringBuilder bldr = new StringBuilder();
            foreach (var param in parameters.Actions.Where(o => o.HttpAction =="GET").ToList())
            {
                string getTemplate = @$"                        
                        [HttpGet(""{param.Route}"")]
                        public async Task<IActionResult> {param.MethodName}({param.MethodParameterDefinition})
                        {{

                            var result = await this.mediator.Send(new {param.CQRSRequestName}({param.MethodParametersString})).Result;
                            return Ok(result);
                        }}";
                bldr.AppendLine(getTemplate);
            }
            return bldr.ToString();
        }

        internal static string ActionMethodsPostBodies(ApiTemplateParameter parameters)
        {
            StringBuilder bldr = new StringBuilder();
            foreach (var param in parameters.Actions.Where(o => o.HttpAction =="POST").ToList())
            {
                string postTemplate = @$"                        
                        [HttpPost(""{param.Route}"")]
                        public async Task<IActionResult> {param.MethodName}({param.MethodParameterDefinition})
                        {{

                            var result = await this.mediator.Send(new {param.CQRSRequestName}({param.MethodParametersString})).Result;
                            return Ok(result);
                        }}";
                bldr.AppendLine(postTemplate);
            }
            return bldr.ToString();
        }

        internal static string ActionMethodsPutBodies(ApiTemplateParameter parameters)
        {
            StringBuilder bldr = new StringBuilder();
            foreach (var param in parameters.Actions.Where(o => o.HttpAction =="PUT").ToList())
            {
                string putTemplate = @$"                        
                        [HttpPut(""{param.Route}"")]
                        public async Task<IActionResult> {param.MethodName}({param.MethodParameterDefinition})
                        {{

                            var result = await this.mediator.Send(new {param.CQRSRequestName}({param.MethodParametersString})).Result;
                            return Ok(result);
                        }}";
                bldr.AppendLine(putTemplate);
            }
            return bldr.ToString();
        }

        internal static string ArgumentsTypeNameString(MethodItemInfo currentMethod)
        {
            StringBuilder bldr = new StringBuilder();
                int index=0;
            foreach (ArgumentItemInfo prop in currentMethod.Arguments)
            {
                index +=1;
                bldr.AppendLine($"{prop.Type} {prop.Name}" +(index == currentMethod.Arguments.Count ? "" : ",")); 
            }
            return bldr.ToString();
        }

        internal static string RequestArgumentsString(MethodItemInfo currentMethod)
        {
            StringBuilder bldr = new StringBuilder();
            int index = 0;
            foreach (ArgumentItemInfo prop in currentMethod.Arguments)
            {
                index +=1;
                bldr.AppendLine($"request.{prop.Name}" +(index == currentMethod.Arguments.Count ? "" : ","));
            }
            return bldr.ToString();
        }
        #endregion
    }

}
