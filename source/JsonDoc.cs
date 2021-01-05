using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Dennysoft.Core.JsonDoc
{
    /// <summary>
    /// this static class holds all the methods for post processing a json document.
    /// for the serialization Newtonsoft.Json is used.
    /// </summary>
    public static class JsonDoc
    {
        /// <summary>
        /// creates an indented json document with default values and no type name handling.
        /// </summary>
        /// <param name="objectToSerialize"></param>
        /// <returns></returns>
        public static string ToJson(object objectToSerialize)
        {
            var jsonSerializer = JsonSerializer.Create(
                new JsonSerializerSettings()
                {
                    DefaultValueHandling = DefaultValueHandling.Include,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Formatting = Formatting.Indented,
                    TypeNameHandling = TypeNameHandling.None
                });


            using (var textWriter = new StringWriter())
            {
                jsonSerializer.Serialize(textWriter, objectToSerialize);
                var json = textWriter.ToString();

                return json;
            }
        }

        /// <summary>
        /// creates an indented json document, and post-processes it so the Doc properties of the JsonDocAttributes will be set above the properties itself.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToDocumentedJson<T>(T obj)
        {
            var res = ToJson(obj);

            if (res == null) return null;

            var documented = PostProcess<T>(res);

            return documented;
        }

        /// <summary>
        /// this is the actual post processor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonObj"></param>
        /// <returns></returns>
        private static string PostProcess<T>(string jsonObj)
        {
            var type = typeof(T);
            StringBuilder sb = new StringBuilder();
            GetClassLevelJsonDoc(sb, type, "");

            var lines = jsonObj.Split('\n');

            Stack<PropertyData> propStack = new Stack<PropertyData>();

            var props = type.GetProperties();
            bool isGenericType = false;

            propStack.Push(new PropertyData(){Properties =  props});

            int inArrayLevel = 0;

            if(props != null && props.Any())
            {
                foreach (var actualLine in lines)
                {
                    var parts = actualLine.Split(':');
                    var indLevel = actualLine.ToCharArray().FirstIndexOf(x => x != ' ');
                    string indent = actualLine.Substring(0, indLevel);

                    if (parts.Length > 1)
                    {
                        var name = parts[0].Replace("\"", "").Trim();
                        if (name.HasValue() && props != null)
                        {
                            foreach (var propertyInfo in props)
                            {
                                if (propertyInfo.Name == name)
                                {
                                    var propType = propertyInfo.PropertyType;

                                    var attribs = propertyInfo.GetCustomAttributes(typeof(JsonDocAttribute), false);
                                    if (attribs != null && attribs.Any())
                                    {
                                        foreach (JsonDocAttribute propAttrib in attribs)
                                        {

                                            sb.AppendLine("");
                                            sb.AppendLine($"{indent}//{propAttrib.Doc}");

                                        }
                                    }

                                    if ((propType.IsArray || (propType.IsGenericType && propType.GetGenericTypeDefinition().IsIn(
                                        typeof(List<>), 
                                        typeof(ObservableCollection<>), 
                                        typeof(Collection<>)))))
                                    {
                                        var genTypes = propType.GetGenericArguments();
                                        if (genTypes.Length == 1)
                                        {
                                            props = genTypes[0].GetProperties();
                                            isGenericType = true;
                                            propStack.Push(new PropertyData(){IsGenericType = true, Properties = props});
#if DEBUG
                                            //sb.AppendLine($"//push type: {propType}");
#endif
                                            GetClassLevelJsonDoc(sb, genTypes[0], indent);
                                            inArrayLevel++;
                                        }
                                        
                                    }
                                    else if (propType.IsClass && propType != typeof(String))
                                    {
                                        props = propType.GetProperties();
                                        isGenericType = false;
                                        propStack.Push(new PropertyData(){ Properties = props});
#if DEBUG
                                        //sb.AppendLine($"//push type: {propType}");
#endif
                                        GetClassLevelJsonDoc(sb, propType, indent);
                                    }
                                    break;
                                }
                            }
                        }
                    }

                    if (actualLine.EndsWith((isGenericType && inArrayLevel > 0)? "],\r": "},\r"))
                    {
                        //end of class
                        if (propStack.Count > 0)
                        {
                            propStack.Pop();
#if DEBUG
                            //sb.AppendLine($"//pop type, isGeneric: {isGenericType}, arrayLevel: {inArrayLevel}");
#endif

                            if(isGenericType && inArrayLevel > 0) inArrayLevel--;

                            if (propStack.Count > 0)
                            {
                                var peek = propStack.Peek();
                                props = peek.Properties;
                                isGenericType = peek.IsGenericType;
                            }
                            else
                            {
                                props = null;
                                isGenericType = false;
                            }
                        }
                    }
                    sb.Append(actualLine);
                }
            }

            

            return sb.ToString();
        }

        /// <summary>
        /// this method appends the content of the JsonDocAttribute applied on a class.
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="type"></param>
        /// <param name="indent"></param>
        private static void GetClassLevelJsonDoc(StringBuilder sb, Type type, string indent)
        {
            var classAttribs = type.GetCustomAttributes(typeof(JsonDocAttribute), true);
            

            if (classAttribs != null && classAttribs.Any())
            {
                foreach (JsonDocAttribute classAttrib in classAttribs)
                {
                    sb.AppendLine($"{indent}//{classAttrib.Doc}");
                }
            }

            
        }

        
    }
}