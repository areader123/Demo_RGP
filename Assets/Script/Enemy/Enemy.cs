using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] public LayerMask WhatIsPlayer;

    [Header("TimeFreeze Info")]
    public bool timeFrozen;

    [Header("Stunned Info")]
    public float stunDuration;
    public Vector2 stunDirection;
    protected bool canBestunned;
    [SerializeField] protected GameObject counterImage;
    public EnemyStateMachine stateMachine { get; private set; }

    [Header("Move Info")]
    public float MoveSpeed;
    public float battleTime;
    public float defaultMoveSpeed;

    [Header("Attack info")]
    public float attackDistance;
    public float attackCooldown;
    [SerializeField] public float lastTimeAttack;


    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
    }
    protected override void Update()
    {
        base.Update();

        stateMachine._currentState.Update();
        if (IsPlayerDetected())
        {
        }
        // if (!isKoncked)
         //{
         // FlipController(rb.velocity.x);
        //  }
    }

    public virtual void OpenCounterAttackWindow()
    {
       // Debug.Log("����ɷ���"); 
        canBestunned = true;
        counterImage.SetActive(true);
    }
    public virtual void CloseCounterAttackWindow()
    {
        canBestunned = false;
        counterImage.SetActive(false);
    }

    public virtual bool CanBeStunned()
    {
        if(canBestunned)
        {
            CloseCounterAttackWindow();
            return true;
        }
        return false;
    }

    public virtual void FreezeTime(bool _timeFrozen)
    {
        if (_timeFrozen)
        {
            MoveSpeed = 0;
            animator.speed = 0;
            timeFrozen = _timeFrozen;
        }
        else
        {
            MoveSpeed = defaultMoveSpeed;
            animator.speed = 1;
        }
    }
    public virtual void FreezeTimeFor(float _duration) => StartCoroutine(FreezeTimerCoroutine(_duration));
    public virtual IEnumerator FreezeTimerCoroutine(float _second)
    {
        FreezeTime(true);
        yield return new WaitForSeconds(_second);
        FreezeTime(false);
    }

    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallcheck.position, Vector2.right * faceDir, 50, WhatIsPlayer);

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;        
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance*faceDir,transform.position.y));
    }
    //������enemystate�еķ�����¶��enemy�� ���� ������skeletonAnimationtriggers ������triggers������ʹ�� ���Ҹ������˵�triggers��Ҫ���ڶ����ϵ�
    public virtual void AnimationFinishTrigger() => stateMachine._currentState.AnimationFinishTrigger();
}
