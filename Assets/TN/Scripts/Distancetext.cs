using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Distancetext : MonoBehaviour
{
    public Text scoreText;

    public float totalDistance = -7;
    public bool record = true;
    private Vector3 previousLoc;
 
    
    
    void FixedUpdate()
    {
        if (record)
            RecordDistance();
    }
    void RecordDistance()
    {
        totalDistance += Vector3.Distance(transform.position, previousLoc);
        previousLoc = transform.position;
        scoreText.text = totalDistance.ToString();
    }
    void ToggleRecord() => record = !record;



}
