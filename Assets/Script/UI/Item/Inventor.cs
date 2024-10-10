using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Inventor : MonoBehaviour ,ISaveManager
{
    public static Inventor instance;
    
    public List<InventorItem> startItem;


    
    public List<InventorItem> inventory;
    private Dictionary<ItemObject,InventorItem> inventoryDictionary;

    public List<InventorItem> stash;
    private Dictionary<ItemObject,InventorItem> stashDictionary;

    public List<InventorItem> equipment;
    private Dictionary<Item_Equipment,InventorItem> equipmentDictionary;

    
 
    [Header("inventory UI")]
    [SerializeField]private Transform inventoryItemSlotParent;
    [SerializeField]private Transform stashItemSlotParent;
    [SerializeField]private Transform equipmentSlotParent;
    [SerializeField]private Transform statSlotParent; 
    private ItemSlot[] inventoryItemSlot;
    private ItemSlot[] stashItemSlot; 

    private Item_Equipment_slot[] equipmentSlot;

    private UI_Stat_Slot[] statSlot;

    [Header("Item CoolDown")]
    private float lastTimeUseFlask;
    private float lastTimeUseArmor;

    public float flaskCoolDown{get;private set;}
    private float ArmorCoolDown;

    [Header("data Base")]
    [SerializeField]private List<InventorItem> loadeditems; 
    [SerializeField]private List<Item_Equipment> loadedEquipment ; 
    public List<ItemObject> itemDataBase;


    public Player_Item_Drop itemDrop;



  




    private void  Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        inventory = new List<InventorItem>();
        inventoryDictionary = new Dictionary<ItemObject, InventorItem>();
        inventoryItemSlot = inventoryItemSlotParent.GetComponentsInChildren<ItemSlot>();

        stash = new List<InventorItem>();
        stashDictionary = new Dictionary<ItemObject, InventorItem>();
        stashItemSlot = stashItemSlotParent.GetComponentsInChildren<ItemSlot>();

        equipment = new List<InventorItem>();
        equipmentDictionary = new Dictionary<Item_Equipment, InventorItem>();
        equipmentSlot = equipmentSlotParent.GetComponentsInChildren<Item_Equipment_slot>();

        statSlot = statSlotParent.GetComponentsInChildren<UI_Stat_Slot>();

        itemDrop = PlayerManger.instance.player.GetComponent<Player_Item_Drop>();
        AddStartItem();
    }

    private void AddStartItem()
    {
        foreach(Item_Equipment item in loadedEquipment) {
            EquipItem(item);
        }
        if(loadeditems!=null &&loadeditems.Count > 0)
        {
            foreach(InventorItem item in loadeditems)
            {
                for(int i = 0; i < item.stackSize; i++) 
                {
                    AddItem(item.data);   
                }
            }

            return;
        }

        for (int i = 0; i < startItem.Count; i++)
        {
            AddItem(startItem[i].data);
        }
    }

    public void EquipItem(ItemObject _item)
    {
        Item_Equipment newItem_Equipment = _item as Item_Equipment;
        InventorItem newItem = new InventorItem(newItem_Equipment);

        Item_Equipment oldItemKey = null;

        foreach (KeyValuePair<Item_Equipment, InventorItem> item in equipmentDictionary)
        {
            if (item.Key.equipmentType == newItem_Equipment.equipmentType)
            {
                oldItemKey = item.Key;
            }
        }

        if(oldItemKey != null)
        {
            UnEquipment(oldItemKey);
             AddItem(oldItemKey);
           
        }

        equipment.Add(newItem);
        equipmentDictionary.Add(newItem_Equipment, newItem);
        RemoveItem(_item);

        newItem_Equipment.AddModifier();

        UpdataItemSlots();

    }

    public void UnEquipment(Item_Equipment oldItemKey)
    {
        if (equipmentDictionary.TryGetValue(oldItemKey, out InventorItem inventorItem))
        {
            equipment.Remove(inventorItem);
           
            equipmentDictionary.Remove(oldItemKey);
            oldItemKey.RemoveModifiers();
            UpdataItemSlots();
        }
    }

    public void AddItem (ItemObject itemObject) {
        if(itemObject.Type == ItemType.Equipment && canAddItem())
        {
            AddInventory(itemObject);
        }
        if (itemObject.Type == ItemType.Material)
        {
            AddStash(itemObject);
        }

        UpdataItemSlots();

    }

    public bool canAddItem()
    {
        if(inventory.Count >= inventoryItemSlot.Length )
        {
            Debug.Log("不能拿装备了");
            return false;
        }
        return true;    
    }

    public bool CanCraft(Item_Equipment _item_Equipment,List<InventorItem> _requirements)
    {
        List<InventorItem> _requiredToRemove = new List<InventorItem>();
        for(int i = 0; i < _requirements.Count;i++)
        {
            if(stashDictionary.TryGetValue(_requirements[i].data,out InventorItem _stashValue))
            {
                if(_stashValue.stackSize < _requirements[i].stackSize)
                {
                    Debug.Log("材料数量不够");
                    return false;
                }else
                {
                    _requiredToRemove.Add(_stashValue);
                }
            }
            else
            {
                Debug.Log("没有这个物品");
                return false;
            }
        }
        for(int j = 0; j < _requiredToRemove.Count; j++) 
        {
            for(int k = 0; k < _requiredToRemove[j].stackSize ; k++)
            {
                RemoveItem(_requiredToRemove[j].data);
            }
        }
        AddItem(_item_Equipment);
        Debug.Log(_item_Equipment.name);
        return true;
    }

    private void AddStash(ItemObject itemObject)
    {
        if (stashDictionary.TryGetValue(itemObject, out InventorItem stashvalue))
        {
            stashvalue.AddStack();
        }
        else
        {
            InventorItem newItem = new InventorItem(itemObject);
            stash.Add(newItem);
            stashDictionary.Add(itemObject, newItem);
        }
    }

    private void AddInventory(ItemObject itemObject)
    {
        if (inventoryDictionary.TryGetValue(itemObject, out InventorItem value))
        {
            value.AddStack();
        }
        else
        {
           
            InventorItem newItem = new InventorItem(itemObject);
            inventory.Add(newItem);
            inventoryDictionary.Add(itemObject, newItem);
        }
    }

    public void RemoveItem (ItemObject itemObject) 
    {
        if(itemObject.Type == ItemType.Equipment)
        {
            RemoveInventory(itemObject);
        }

        if (itemObject.Type == ItemType.Material)
        {
            RemoveStash(itemObject);
        }

        UpdataItemSlots();
    }

    private void RemoveStash(ItemObject itemObject)
    {
        if (stashDictionary.TryGetValue(itemObject, out InventorItem stashvalue))
        {
            if (stashvalue.stackSize <= 1)
            {
                stash.Remove(stashvalue);
                stashDictionary.Remove(itemObject);
            }
            else
            {
                stashvalue.RemoveStack();
            }
        }
    }

    private void RemoveInventory(ItemObject itemObject)
    {
        if (inventoryDictionary.TryGetValue(itemObject, out InventorItem value))
        {
            if (value.stackSize <= 1)
            {
                inventory.Remove(value);
                inventoryDictionary.Remove(itemObject);
            }
            else
            {
                value.RemoveStack();
            }
        }
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            ItemObject itemObject = stash[stash.Count - 1].data;
            RemoveItem(itemObject);
        }
    }

    private void UpdataItemSlots ()
    {
        for (int i = 0; i < equipmentSlot.Length; i++)
        {
            equipmentSlot[i].CleanUpSlot();
            foreach (KeyValuePair<Item_Equipment, InventorItem> item in equipmentDictionary)
            {
                if (item.Key.equipmentType == equipmentSlot[i].equipmentType)
                {
                    equipmentSlot[i].UpdateSlot(item.Value);

                }
            }
        }

        for (int i = 0; i < inventoryItemSlot.Length; i++)
        {
            inventoryItemSlot[i].CleanUpSlot();
        }
        for (int i = 0; i < stashItemSlot.Length; i++)
        {
            stashItemSlot[i].CleanUpSlot();
        }

        for (int i = 0; i < inventory.Count; i++)
        {
            inventoryItemSlot[i].UpdateSlot(inventory[i]);
        }
        for (int i = 0; i < stash.Count; i++)
        {
            stashItemSlot[i].UpdateSlot(stash[i]);
        }

        UpdateUIStatSlot();
    }

    public void UpdateUIStatSlot()
    {
        for (int i = 0; i < statSlot.Length; i++)
        {
            statSlot[i].UpdateValue();
        }
    }

    public void WannaDropItem(ItemObject _item)
    {
        itemDrop.DropItem(_item);
    }
    
    public List<InventorItem> Getequipment(){
        return equipment;
    }

    public List<InventorItem> GetStash(){
        return stash;
    }

    public List<InventorItem> GetInventor(){
        return inventory;
    }


    public Item_Equipment GetSingleEquipment(Equipment equipment_Type){
        List<InventorItem> Allequipment = Getequipment();
        foreach(var obj in Allequipment) {
            Item_Equipment item_Equipment = obj.data as Item_Equipment;
            if(item_Equipment.equipmentType == equipment_Type){
                return item_Equipment;
            }
        }
        return null;
    }

    public void useFlask()
    {
        Item_Equipment flask = GetSingleEquipment(Equipment.Flask);
        if(flask == null)
            return;
        bool canUseFlask = Time.time > lastTimeUseFlask + flaskCoolDown;
        if(canUseFlask)
        {
            flaskCoolDown = flask.CoolDown;
            flask.Effect(null);
            lastTimeUseFlask = Time.time;
        }
        else
        {
            Debug.Log("血瓶冷却中");
        }
    }
    public bool useArmor()
    {
        Item_Equipment armor = GetSingleEquipment(Equipment.Armor);
        bool canUseArmor = Time.time > lastTimeUseArmor + ArmorCoolDown ;
        if(canUseArmor)
        {
            ArmorCoolDown = armor.CoolDown;
            lastTimeUseArmor = Time.time;
            Debug.Log("护甲被使用了");
            return true;
        }else
        {
            Debug.Log("护甲暂时不能使用");
            return false;
        }
    }

    public void LoadData(GameData _data)
    {
       foreach(KeyValuePair<string,int> pair in _data.inventory) 
       {
            foreach(var item in GetItemDataBase()) 
            {
                if(item != null && item.itemId == pair.Key)
                {
                    InventorItem inventorItem = new InventorItem(item);
                    inventorItem.stackSize = pair.Value;

                    loadeditems.Add(inventorItem);
                }    
            }
       }

       foreach(string loadItemId in _data.equipment) 
       {
            foreach(var item in GetItemDataBase()) 
            {
                 if(item != null && loadItemId == item.itemId)
                 {
                    loadedEquipment.Add(item as Item_Equipment);
                 }
            }
       }
    }

    public void SaveData(ref GameData _data)
    {
        _data.inventory.Clear();
        _data.equipment.Clear();
        
        foreach(KeyValuePair<ItemObject,InventorItem> pair in inventoryDictionary) 
        {
            _data.inventory.Add(pair.Key.itemId,pair.Value.stackSize);    
        }
         foreach(KeyValuePair<ItemObject,InventorItem> pair in stashDictionary) 
        {
            _data.inventory.Add(pair.Key.itemId,pair.Value.stackSize);    
        }
         foreach(KeyValuePair<Item_Equipment,InventorItem> pair in equipmentDictionary) 
        {
            _data.equipment.Add(pair.Key.itemId);    
        }
    }

    private List<ItemObject> GetItemDataBase()
    {
        itemDataBase = new List<ItemObject>();
        string[] assetNames = AssetDatabase.FindAssets("",new[] {"Assets/Item_And_Inventory/Data"});

        foreach(string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var itemData = AssetDatabase.LoadAssetAtPath<ItemObject>(SOpath);
            itemDataBase.Add(itemData);
        }

        return itemDataBase;

    }
}