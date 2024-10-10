using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "Ice_Fire_Effect", menuName = "Data/Item effect/Ice_Fire")]
public class Ice_Fire_Effect : ItemEffect
{
   [SerializeField] private GameObject iceFirePrefab;

   [SerializeField] private Vector2 velocity;    

   public override void ExcuteEffect(Transform _transform)
    {
        Player player = PlayerManger.instance.player;
        bool isThirdAttack = player.AttackState.comboCounter == 2;
        if(isThirdAttack)
        {
            GameObject newObject = Instantiate(iceFirePrefab,_transform.position + new Vector3(0, (float)0.5,0),quaternion.identity);
            if(player.faceDir < 0)
            {
                newObject.transform.Rotate(0,180,0);
            }
            newObject.GetComponent<Rigidbody2D>().velocity = velocity * player.faceDir;

            Destroy(newObject,5f);
        }
        
    }
}
