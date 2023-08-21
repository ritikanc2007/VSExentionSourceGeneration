using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Restarted.Generators.Common.BaseClasses
{
    public abstract class GeneratorAttributeBase : Attribute
    {
        public bool Disable { get; set; }
        // Used in the Generated file paths and namespaces
        public string FeatureName { get; set; } = null;
        public string FeatureModuleName { get; set; } = null;
        /// <summary>
        /// Convention is used to define pattern for DTO Name. e.g.{Entity}DTO will generate name UserDTO.cs
        /// Repositories e.g. {Entity}Repository OR  I{Entity}Repository
        /// </summary>
        public string Convention { get; set; } = null;

        /// <summary>
        /// this should be PathConfiguration.COMMON_DTO_PATH
        /// </summary>
        public string GenerationPath { get; set; } = null;
    }
}