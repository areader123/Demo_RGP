using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Equipment{
    Weapon,
    Armor,
    Amulet,
    Flask
}
[CreateAssetMenu(fileName ="New Item Data", menuName = "Data/Equipment")]
public class Item_Equipment : ItemObject
{
    [Header("Major Stats")]
    public int strength;
    public int agility;
    public int intelligence;
    public int vitality;

    [Header("Offensive Stats")]
    public int critChance;
    public int critPower;
    public int damage;

    [Header("Defensive stats")]
    public int maxHP;
    public int armor;
    public int evasion;
    public int MagicResistance; 

    [Header("Magic Attack")]
    public int ignite;
    public int ice;
    public int lighting;
    

    [Header("Item Setting")]
    public float CoolDown;
    public Equipment equipmentType;
    public List<ItemEffect> itemEffects;

    [Header("Craft Requirement")]
    public List<InventorItem> craftingMaterial;

    private int descriptionLength;


    public void AddModifier () {
        PlayerManger.instance.player.GetComponent<Character_Stat>().strength.AddModifiers(strength);
        PlayerManger.instance.player.GetComponent<Character_Stat>().agility.AddModifiers(agility);
        PlayerManger.instance.player.GetComponent<Character_Stat>().intelligence.AddModifiers(intelligence);
        PlayerManger.instance.player.GetComponent<Character_Stat>().vitality.AddModifiers(vitality);
        PlayerManger.instance.player.GetComponent<Character_Stat>().critChance.AddModifiers(critChance);
        PlayerManger.instance.player.GetComponent<Character_Stat>().critPower.AddModifiers(critPower);
        PlayerManger.instance.player.GetComponent<Character_Stat>().damage.AddModifiers(damage);
        PlayerManger.instance.player.GetComponent<Character_Stat>().maxHP.AddModifiers(maxHP);
        PlayerManger.instance.player.GetComponent<Character_Stat>().armor.AddModifiers(armor);
        PlayerManger.instance.player.GetComponent<Character_Stat>().evasion.AddModifiers(evasion);
        PlayerManger.instance.player.GetComponent<Character_Stat>().MagicResistance.AddModifiers(MagicResistance);
        PlayerManger.instance.player.GetComponent<Character_Stat>().ignite.AddModifiers(ignite);
        PlayerManger.instance.player.GetComponent<Character_Stat>(). ice.AddModifiers(ice);
        PlayerManger.instance.player.GetComponent<Character_Stat>().lighting.AddModifiers(lighting);
    }

    public void RemoveModifiers () {
        PlayerManger.instance.player.GetComponent<Character_Stat>().strength.RemoveModifiers(strength);
        PlayerManger.instance.player.GetComponent<Character_Stat>().agility.RemoveModifiers(agility);
        PlayerManger.instance.player.GetComponent<Character_Stat>().intelligence.RemoveModifiers(intelligence);
        PlayerManger.instance.player.GetComponent<Character_Stat>().vitality.RemoveModifiers(vitality);
        PlayerManger.instance.player.GetComponent<Character_Stat>().critChance.RemoveModifiers(critChance);
        PlayerManger.instance.player.GetComponent<Character_Stat>().critPower.RemoveModifiers(critPower);
        PlayerManger.instance.player.GetComponent<Character_Stat>().damage.RemoveModifiers(damage);
        PlayerManger.instance.player.GetComponent<Character_Stat>().maxHP.RemoveModifiers(maxHP);
        PlayerManger.instance.player.GetComponent<Character_Stat>().armor.RemoveModifiers(armor);
        PlayerManger.instance.player.GetComponent<Character_Stat>().evasion.RemoveModifiers(evasion);
        PlayerManger.instance.player.GetComponent<Character_Stat>().MagicResistance.RemoveModifiers(MagicResistance);
        PlayerManger.instance.player.GetComponent<Character_Stat>().ignite.RemoveModifiers(ignite);
        PlayerManger.instance.player.GetComponent<Character_Stat>().ice.RemoveModifiers(ice);
        PlayerManger.instance.player.GetComponent<Character_Stat>().lighting.RemoveModifiers(lighting);
    }

    public void Effect(Transform transform)
    {
        if(itemEffects == null)
        {
            return;
        }

        foreach(var obj in itemEffects) {
            obj.ExcuteEffect(transform);
        }
    }
    public override string GetDescription()
    {
        sb.Length = 0;
        descriptionLength = 0;
        AddItemDescription(strength,"strength");
        AddItemDescription(agility,"agility");
        AddItemDescription(intelligence,"intelligence");
        AddItemDescription(vitality,"vitality");
        AddItemDescription(critChance,"critChance");
        AddItemDescription(critPower,"critPower");
        AddItemDescription(damage,"damage");
        AddItemDescription(maxHP,"maxHP");
        AddItemDescription(armor,"armor");
        AddItemDescription(evasion,"evasion");
        AddItemDescription(MagicResistance,"MagicResistance");
        AddItemDescription(ignite,"ignite");
        AddItemDescription(ice,"ice");
        AddItemDescription(lighting,"lighting");

        if(descriptionLength < 5)
        {
            for(int i = 0; i < 5-descriptionLength;i++)
            {
                sb.AppendLine();
                sb.Append("");
            }
        }

        return sb.ToString();
    }

    private void AddItemDescription(int _value,string _name)
    {
        if(_value != 0)
        {
            if(sb.Length > 0)
                sb.AppendLine();
            if(_value >0)
            {
                sb.Append(_name + ":" + _value);
            }
            descriptionLength++;
        }
    }
}
