using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject map;

    void Start()
    {
        //map = GameObject.Find("Map");
        map.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            map.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            map.SetActive(false);
        }
    }
}
