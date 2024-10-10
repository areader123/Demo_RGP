using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(string _animboolname, PlayStateMachine _stateMachine, Player _player) : base(_animboolname, _stateMachine, _player)
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
        if(player.IsWallDetected()) 
        {
            stateMachine.ChangeState(player.WallSlipState);
        }
       if(player.IsGroundDetected()) 
       {
         stateMachine.ChangeState(player.IdolState);
       }
        if(xinput != 0) 
       {
           player.SetVelocity(player.movespeed * xinput * .8f, player.rb.velocity.y);               
        }
        
        
        
        
    }
}
