using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGroundState : EnemyState
{
    protected Transform player;
    protected Enemy_Skeleton enemy;
    public SkeletonGroundState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName ,Enemy_Skeleton enemy) : base(stateMachine, enemyBase, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player =PlayerManger.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (enemy.IsPlayerDetected()|| Vector2.Distance(enemy.transform.position,player.position)<2)
        {
            stateMachine.ChangeState(enemy.Skeleton_BattleState);
        }

    }
}
