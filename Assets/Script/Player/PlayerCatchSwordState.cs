using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatchSwordState : PlayerState
{
    public Transform sword;
    public PlayerCatchSwordState(string _animboolname, PlayStateMachine _stateMachine, Player _player) : base(_animboolname, _stateMachine, _player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        sword = player.sowrd.transform;
        player.SetVelocity(0, 0);
        if (player.transform.position.x > sword.position.x && player.faceDir == 1)
        {
            player.Flip();
        }
        else if (player.transform.position.x < sword.position.x && player.faceDir == -1)
        {
            player.Flip();
        }
        //注意此处的速度不能过大 否则会发生翻转
        player.rb.velocity = new Vector2(player.swordReturnForce * - player.faceDir,player.rb.velocity.y);
    }


    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(triggercalled) 
        {
            stateMachine.ChangeState(player.IdolState);
        } 
    }
}
