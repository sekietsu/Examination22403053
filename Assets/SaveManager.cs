using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static Player;
using UnityEngine.SocialPlatforms.Impl;

public class SaveManager : MonoBehaviour
{
    public int score = 0;
    private string filepath;
    private GameSaveData saveData;
    void SaveScore()
    {
        saveData = new GameSaveData(score);
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(filepath, json);
    }

    void LoadData()
    {
        if (File.Exists(filepath))
        {
            string json = File.ReadAllText(filepath);
            saveData = JsonUtility.FromJson<GameSaveData>(json);
            score = saveData.Score;
        }
        else
        {
            saveData = new GameSaveData(score);
        }
    }



}
