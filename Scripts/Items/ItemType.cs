using System.Collections.Generic;
using UnityEngine;

public class ItemType
{
    
    /* This works like Minecraft's Materials stuff (not really),
     * Everytime u want to add a new item you just paste this code in:
     * public static readonly ItemType whatever = Register("key", maxItemSize, ItemIMG, "Name", Prefab);
     * Replace the whatever with the name of the item, then replace the "key" with whatever you want the key to be,
     * preferably use like the lowercase version of the item's name, then the next you want to enter the max size the item stack can get
     * after that you can enter the itemIMG, just add the image to the Resources/ItemSprites folder, then just use:
     * Resources.Load<Sprite>("ItemSprites/whatever") to load the image as a sprite. Lastly you want to add the Item's default name and
     * prefab for the item.
     */
    
    public static List<ItemType> Items = new List<ItemType> { TEST, AIR };
    
    
    public static readonly ItemType TEST = Register("test", 50, ItemData.DEFAULT, Resources.Load<Sprite>("ItemSprites/TestImage"), "Test", Resources.Load<GameObject>("Items/Prefabs/TestItem"));
    public static readonly ItemType AIR = Register("air", 0, ItemData.DEFAULT, null, "AIR", null);

    public static readonly ItemType AXE = Register("axe", 1, new ItemData(4.0f, true, "isSwinging",Quaternion.Euler(0, 0, -90), new Vector3(0, -0.4f, 0), new Vector3(0.7f, 0.7f, 0.7f)), null, "Axe", Resources.Load<GameObject>("Items/Prefabs/Axe"));

    private string _key;
    public Sprite Sprite { get; set; }
    public int MaxSize { get; }
    public string Name { get; }
    public GameObject Prefab;
    public ItemData Data;

    private static ItemType Register(string key,int maxSize, ItemData itemData, Sprite itemImage, string name, GameObject prefab)
    {
        return new ItemType(key, maxSize, itemData, itemImage, name, prefab);
    }

    public ItemType(string itemKey,int maxSize, ItemData itemData, Sprite itemImage, string name, GameObject prefab)
    {
        _key = itemKey;
        Sprite = itemImage;
        MaxSize = maxSize;
        Name = name;
        Prefab = prefab;
        Data = itemData;
    }

    public static ItemType parse(string parse)
    {
        for (int i = 0; i <= Items.Count-1; i++)
        {
            ItemType item = Items[i];
            if (item.Name == parse)
            {
                return item;
            }
        }

        return AIR;
    }

}
