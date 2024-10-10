using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Craft_Slot : ItemSlot
{
    // Start is called before the first frame update
     void OnValidate()
    {
        UpdateSlot(inventorItem);
    }

    public void setUpCraftSlot(Item_Equipment _data)
    {
        if(_data == null)
            return;
        inventorItem.data = _data;
        image.sprite = _data.icon;
        textMeshProUGUI.text = _data.ItemName;

        if(textMeshProUGUI.text.Length > 12)
        {
            textMeshProUGUI.fontSize = textMeshProUGUI.fontSize * .7f;
        }else
        {
            textMeshProUGUI.fontSize = 24;
        }

    }


    public override void OnPointerDown(PointerEventData eventData)
    {
      ui.ui_Craft_Window.SetUpCraftWindow(inventorItem.data as Item_Equipment);
    }
}
