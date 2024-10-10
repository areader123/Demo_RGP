using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTrigger : MonoBehaviour
{
   private Player player;
   private Player_Stat player_Stat;
   private void Awake() {
        player = GetComponentInParent<Player>();
        player_Stat = player.GetComponentInParent<Player_Stat>();
   }
    private void AnimationTrigger() //trigger������playerstate�� Ϊ����ĳһ֡�Ĵ������� ���ǽ���������д��playerstate�� Ȼ����statemachine�е�currentstate��¶��player�� ���¶��animation��
    {
        player.AnimationTrigger();
    }
    
    private void AttackTrigger()//�ж��Ƿ������ײ ����ײ���Ƿ�Ϊ���� ���� ����õ��˵�Damage����
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach(var hit in colliders)
        {
            if(hit.GetComponent<Enemy>() != null)
            {
                //player_Stat.DoDamage(hit.GetComponent<Enemy_Stat>());
                hit.GetComponent<Enemy_Stat>().DoDamage(player_Stat);
                if(Inventor.instance.GetSingleEquipment(Equipment.Weapon) != null){
                    Inventor.instance.GetSingleEquipment(Equipment.Weapon).Effect(hit.GetComponent<Enemy_Stat>().transform);
                }
               
                //hit.GetComponent<Enemy>().Damage();
            }
        }
    }
    private void SwordTrigger()
    {
        //�ڷ��佣��ĳһ֡�� ����ʵ����
        SkillManger.Instance.throwSword.CreatSword(player.transform);
    }
}
