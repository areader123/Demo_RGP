using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(string _animboolname, PlayStateMachine _stateMachine, Player _player) : base(_animboolname, _stateMachine, _player)
    {

    }
    public override void Enter()
    {
        base.Enter();
       // stateMachine.ChangeState(player.IdolState);
    }
    public override void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Inventor.instance.useFlask();
        }

        if(Input.GetKeyDown(KeyCode.N))
        {
            stateMachine.ChangeState(player.BlackHoleState);
        }

        if(Input.GetKeyDown(KeyCode.Mouse1)&& HasNoSword()) 
        {
            stateMachine.ChangeState(player.AimSwordState);
        }
        if(Input.GetKeyDown(KeyCode.Mouse0) && !Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.Z))
        {
            stateMachine.ChangeState(player.AttackState);
        }
        if(!player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.AirState);
        }

        if(Input.GetKeyDown(KeyCode.Space)&& player.IsGroundDetected()) 
        {
            stateMachine.ChangeState(player.JumpState);
        }
        if(Input.GetKeyDown(KeyCode.Q) && SkillManger.Instance.parry_Skill.parryUnlock)
        {
            stateMachine.ChangeState(player.CounterAttackState);
        }
    }
    public override void Exit() 
    {
        base.Exit();
    }
    private bool HasNoSword()
    {
        // �˴����������Ƿ��н� ���Ѵ��ڽ�����ս� ͬʱ�����������
        // ����bug ����һ�ѽ����ڵ�������
        if (!player.sowrd)
        {
            return true;
        }
        player.sowrd.GetComponent<Sword_Skill_Controller>().ReturnSword();
        return false;
    }
}
