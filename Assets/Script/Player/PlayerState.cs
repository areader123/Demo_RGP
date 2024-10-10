using System;
using System.Collections;
using UnityEngine;

public class PlayerState
{
    private string animboolname;

    protected float xinput;
    protected float yinput;
    protected PlayStateMachine stateMachine;
    protected Player player;
    public float StateTimer;
    // ��stateTimer ����������ͨ�õ� һ��ĳ����ʹ���� ���Թ���һ�������� ��Ϊ�������¸����ำֵ������� false // dash ������ȴ ����
    //ͬһstatetimer �Ḳ��ԭ�е�statetimer 
    protected bool triggercalled;

    public PlayerState(string _animboolname, PlayStateMachine _stateMachine, Player _player)
    {
        this.animboolname = _animboolname;
        this.stateMachine = _stateMachine;
        this.player = _player;
    }

    public virtual void Enter()
    {
        player.animator.SetBool(animboolname, true);
        //Debug.Log("I am entering" + animboolname);
        triggercalled = false;
    }
    public virtual void Update()
    {
        xinput = Input.GetAxisRaw("Horizontal");
        yinput = Input.GetAxisRaw("Vertical");
      //  Debug.Log("I am in"+animboolname);
        player.animator.SetFloat("Y_velocity", player.rb.velocity.y);
        StateTimer -= Time.deltaTime;
    }
    public virtual void Exit()
    {
        player.animator.SetBool(animboolname, false);
       // Debug.Log("I am exit" + animboolname);
    }
    public virtual void AnimationFinishTrigger() 
    {
        triggercalled = true;
    }
}
