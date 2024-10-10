using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    protected Enemy_Skeleton enemy;
    private Transform player;
    private int moveDir ;
    public SkeletonBattleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName,Enemy_Skeleton enemy) : base(stateMachine, enemyBase, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManger.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
       
        if(enemy.IsWallDetected() || !enemy.IsGroundDetected()){
            stateMachine.ChangeState(enemy.Skeleton_MoveState);
        }else{

            if (enemy.IsPlayerDetected())
            {
                
               // Debug.Log("入战");
                stateTimer = enemy.battleTime;
                //��Ҽ���Ƿ��ڹ�������
                if (enemy.IsPlayerDetected().distance < enemy.attackDistance) 
                {
                    //������ȴ
                    if (CanAttack() )
                    {
                        stateMachine.ChangeState(enemy.Skeleton_AttackState);
                    }
                    
                }
            }
            else
            {
                if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 10)
                {
                    Debug.Log("脱战");
                    stateMachine.ChangeState(enemy.Skeleton_IdolState);
                }
            }
        }
       // if (enemy.IsPlayerDetected().distance < enemy.attackDistance * moveDir){
            //stateMachine.ChangeState(enemy.Skeleton_IdolState);
      //  }else{
        
            if (player.position.x > enemy.transform.position.x && !enemy.faceright)
            {
               //enemy.faceDir = 1;
                enemy.Flip();
            }else if(player.position.x < enemy.transform.position.x && enemy.faceright)
            {
                //moveDir=-1; 
                enemy.Flip();
            }
            enemy.SetVelocity(enemy.MoveSpeed * enemy.faceDir, rb.velocity.y);
           // enemy.FlipController(rb.velocity.x);

      //  }
     
    }
    // ����ʱ����ȴ
    private bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttack + enemy.attackCooldown)
        {
            enemy.lastTimeAttack = Time.time;
            return true;
        }
        return false;
    }
}
