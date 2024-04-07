using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class ItemManager : MonoBehaviour
{
   public static Dictionary<String, ItemStack> droppedItems = new Dictionary<string, ItemStack>();

   public static ItemStack GetDroppedItemStack(String id)
   {
      if (droppedItems.TryGetValue(id, out ItemStack itemStack))
      {
         return itemStack;
      }
      else
      {
         return new ItemStack(ItemType.AIR, 0);
      }
   }

   private static GameObject PrvSpawnItem(ItemStack itemStack, Vector3 location, Vector3 velocity = default)
   {
      GameObject gameObject = Instantiate(itemStack.Type.Prefab);
      gameObject.transform.localPosition = location;
      if (gameObject != null)
      {
         gameObject.GetComponentInChildren<Rigidbody>().velocity = velocity;
         gameObject.transform.localPosition = location;
         return gameObject;
      }
      return null;
   }

   public static void SpawnItem(ItemStack itemStack, Vector3 location, Vector3 velocity = default)
   {
      GameObject obj = PrvSpawnItem(itemStack, location, velocity);
      float random = Random.Range(1000000, 2000000);
      string id = Convert.ToString(random);
      if (droppedItems.ContainsKey(id))
      {
         while (droppedItems.ContainsKey(id))
         {
            random = Random.Range(1000000, 2000000);
            id = Convert.ToString(random);
         }
      }
      droppedItems.Add(id, itemStack);
      obj.gameObject.name = id;
   }


}
