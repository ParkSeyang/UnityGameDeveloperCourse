using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace FileIO.JsonDataModule
{
   public interface IJsonDataModule
   {
      string Key { get; }
      
      void OnLoad(JToken dataSegment);
      JToken OnSave();
   }
}
