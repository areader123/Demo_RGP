using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "buff_Effect", menuName = "Data/Item effect/buff_Effect")]
public class Buff_Effect : ItemEffect
{   
    [SerializeField]private StatType statType;
    private Player_Stat player_Stat;
    [SerializeField]private int amount;
    [SerializeField]private float duration;

    public override void ExcuteEffect(Transform _Transform)
    {
        player_Stat = PlayerManger.instance.player.GetComponent<Player_Stat>();
        player_Stat.IncreaseStatBy(amount,duration,player_Stat.GetStat(statType));
    }



}
