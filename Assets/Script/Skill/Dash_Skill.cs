using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dash_Skill : SKill
{
    [Header("Clone Dash Skill")]
    public bool dashUnlocked ;
    [SerializeField]private UI_Skill_Slot dashUnlockButton;
    [Header("Clone Dash Skill")]
    public bool CloneDashUnlock ;
    [SerializeField]private UI_Skill_Slot CloneDashUnlockButton;
    [Header("Clone On Arrive")]
        public bool CloneOnArriveUnlock ;
    [SerializeField]private UI_Skill_Slot CloneOnArriveUnlockButton;

    public override void UseSkill()
    {
         base.UseSkill();
    }

    protected override void Start()
    {
        dashUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockDash);
        CloneDashUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCloneDash);
        CloneOnArriveUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCloneOnArrive);
        base.Start();
    }

    protected override void CheckUnlock()
    {
        UnlockDash();
        UnlockCloneDash();
        UnlockCloneOnArrive();
    }

    public void UnlockDash()
    {
        Debug.Log("尝试");
        if(dashUnlockButton.unLock)
        {
            Debug.Log("成功");
            dashUnlocked = true;
        }
    }
    
    public void UnlockCloneDash()
    {
        if(CloneDashUnlockButton.unLock)
        {
            CloneDashUnlock = true;
        }
    }
     public void UnlockCloneOnArrive()
    {
        if(CloneOnArriveUnlockButton.unLock)
        {
            CloneOnArriveUnlock = true;
        }
    }

    public void CloneOnDash()
    {
        if(CloneDashUnlock)
        {
            SkillManger.Instance.clone.CreatClone(SkillManger.Instance.player.transform,Vector3.zero);
        }
    }
        public void CloneOnArrival()
    {
        if(CloneDashUnlock)
        {
            SkillManger.Instance.clone.CreatClone(SkillManger.Instance.player.transform,Vector3.zero);
        }
    }
}

