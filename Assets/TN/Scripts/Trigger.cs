using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    Spawn_Death Manager;



    // Start is called before the first frame update
    void Start()
    {
        Manager = GameObject.FindWithTag("GameController").GetComponent<Spawn_Death>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            Manager.spawnSpike();
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
