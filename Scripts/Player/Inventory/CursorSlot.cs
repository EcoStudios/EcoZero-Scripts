using System;
using System.Collections;
using System.Collections.Generic;
using Player.Inventory;
using UnityEngine;
using UnityEngine.UI;

public class CursorSlot : MonoBehaviour
{
    private static GameObject _slot;
    private static ItemStack _itemStack;
    public static bool IsActive;

    public static void CreateCursorSlot()
    {
        if (Cursor.visible)
        {
            _slot = new GameObject("TEMP_CursorSlot");
            _slot.transform.parent = InventoryManager.GetInventory().transform;
            _slot.AddComponent<Image>();
            _slot.GetComponent<Image>().raycastTarget = false;
            _slot.GetComponent<Image>().enabled = false;
            IsActive = true;
        }
    }

    public static void DeleteCursorSlot()
    {
        Destroy(_slot);
        _itemStack = null;
        IsActive = false;
        _slot = null;
    }
    

    public static ItemStack GetItemStack()
    {
        return _itemStack;
    }

    public static GameObject GetGameObject()
    {
        return _slot;
    }

    public static void ClearSlot()
    {
        Image image = _slot.GetComponent<Image>();
        image.sprite = null;
        _itemStack = null;
        _slot.GetComponent<Image>().enabled = false;
    }

    public static void SetSlot(ItemStack itemStack)
    {
        if (_slot != null)
        {
            Image image = _slot.GetComponent<Image>();
            image.sprite = itemStack.Type.Sprite;
            image.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            _slot.GetComponent<Image>().enabled = true;
            _itemStack = itemStack;
            _slot.GetComponent<Image>().raycastTarget = false;
        }
    }

    public static Vector3? GetPosition()
    {
        if (_slot != null)
        {
            return _slot.transform.position;
        }

        return null;
    }

    void Update()
    {
        if (IsActive)
        {
            _slot.transform.position = Input.mousePosition;
        }
    }
}
