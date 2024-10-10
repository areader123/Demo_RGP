using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InventorItem 
{
    public ItemObject data;
    public int stackSize;

    public InventorItem(ItemObject _itemObject){
        data = _itemObject;
        AddStack(); 
    }

    public void AddStack () {
        ++stackSize;
    }
    public void RemoveStack () {
        Debug.Log("减少一个物品");
        --stackSize;
    }
}
