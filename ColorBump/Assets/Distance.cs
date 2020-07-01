using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Distance : MonoBehaviour
{

    Transform Player;
    float InitDistance;

    [SerializeField] Slider DistanceSlider;

    void Start()
    {

        Player = GameObject.Find("Player").transform;

        InitDistance = transform.position.z - Player.position.z;
    }

  
    void Update()
    {
        float currentDistance = transform.position.z - Player.position.z;

     
        DistanceSlider.value = (InitDistance - currentDistance) / InitDistance;
    }
}
