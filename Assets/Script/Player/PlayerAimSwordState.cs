using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class PlayerAimSwordState : PlayerState
{
    public PlayerAimSwordState(string _animboolname, PlayStateMachine _stateMachine, Player _player) : base(_animboolname, _stateMachine, _player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //����
        SkillManger.Instance.throwSword.SetDotsActive(true);
    }

    public override void Exit()
    {
        base.Exit();
        //��BusyFor ����һ��ʱ�����������ThrowSword����ʱ ��������ƶ�
        //������û�н������ã���״̬�Ѿ����������� ���ҿ���ֱ�ӽ����ƶ�״̬��
        //��Ϊ�öζ���û�ж�Ӧ��״̬ �޷������ٶ�
        player.StartCoroutine("BusyFor", .2f);
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(0, 0);
        //�ٴε�� �Ƚ��뵽���佣�Ķ��� Ȼ�����ö���
        if(Input.GetKeyDown(KeyCode.Mouse1)) 
        {
            stateMachine.ChangeState(player.IdolState);
        }

        //���﷽������Ͷ����������ת
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
         if(player.transform.position.x > mousePosition.x && player.faceDir == 1) 
        {
            player.Flip();
        }
        else if(player.transform.position.x < mousePosition.x && player.faceDir == -1)
        {
            player.Flip();
        }

    }
}
