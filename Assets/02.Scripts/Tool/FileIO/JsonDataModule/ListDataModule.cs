using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

namespace FileIO.JsonDataModule
{
    public class ListDataModule<T> : IJsonDataModule
    {
        public string Key { get; private set; }
        public List<T> Value { get; private set; }

        public ListDataModule(string key, List<T> defaultValue = null)
        {
            Key = key;
            Value = defaultValue ?? new List<T>();
        }
   
        public void OnLoad(JToken dataSegment)
        {
            if (dataSegment != null && dataSegment.Type != JTokenType.Null)
            {
                Value = dataSegment.ToObject<List<T>>();
            }
        }

        public JToken OnSave()
        {
            return JToken.FromObject(Value);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine($"Key: {Key}");
       
            foreach (var item in Value)
            {
                builder.AppendLine(item.ToString());
            }
       
            return builder.ToString();
        }
    }
}