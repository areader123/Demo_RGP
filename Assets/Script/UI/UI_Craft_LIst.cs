using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Craft_LIst : MonoBehaviour,IPointerDownHandler
{   
    [SerializeField]private Transform craftSlotParent;
    [SerializeField]private GameObject craftSlotPrefab;
    [SerializeField]private List<Item_Equipment> craftEquipment;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.parent.GetChild(0).GetComponent<UI_Craft_LIst>().SetUpCraftList();
        SetUpDefaultCraftWindow();
    }

    
    public void SetUpCraftList()
    {
        for(int i = 0 ; i < craftSlotParent.childCount; i++)
        {
            Destroy(craftSlotParent.GetChild(i).gameObject);
        }



        for(int j = 0; j<craftEquipment.Count;j++)
        {
            GameObject _newSlot = Instantiate(craftSlotPrefab,craftSlotParent.transform);
            _newSlot.GetComponent<UI_Craft_Slot>().setUpCraftSlot(craftEquipment[j]);
        }
    }

    public void SetUpDefaultCraftWindow()
    {
        if(craftEquipment != null)
        {
            GetComponentInParent<UI>().ui_Craft_Window.SetUpCraftWindow(craftEquipment[0]);
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
       SetUpCraftList();
    }
}
