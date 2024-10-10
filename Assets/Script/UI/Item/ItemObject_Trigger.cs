using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject_Trigger : MonoBehaviour
{
    public ItemScript itemScript =>GetComponentInParent<ItemScript>();
    public float timer;
    public float timeCoolDown;

    private void Update() {
        timer -= Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Player>() != null)
        {
            
            if(!other.GetComponent<Character_Stat>().isDead && other.GetComponent<Character_Stat>().canGetItem)
            {
                Debug.Log("other.GetComponent<Character_Stat>().canGetItem"+other.GetComponent<Character_Stat>().canGetItem);
                itemScript.PickUpItem();
            }
        }
    }
    
}
