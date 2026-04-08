using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;


namespace FileIO.JsonDataModule
{
    public class JsonDataModule<T> : IJsonDataModule
    {
        public string Key { get; private set; }
        public T Value { get; private set; }

        public JsonDataModule(string key, T value)
        {
            Key = key;
            Value = value;
        }

        public void OnLoad(JToken dataSegment)
        {
            if (dataSegment != null && dataSegment.Type != JTokenType.Null)
            {
                Value = dataSegment.ToObject<T>();
            }
        }

        public JToken OnSave()
        {
            return JToken.FromObject(Value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
        
    }
    
}