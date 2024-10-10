using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundState
{
    public PlayerMoveState(string _animboolname, PlayStateMachine _stateMachine, Player _player):base(_animboolname, _stateMachine, _player) 
    {
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit() 
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        player.SetVelocity(xinput*player.movespeed, player.rb.velocity.y);
        if (xinput == 0)
        {
            //player.SetVelocity(0, 0);
          player.rb.velocity = new Vector2(0, 0);
            stateMachine.ChangeState(player.IdolState);
        }
    }
}
