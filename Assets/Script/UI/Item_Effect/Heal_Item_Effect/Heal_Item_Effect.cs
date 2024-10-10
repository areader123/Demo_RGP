using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Heal_Effect", menuName = "Data/Item effect/Heal")]

public class Heal_Item_Effect : ItemEffect
{
    [SerializeField]private float healPercent;
    public override void ExcuteEffect(Transform _Transform)
    {
       // base.ExcuteEffect(_Transform);
        Player_Stat player_Stat = PlayerManger.instance.player.GetComponent<Player_Stat>();

        int healHeath = Mathf.RoundToInt(player_Stat.GetMaxHealth() * healPercent/100);

        player_Stat.IncreaseHealthOnly(healHeath);
    }



}
