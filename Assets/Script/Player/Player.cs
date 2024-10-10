using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

public class Player : Entity
{
    [Header("Attack detail")]
    public Vector2[] attackmove;
    public float counterAttackDuration = .2f;

    public bool isbusy { get; private set; }
    [Header("move info")]
    public float movespeed = 12f;
    public float JumpForce;
    public float swordReturnForce;
    [Header("Dash Info")]
    public float DashDuration;
    public float DashSpeed;
    public float DashDir { get; private set; }



    private float xinput;





    public PlayStateMachine stateMachine { get; private set; }

    public PlayerIdolState IdolState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerGroundState PlayGroundState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerAirState AirState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerWallSlipState WallSlipState { get; private set; }

    public PlayerWallSlipJump WallSlipJump { get; private set; }

    public PlayerAttackState AttackState { get; private set; }
    public PlayerCounterAttackState CounterAttackState { get; private set; }
    public PlayerAimSwordState AimSwordState { get; private set; }
    public PlayerCatchSwordState CatchSwordState { get; private set; }

    public PlayerBlackHoleState BlackHoleState{get; private set;}

    public Player_Die_State DieState{get; private set;}

    public GameObject sowrd { get; private set; }

    


    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayStateMachine();
        MoveState = new PlayerMoveState("Move", stateMachine, this);
        IdolState = new PlayerIdolState("Idol", stateMachine, this);
        PlayGroundState = new PlayerGroundState("Ground", stateMachine, this);
        JumpState = new PlayerJumpState("Jump", stateMachine, this);
        AirState = new PlayerAirState("Air", stateMachine, this);
        DashState = new PlayerDashState("Dash", stateMachine, this);
        WallSlipState = new PlayerWallSlipState("WallSlip", stateMachine, this);
        WallSlipJump = new PlayerWallSlipJump("Jump", stateMachine, this);
        AttackState = new PlayerAttackState("Attack", stateMachine, this);
        CounterAttackState = new PlayerCounterAttackState("CounterAttack", stateMachine, this);
        AimSwordState = new PlayerAimSwordState("AimSword", stateMachine, this);
        CatchSwordState = new PlayerCatchSwordState("CatchSword", stateMachine, this);
        BlackHoleState = new PlayerBlackHoleState("Jump",stateMachine,this);
        DieState = new Player_Die_State("Die", stateMachine,this);

    }
    protected override void Start()
    {
        base.Start();

        stateMachine.Intialize(IdolState);

    }

    protected override void Update()
    {
        base.Update();
        CheckDashInput();
        stateMachine.currentstate.Update();
        xinput = UnityEngine.Input.GetAxisRaw("Horizontal");
        FlipController(xinput);
        if(UnityEngine.Input.GetKeyDown(KeyCode.F) && SkillManger.Instance.crystal_Skill.crystalUnlock){
            SkillManger.Instance.crystal_Skill.CanUseSkill();
        }
    }

    public void AssignSword(GameObject _newSword)
    {
        this.sowrd = _newSword;
    }
    public void CatchTheSword()
    {
        stateMachine.ChangeState(CatchSwordState);
        Destroy(this.sowrd);
    }

    public void ExitBlackHole(){
        stateMachine.ChangeState(AirState);
    }


    private void CheckDashInput() 
    {
        if(IsWallDetected())
        {
            return;
        }

        if(SkillManger.Instance.dash.dashUnlocked == false) 
        {
            return;
        }
        if(UnityEngine.Input.GetKeyDown(KeyCode.LeftShift)&& SkillManger.Instance.dash.CanUseSkill())   
        {
            DashDir = UnityEngine.Input.GetAxisRaw("Horizontal");
            if(DashDir == 0) 
            {
                DashDir = faceDir;
            }
            stateMachine.ChangeState(DashState);
        }
    }
    public void AnimationTrigger()=>stateMachine.currentstate.AnimationFinishTrigger();

    public IEnumerator BusyFor(float _seconds)
    {
        isbusy = true;
        yield return new WaitForSeconds(_seconds);
        isbusy = false;
    }
}
