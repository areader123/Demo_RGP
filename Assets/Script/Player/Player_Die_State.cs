using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Die_State : PlayerState
{
    public Player_Die_State(string _animboolname, PlayStateMachine _stateMachine, Player _player) : base(_animboolname, _stateMachine, _player)
    {
    }
    public override void Update(){
        base.Update();
        player.SetVelocity(0, 0);
    }
    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit(){
        base.Exit();
    }
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }
}
