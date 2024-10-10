using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKill : MonoBehaviour
{
    //Ϊ���м��ܸ���
    public float cooldown;
    protected float cooldowmTImer;
    protected Player player;

    
    protected virtual void Start() 
    {
         player = PlayerManger.instance.player;
         CheckUnlock();
    }

    protected  virtual void Update()
    {
        cooldowmTImer -= Time.deltaTime;
    }
    public virtual bool CanUseSkill() 
    {
        if (cooldowmTImer < 0)
        {
            UseSkill();
            cooldowmTImer = cooldown;
            return true;
        }
        return false;

    }

    protected virtual void CheckUnlock()
    {

    }

    public virtual void UseSkill()
    {
        
    }

    protected virtual Transform FindCloestEnemy(Transform transform){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,25);
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;
        foreach (var hit in colliders)
        {
            if(hit.GetComponent<Enemy>() != null) 
            {
                float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);
                if(distanceToEnemy < closestDistance) 
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = hit.transform;
                }
            }
        }

        return closestEnemy;
    }
}
