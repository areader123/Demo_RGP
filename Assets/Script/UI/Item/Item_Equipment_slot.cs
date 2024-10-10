using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item_Equipment_slot : ItemSlot
{
    public Equipment equipmentType;
    private void OnValidate() {
        gameObject.name = equipmentType.ToString();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if(inventorItem != null)
        {
            Inventor.instance.AddItem(inventorItem.data as Item_Equipment);
            Inventor.instance.UnEquipment(inventorItem.data as Item_Equipment);
            CleanUpSlot();
        }
        ui.uI_Stat_Tool_Tip.HideToolTip();
    }
}
