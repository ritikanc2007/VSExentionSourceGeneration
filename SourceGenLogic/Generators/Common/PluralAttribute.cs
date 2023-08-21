using System;
using System.Collections.Generic;
using System.Text;

namespace Restarted.Generators.Generators.Common
{
    [AttributeUsage(AttributeTargets.Interface| AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class PluralNameAttribute:Attribute
    {
        public PluralNameAttribute( string pluralEntity, string entity= null)
        {
            Entity=entity;
            PluralEntity=pluralEntity;
        }

        public string Entity { get; set; }

        public string PluralEntity { get; set; }
    }
}
