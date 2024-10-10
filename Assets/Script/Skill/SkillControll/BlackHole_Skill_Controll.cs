using Autodesk.Fbx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole_Skill_Controll : MonoBehaviour
{
    [SerializeField] private GameObject hotKeyPrefab;
    [SerializeField] private List<KeyCode> keyCodeList;

    private float maxSize;
    private float growSpeed;
    private float shrinkSpeed;
    private bool cangrow = true;
    private bool canShrink;
    private bool CloneCanAttack;

    private bool canCreatHotKey = true;

    private bool canBeTransparent = true;

    public bool PlayerCanExitBlackHole {get ; private set;}



    private float amountOfAttack;
    private float cloneAttackCoolDown;
    private float cloneAttackTimer;

    private float skillDuration;
    private float skillDurationTimer;

    private List<Transform> targets = new List<Transform>();
    private List<GameObject> hotKeyTargets = new List<GameObject>();


    
    public void SetUp (float _maxsize,float _growSpeed,float _shrinkSpeed,float _amountOfAttack,float _cloneAttackCoolDown,float _skillDuration) {
        maxSize =_maxsize;
        growSpeed = _growSpeed;
        shrinkSpeed = _shrinkSpeed;
        amountOfAttack = _amountOfAttack;
        cloneAttackCoolDown = _cloneAttackCoolDown;
        skillDuration = _skillDuration;
    }


    private void Update()
    {
         
        CloneAttakLogic();
        Grow();
        Shrink();
    }

    private void Shrink()
    {
        if (canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(-1, -1), shrinkSpeed * Time.deltaTime);
            if (transform.localScale.x < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Grow()
    {
        if (cangrow && !canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);
        }
    }

    private void CloneAttakLogic()
    {
        cloneAttackTimer -= Time.deltaTime;
        skillDurationTimer += Time.deltaTime;
        CanUseSkillLogic();

        float xOffset;
        if (Random.Range(0, 100) > 50)
        {
            xOffset = 1;
        }
        else
        {
            xOffset = -1;
        }

        CreateCloneAttackLogic(xOffset);
    }

    private void CanUseSkillLogic()
    {
        if (Input.GetKey(KeyCode.M) || skillDurationTimer > skillDuration)
        {
            DestroyHotKey();
            CloneCanAttack = true;
            canCreatHotKey = false;
            if (canBeTransparent)
            {
                canBeTransparent = false;
                PlayerManger.instance.player.MakeTransparent(true);
            }
        }
    }

    private void CreateCloneAttackLogic(float xOffset)
    {
        if (CloneCanAttack && cloneAttackTimer < 0)
        {
            if (targets.Count <= 0)
            {
                FinishBlackHole();
                return;
            }
            int RandomInt = Random.Range(0, targets.Count);


            SkillManger.Instance.clone.CreatClone(targets[RandomInt], new Vector3(xOffset, 0, 0));

            amountOfAttack--;
            cloneAttackTimer = cloneAttackCoolDown;
            if (amountOfAttack <= 0)
            {
                Invoke("FinishBlackHole", 1f);
            }

        }
    }


    private void FinishBlackHole()
    {
        PlayerCanExitBlackHole = true;
        //PlayerManger.instance.player.ExitBlackHole();
        canShrink = true;
        CloneCanAttack = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Enemy>() != null) 
        {
            collision.GetComponent<Enemy>().FreezeTime(true);
            CreateHotKey(collision);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
         if(collision.GetComponent<Enemy>() != null){
            collision.GetComponent<Enemy>().FreezeTime(false);
         } 
    }

    private void DestroyHotKey () {
        if(hotKeyTargets.Count < 0){
            return;
        }
        for(int i = 0; i < hotKeyTargets.Count ; i++) {
            Destroy(hotKeyTargets[i]);
        }
    }

    private void CreateHotKey(Collider2D collision)
    {
        if (keyCodeList.Count <= 0)
        {
            Debug.LogWarning("Not enough hot keys in a key code list!");
            return;
        }
        if(!canCreatHotKey){
            return;
        }
        GameObject newHotKey = Instantiate(hotKeyPrefab, collision.transform.position + new Vector3(0, 2), Quaternion.identity);
        hotKeyTargets.Add(newHotKey);


        KeyCode choosenKey = keyCodeList[Random.Range(0, keyCodeList.Count)];
        keyCodeList.Remove(choosenKey);

        HotKey_Controller newHotKeyScript = newHotKey.GetComponent<HotKey_Controller>();

        newHotKeyScript.SetupHotKey(choosenKey, collision.transform, this);
    }

    public void AddEnemyToList(Transform _enemyTransform) => targets.Add(_enemyTransform);
}
