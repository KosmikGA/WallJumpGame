using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Distancetext : MonoBehaviour
{
    public Text scoreText;

    public float totalDistance = 0;
    public bool record = true;
    private Vector3 previousLoc;

    private int roundedDistance = 0;


    private void Start()
    {
        previousLoc = transform.position;
    }





    void FixedUpdate()
    {
        if (record)
            RecordDistance();
    }
    void RecordDistance()
    {
        totalDistance += Vector3.Distance(transform.position, previousLoc);
        previousLoc = transform.position;
        roundedDistance = (int)totalDistance;
        scoreText.text = roundedDistance.ToString();
    }
    void ToggleRecord() => record = !record;



}
