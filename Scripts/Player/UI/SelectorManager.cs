using System;
using UnityEngine;


namespace Player.UI
{
    public class SelectorManager : MonoBehaviour
    {

        public GameObject selector;
        public static GameObject Selector;
        private static GameObject _target;

        private void Start()
        {
            Selector = selector;
        }

        public static GameObject GetTarget()
        {
            return _target;
        }

        private void Update()
        {
            if (Physics.Raycast(PlayerManager.PlayerCamera.transform.position, PlayerManager.PlayerCamera.transform.forward, out RaycastHit hit))
            {
                _target = hit.transform.gameObject;
                if (ItemManager.GetDroppedItemStack(hit.transform.gameObject.name).Type != ItemType.AIR && hit.distance <= PlayerManager.PickupDistance)
                {
                    ItemInfo.Set(ItemManager.GetDroppedItemStack(hit.transform.gameObject.name));
                }
                else
                {
                    ItemInfo.Set(new ItemStack(ItemType.AIR, 0));
                }
                if (Input.GetMouseButtonUp(0))
                {
                    Debug.Log(hit.transform.gameObject.name);
                    if (hit.transform.gameObject.layer == 3)
                    {
                        ItemPickup.Pickup(hit.transform.gameObject);
                    }
                } 
                
            }
        }
    }
}