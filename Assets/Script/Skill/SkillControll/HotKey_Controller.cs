using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HotKey_Controller : MonoBehaviour
{
    private SpriteRenderer sr;
    private KeyCode myHotKey;
    private TextMeshProUGUI myText;

    private Transform myEnemy;
    private BlackHole_Skill_Controll blackHole;

    public void SetupHotKey(KeyCode _myNewHotKey, Transform _myEnemy, BlackHole_Skill_Controll _myBlackHole)
    {
        sr = GetComponent<SpriteRenderer>();
        myText = GetComponentInChildren<TextMeshProUGUI>();

        myEnemy = _myEnemy;
        blackHole = _myBlackHole;

        myHotKey = _myNewHotKey;
        myText.text = _myNewHotKey.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(myHotKey))
        {
            blackHole.AddEnemyToList(myEnemy);
           
            myText.color = Color.clear;
            sr.color = Color.clear;
        }
    }
}
