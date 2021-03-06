﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection;
using Aritiafel.Locations.StorageHouse;
using Aritiafel.Artifacts.Calculator;

namespace Aritiafel.Artifacts.TinaValidator.Serialization
{

    public class OtherJsonConverter : DefaultJsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
            => typeof(IObject).IsAssignableFrom(typeToConvert) || 
               typeof(Unit).IsAssignableFrom(typeToConvert) ||
               typeToConvert == typeof(string) || typeToConvert == typeof(char);
        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            Type[] type = new Type[] { null };
            if (typeof(IObject).IsAssignableFrom(typeToConvert))
                type[0] = typeof(IObject);
            else if (typeof(Unit).IsAssignableFrom(typeToConvert))
                type[0] = typeof(Unit);
            else if (typeof(char) == typeToConvert)
                type[0] = typeof(char);
            else
                type[0] = typeof(string);
                    
            return (JsonConverter)Activator.CreateInstance(
                typeof(OtherJsonConverterInner<>).MakeGenericType(type),
                BindingFlags.Instance | BindingFlags.Public, null, Array.Empty<object>(), null);
        }

        private class OtherJsonConverterInner<T> : DefaultJsonConverter<T>
        {
            public OtherJsonConverterInner()
                :base()
            { }
        }
    }
}
