using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public ItemObject itemObject;

    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    private SpriteRenderer sr => GetComponent<SpriteRenderer>();

    // Start is called before the first frame update
    public void SetUpItem(ItemObject _itemObject,Vector2 _velocity ){
        itemObject = _itemObject;
        rb.velocity = _velocity;    
        sr.sprite = itemObject.icon;
    }

    public void PickUpItem () {
        if(!Inventor.instance.canAddItem() && itemObject.Type == ItemType.Equipment)
        {
            rb.velocity = new Vector2(0,7);
            return;
        }
        Debug.Log("得到物品");
        Inventor.instance.AddItem(itemObject);
        Destroy(gameObject);
    
    }

}
