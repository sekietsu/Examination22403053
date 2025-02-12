using MongoDB.Bson.IO;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using System.IO;
using SharpCompress.Common;
using static SaveManager;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public int score=0;
    private string filepath;
    private GameSaveData saveData;
    int n =0;
    // Start is called before the first frame update
    void Start()
    {
        filepath = Path.Combine(Application.dataPath, "data.json");
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0.01f, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, -0.01f, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate( -0.01f,0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate( 0.01f,0, 0);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        }



    }

    public void AddScore()
    {
        score++;
        SaveScore();
        n++;
        if (n == 3)
        {
            SceneManager.LoadScene("GameClear");
        }
    }
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

    [System.Serializable]
    public class GameSaveData
    {
        public int Score;

        public GameSaveData(int currentScore)
        {
            Score = currentScore; 
        }
    }

}
