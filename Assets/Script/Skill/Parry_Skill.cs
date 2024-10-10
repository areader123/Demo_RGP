using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parry_Skill : SKill
{
    [Header("Parry")]
    [SerializeField]private UI_Skill_Slot parryUnlockButton;
    public bool parryUnlock;
    [Header("Parry restore")]
    [SerializeField]private UI_Skill_Slot restoreUnlockButton;
    public bool restoreUnlock;
    [SerializeField]private float restoreToHealthPercent;
    [Header("Parry with mirage")]
    [SerializeField]private UI_Skill_Slot ParryWithmirageUnlockButton;
    public bool ParryWithmirageUnlock;
    


    public override void UseSkill()
    {
        base.UseSkill();

        if(restoreUnlock)
        {
            int restoreHealth =Mathf.RoundToInt(PlayerManger.instance.player.GetComponent<Player_Stat>().GetMaxHealth() * restoreToHealthPercent / 100);
            PlayerManger.instance.player.GetComponent<Player_Stat>().IncreaseHealthOnly(restoreHealth);
        }
    }

    protected override void CheckUnlock()
    {
        UnlockParry();
        Unlockrestore();
        UnlockParryWithMirage();
    }


    protected override void Start()
    {
        base.Start();
         parryUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParry);
         restoreUnlockButton.GetComponent<Button>().onClick.AddListener(Unlockrestore);
         ParryWithmirageUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParryWithMirage);
    }

    public void UnlockParry()
    {
        if(parryUnlockButton.unLock)
        {
             parryUnlock = true;
        }
    }
      public void Unlockrestore()
    {
        if(restoreUnlockButton.unLock)
        {
             restoreUnlock = true;
        }
    }
    public void UnlockParryWithMirage()
    {
        if(ParryWithmirageUnlockButton.unLock)
        {
             ParryWithmirageUnlock = true;
        }
    }


    public void MakeMirageOnParry(Transform _enemyTransform)
    {
        if(ParryWithmirageUnlock)
        {
            SkillManger.Instance.clone.CreateCloneWithDelay(_enemyTransform);
        }
    }
}
