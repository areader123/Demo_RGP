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
      // �����Ƚ��� jump Ȼ�� air �� air �ǿ��Ի����� ������ͬһ��ǽ���Ͽ����������� �Ҷ����෴
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
