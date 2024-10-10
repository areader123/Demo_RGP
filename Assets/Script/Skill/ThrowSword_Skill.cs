using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;


public enum SwordType
{
    Regular,
    Bounce,
    pierce,
    Spin,
}
public class ThrowSword_Skill : SKill
{
    public SwordType swordType;

    [Header("spin Info")]
    [SerializeField]private float maxTravelDistance;
    [SerializeField]private float travelDuration;
    [SerializeField]private float hitCoolDown;


    [Header("Bounce Info")]
    [SerializeField] private int amountOfBounce;
    [SerializeField] private float bounceGravity;

    [Header("Pierce Info")]

    [SerializeField] private int amountOfPierce;
    [SerializeField] private int pierceGravity;

    [Header("Skill Info")]
    [SerializeField] private GameObject SwordPrefab;
    [SerializeField] private Vector2 lanchForce;
    [SerializeField] private float swordGravity;

    private Vector2 finalDir;
    [Header("Aim dots")]
    [SerializeField] private int numberOfdots;
    [SerializeField] private float spaceBetwonDots;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Transform dotsParent;

    private GameObject[]  dots;

    protected override void Start()
    {
        base.Start();
        GenerateDots();
        SetUpGravity();
    }

    private void SetUpGravity()
    {
        if(swordType == SwordType.pierce){
            swordGravity = pierceGravity;
        }
        if(swordType == SwordType.Bounce){
            swordGravity = bounceGravity;
        }
    }

    protected override void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1)) 
        {
            //�˴�������ʸ����һ������
            finalDir = new Vector2 (AimDiretion().normalized.x * lanchForce.x ,AimDiretion().normalized.y *lanchForce.y );
        }
        if( Input.GetKey(KeyCode.Mouse1)) 
        {
            for (int i = 0; i < numberOfdots; i++)
            {
                dots[i].transform.position = DotsPosition(i * spaceBetwonDots);
            }
        }

    }

    public void CreatSword(Transform _swordTransform)
    {
        SetDotsActive(false);
        GameObject newObject = Instantiate(SwordPrefab,_swordTransform.position,_swordTransform.rotation);
        newObject.GetComponent<Sword_Skill_Controller>().SetUpSword(finalDir,swordGravity);
        if( swordType == SwordType.Bounce)
        {
            newObject.GetComponent<Sword_Skill_Controller>().SetUpBounce(true, amountOfBounce);
        }
        else if(swordType == SwordType.pierce){
            newObject.GetComponent<Sword_Skill_Controller>().SetUpPierce(true,amountOfPierce);
        }else if(swordType == SwordType.Spin){
            newObject.GetComponent<Sword_Skill_Controller>().SetUpSpin(true,maxTravelDistance,travelDuration,hitCoolDown);
        }
        
        PlayerManger.instance.player.AssignSword(newObject);
        
    }
    #region Aim
    public Vector2 AimDiretion()
    {
        // ����õ�һ����Ͷ����ʸ��
        Vector2 playerPosition = PlayerManger.instance.player.transform.position;
        Vector2 mousepositon=Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousepositon - playerPosition;
        return direction;
    }

    public void SetDotsActive(bool _isActive)
    {
        for (int i = 0; i < numberOfdots; i++)
        {
            dots[i].SetActive(_isActive);
        }
    }

    private void GenerateDots()
    {
        //����ԭ��
        dots =new GameObject[numberOfdots];
        for (int i = 0; i < numberOfdots; i++)
        {
            dots[i] = Instantiate(dotPrefab, PlayerManger.instance.player.transform.position,Quaternion.identity,dotsParent);
            
            dots[i].SetActive(false);
        }
    }
    private Vector2 DotsPosition(float t)
    {
       //ʹ��vt+1/2gt*t 
       Vector2 position = (Vector2) PlayerManger.instance.player.transform.position + new Vector2(AimDiretion().normalized.x * lanchForce.x, 
           AimDiretion().normalized.y * lanchForce.y)* t + .5f*  (Physics2D.gravity * swordGravity)*t*t;
        return position;
    }
    #endregion
}
