using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using System;
using SharpCompress.Common;
using static Player;
using UnityEngine.SocialPlatforms.Impl;
using System.IO;

public class Access : MonoBehaviour
{
    private MongoClient client;
    private IMongoDatabase database;
    private IMongoCollection<BsonDocument> collection;


    //private GameObject player;
    private float timer = 0.0f;
    private float interval = 0.1f;

    private string filepath;
    private GameSaveData saveData;

    // Start is called before the first frame update
    void Start()
    {
        filepath = Path.Combine(Application.dataPath, "data.json");
        client = new MongoClient("mongodb+srv://game22403053:opWhju30bT1LcQVe@cluster0.vdtfr.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0");
        database = client.GetDatabase("unity_test");
        collection = database.GetCollection<BsonDocument>("unity_test1");
        //player = GameObject.Find("Player"); 


    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            LoadData();
            var result = collection.Find("{player_id:0}").FirstOrDefault();
            Debug.Log(result);

            var filter = Builders<BsonDocument>.Filter.Eq("player_id", 0);
            var updateScore = Builders<BsonDocument>.Update.Set("player_score", saveData.Score);
            collection.UpdateOne(filter, updateScore);

            timer = 0.0f;

        }

    }


    void LoadData()
    {
        if (File.Exists(filepath))
        {
            string json = File.ReadAllText(filepath);
            saveData = JsonUtility.FromJson<GameSaveData>(json);
        }
        else
        {
            saveData = new GameSaveData(0);
        }
    }

}
