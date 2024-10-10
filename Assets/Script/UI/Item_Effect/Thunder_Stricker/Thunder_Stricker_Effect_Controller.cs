using System;
using UnityEngine;

public class Thunder_Stricker_Effect_Controller : MonoBehaviour
{
   Player_Stat  player_Stat;

   private void Start() {
     player_Stat = PlayerManger.instance.player.GetComponent<Player_Stat>();
   }

   void OnTriggerEnter2D(Collider2D other)
   {
    if(other.GetComponent<Enemy>() != null)
    {
      Enemy_Stat enemy_Stat = other.GetComponent<Enemy_Stat>();
      enemy_Stat.DoMagicDamage(player_Stat);
    }
   }

 
}
