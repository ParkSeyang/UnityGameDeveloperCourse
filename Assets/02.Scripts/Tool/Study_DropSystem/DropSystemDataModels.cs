using System;
using System.Globalization;
using UnityEngine;

public class Package
 {
     public int PackageID { get; set; }
     public string PackageName { get; set; }
     public string ItemGroupIDs { get; set; }
     public ItemGroup[] ItemGroup { get; private set; }

     public void SetItemGroup(ItemGroup[] allItemGroup)
     {
         var itemGroupIds = ItemGroupIDs.Trim('[',']').Split(',');
         ItemGroup = new ItemGroup[itemGroupIds.Length];

         for (int i = 0; i < itemGroupIds.Length; i++)
         {
             int targetID = int.Parse(itemGroupIds[i]);
             
             for (int j = 0; j < allItemGroup.Length; j++)
             {
                 if(targetID.Equals(allItemGroup[j].GroupID) == false) continue;
                 ItemGroup[i] = allItemGroup[j];
             }
         }
         
     }
 }
 
 public class ItemGroup
 {
     public int GroupID { get; set; }
     public string GroupName { get; set; }
     public string ItemKeys { get; set; }
     public string ChanceArray { get; set; }

     //이거 그냥 Key만 가지고 있어도 상관없음
     public ItemData[] Items { get; private set; }
     public float[] Chance { get; private set; }

     public void SetChance()
     {
         // [0,1,7] => 0,1,7 => 0 | 1 | 7
         var itemChance = ChanceArray.Trim('[',']').Split(',');
         // itemIDs = {0,1,7}
         Chance = new float[itemChance.Length];
         
         for (int i = 0; i < itemChance.Length; i++)
         {
             Chance[i] = float.Parse(itemChance[i], CultureInfo.InvariantCulture);
         }
     }

     //이거 그냥 Key만 가지고 있어도 상관없음
     public void SetItems(ItemData[] items)
     {
         var itemIDs = ItemKeys.Trim('[',']').Split(',');
         Items = new ItemData[itemIDs.Length];

         for (int i = 0; i < itemIDs.Length; i++)
         {
             int itemID = int.Parse(itemIDs[i], CultureInfo.InvariantCulture);
             for (int j = 0; j < items.Length; j++)
             {
                 if(items[j].Key == itemID) Items[i] = items[j];
             }
         }
     }

     public ItemData GetItemByChance()
     {
         while (true)
         {
             for (int i = 0; i < Items.Length; i++)
             {
                 float chance = Chance[i];
                 float randomValue = UnityEngine.Random.Range(0.0f, 1.0f);
             
                 if(randomValue < chance) return Items[i];
             }
         }
     }
 }
 
 public class ItemData
 {
     public int Key { get; set; }
     public string ResourceKey { get; set; }
     public string Name { get; set; }
 }