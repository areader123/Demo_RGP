using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }

    public EntityFX fx { get; private set; }

    public SpriteRenderer sr { get; private set; }
    #endregion

    [Header("Knockback Info")]
    [SerializeField] protected Vector2 knockbackDirection;
    public bool isKoncked;
    [SerializeField] protected float konckbackDuration;

    [Header("Collision info")]
    public Transform attackCheck;
    public float attackCheckRadius;
    [SerializeField] protected Transform groundcheck;
    [SerializeField] protected float grounddistance;
    [SerializeField] protected Transform wallcheck;
    [SerializeField] protected float walldistance;
    [SerializeField] protected LayerMask WhatIsGround;

    




    public int faceDir { get; private set; } = 1;
    public bool faceright = true;

    public System.Action OnFlipped;

    protected virtual void Awake() 
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        fx = GetComponent<EntityFX>();  
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

  protected virtual void Start() 
  {

    }

  protected virtual void Update() 
  {

  }

    public virtual void Damage()
    {
        fx.StartCoroutine("FlashFX");
        StartCoroutine("HitKnockback");
        Debug.Log(gameObject.name+"damage");
    }

    protected virtual IEnumerator HitKnockback()
    {
        isKoncked = true;
        Debug.Log("ture");
        rb.velocity = new Vector2(knockbackDirection.x * (-faceDir), knockbackDirection.y);
        yield return new WaitForSeconds(konckbackDuration);
        //rb.velocity = new Vector2(0,0);
        isKoncked = false;
        
        Debug.Log("false");
    }


    #region Collision
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallcheck.position, Vector2.right * faceDir, walldistance, WhatIsGround);
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundcheck.position, Vector2.down, grounddistance, WhatIsGround);
    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallcheck.position, new Vector3(wallcheck.position.x + walldistance * faceDir, wallcheck.position.y));
        Gizmos.DrawLine(groundcheck.position, new Vector3(groundcheck.position.x, groundcheck.position.y - grounddistance));
        Gizmos.DrawWireSphere(attackCheck.position,attackCheckRadius);
    }
    #endregion



    #region Flip
    public virtual void Flip()
    {
        faceDir *= -1;
        faceright = !faceright;
        transform.Rotate(0, 180, 0);


        if(OnFlipped != null)
        {
            OnFlipped();
        }
        
    }
    public virtual void FlipController(float _x)
    {

        if (_x > 0 && !faceright)
        {
            Flip();
            Debug.Log("I am in fliping");
        }
         if (_x < 0 && faceright)
        {
            Flip();
            Debug.Log("I am in fliping");
        }

    }
    #endregion

    #region Velocity
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        if (isKoncked)
        {
            return;
        }
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
    }
    #endregion

    public void MakeTransparent(bool _transParent){
        if(_transParent){
            sr.color = Color.clear;
        }else
        {
            sr.color = Color.white;
        }

    }
}
