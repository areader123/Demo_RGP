    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStunnedState : EnemyState
{
    private Enemy_Skeleton enemy;
    public SkeletonStunnedState(Enemy enemyBase,EnemyStateMachine stateMachine,  string animBoolName,Enemy_Skeleton enemy) : base(stateMachine, enemyBase, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("�����ܻ�״̬");
        enemy.fx.InvokeRepeating("RedColorBlink",0,.1f);
        stateTimer = enemy.stunDuration;
        rb.velocity=new Vector2(-enemy.faceDir * enemy.stunDirection.x,enemy.stunDirection.y);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.fx.Invoke("CancleColorChange",0);
      //  rb.velocity = new Vector2(0, 0);
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(enemy.Skeleton_IdolState);
        }
    }
}
