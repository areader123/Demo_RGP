using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemTool : MonoBehaviour
{
   [SerializeField]private TextMeshProUGUI itemNameText;
   [SerializeField]private TextMeshProUGUI itemTypeText;
   [SerializeField]private TextMeshProUGUI itemDescripText;


   public void ShowItemTool(Item_Equipment item_Equipment)
   {
      if (item_Equipment == null)
         return;
      itemNameText.text = item_Equipment.name;
      itemTypeText.text = item_Equipment.Type.ToString();
      itemDescripText.text = item_Equipment.GetDescription();
      if(itemNameText.text.Length > 12)
      {
         itemNameText.fontSize = itemNameText.fontSize * .7f;
      }
      gameObject.SetActive(true);
   }

   public void HideItemTool() 
   {
      itemNameText.fontSize = 32f;
      gameObject.SetActive(false);
   }
}
