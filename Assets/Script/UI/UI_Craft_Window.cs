using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Craft_Window : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private Image itemIcon;

    [SerializeField] private Image[] materialImage;

    [SerializeField] private Button craftButton;



    public void SetUpCraftWindow(Item_Equipment _data)
    {
        craftButton.onClick.RemoveAllListeners();

        for (int i = 0; i < materialImage.Length; i++)
        {
            materialImage[i].color = Color.clear;
            materialImage[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.clear;
        }
        for (int j = 0; j < _data.craftingMaterial.Count; j++)
        {
            materialImage[j].sprite = _data.craftingMaterial[j].data.icon;
            materialImage[j].color = Color.white;
            materialImage[j].GetComponentInChildren<TextMeshProUGUI>().text = _data.craftingMaterial[j].stackSize.ToString();
            materialImage[j].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        }

        itemName.text = _data.ItemName;
        itemIcon.sprite = _data.icon;
        itemDescription.text = _data.GetDescription();

        craftButton.onClick.AddListener(() => Inventor.instance.CanCraft(_data, _data.craftingMaterial));
        //craftButton.onClick.AddListener(Inventor.instance.CanCraft(_data,_data.craftingMaterial));
    }
}
