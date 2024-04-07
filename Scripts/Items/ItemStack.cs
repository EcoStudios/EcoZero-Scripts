using System;
using UnityEngine;

public class ItemStack
{
    /*
     * This is basically Minecraft's Itemstack system (yes we are just stealing their shit stfu),
     * Everytime u want a itemstack u just do:
     * new ItemStack(ItemType, stackSize, slot number, itemName)
     * for the ItemType you just do ItemType.item to get the item u want
     * for the stacksize just put in any number that's less than or equal to the max itemstack size of the itemtype (will be clamped at the
     * max item stack size). For the slot number just enter in a slot that can fit in the player's inventory and if it isnt set it'll be zero
     * Finally you enter the Item's name if it has one, if it doesnt it'll just be null.
     */
    
    public ItemType Type { get; }
    public int StackSize { get; set; }
    public int Slot { get; set; }
    public string ItemName { get; set; }
    public ItemData Data { get; }


    public ItemStack(ItemType type,int stackSize, int slot = 0, string itemName = null, ItemData itemData = null)
    {
        Type  = type;
        StackSize = Mathf.Clamp(stackSize, 0, type.MaxSize);
        Slot = slot;
        ItemName = itemName;
        Data = itemData;
        if (ItemName == null) { ItemName = Type.Name; }
        if (itemData == null) { Data = type.Data; }
    }
    
}
