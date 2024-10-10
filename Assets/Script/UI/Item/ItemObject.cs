using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEditor;

public enum ItemType{
    Material,
    Equipment
}

[CreateAssetMenu(fileName ="New Item Data", menuName = "Data/Item")]
public class ItemObject : ScriptableObject
{
    public ItemType Type;
    public String ItemName;
    public Sprite icon;
    public string itemId;

    [Range(0,100)]
    public float dropChance;

    protected StringBuilder sb = new StringBuilder();
    
    private void OnValidate() 
    {
        #if UNITY_EDITOR
            string path = AssetDatabase.GetAssetPath(this);
            itemId =  AssetDatabase.AssetPathToGUID(path);
        #endif
    }


    public virtual string GetDescription()
    {
        return "";
    }
}
