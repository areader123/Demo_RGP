using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlipState : PlayerState
{
    public PlayerWallSlipState(string _animboolname, PlayStateMachine _stateMachine, Player _player) : base(_animboolname, _stateMachine, _player)
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
        if(xinput != 0&& player.faceDir != xinput)
        {
            stateMachine.ChangeState(player.IdolState);
        }


        if(player.IsGroundDetected()) 
                {
                    stateMachine.ChangeState(player.IdolState);
                }



        if(yinput <0)
        {
            player.rb.velocity = new Vector2(0, player.rb.velocity.y);
        }
        else 
        { 
            player.rb.velocity = new Vector2(0,player.rb.velocity.y*.7f);
        }
        
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            stateMachine.ChangeState(player.WallSlipJump);
        }
        
    }
}
