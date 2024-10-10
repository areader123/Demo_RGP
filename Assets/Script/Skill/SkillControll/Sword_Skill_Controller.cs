using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.AI;

public class Sword_Skill_Controller : MonoBehaviour
{
    [SerializeField] private float returmSpeed =12;

   
    private Animator anim;
    private Rigidbody2D rb;
    private CircleCollider2D cd;
    private bool isReturning;
    private bool canRotate =true;

    private Player player;

    [Header("bounce Info")]
    private bool isBounce ;
    private int amountOfBounce;
    private List<Transform> enemyTarget = new List<Transform>();
    private int targetindex = 0;
    [SerializeField]private float bounceSpeed;

    [Header("pierce Info")]
    private bool isPierce;
    private int amountOfPierce;

    [Header("spin Info")]

    private float travelDuration;
    private float maxTravelDistance;
    private float spinTimer;
    private bool spinStopped;
    private bool isSpinning;

    private float hitTimer;
    private float hitCoolDown;

    private void Awake()
    {
        //����õ���������߸����Ҫʹ��awake 
        //��Ϊ�����ط�ʹ�� setUpSwordʱ �������start ����awake
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
        player =  PlayerManger.instance.player;
    }
    private void Update()
    {
        if (canRotate)
        {
            //��ͷ�����ٶȷ���
            transform.right = rb.velocity;
        }
        if (isReturning)
        {
            //���ý����صķ���(Ϊʵʱ�仯) �� ������һ������ʱ ���붯�� �����ٽ�����
            transform.position = Vector2.MoveTowards(transform.position, PlayerManger.instance.player.transform.position, returmSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, PlayerManger.instance.player.transform.position) < 1)
            {
                PlayerManger.instance.player.CatchTheSword();
            }
        }
        if (isBounce && enemyTarget.Count > 0) //�������б������ �����
        {
            transform.position = Vector2.MoveTowards(transform.position, enemyTarget[targetindex].position, bounceSpeed * Time.deltaTime);
            //ʹ��������ĵ��˷�
            if (Vector2.Distance(transform.position, enemyTarget[targetindex].position) < .1f)//�жϽ��Ƿ񵽴���˵�λ��
            {
                enemyTarget[targetindex].GetComponent<Enemy_Stat>().DoDamage(player.GetComponent<Player_Stat>());
               
                if(enemyTarget[targetindex].GetComponent<Enemy_Stat>() != null)
                {
                    if(Inventor.instance.GetSingleEquipment(Equipment.Amulet))
                    {
                        Inventor.instance.GetSingleEquipment(Equipment.Amulet).Effect(enemyTarget[targetindex].transform);
                    }
                }

                targetindex++;//��ת����һ��
                amountOfBounce--;//����������һ
                if (targetindex >= enemyTarget.Count)
                {
                    targetindex = 0;
                }
                if (amountOfBounce <= 0)
                {
                    isBounce = false;//���ؽ�
                    isReturning = true;
                }
            }
        }
        SpinLogic();

    }

    private void SpinLogic()
    {
        if (isSpinning)
        {
            if (Vector2.Distance(PlayerManger.instance.player.transform.position, transform.position) > maxTravelDistance && !spinStopped)
            {
                StopWhenSpinning();
             
            }

            if (spinStopped)
            {
                spinTimer -= Time.deltaTime;
                rb.velocity = new Vector3(5,5,0);
                //transform.position = Vector2.MoveTowards(transform.position,player.faceDir * new Vector3(100,100,100),(float)0.0001 * Time.deltaTime);
                if (spinTimer < 0)
                {
                    isSpinning = false;
                    isReturning = true;
                }

                hitTimer -= Time.deltaTime;
                if (hitTimer < 0)
                {
                    hitTimer = hitCoolDown;
                    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1);
                    foreach (var hit in colliders)
                    {
                        if (hit.GetComponent<Enemy_Stat>() != null)
                        {
                            hit.GetComponent<Enemy_Stat>().DoDamage(player.GetComponent<Player_Stat>());
                            if(Inventor.instance.GetSingleEquipment(Equipment.Amulet))
                            {
                                Inventor.instance.GetSingleEquipment(Equipment.Amulet).Effect(hit.transform);
                            }
                        }
                    }
                }
            }
        }
    }

    private void StopWhenSpinning()
        {
            spinStopped = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            
            spinTimer = travelDuration;
        }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isReturning)
        {
            //����ʱ������ײ����
            return;
        }
        if(collision.GetComponent<Enemy_Stat>() != null)
        {
            collision.GetComponent<Enemy_Stat>().DoDamage(player.GetComponent<Player_Stat>());
        }

        BounceLogic(collision);

        StuckInto(collision);
    }

    private void BounceLogic(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)//����һ����ײ�����Ƿ�Ϊ����
                                                    //��Ϊ���� �򲻻����������� ���ҽ��뿨ס���
        {
            if (isBounce && enemyTarget.Count <= 0)//�˴���������enemy��transform�����б� 
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);
                foreach (var hit in colliders)
                {
                    if (hit.GetComponent<Enemy>() != null)
                    {
                        enemyTarget.Add(hit.transform);
                    }
                }
            }
        }
    }

    private void StuckInto(Collider2D collision)
    {
        if(amountOfPierce > 0 && collision.GetComponent<Enemy>() != null){
            amountOfPierce--;
            return;
        }

        if(isSpinning){
           StopWhenSpinning();
            return;
        }
        canRotate = false; //��Ҫ��CirCle ��ײ���� ��ѡIsTrigger ����ú�����������        
        cd.enabled = false;//�ر�Բ����ײ��       
        rb.isKinematic = true;//�ر�2D������������� ������       
        rb.constraints = RigidbodyConstraints2D.FreezeAll; //�ر�2D�����еĸ�������
        if (isBounce && enemyTarget.Count>0)
        {
            return;//�˴� ��������������0��isBounceΪ�� ������뿨ס״̬
        }
        anim.SetBool("Rotation", false);
        transform.parent = collision.transform;//���� ������ײ�������
                                               //����������ײ��һ���ƶ�
                                               //��Ϊ���Ѿ�����ײ��Ϊһ�� ��ײ����flip
    }

    public void ReturnSword()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;//�˴������ƶ� ���⽣�ɳ���δ���ʱ�����ٶ� Ӱ��ɻص�Ч��
        //rb.isKinematic=false;
        transform.parent = null;
        isReturning = true;
    }
    public void SetUpSword(Vector2 _dir,float _gravityScale)
    {
         rb.velocity = _dir;
         rb.gravityScale = _gravityScale;
         if(amountOfPierce <= 0){
            anim.SetBool("Rotation", true);
         }
       // Debug.Log("������һ�ѽ�");
    }
    public void SetUpBounce(bool _isBounce,int _amountOfBounce)
    {
        this.isBounce = _isBounce;
        this.amountOfBounce = _amountOfBounce;
        
    }
    public void SetUpPierce(bool _isPierce,int _amountOfPierce){
        this.isPierce = _isPierce;
        this.amountOfPierce = _amountOfPierce;
        
    }
    public void SetUpSpin (bool _isSpinning,float _maxTravelDistance,float _travelDuration,float _hitTimeCool) {
        this.maxTravelDistance = _maxTravelDistance;
        this.isSpinning = _isSpinning;
        this.travelDuration = _travelDuration;
        hitCoolDown = _hitTimeCool;
    }
}
