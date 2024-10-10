using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_Skill : SKill
{
    [Header("Clone Info")]
    [SerializeField] private GameObject clonePrefab;
    [SerializeField] private float cloneDuration;
    [SerializeField] private bool canAttack;

    public void CreatClone(Transform _cloneTransform,Vector3 _offSet) 
    {
        GameObject newClone = Instantiate(clonePrefab);
        newClone.GetComponent<Clone_Skill_Controller>().SetUpClone( _cloneTransform,cloneDuration,canAttack,_offSet,FindCloestEnemy(newClone.transform));
    }

    public void CreateCloneWithDelay(Transform _enemyTransform)
    {
        StartCoroutine(CloneDelayCorotine(_enemyTransform,new Vector3((float)(1.5 * PlayerManger.instance.player.faceDir), 1)));
    }

    public IEnumerator CloneDelayCorotine(Transform _transform,Vector3 _offSet)
    {
        yield return new WaitForSeconds(.4f);
            CreatClone(_transform,_offSet);
    }
}
