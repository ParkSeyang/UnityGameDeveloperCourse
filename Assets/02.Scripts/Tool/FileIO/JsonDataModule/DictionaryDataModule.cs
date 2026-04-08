using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text;

namespace FileIO.JsonDataModule
{
    public class DictionaryDataModule<TKey, TValue> : IJsonDataModule
    {
        public string Key { get; private set; }
        public Dictionary<TKey, TValue> Value { get; private set; }

        public DictionaryDataModule(string key, Dictionary<TKey, TValue> defaultValue = null)
        {
            Key = key;
            Value = defaultValue ?? new Dictionary<TKey, TValue>();
        }
 
        public void OnLoad(JToken dataSegment)
        {
            if (dataSegment != null && dataSegment.Type != JTokenType.Null)
            {
                Value = dataSegment.ToObject<Dictionary<TKey, TValue>>();
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