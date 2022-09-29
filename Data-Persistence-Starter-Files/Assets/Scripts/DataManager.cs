using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public string userName;
    public string highScoreName;
    public int highScore;

    public void Awake()
    {
        if (instance != null) 
        {            
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighScore();
    }

    class SaveData 
    {
        public string name;
        public int highScore;
    }

    public void SaveHighScore() 
    {
        SaveData save = new SaveData();
        save.highScore = highScore;
        save.name = highScoreName;

        string json = JsonUtility.ToJson(save);

        File.WriteAllText(Application.persistentDataPath + "/highScore.json", json);
    }

    public void LoadHighScore() 
    {
        string path = Application.persistentDataPath + "/highScore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData save = JsonUtility.FromJson<SaveData>(json);

            highScore = save.highScore;
            highScoreName = save.name;
        }
        else 
        {
            highScoreName = "";
            highScore = 0;
        }
    }

    private void OnApplicationQuit()
    {
        SaveHighScore();
    }
}
