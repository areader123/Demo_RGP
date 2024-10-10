using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlipJump : PlayerJumpState
{
    public PlayerWallSlipJump(string _animboolname, PlayStateMachine _stateMachine, Player _player) : base(_animboolname, _stateMachine, _player)
    {
    }

    public override void Enter()
    {
        player.rb.velocity += new Vector2(player.movespeed*player.faceDir*-1    , player.JumpForce);
      //  stateMachine.ChangeState(player.JumpState);
      // 否则先进入 jump 然后 air 在 air 是可以换方向 导致在同一面墙壁上可以连续攀爬 且动画相反
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

    }
}
