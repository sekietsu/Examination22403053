using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square3Access : MonoBehaviour
{
    private MongoClient client;
    private IMongoDatabase database;
    private IMongoCollection<BsonDocument> collection;


    private Transform targetObject0;
    private Transform targetObject1;
    private Transform targetObject2;

    private Vector3 newPosition;


    private float timer = 0.0f;
    private float interval = 0.1f;




    // Start is called before the first frame update
    void Start()
    {
        client = new MongoClient("mongodb+srv://tateno:ae21215926@cluster0.qryer.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0");
        database = client.GetDatabase("unity_test");
        collection = database.GetCollection<BsonDocument>("unity_test0");
        targetObject0 = GameObject.Find("Square3").transform;
        targetObject1 = GameObject.Find("Square").transform;


        var result = collection.Find("{playerid:37}").FirstOrDefault();
        var resultx = result["x"];
        var resulty = result["y"];
        double resultx0 = resultx.AsDouble;
        double resulty0 = resulty.AsDouble;

        newPosition.x = (float)resultx0;
        newPosition.y = (float)resulty0;
        targetObject1.position = newPosition;

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            //var result = collection.Find("{playerid:0}").FirstOrDefault();
            //var resultx = result["x"];
            //var resulty = result["y"];
            //double resultx0 = resultx.AsDouble;
            //double resulty0 = resulty.AsDouble;

            //newPosition.x = (float)resultx0;
            //newPosition.y = (float)resulty0;
            //targetObject0.position  = newPosition;





            float x = targetObject1.position.x;
            float y = targetObject1.position.y;


     

            var filter = Builders<BsonDocument>.Filter.Eq("playerid", 37);
            var updatex = Builders<BsonDocument>.Update.Set("x", x);
            var updatey = Builders<BsonDocument>.Update.Set("y", y);
            collection.UpdateOne(filter, updatex);
            collection.UpdateOne(filter, updatey);

            var result37 = collection.Find("{playerid:37}").FirstOrDefault();
            Debug.Log(result37);


            timer = 0.0f;

        }
    }




}
