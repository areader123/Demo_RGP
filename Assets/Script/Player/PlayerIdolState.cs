using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdolState : PlayerGroundState
{
 public PlayerIdolState(string _animboolname, PlayStateMachine _stateMachine, Player _player):base(_animboolname, _stateMachine ,_player)
    {
    }
    public override void Update() 
    {
        base.Update();
       //player.rb.velocity = new Vector2(0, 0);
        if (xinput == player.faceDir &&player.IsWallDetected())
        {
           return;
        }
        if(xinput != 0 &&!player.isbusy) 
        {
           stateMachine.ChangeState(player.MoveState);
        }
    }
    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(0, 0);
       //player.rb.velocity = new Vector2(0, 0);
    }
    public override void Exit()
    {
        base.Exit();
         player.SetVelocity(0, 0); 
     // player.rb.velocity = new Vector2(0, 0);
    }
}
