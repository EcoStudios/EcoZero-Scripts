using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TMP_Text itemTXT;
    [SerializeField] private Image backGround;
    public static Image ItemSprite;
    public static TMP_Text ItemTXT;
    public static Image BackGround;

    private void Start()
    {
        ItemTXT = itemTXT;
        ItemSprite = itemSprite;
        BackGround = backGround;
    }

    public static void Set(ItemStack itemStack)
    {
        if (itemStack.Type != ItemType.AIR)
        {
            // setting the iteminfo stuff
            BackGround.gameObject.SetActive(true);
            ItemSprite.sprite = itemStack.Type.Sprite;
            if (itemStack.StackSize <= 1)
            {
                ItemTXT.text = itemStack.Type.Name;
            }
            else
            {
                ItemTXT.text = $"({itemStack.StackSize}X) {itemStack.Type.Name}";
            } 
        }
        else
        {
           // disabling iteminfo stuff
           ItemTXT.text = null;
           ItemSprite.sprite = null;
           BackGround.gameObject.SetActive(false);
        }
    }

    public static bool IsActive()
    {
        return BackGround.gameObject.activeSelf;
    }



}
