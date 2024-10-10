using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Crystal_Skill : SKill
{
    [SerializeField]private GameObject ctystalPrefab;
    [SerializeField]private float skillDuration;

    [Header("Vrystal Bloom")]
    [SerializeField]private float growSpeed;
    
    private Crystal_Skill_Controller crystal_skill_controller_Script;
    private GameObject currentCrystal;


    [Header("Crystal simple")]
    [SerializeField]private UI_Skill_Slot unlockCrystalButton;
    public bool crystalUnlock;
    [Header("Crystal mirage")]//这个还没有做
    [SerializeField]private UI_Skill_Slot unlockCloneInsteadButton;
    [SerializeField]private bool cloneInsteadOfCrystal;
    [Header("Explorsive crystal")]
    [SerializeField]private UI_Skill_Slot unlockExplorsiveButton;
    [SerializeField]private bool canBoom;
    [Header("Moving crystal")]
    [SerializeField]private UI_Skill_Slot unlockMovingCrystalButton;
    [SerializeField]private bool canMove;
    [SerializeField]private float moveSpeed;
    
    [Header("Multi stacking crystal")]
    [SerializeField]private UI_Skill_Slot unlockMultiStackingCrystalButton;
    [SerializeField]private bool canUseMultyCrystal;



    [SerializeField]private List<GameObject> crystalList = new List<GameObject>();
    [SerializeField]private int amountOfCrystal;
    
    [SerializeField]private float multyCoolDown;
    [SerializeField]private float windowCoolDown;

    protected override void Start()
    {
        base.Start();
        unlockCrystalButton.GetComponent<Button>().onClick.AddListener( UnlockCrystal);
        unlockCloneInsteadButton.GetComponent<Button>().onClick.AddListener(UnlockCloneInstead);
        unlockExplorsiveButton.GetComponent<Button>().onClick.AddListener(UnlockExplorsive);
        unlockMovingCrystalButton.GetComponent<Button>().onClick.AddListener(UnlockMovingCrystal);
        unlockMultiStackingCrystalButton.GetComponent<Button>().onClick.AddListener(UnlockMultiStackingCrystal);
    }

    public void UnlockCrystal()
    {
        if(unlockCrystalButton.unLock)
        {
            crystalUnlock = true;
        }
    }
     public void UnlockCloneInstead(){
        
        if(unlockCrystalButton.unLock)
        {
           cloneInsteadOfCrystal = true;
        }
     }
      public void UnlockExplorsive(){
        
        if(unlockCrystalButton.unLock)
        {
            canBoom = true;
        }
      }
       public void UnlockMovingCrystal(){
        
        if(unlockCrystalButton.unLock)
        {
            canMove = true;
        }
       }
        public void UnlockMultiStackingCrystal(){
            
        if(unlockCrystalButton.unLock)
        {
            canUseMultyCrystal = true;
        }
        }




    public override void UseSkill()
    {
        base.UseSkill();
        if(CanUseMultyCrystalSkill())
        {
            return ;
        }
        if(currentCrystal == null)
        {
            currentCrystal = Instantiate(ctystalPrefab,PlayerManger.instance.player.transform.position,Quaternion.identity);
            crystal_skill_controller_Script = currentCrystal.GetComponent<Crystal_Skill_Controller>();
            crystal_skill_controller_Script.SetUp(skillDuration,canBoom,growSpeed,FindCloestEnemy(currentCrystal.transform),moveSpeed,canMove);
        }
        else
        {
            if(canMove == true){
                return;
            }
            TransformPlayerAndCrystal();
            currentCrystal.GetComponent<Crystal_Skill_Controller>()?.FinishCrystal();
        }
    }

    private void TransformPlayerAndCrystal()
    {
        Vector3 playerPos = PlayerManger.instance.player.transform.position;
        PlayerManger.instance.player.transform.position = currentCrystal.transform.position;
        currentCrystal.transform.position = playerPos;
    }

    public bool CanUseMultyCrystalSkill() {
        if(canUseMultyCrystal){
            if(crystalList.Count > 0)

                cooldown = 0;
                if(crystalList.Count < amountOfCrystal+1)
                {
                    Debug.Log("");
                    Invoke("CloseSkillWindow",windowCoolDown);
                }
                GameObject crystalToSpan = crystalList[crystalList.Count-1];
                GameObject newCrystal = Instantiate(crystalToSpan,PlayerManger.instance.player.transform.position,Quaternion.identity);
                crystalList.Remove(crystalToSpan);

                crystal_skill_controller_Script = newCrystal.GetComponent<Crystal_Skill_Controller>();
                crystal_skill_controller_Script.SetUp(skillDuration,canBoom,growSpeed,FindCloestEnemy(newCrystal.transform),moveSpeed,canMove);

                if(crystalList.Count <= 0){
                    cooldown = multyCoolDown;
                    cooldowmTImer = cooldown;
                    RefillCryStal();
                }
            return true;
        }
        return false;
    }
    private void CloseSkillWindow () {
        Debug.Log("进入窗口");
        cooldown = multyCoolDown;
        cooldowmTImer = cooldown;
        RefillCryStal();
    }
    private void RefillCryStal(){
        int amountOfAdd = amountOfCrystal - crystalList.Count;
        for(int i = 0; i <amountOfAdd ; i++) {
            
        crystalList.Add(ctystalPrefab);
        }
    }
}
