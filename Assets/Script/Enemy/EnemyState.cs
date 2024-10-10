using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyState 
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemyBase;
    protected Rigidbody2D rb;

    protected bool triggerCalled;
    private string animBoolName;
    protected float stateTimer;

    public EnemyState(EnemyStateMachine stateMachine, Enemy enemyBase,  string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.enemyBase = enemyBase;
        this.animBoolName = animBoolName;
    }
    public virtual void Enter()
    {
        triggerCalled = false;
        rb = enemyBase.rb;
        enemyBase.animator.SetBool(animBoolName, true);
       // Debug.Log(animBoolName+"进入");
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
       // Debug.Log(animBoolName+"正在");
    }
    public virtual void Exit() 
    {
        enemyBase.animator.SetBool(animBoolName, false);
      //  Debug.Log(animBoolName + "退出");
    }
    public virtual void AnimationFinishTrigger() 
    {
        triggerCalled = true;
    }
}
