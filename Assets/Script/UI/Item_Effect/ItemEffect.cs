using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



//[CreateAssetMenu(fileName = "New Item Effect", menuName = "Data/Item effect")]
public  class ItemEffect : ScriptableObject
{
   public virtual void ExcuteEffect(Transform _Transform)
   {
    Debug.Log("Item effect");
   }

}
