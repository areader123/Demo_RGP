using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Timeline.Actions;
using UnityEngine;


[CreateAssetMenu(fileName = "Thunder_Stricker_Effect", menuName = "Data/Item effect/Thunder_Strick")]
public class Thunder_Strick_Item_Effect : ItemEffect
{
    [SerializeField] private GameObject thunderStrikerPrefab;
    private GameObject currentObject;
    public override void ExcuteEffect(Transform _enemyTransform)
    {
        //base.ExcuteEffect(_enemyTransform);
        currentObject = Instantiate(thunderStrikerPrefab,_enemyTransform.position + new Vector3(0, (float)0.5,0),quaternion.identity);
        Destroy(currentObject,.5f);
    }
}
