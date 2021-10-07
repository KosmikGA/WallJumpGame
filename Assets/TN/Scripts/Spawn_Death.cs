using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Death : MonoBehaviour
{
    public GameObject spikePrefab = null;
    public GameObject player = null;


    public int spikesToSpawn = 3;




    Queue<GameObject> spikeColection = new Queue<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
   
    }

    public void spawnSpike()
    {
        for (int i = 0; i < spikesToSpawn; i++)
        {
            GameObject a = Instantiate(spikePrefab);
            int value = Random.Range(-1, 1);
            if (value >= 0)
            {
                Debug.Log("spawn right");

                a.transform.position = new Vector2(2.5f, 0);
            }
            else
            {
                Debug.Log("spawn left");
                a.transform.position = new Vector2(-2.5f, 0);
            }

            spikeColection.Enqueue(a);
        }
    }   


    // Update is called once per frame
    void Update()
    {
        
    }
}
