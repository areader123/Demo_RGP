using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("Flash FX")]
    [SerializeField] private Material hitMat;
    private Material originalMat;
    [SerializeField] private float flashDuration;

    [SerializeField] private Color[] chillColor;
    [SerializeField] private Color[] igniteColor;
    [SerializeField] private Color[] lightingColor;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMat = sr.material;
    }
    //�ӳ���Ч
    private IEnumerator FlashFX()
    {
        sr.material = hitMat;
        Color currentColor = sr.color;
        sr.color = Color.white;
        yield return new WaitForSeconds(flashDuration);
        sr.color = currentColor;
        sr.material = originalMat;
    }
    private void RedColorBlink()
    {
        if(sr.color != Color.white) 
        {
            sr.color = Color.white;
        }else
            sr.color = Color.red;
    }
    public void CancleColorChange()
    {
        CancelInvoke();
        sr.color = Color.white;
    }
    
    public void IgniteFXFor(float _second){
        InvokeRepeating("IgniteColorFX",0,.3f);
        Invoke("CancleColorChange",_second);
    }

    private void IgniteColorFX(){
        if(sr.color != igniteColor[0])
        {
            sr.color = igniteColor[0];
        }else
        {
            sr.color = igniteColor[1];
        }
    }

    public void ChillFXFor(float _second){
        InvokeRepeating("ChillColorFX",0,.3f);
        Invoke("CancleColorChange",_second);
    }

    private void ChillColorFX()
    {
         if(sr.color != chillColor[0])
        {
            sr.color = chillColor[0];
        }else
        {
            sr.color = chillColor[1];
        }
    }

    public void LightingFXFor(float _second){
        InvokeRepeating("LightingColorFX",0,.3f);
        Invoke("CancleColorChange",_second);
    }

    private void LightingColorFX(){
        if(sr.color != lightingColor[0])
        {
            sr.color = lightingColor[0];
        }else
        {
            sr.color = lightingColor[1];
        }
    }
    
}
