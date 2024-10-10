using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour , IPointerDownHandler, IPointerEnterHandler,IPointerExitHandler
{
   
   [SerializeField]protected Image image;
   [SerializeField]protected TextMeshProUGUI textMeshProUGUI;
   public InventorItem inventorItem;



   private bool canSold;
   private bool canDrop;

   protected UI ui => GetComponentInParent<UI>();

   
    public void UpdateSlot(InventorItem _item)
    {
        inventorItem = _item;
        image.color = Color.white;
        if(inventorItem != null)
        {
            image.sprite = inventorItem.data.icon;
            if(inventorItem.stackSize > 1){
                textMeshProUGUI.text = inventorItem.stackSize.ToString();
            }else{
                textMeshProUGUI.text = "";
            }
        }
    }
    public void CleanUpSlot()
    {
        inventorItem = null;
        image.color = Color.clear;
        image.sprite = null;
        textMeshProUGUI.text = "";
    }


    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if(inventorItem != null)
            {
                if(canDrop)
                {
                    PlayerManger.instance.player.GetComponent<Player_Stat>().SetUpGetItemTimer();
                    Inventor.instance.itemDrop.PlayerDropItem(inventorItem.data);
                    Inventor.instance.RemoveItem(inventorItem.data);
                    return;
                }
                if(canSold)
                {
                    Inventor.instance.RemoveItem(inventorItem.data);
                    return;
                }

                if(inventorItem.data.Type == ItemType.Equipment)
                {
                    Inventor.instance.EquipItem(inventorItem.data);
                    return;
                }
            }
        ui.itemTool.HideItemTool();
    }
    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
            canSold = true;
        if (Input.GetKeyUp(KeyCode.LeftControl))
            canSold = false;
        if(Input.GetKeyDown(KeyCode.Z))
            canDrop = true;
        if(Input.GetKeyUp(KeyCode.Z))
            canDrop = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(inventorItem != null)
        {
            ui.itemTool.ShowItemTool(inventorItem.data as Item_Equipment);
        }    
        return;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(inventorItem != null) 
        {
            ui.itemTool.HideItemTool();
        }
        return;
    }
}
