using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Item_Drop : ItemDrop
{
    [Header("Player Drop")]
    [SerializeField]private float chanceToLoseItem;

    public override void generateDrop()
    {
        Inventor inventor = Inventor.instance;
        List<InventorItem> currentStash = inventor.GetStash();
        List<InventorItem> currentEquipment = inventor.Getequipment();
        List<InventorItem> currentInventor = inventor.GetInventor();
        List<InventorItem> EquipmentToUnequip = new List<InventorItem>();
        List<InventorItem> StashToLose = new List<InventorItem>();
        List<InventorItem> InventorTolose = new List<InventorItem>();

       
        
            foreach(var obj in currentEquipment) 
            {
            // if(Random.Range(0,100) <= chanceToLoseItem){
                    DropItem(obj.data);
                    EquipmentToUnequip.Add(obj);    
            // }
            }
            for(int i = 0 ; i < EquipmentToUnequip.Count ; i++)
            {
                    Debug.Log("unEquip");
                    inventor.UnEquipment(EquipmentToUnequip[i].data as Item_Equipment);
            }

             foreach(var obj in currentStash) 
            {
            // if(Random.Range(0,100) <= chanceToLoseItem){
                    DropItem(obj.data);
                    StashToLose.Add(obj);
            // }
            }

             for(int i = 0 ; i < StashToLose.Count ; i++)
             {
                inventor.RemoveItem(StashToLose[i].data);
             }

            foreach(var obj in currentInventor) 
            {
            // if(Random.Range(0,100) <= chanceToLoseItem){
                    DropItem(obj.data);
                    InventorTolose.Add(obj);
            // }
            }

             for(int i = 0 ; i < InventorTolose.Count ; i++)
             {
                inventor.RemoveItem(InventorTolose[i].data);
             }
        

    }   
   
}