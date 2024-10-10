using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    public PlayerCounterAttackState(string _animboolname, PlayStateMachine _stateMachine, Player _player) : base(_animboolname, _stateMachine, _player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StateTimer = player.counterAttackDuration;
        player.animator.SetBool("SuccessfulCounterAttack", false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(0, 0);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                if(hit.GetComponent<Enemy>().CanBeStunned() )
                {
                    StateTimer = 10; //�����һ��ʱ�䵫�ǲ�����̫�� Ӧ����������״̬Ҫ�õ�������� ̫�̿��ܳ�bug������ʱ��̫�� ����ǰ�˳�
                    player.animator.SetBool("SuccessfulCounterAttack", true);

                    SkillManger.Instance.parry_Skill.UseSkill();//restore to health
                    if(SkillManger.Instance.parry_Skill.ParryWithmirageUnlock)
                    {
                        SkillManger.Instance.parry_Skill.MakeMirageOnParry(hit.transform);
                    }

                }
            }
        }
        if (StateTimer < 0 || triggercalled)
        {
            stateMachine.ChangeState(player.IdolState);
        }

    }
}
