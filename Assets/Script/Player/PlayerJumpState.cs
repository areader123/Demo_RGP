using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(string _animboolname, PlayStateMachine _stateMachine, Player _player) : base(_animboolname, _stateMachine, _player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.rb.velocity = new Vector2(player.rb.velocity.x, player.JumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(player.rb.velocity.y < 0) 
        {
            stateMachine.ChangeState(player.AirState);
        }
    }
}
