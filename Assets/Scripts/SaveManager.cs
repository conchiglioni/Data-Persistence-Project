using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    public string playerName;
    public string highScoreName;
    public int highScore;
    public int score;

    private TMP_InputField nameInputField;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadHighScore();
            nameInputField = GameObject.Find("Name Input").GetComponent<TMP_InputField>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateName()
    {
        playerName = nameInputField.text;
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScore = data.highScore;
            highScoreName = data.highScoreName;
        }
    }

    public void UpdateHighScore(int score)
    {
        if(score > highScore)
        {
            highScore = score;
            highScoreName = playerName;
            
            SaveData data = new SaveData();
            data.highScore = highScore;
            data.highScoreName = highScoreName;

            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }
    }

    public void SwitchScene()
    {
        SceneManager.LoadScene(1);
    }

    [Serializable]
    class SaveData
    {
        public string highScoreName;
        public int highScore;
    }
}
