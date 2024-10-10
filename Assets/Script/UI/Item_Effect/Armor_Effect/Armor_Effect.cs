using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Armor_Effect", menuName = "Data/Item effect/Armor_Effect")]
public class Armor_Effect : ItemEffect
{
    [SerializeField]private float freezeDuration;
    public override void ExcuteEffect(Transform _Transform)
    {    
        Player_Stat player_Stat = PlayerManger.instance.player.GetComponent<Player_Stat>();
        if( player_Stat._currentHP > player_Stat.GetMaxHealth() * .3f)
            return;
        if(Inventor.instance.useArmor())
            return;
        Collider2D[] collider = Physics2D.OverlapCircleAll(_Transform.position,2);
        foreach(var hit in collider) 
        {
            if(hit.GetComponent<Enemy>()!=null) 
            {
                hit.GetComponent<Enemy>().FreezeTimeFor(freezeDuration);
            }
        }
    }
}
