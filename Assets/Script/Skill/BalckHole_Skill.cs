using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BalckHole_Skill : SKill
{
    [SerializeField]private float amountOfAttack = 4;
    [SerializeField]private float cloneAttackCoolDown = .5f;
    [Space(30)]
    [SerializeField]private GameObject BlackHolePrefab;
    [SerializeField]private float maxSize;
    [SerializeField]private float growSpeed;
    [SerializeField]private float shrinkSpeed;
    
    [SerializeField]private float skillDuration;

    private BlackHole_Skill_Controll currentBlackHole;


    protected override void Start() {
        base.Start();

    }

    protected override void Update() {
        base.Update();
    }

    public override void UseSkill()
    {
        base.UseSkill();
        GameObject newBlackHole =  Instantiate(BlackHolePrefab,PlayerManger.instance.player.transform.position + new Vector3 (0,5),quaternion.identity);

        currentBlackHole =  newBlackHole.GetComponent<BlackHole_Skill_Controll>();
        currentBlackHole.SetUp(maxSize,growSpeed,shrinkSpeed,amountOfAttack,cloneAttackCoolDown,skillDuration);
    }

    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public bool BlackHoleFinished(){
        if(!currentBlackHole){
            return false;
        }
        if(currentBlackHole.PlayerCanExitBlackHole){
            currentBlackHole = null;
            return true;
        }
        return false;

    }
}
