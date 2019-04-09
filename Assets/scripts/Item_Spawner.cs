
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Spawner : MonoBehaviour
{
    public GameObject player;
    public GameObject[] items;
    private float itemSpawnTimer = 7.0f;
    public int itemDistance;
    public int itemCount;


    private void Start()
    {

        SpawnSO();


    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void SpawnSO()
    {
        int j = itemDistance;
        for (int i = 0; i < itemCount; i++)
        {

            Instantiate(items[(Random.Range(0, items.Length))], new Vector3(Random.Range(-70f, 70f), 0, j), Quaternion.identity);
            //soSpawnTimer = Random.Range(1.0f, 3.0f);
            j = j + itemDistance;
        }


    }
}
