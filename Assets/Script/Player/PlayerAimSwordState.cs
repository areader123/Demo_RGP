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
        //进入
        SkillManger.Instance.throwSword.SetDotsActive(true);
    }

    public override void Exit()
    {
        base.Exit();
        //用BusyFor 设置一段时间来避免进入ThrowSword动画时 人物可以移动
        //（动画没有进入闲置，但状态已经进入了闲置 并且可以直接进入移动状态）
        //因为该段动画没有对应的状态 无法设置速度
        player.StartCoroutine("BusyFor", .2f);
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(0, 0);
        //再次点击 先进入到发射剑的动画 然后到闲置动画
        if(Input.GetKeyDown(KeyCode.Mouse1)) 
        {
            stateMachine.ChangeState(player.IdolState);
        }

        //人物方向随着投掷方向发生翻转
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
