using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Health_Bar : MonoBehaviour
{
    private Entity entity;
    private Transform myTransform;
    private Slider slider;

    private Character_Stat myStat;
    // Start is called before the first frame update
    void Start()
    {
        entity = GetComponentInParent<Entity>();
        myTransform = GetComponent<Transform>();
        myStat = GetComponentInParent<Character_Stat>();
        slider = GetComponentInChildren<Slider>();

        entity.OnFlipped += FlipUI;
        myStat.OnHealthChange += UpdataHealthBar;
    }

    private void UpdataHealthBar(){
        slider.maxValue = myStat.GetMaxHealth();
        slider.value = myStat._currentHP;
    }

    public void FlipUI(){
        myTransform.Rotate(0,180,0);
    }

    private void OnDisable() {
        entity.OnFlipped -= FlipUI;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
