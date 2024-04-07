using Player.Inventory;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    
    public static void Pickup(GameObject target)
    {
        ItemStack itemStack = ItemManager.droppedItems[target.name];
        itemStack.Slot = -50;
        if (InventoryManager.CanHoldItem(itemStack) && !GameUtils.IsPaused)
        {
            InventoryManager.AddItemStack(itemStack);
            ItemManager.droppedItems.Remove(target.name);
            Destroy(target);
        }
    }


}
