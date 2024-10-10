using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public int comboCounter{ get; private set;}
    private float lastTimeAttacked;
    private float combowindow=2;
    public PlayerAttackState(string _animboolname, PlayStateMachine _stateMachine, Player _player) : base(_animboolname, _stateMachine, _player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        xinput = 0;
        if(comboCounter >2 || Time.time >= lastTimeAttacked+combowindow) 
        {
            comboCounter = 0;
        }
        float attackDir = player.faceDir;
        if (xinput != 0) 
        {
            attackDir = xinput;
        }
        player.animator.SetInteger("ComboCounter", comboCounter);   
        StateTimer = .1f;
        player.SetVelocity(player.attackmove[comboCounter].x * attackDir, player.attackmove[comboCounter].y);
       // player.animator.speed = 2;
    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("BusyFor", .1f);
        comboCounter++;
       lastTimeAttacked = Time.time;
      //  player.animator.speed=1;
    }

    public override void Update()
    {
        base.Update();
        if(triggercalled) 
        {
            stateMachine.ChangeState(player.IdolState);
        }
        if(StateTimer<0)
        {
            player.SetVelocity(0, 0);
        }
    }
}
