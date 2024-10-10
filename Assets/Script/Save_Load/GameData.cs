using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public int currency;
    public SerializableDictionary<string,bool> skillTree;
    public SerializableDictionary<string,int> inventory;
     public List<string> equipment;
    public GameData()
    {
        currency = 0;
        inventory = new SerializableDictionary<string,int>();
        skillTree = new SerializableDictionary<string,bool>();
    }
}
