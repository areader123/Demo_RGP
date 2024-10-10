using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stat : Character_Stat
{
    Player player;
    Player_Item_Drop player_Item_DropSystem;

    

    
    protected override void Start()
    {
        base.Start();
        player = GetComponent<Player>();
        player_Item_DropSystem = GetComponent<Player_Item_Drop>();
    }

     public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        player.Damage();
    }

    protected override void Die()
    {
        base.Die();
        player.stateMachine.ChangeState(player.DieState);
        player_Item_DropSystem.generateDrop();
        isDead = true;
    }

    protected override void DecreaseHealthOnly(float damage)
    {
        base.DecreaseHealthOnly(damage);
        Item_Equipment armor = Inventor.instance.GetSingleEquipment(Equipment.Armor);
        if(armor != null)
        {
            armor.Effect(PlayerManger.instance.player.transform);
        }
    }

    public override void OnEvasion()
    {
        Debug.Log("闪避成功");
        SkillManger.Instance.dodge_Skill.CreateMirageOnDodge();
    }
}
   
