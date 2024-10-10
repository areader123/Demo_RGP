using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManger : MonoBehaviour
{
    //�˴�������м���
    // ���켼�ܶ���� ��Ӧ�ű�����SkillManger��
   public static SkillManger Instance;
    public ThrowSword_Skill throwSword { get; private set; }    
    public Dash_Skill dash { get; private set; }
    public Clone_Skill clone { get; private set; }  

    public BalckHole_Skill blackHole{ get; private set; }  

    public Crystal_Skill crystal_Skill{ get; private set; }

    public Parry_Skill parry_Skill{ get; private set; }

    public Dodge_Skill dodge_Skill{ get; private set; }

    public Player player;
    private void Awake()
    {
        if(Instance != null) 
        {
           Destroy(Instance);
        }
        else 
        {
            Instance = this;
        }

    }
    private void Start()
    {
        dash = GetComponent<Dash_Skill>();
        clone = GetComponent<Clone_Skill>();
        throwSword = GetComponent<ThrowSword_Skill>();
        blackHole = GetComponent<BalckHole_Skill>();
        crystal_Skill = GetComponent<Crystal_Skill>();
        parry_Skill = GetComponent<Parry_Skill>();
        dodge_Skill = GetComponent<Dodge_Skill>();
    }
}
