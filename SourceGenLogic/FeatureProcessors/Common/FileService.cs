using Restarted.Generators.Common.Configurations;
using Restarted.Generators.Definitions;
using System;
using System.Collections.Generic;
using System.Text;
using static Restarted.Generators.Common.Configurations.GeneratorConfigurations;

namespace Restarted.Generators.FeatureProcessors.Common
{
    internal static class FileService
    {
        internal static FolderAndNamespacePath GenerateFolderPathAndNamespace(string generationPhysicalPath)
        {



            string allFolderPathSuffix = "Generated";
            string finalPath = generationPhysicalPath;


          


            string nameSpace = finalPath.Replace('\\', '.');
            if (nameSpace.Substring(nameSpace.Length - 1, 1) == ".")
                nameSpace = nameSpace.Remove(nameSpace.Length - 1, 1);
            


            if (!string.IsNullOrEmpty(allFolderPathSuffix))
                finalPath = $"{finalPath}\\{allFolderPathSuffix}\\";
            return new FolderAndNamespacePath(finalPath, nameSpace);
        }


        internal static String ConventionBasedPath(string conventionPath, string featureName, string featureModuleName, string methodName = null)
        {
            string finalPath = conventionPath;


            if (!string.IsNullOrEmpty(featureName))
                finalPath=  finalPath.Replace("{Feature}", featureName);
            else
                finalPath=  finalPath.Replace("\\{Feature}", "");

            if (!string.IsNullOrEmpty(featureModuleName))
                finalPath=  finalPath.Replace("{FeatureModule}", featureModuleName);
            else
                finalPath=  finalPath.Replace("\\{FeatureModule}", "");

            if (!string.IsNullOrEmpty(methodName))
                finalPath=  finalPath.Replace("{MethodName}", methodName);
            else
                finalPath=  finalPath.Replace("\\{MethodName}", "");


            return finalPath;
        }

        internal static string GenerateSourceAtFolderLocation(string filePath, string fileName, string generatedSourceCode)
        {

            string directory = filePath;
            string extension = ".g.cs";
            string generatedFolder = "Generated\\";
            if (directory.Substring(directory.Length -1, 1) != "\\")
            {
                directory = filePath + "\\";
            }
            directory = directory + generatedFolder;
            string path = $"{directory}{fileName}{extension}";
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            File.WriteAllText(path, generatedSourceCode);

            return path;
        }
    }
}
