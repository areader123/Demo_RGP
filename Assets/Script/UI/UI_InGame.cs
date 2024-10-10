using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Player_Stat player_Stat;

    [SerializeField] private Image dashCoolDownImage;
    private float dashCoolDown;
    [SerializeField] private Image parryCoolDownImage;
    private float parryCoolDown;

    [SerializeField] private Image crystalCoolDownImage;
    private float crystalCoolDown;

    [SerializeField] private Image swordCoolDownImage;
    private float swordCoolDown;
    [SerializeField] private Image blackholeCoolDownImage;
    private float blackholeCoolDown;

    [SerializeField] private Image flaskCoolDownImage;
    private float flaskCoolDown;

    [SerializeField] private TextMeshProUGUI currencyText;


    private int currency;
    private SkillManger skillManger;

    // private void Awake() 
    // {
    //     player_Stat = PlayerManger.instance.player.GetComponent<Player_Stat>();
    // }

    void Start()
    {


        if (player_Stat != null)
        {
            player_Stat.OnHealthChange += UpdataHealthBar;
        }
        skillManger = SkillManger.Instance;
        dashCoolDown = skillManger.dash.cooldown;
        parryCoolDown = skillManger.parry_Skill.cooldown;
        crystalCoolDown = skillManger.crystal_Skill.cooldown;
        swordCoolDown = skillManger.throwSword.cooldown;
        blackholeCoolDown = skillManger.blackHole.cooldown;
    }

    // Update is called once per frame
    void Update()
    {

        currencyText.text = PlayerManger.instance.GetCurrency().ToString("#,#");

        if (Input.GetKeyDown(KeyCode.LeftShift) && skillManger.dash.dashUnlocked)
        {
            SetCoolDown(dashCoolDownImage);
        }
        if (Input.GetKeyDown(KeyCode.Q) && skillManger.parry_Skill.parryUnlock)
        {
            SetCoolDown(parryCoolDownImage);
        }
        if (Input.GetKeyDown(KeyCode.F) && skillManger.crystal_Skill.crystalUnlock)
        {
            SetCoolDown(crystalCoolDownImage);
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            SetCoolDown(swordCoolDownImage);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            SetCoolDown(blackholeCoolDownImage);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && Inventor.instance.GetSingleEquipment(Equipment.Flask) != null)
        {
            SetCoolDown(flaskCoolDownImage);
        }

        CheckCoolDownOf(dashCoolDownImage, dashCoolDown);
        CheckCoolDownOf(parryCoolDownImage, parryCoolDown);
        CheckCoolDownOf(crystalCoolDownImage, crystalCoolDown);
        CheckCoolDownOf(swordCoolDownImage, swordCoolDown);
        CheckCoolDownOf(blackholeCoolDownImage, blackholeCoolDown);
        CheckCoolDownOf(flaskCoolDownImage, Inventor.instance.flaskCoolDown);
    }

    private void UpdataHealthBar()
    {
        slider.maxValue = player_Stat.GetMaxHealth();
        slider.value = player_Stat._currentHP;
    }

    private void SetCoolDown(Image _image)
    {
        if (_image.fillAmount <= 0)
        {
            _image.fillAmount = 1;
        }
    }

    private void CheckCoolDownOf(Image _image, float _coolDown)
    {
        if (_image.fillAmount > 0)
        {
            _image.fillAmount -= 1 / _coolDown * Time.deltaTime;
        }
    }

}
