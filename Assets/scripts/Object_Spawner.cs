using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Spawner : MonoBehaviour
{
    public GameObject player;
    public GameObject obstacle;
    public GameObject[] smallObstacles;
    public GameObject[] obstacleArray;
    public float soSpawnTimer = 3.0f;
    public int obstacleCount;
    public int obstacleDist;

    

    private void Start()
    {

        SpawnSO();
       

    }

    // Update is called once per frame
    void Update()
    {
        soSpawnTimer -= Time.deltaTime;

        if (soSpawnTimer < 0.01)
        {
            
        }
    }

    void SpawnSO()
    {
        int j = obstacleDist;
        for (int i = 0; i < obstacleCount; i++)
        {
            
                Instantiate(smallObstacles[(Random.Range(0, smallObstacles.Length))], new Vector3(Random.Range(-80f, 80f), -150, j), Quaternion.identity);
            //soSpawnTimer = Random.Range(1.0f, 3.0f);
            j = j + obstacleDist;
        }


    }
}
