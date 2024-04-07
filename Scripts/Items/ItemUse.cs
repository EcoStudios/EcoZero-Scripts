using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class ItemUse 
{

    public static void Attack(GameObject item, Mob mob)
    {
        Animator animator = item.GetComponent<Animator>();
        ItemStack itemStack = ItemManager.GetDroppedItemStack(item.transform.name);
        if (animator == null && itemStack.Data.HasCustomAttackAnimation)
        {
            Debug.Log("Animator is null!");
        }
        else
        {
            
        }
    }

}
