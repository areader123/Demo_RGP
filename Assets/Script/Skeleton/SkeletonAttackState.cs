using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackState : EnemyState
{
    private Enemy_Skeleton enemy;
    public SkeletonAttackState(Enemy enemyBase,EnemyStateMachine stateMachine,  string animBoolName,Enemy_Skeleton enemy) : base(stateMachine, enemyBase, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
       // enemy.SetVelocity(0,0);
    }

    public override void Exit()
    {
        base.Exit();
        //enemy.SetVelocity(0,0);
        enemy.lastTimeAttack = Time.time;
    }

    public override void Update()
    {
        base.Update();
        enemy.SetVelocity(0, 0);
        if(triggerCalled) 
        {
            stateMachine.ChangeState(enemy.Skeleton_BattleState);
        }

    }

}
