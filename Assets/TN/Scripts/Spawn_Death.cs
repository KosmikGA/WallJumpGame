using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Death : MonoBehaviour
{
    public GameObject spikePrefab = null;
    public GameObject player = null;
    public GameObject wallPrefab = null;


    public int spikesToSpawn = 3;

    public float spawnHeightOffset = 1f;
    public float wallHeightOffset = 1f;


    Queue<GameObject> spikeColection = new Queue<GameObject>();
    Queue<GameObject> wallColection = new Queue<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
   
    }

    public void spawnSpike()
    {
        float offset = 0;

        for (int i = 0; i < spikesToSpawn; i++)
        {
            GameObject a = Instantiate(spikePrefab);
            int value = Random.Range(-1, 1);
            if (value >= 0)
            {
                Debug.Log("spawn right");

                a.transform.position = new Vector2(2.5f, player.transform.position.y + spawnHeightOffset + offset);
            }
            else
            {
                Debug.Log("spawn left");
                a.transform.position = new Vector2(-2.5f, player.transform.position.y + spawnHeightOffset + offset);
            }
            offset += 4;
            spikeColection.Enqueue(a);
        }
    }   

    public void spawnWall(GameObject spawnPos)
    {
        GameObject b = Instantiate(wallPrefab);
        b.transform.position = new Vector2(0, spawnPos.transform.position.y + wallHeightOffset);
        wallColection.Enqueue(b);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
