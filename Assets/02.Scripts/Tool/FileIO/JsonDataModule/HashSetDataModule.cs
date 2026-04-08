using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace FileIO.JsonDataModule
{
    public class HashSetDataModule<TKey> : IJsonDataModule
    {
        public string Key { get; private set; }

        public HashSet<TKey> Value { get; private set; }

        public HashSetDataModule(string key, HashSet<TKey> hashValue)
        {
            Key = key;
            Value = hashValue ?? new HashSet<TKey>();
        }

        /* ?? 는 C#의 **null 병합 연산자(null-coalescing operator)이다.
         * ??을 사용하게되면 해당 연산자의 의미는 왼쪽 값이 null이 아니면 왼쪽을 사용하고
         * 왼쪽 값이 null이면 오른쪽을 사용한다.
         * ?? 연산자의 예시)
         * string name = inputName ?? "Unknown";
         * - inputName이 null이 아니면 그 값이 name
         * - null이면 "Unknown"이 name
         */
        
        public void OnLoad(JToken dataSegment)
        {
            if (dataSegment != null && dataSegment.Type != JTokenType.Null)
            {
                Value = dataSegment.ToObject<HashSet<TKey>>();
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