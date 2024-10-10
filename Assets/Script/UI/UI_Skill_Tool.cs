using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Skill_Tool : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI SkillDescription;
    [SerializeField]private TextMeshProUGUI SkillName;
 
    public void ShowSkillTip(string _text,string _name)
    {
        SkillDescription.text = _text;
        SkillName.text = _name;
        gameObject.SetActive(true);
    }

    public void HideSkillTip()
    {
        gameObject.SetActive(false);
    }
}
