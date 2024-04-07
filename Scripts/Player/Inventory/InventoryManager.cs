using Player.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Player.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        
    
        // Unity Export Vars
        public static int MaxInventorySlots = 21;
        public GameObject slotsGameObject;
        public GameObject inventory;
        public GameObject hotbar;
        public GameObject hotbarSlots;
        public GameObject hand;
        public float dropVelocity;
    
        // Other Vars
        private static ItemStack[] _inventory = new ItemStack[MaxInventorySlots];
        public static GameObject SlotsCont;
        public static GameObject InventoryCont;
        public static GameObject hotbarSlot;
        public static GameObject hotbarGameObject;
        public static GameObject HandSlot;
        public static int CurrentHeldSlot;
        public static ItemStack HandItem;
        public static bool IsHandFull;
        public static float DropVelocity;

        
        void Start()
        {
            hotbarSlot = hotbarSlots;
            InventoryCont = inventory;
            SlotsCont = slotsGameObject;
            hotbarGameObject = hotbar;
            HandSlot = hand;
            DropVelocity = dropVelocity;
            if (_inventory.ToString() == "ItemStack[]")
            {
                for (int i = 0; i <= MaxInventorySlots-1; i++)
                {
                    ItemStack air = new ItemStack(ItemType.AIR, 0);
                    _inventory[i] = air;
                }
            }
        }
    
        // Held Item Utils
        public static void SetHandQuick(int slot)
        {
            if (GetInventorySlot(slot).Type != ItemType.AIR)
            {
                SetHandItem(GetInventorySlot(slot));
                CurrentHeldSlot = slot;
            }
            else
            {
                ClearHandItem();
            }
        }

        public static void SetHandItem(ItemStack itemStack)
        {
            if (IsHandFull) ClearHandItem();
            GameObject gameObject = Instantiate(itemStack.Type.Prefab, HandSlot.transform, true);
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.GetComponentInChildren<Rigidbody>().isKinematic = true;
            gameObject.transform.position = HandSlot.transform.position;
            gameObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
            gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            if (itemStack.Data.HandRotation != null) gameObject.transform.localRotation = itemStack.Data.HandRotation.Value;
            if (itemStack.Data.HandScale != null) gameObject.transform.localScale = itemStack.Data.HandScale.Value;
            if (itemStack.Data.HandVector3 != null) gameObject.transform.localPosition = itemStack.Data.HandVector3.Value;
            HandItem = itemStack;
            IsHandFull = true;
        }

        public static void ClearHandItem()
        {
            if (IsHandFull)
            {
                IsHandFull = false;
                HandItem = new ItemStack(ItemType.AIR, 0);
                Destroy(HandSlot.transform.GetChild(0).gameObject);
            }
        }
        
        public static void DropItem(ItemStack itemStack, Vector3 location, int slot = -50)
        {
            if (slot == -50) slot = CurrentHeldSlot;
            ItemManager.SpawnItem(itemStack, location, PlayerManager.Player.forward + new Vector3(0, 0, DropVelocity));
            ItemStack currentItem = GetInventorySlot(slot);
            
            RemoveItemStack(slot);
            if (currentItem.StackSize != itemStack.StackSize)
            {
                int newStackSize = currentItem.StackSize - itemStack.StackSize;
                currentItem.StackSize = newStackSize;
                AddItemStack(currentItem, slot);
            }
            else
            {
                ClearHandItem();
            }
        }
        // End of Held Item Utils
        
        
        
        // Inventory Utils
        public static ItemStack GetInventorySlot(int slot)
        {
            return _inventory[slot];
        }

        public static GameObject GetInventory()
        {
            return InventoryCont;
        }
        
        public static int GetEmptySlot()
        {
            for (int i = 0; i < MaxInventorySlots; i++)
            {
                if (_inventory[i].Type == ItemType.AIR)
                {
                    return i;
                }
            }

            return -404;
        }
        
        public static bool HasItem(ItemStack itemStack)
        {
            for (int i = 0; i <= MaxInventorySlots-1; i++)
            {
                ItemStack item = _inventory[i];
                if (item.Type == itemStack.Type)
                {
                    return true;
                }
            }

            return false;
        }
    
        public static bool CanHoldItem(ItemStack item)
        {
            if (GetEmptySlot() != -404)
            {
                return true;
            }

            if (GetStackableItem(item).Type != ItemType.AIR)
            {
                return true;
            }

            return false;
        }
        
        private static ItemStack GetStackableItem(ItemStack itemStack)
        {
            for (int i = 0; i <= MaxInventorySlots-1; i++)
            {
                ItemStack item = _inventory[i]; 
                if (itemStack.Type.Name == item.Type.Name)
                {
                    if (item.StackSize < item.Type.MaxSize)
                    {
                        return item;
                    }
                }
            }
            return new ItemStack(ItemType.AIR, 0);
        }

        public static void OpenInventory()
        {
            if (!GameUtils.IsPaused)
            {
                GameObject inventory = GetInventory();
                inventory.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                SelectorManager.Selector.SetActive(false);
                hotbarGameObject.SetActive(false);
            }
           
        }

        public static void CloseInventory()
        {
            if (!GameUtils.IsPaused)
            {
                GameObject inventory = GetInventory();
                inventory.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                SelectorManager.Selector.SetActive(false);
                hotbarGameObject.SetActive(false);
            }
        }

        private static void AddItem(ItemStack itemStack, int slot)
        {
            _inventory[slot] = itemStack;
            itemStack.Slot = slot;
            SlotManager.SetSlot(itemStack.Slot, itemStack);
        }
        
        public static void AddItemStack(ItemStack itemStack, int index = -50)
        {
            int slot = index;
            if (slot == -50)
            { 
                slot = GetEmptySlot();
                // Stackable items
                ItemStack i = GetStackableItem(itemStack);
                if (i.Type.Name == itemStack.Type.Name)
                {
                    int amount = i.StackSize + itemStack.StackSize;
                    if (amount <= i.Type.MaxSize)
                    {
                        SlotManager.SetAmount(i.Slot, amount);
                        _inventory[i.Slot].StackSize += itemStack.StackSize;
                        if (i.Slot <= 4)
                        {
                            HotbarManager.AddItem(i.Slot, _inventory[i.Slot]);
                        }
                    }
                    else
                    {
                        if (i.StackSize != i.Type.MaxSize)
                        {
                            int needed = i.Type.MaxSize - i.StackSize;
                            int put = i.StackSize + needed;
                            SlotManager.SetAmount(i.Slot, put);
                            _inventory[i.Slot].StackSize = put;
                            if (i.Slot <= 4)
                            {
                                HotbarManager.AddItem(i.Slot, _inventory[i.Slot]);
                            }
                            if (put != amount)
                            {
                                int leftover = amount - put;
                                AddItem(new ItemStack(itemStack.Type, leftover), slot);
                                if (i.Slot <= 4)
                                {
                                    HotbarManager.AddItem(slot, _inventory[slot]);
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (slot != -404)
                    {
                        AddItem(itemStack, slot);
                        if (slot <= 4)
                        {
                            HotbarManager.AddItem(slot, _inventory[slot]);
                        }
                    
                    }
                }
            }
            else
            {
                if (slot != -404)
                {
                    AddItem(itemStack, slot);
                    if (slot <= 4)
                    {
                        HotbarManager.AddItem(slot, _inventory[slot]);
                    }
                }
            }
        }

        public static void RemoveItemStack(int slotNumber)
        {
            if (_inventory.Length != 0 || _inventory[slotNumber].StackSize != 0 || slotNumber != -404)
            {
                ItemStack air = new ItemStack(ItemType.AIR, 0, slotNumber);
                _inventory[slotNumber] = air;
                SlotManager.SetSlot(slotNumber, air);
                if (slotNumber <= 4)
                {
                    HotbarManager.AddItem(slotNumber, _inventory[slotNumber]);
                    if (CurrentHeldSlot == slotNumber)
                    {
                        ClearHandItem();
                    }
                }
            }
        }

        public static GameObject Slots()
        {
            return SlotsCont;
        }
        // End of Inventory Utils
    
    }

    public class SlotManager
    {
        // Gets the ui slot's gameobject
        public static GameObject GetSlot(int slot)
        {
            Transform trans = InventoryManager.SlotsCont.transform.Find($"InventorySlot ({slot})"); 
            return trans.gameObject;
        }

        public static void SetSlot(int slotNumber, ItemStack item)
        {
            GameObject slot = GetSlot(slotNumber);
            Image[] slotImages = slot.GetComponentsInChildren<Image>(true);
            if (slotImages.Length == 2)
            {
                if (slotImages[1].gameObject.name == "Image")
                {
                    Image slotImage = slotImages[1];
                    TMP_Text amountText = slot.GetComponentInChildren<TMP_Text>(true);
                    if (item.Type == ItemType.AIR)
                    {
                        slotImage.sprite = null;
                        amountText.text = "";
                        amountText.gameObject.SetActive(false);
                    }
                    else
                    {
                        slotImage.sprite = item.Type.Sprite;
                        amountText.gameObject.SetActive(true);
                        SetAmount(slotNumber, item.StackSize);
                        if (item.StackSize == item.Type.MaxSize) { amountText.color = Color.red; }
                        if (item.StackSize != item.Type.MaxSize) { amountText.color = Color.white; }
                    }
                }
            }
        }

        public static void SetAmount(int slotNumber, int amount)
        {
            GameObject slot = GetSlot(slotNumber);
            TMP_Text amountText = slot.GetComponentInChildren<TMP_Text>(true);
            amountText.text = amount.ToString();
        }

    }


    public class HotbarManager
    {
        
        
        public static GameObject GetSlot(int slot)
        {
            Transform trans = InventoryManager.hotbarSlot.transform.Find($"HotBarSlot ({slot})"); 
            return trans.gameObject;
        }

        public static void AddItem(int slotNumber, ItemStack item)
        {
            GameObject slot = GetSlot(slotNumber);
            Image[] slotImages = slot.GetComponentsInChildren<Image>(true);
            if (slotImages.Length == 2)
            {
                if (slotImages[1].gameObject.name == "Image")
                {
                    Image slotImage = slotImages[1];
                    TMP_Text amountText = slot.GetComponentInChildren<TMP_Text>(true);
                    if (item.Type == ItemType.AIR)
                    {
                        slotImage.sprite = null;
                        amountText.text = "";
                        amountText.gameObject.SetActive(false);
                    }
                    else
                    {
                        slotImage.sprite = item.Type.Sprite;
                        amountText.gameObject.SetActive(true);
                        amountText.text = item.StackSize.ToString();
                        if (item.StackSize == item.Type.MaxSize) { amountText.color = Color.red; }
                        if (item.StackSize != item.Type.MaxSize) { amountText.color = Color.white; }
                    }
                }
            } 
        }

        //Returns the True if the slot is active
        public static bool IsActive(int slot)
        {
            Image[] slotImages = GetSlot(slot).GetComponentsInChildren<Image>(true);
            return slotImages[1].IsActive();
        }
    

        public static bool HasRoom()
        {
            int active = 0;
            for (int i = 0; i <= 4; i++)
            {
                if (IsActive(i))
                {
                    active++;
                }
            }
            if (active == 4)
            {
                return false;
            }
            return true;
        }

    }
}