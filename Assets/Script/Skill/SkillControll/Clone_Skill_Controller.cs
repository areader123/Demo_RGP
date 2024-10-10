using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_Skill_Controller : MonoBehaviour
{
    private SpriteRenderer sr;
    private Animator anim;
    [SerializeField] private float colorLosingSpeed;
    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackCheckRadius = .8f;
    private float cloneTImer;
    private Transform closestEnemy;


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();   
    }
    private void Update()
    {
        cloneTImer -= Time.deltaTime;
        if (cloneTImer < 0) 
        {
            sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * colorLosingSpeed));
            if(sr.color.a < 0) 
            {
                //���������prefab�� ���Զ�Ӧ�����ĸ��� �����������ͳ�ʼ��
                Destroy(gameObject);
            }
        }
    }

    public void SetUpClone(Transform _newPosition, float cloneDuration, bool canAttack, Vector3 _offSet,Transform _closestEnemy)
    {
        if (canAttack) 
        {
            anim.SetInteger("AttackNumber",Random.Range(1,3));
        }   
        transform.position = _newPosition.position + _offSet;
        cloneTImer = cloneDuration;
        closestEnemy = _closestEnemy;
        FaceCloseTarget();
    }

    private void AnimationTrigger()
    {
        cloneTImer = -1f;
        // ��ʱ����Ϊ�� clone ֱ�ӿ�ʼ��ʧ
        // ����animationTrigger 
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                hit.GetComponent<Enemy>().Damage();
            }
        }
    }
     private void FaceCloseTarget() 
    {
        if(closestEnemy != null)
        {
            if (transform.position.x > closestEnemy.position.x) 
            {
                transform.Rotate(0, 180, 0);
            }
        }
    }
}
