using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dodge_Skill : SKill
{
   [Header("Dodge")]
   [SerializeField]private UI_Skill_Slot unlockDodgeButton;
   [SerializeField]private int evasionAmount;
   public bool dodgeUnlocked;
   [Header("Mirage dodge")]
   [SerializeField]private UI_Skill_Slot unlockMirageDodge;
   public bool mirageUnlocked;


    protected override void Start()
    {
        unlockDodgeButton.GetComponent<Button>().onClick.AddListener(UnlockDodge);
        unlockMirageDodge.GetComponent<Button>().onClick.AddListener(UnlockMirageDodge);
        base.Start();
    }

    // protected override void CheckUnlock()
    // {
    //     UnlockDodge();
    //     UnlockMirageDodge();
    // }

    private void UnlockDodge()
    {
        if(unlockDodgeButton.unLock)
        {
            player.GetComponent<Player_Stat>().evasion.AddModifiers(evasionAmount);
            Inventor.instance.UpdateUIStatSlot();
            dodgeUnlocked = true;
        }
    }

    private void UnlockMirageDodge()
    {
        if(unlockMirageDodge.unLock)
            mirageUnlocked = true;
    }

    public void CreateMirageOnDodge()
    {
        if(mirageUnlocked)
        {
            Debug.Log("创造");
            SkillManger.Instance.clone.CreatClone(PlayerManger.instance.player.transform,new Vector3((float)(player.faceDir * 1.5), 0));
        }
    }

}
