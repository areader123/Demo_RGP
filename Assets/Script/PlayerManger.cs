using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class PlayerManger : MonoBehaviour,ISaveManager
{
    //�����ڸ����ط����ʵ�player
    public static PlayerManger instance;
    public Player player;

   

    public int currency;
   
    private void Awake()
    {
        //�����������ظ���instance
        //ֱ������gameobject ��Ҫʱ�ٴ��� 
        if(instance != null) 
        {
            Destroy(instance.gameObject);
        }
        else 
        {  
            instance = this;
        }
         
    }

    public bool HaveEnoughcurrnecy(int _price)
    {
        if(currency < _price)
        {
            return false;
        }else
        {
            currency -= _price;
            return true;
        }
    }
    
    public int GetCurrency() => currency;

    public void LoadData(GameData _data)
    {
       this.currency = _data.currency;
    }

    public void SaveData(ref GameData _data)
    {
        _data.currency = this.currency;
    }


}

