using System;
using System.Collections.Generic;
using System.Threading;
using Player.Inventory;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IPointerEnterHandler, IPointerUpHandler, IPointerDownHandler
{

    private static bool _isBeingDragged;
    private static int _startingSlot;
    private static int _endingSlot;
    private static GameObject _gameObject;
    private static ItemStack _itemStack;
    private static GameObject _currentGameObject;
    private static bool _tempBool;
    

    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject obj = _currentGameObject;
        if (!_isBeingDragged)
        {
            string name = obj.name;
            name = name.Replace("InventorySlot", "").Replace("(", "").Replace(")", "");
            if (InventoryManager.GetInventorySlot(Convert.ToInt32(name)).Type != ItemType.AIR)
            {
                _gameObject = obj;
                _startingSlot = Convert.ToInt32(name);
                _itemStack = InventoryManager.GetInventorySlot(Convert.ToInt32(name));
                CursorSlot.CreateCursorSlot();
                CursorSlot.SetSlot(_itemStack);
                _isBeingDragged = true;
            }
        }
    }
    

    public void OnPointerUp(PointerEventData eventData)
    {
        CursorSlot.DeleteCursorSlot();
        if (_isBeingDragged)
        {
            if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.transform.parent.parent != null && eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.name.Contains("InventorySlot"))
            {
                string name1 = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.name;
                name1 = name1.Replace("InventorySlot", "").Replace("(", "").Replace(")", "");
                _endingSlot = Convert.ToInt32(name1);
                if (_endingSlot != _startingSlot)
                {
                    ItemStack newItemStack = InventoryManager.GetInventorySlot(_endingSlot);
                    if (newItemStack.Type == ItemType.AIR )
                    {
                        InventoryManager.RemoveItemStack(_startingSlot);
                        InventoryManager.AddItemStack(_itemStack, _endingSlot);
                    }
                    else
                    {
                        if (newItemStack.Type == _itemStack.Type)
                        {
                            if (_itemStack.StackSize + newItemStack.StackSize <= newItemStack.Type.MaxSize)
                            {
                                InventoryManager.RemoveItemStack(_startingSlot);
                                InventoryManager.AddItemStack(_itemStack, _endingSlot);
                            }
                            else
                            {
                                if (_itemStack.StackSize > newItemStack.StackSize)
                                {
                                    InventoryManager.RemoveItemStack(_startingSlot);
                                    InventoryManager.RemoveItemStack(_endingSlot);
                                    InventoryManager.AddItemStack(_itemStack, _endingSlot);
                                    InventoryManager.AddItemStack(newItemStack, _startingSlot);
                                }
                            }
                        }
                        else
                        {
                            if (_itemStack.StackSize > newItemStack.StackSize)
                            {
                                InventoryManager.RemoveItemStack(_startingSlot);
                                InventoryManager.RemoveItemStack(_endingSlot);
                                InventoryManager.AddItemStack(_itemStack, _endingSlot);
                                InventoryManager.AddItemStack(newItemStack, _startingSlot);
                            }
                        }
                    }
                }
                _isBeingDragged = false;
            }
            else
            {
                if (eventData.hovered == null || eventData.hovered.Count <= 1)
                {
                    InventoryManager.DropItem(_itemStack, PlayerManager.Player.localPosition + PlayerManager.Player.forward + PlayerManager.Player.up, _startingSlot);
                }
                _isBeingDragged = false;
            }
        }
    }
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        _currentGameObject = gameObject;
    }
    
}
