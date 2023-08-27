﻿using Restarted.Generators.FeatureProcessors.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restarted.Generators.FeatureProcessors.Models.FilePathHandlers
{
    public static class PathHelper
    {


        public static PathInfo GetPresetPaths(string basePath, string presetPathTemplate, string templateFileNameNoExt,PresetType presetType)
        {
            basePath= @"C:\Users\Narendra\source\repos\VSExentionSourceGeneration\SourceGenLogic\StaticTemplates\Templates"; //AppDomain.CurrentDomain.BaseDirectory;
            string ParentPath = presetPathTemplate.Replace("{PresetType}", presetType.ToString());
            string basefileName = templateFileNameNoExt;
            string ParentTemplate = @$"{basefileName}.txt";
            string ParentPartialTemplate = $@"{basefileName}Partial.txt";
            string Main = $@"{basePath}{ParentPath}{ParentTemplate}";
            string Partial = $@"{basePath}{ParentPath}{ParentPartialTemplate}";
            string ParentUnitTestPath = $@"{ParentPath}UnitTest\";
            string UnitTestMain = $@"{basePath}{ParentPath}UnitTest\{ParentTemplate}";
            string UnitTestPartial = $@"{basePath}{ParentPath}UnitTest\{ParentPartialTemplate}";

            return new PathInfo(Main, Partial, UnitTestMain, UnitTestPartial);
        }
        public static PathInfo GetTypePaths(string basePath, string typePathTemplate)
        {
            string ParentPath = typePathTemplate;
            string basefileName = "TypeTemplate";
            string ParentTemplate = @$"{basefileName}.txt";
            string ParentPartialTemplate = $@"{basefileName}Partial.txt";
            string Main = $@"{basePath}{ParentPath}{ParentTemplate}";
            string Partial = $@"{basePath}{ParentPath}{ParentPartialTemplate}";
            string ParentUnitTestPath = $@"{ParentPath}UnitTest\";
            string UnitTestMain = $@"{basePath}{ParentPath}UnitTest\{ParentTemplate}";
            string UnitTestPartial = $@"{basePath}{ParentPath}UnitTest\{ParentPartialTemplate}";

            return new PathInfo(Main, Partial, UnitTestMain, UnitTestPartial);
        }

        public static PathInfo GetMethodPaths(string basePath, string methodName, MethodTypes methodType, ResultTypes resultType)
        {
            
            string ParentPath = PathConstants.MethodPathTemplate.Replace("{@MethodName}", methodName)
                                                  .Replace("{Commands}", methodType.ToString())
                                                  .Replace("{ResultType}", resultType.ToString());

            string basefileName = $@"{resultType.ToString()}Result";
            string ParentTemplate = $@"{basefileName}.txt";
            string ParentPartialTemplate = $@"{basefileName}Partial.txt";
            string Main = $@"{basePath}{ParentPath}{ParentTemplate}";
            string Partial = $@"{basePath}{ParentPath}{ParentPartialTemplate}";
            string ParentUnitTestPath = $@"{ParentPath}UnitTest\";
            string UnitTestMain = $@"{basePath}{ParentPath}UnitTest\{ParentTemplate}";
            string UnitTestPartial = $@"{basePath}{ParentPath}UnitTest\{ParentPartialTemplate}";

            return new PathInfo(Main, Partial, UnitTestMain, UnitTestPartial);
        }

    }

}
