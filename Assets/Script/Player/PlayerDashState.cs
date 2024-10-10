using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    
    public PlayerDashState(string _animboolname, PlayStateMachine _stateMachine, Player _player) : base(_animboolname, _stateMachine, _player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //SkillManger.Instance.clone.CreatClone(player.transform,new Vector3(0,0,0));
        if(SkillManger.Instance.dash.CloneDashUnlock)
        {
            SkillManger.Instance.dash.CloneOnDash();
        }
        StateTimer = player.DashDuration;
       
        
    }

    public override void Exit()
    {
        base.Exit();
        if(SkillManger.Instance.dash.CloneOnArriveUnlock)
        {
            SkillManger.Instance.dash.CloneOnArrival();
        }
        player.SetVelocity(0, player.rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(player.DashSpeed*player.DashDir, 0);
        if (StateTimer < 0) 
        {
            stateMachine.ChangeState(player.IdolState);
        }
    }
}
