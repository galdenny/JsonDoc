using System;

namespace Dennysoft.Core.JsonDoc
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class JsonDocAttribute : Attribute
    {
        public string Doc { get; set; }

        public JsonDocAttribute()
        {

        }

        public JsonDocAttribute(string doc)
        {
            Doc = doc;
        }
    }
}
