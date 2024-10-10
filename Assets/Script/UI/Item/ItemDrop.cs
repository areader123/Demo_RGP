using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField]private GameObject dropPrefab;


    [SerializeField]private int actualDropAmount;
    [SerializeField]private List<ItemObject> possibleDrop;
    private List<ItemObject> actualDrop;
    private void Start() {
        actualDrop = new List<ItemObject>();
    }
    public virtual void generateDrop(){
        for(int i = 0; i < possibleDrop.Count; i++) {
            if(UnityEngine.Random.Range(0,100) < possibleDrop[i].dropChance){
                actualDrop.Add(possibleDrop[i]);
            }
        }

        for(int i = 0; i < actualDropAmount; i++) {
            ItemObject actualDropObject = actualDrop[UnityEngine.Random.Range(0,actualDrop.Count-1)];
            actualDrop.Remove(actualDropObject);
            DropItem(actualDropObject);
        }
    }



    public  void DropItem(ItemObject _itemObject) 
    {
        GameObject newItem = Instantiate(dropPrefab,transform.position,quaternion.identity);
        Vector2 velocity = new Vector2(UnityEngine.Random.Range(-5,5),UnityEngine.Random.Range(12,15));

        newItem.GetComponent<ItemScript>().SetUpItem(_itemObject,velocity);
    }
    
    public  void PlayerDropItem(ItemObject _itemObject) 
    {
        GameObject newItem = Instantiate(dropPrefab,transform.position,quaternion.identity);
        Vector2 velocity = new Vector2(UnityEngine.Random.Range(4,8) * PlayerManger.instance.player.faceDir,8);
        

        newItem.GetComponent<ItemScript>().SetUpItem(_itemObject,velocity);
    }
}
