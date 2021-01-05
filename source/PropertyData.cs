using System.Reflection;

namespace Dennysoft.Core.JsonDoc
{
    /// <summary>
    /// this is only for organizational purposes.
    /// </summary>
    internal class PropertyData
    {
        public bool IsGenericType { get; set; }

        public PropertyInfo[] Properties { get; set; }
    }
}