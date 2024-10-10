using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public class FileDataHandler 
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public FileDataHandler(string _dataDirPath,string _dataFileName)
    {
        dataDirPath = _dataDirPath;
        dataFileName = _dataFileName; 
    }

    public void Save(GameData _data)
    {
        string fullpath = Path.Combine(dataDirPath,dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullpath));
            string dataToStore = JsonUtility.ToJson(_data,true);
            using(FileStream stream = new FileStream(fullpath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }

        catch (Exception ex)
        {
            Debug.Log("Error on trying to save data to file" + fullpath + "\n" + ex.Message);
        }
    }
    public GameData Load()
    {
        string fullpath = Path.Combine(dataDirPath,dataFileName);
        GameData loadData = null;

        if(File.Exists(fullpath))
        {
            try
            {
                string dataLoad = "";
                using(FileStream stream = new FileStream(fullpath,FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        dataLoad = reader.ReadToEnd();
                    }
                }

                loadData = JsonUtility.FromJson<GameData>(dataLoad);
            }
             catch (Exception ex)
            {
                Debug.Log("Error on trying to read file to data" + fullpath + "\n" + ex.Message);
            }

        } 
        return loadData;
    }
}
