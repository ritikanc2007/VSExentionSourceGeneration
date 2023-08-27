using System;
using System.Collections.Generic;
using System.Text;

namespace Restarted.Generators.FeatureProcessors
{
    public enum TemplateType
    {
        T4,
        Static
    }
    internal class TemplateToUse
    {

        public static TemplateType SwitchFlag = TemplateType.Static;
    }
}
