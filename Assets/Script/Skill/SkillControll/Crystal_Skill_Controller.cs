using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Crystal_Skill_Controller : MonoBehaviour
{
    private float skillDuration ;
    private float skillTimer;
    private Animator animator;

    private bool canBoom;

    private bool canGrow;
    private bool canMove;
    private float growSpeed;
    private float moveSpeed;

    private CircleCollider2D cd;

    private Transform cloestTransform;

    private int amountOfCrystal;
    private bool canUseMultyCrystal;
    private List<GameObject> crystalList;

    private void Awake() {
        animator = GetComponent<Animator>();
        cd = GetComponent<CircleCollider2D>();
    }

    public void SetUp(float _skillDurantin,bool _canBoom,float _growSpeed,Transform _cloestTransform,float _moveSpeed,bool _canMove){
         skillDuration = _skillDurantin;
         canBoom = _canBoom;
         growSpeed = _growSpeed;
         cloestTransform = _cloestTransform;
         moveSpeed = _moveSpeed;
         canMove = _canMove;
        
    }
   
    public void Update()
    {
        skillTimer += Time.deltaTime;
        if (skillTimer > skillDuration)
        {
            FinishCrystal();
        }
        CrystalDamageGrow();
        CrystalMove();
    }

    private void CrystalMove()
    {
        if (canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, cloestTransform.position, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, cloestTransform.position) < 1)
            {
                FinishCrystal();
                canMove = false;
            }
        }
    }

    private void CrystalDamageGrow()
    {
        if (canGrow)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(3, 3), growSpeed * Time.deltaTime);
        }
    }

    public void FinishCrystal()
    {
        
            if (canBoom)
            {
                canGrow = true;
                animator.SetBool("Boom", true);
            }
            else
            {
                Destroy(gameObject);
            }
    }

    public void CrystalBoomFinished () {
        Destroy(gameObject);
    }

    public void CrystalBoomDamage () {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, cd.radius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                hit.GetComponent<Enemy>().Damage();
            }
        }
    }
    
    

}

