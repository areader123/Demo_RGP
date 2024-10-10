using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ice_Fire_Effect_Controller : MonoBehaviour
{
    Player_Stat  player_Stat;
    // Start is called before the first frame update
    void Start()
    {
        player_Stat = PlayerManger.instance.player.GetComponent<Player_Stat>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Enemy>() != null)
        {
            Enemy_Stat enemy_Stat = other.GetComponent<Enemy_Stat>();
            enemy_Stat.DoMagicDamage(player_Stat);
        }
    }
}
