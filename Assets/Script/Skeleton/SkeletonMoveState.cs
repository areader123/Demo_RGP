using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoveState : SkeletonGroundState
{
    public SkeletonMoveState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Skeleton enemy) : base(enemyBase, stateMachine, animBoolName, enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //enemy.SetVelocity(enemy.defaultMoveSpeed * enemy.faceDir, rb.velocity.y);
       enemy.rb.velocity = new Vector2(0, 0);
        //Debug.Log("�����ƶ�");
    }

    public override void Exit()
    {
        base.Exit();
       // enemy.SetVelocity(enemy.defaultMoveSpeed * enemy.faceDir, rb.velocity.y);
       enemy.rb.velocity = new Vector2(0, 0);
        // Debug.Log("�ƶ��˳�");
    }

    public override void Update()
    {
        base.Update();
        if(!enemy.timeFrozen)
            enemy.SetVelocity(enemy.defaultMoveSpeed * enemy.faceDir, rb.velocity.y);
        

        if (enemy.IsWallDetected() || !enemy.IsGroundDetected()) 
        {
            enemy.Flip();
           // Debug.Log("行走暂停");
            stateMachine.ChangeState(enemy.Skeleton_IdolState);
        }
       
    }
}
