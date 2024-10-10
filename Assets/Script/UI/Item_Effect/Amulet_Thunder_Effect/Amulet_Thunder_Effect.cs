using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "Amulet_Thunder_Effect", menuName = "Data/Item effect/Amulet_Thunder_Effect")]
public class Amulet_Thunder_Effect :ItemEffect
{
    [SerializeField] private GameObject thunderStrikerPrefab;
   
    public override void ExcuteEffect(Transform _enemyTransform)
    {
        //base.ExcuteEffect(_enemyTransform);
        GameObject newObject = Instantiate(thunderStrikerPrefab,_enemyTransform.position + new Vector3(0, (float)0.5,0),quaternion.identity);
        Destroy(newObject,.3f);
    }
}
