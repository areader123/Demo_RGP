using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
     private SaveManager instance;
     [SerializeField]private string fileName;

     private GameData gameData;
     public List<ISaveManager> saveManagers;

     private FileDataHandler fileDataHandler;


     private void Start() {
         
     }

     private void Awake() 
     {
          fileDataHandler = new FileDataHandler(Application.persistentDataPath,fileName);
          saveManagers = FindAllSaveManagers();
          LoadGame();
          if( instance != null)
          {
               Destroy(instance.gameObject);
          }
          else
          {
               instance = this;
          }
     }

     private void NewGame()
     {
          gameData = new GameData();
     }

     private void LoadGame()
     {
          gameData = fileDataHandler.Load();
          if(gameData == null)
          {
               //no gameData
               NewGame();
          }
          foreach(var saveManager in saveManagers) 
          {
               saveManager.LoadData(gameData);
          }
          //Debug.Log("Load Currency" + gameData.currency);
     }

     private void SaveGame()
     {
          foreach(var saveManager in saveManagers) 
          {
               saveManager.SaveData(ref gameData);
          }
          fileDataHandler.Save(gameData);
          //Debug.Log("Load Currency" + gameData.currency);
     }

     private void OnApplicationQuit() 
     {
          SaveGame();
     }

     private List<ISaveManager> FindAllSaveManagers()
     {
          IEnumerable<ISaveManager> saveManagers = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>();
          return new List<ISaveManager>(saveManagers);
     }

}