using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class LightsManager : MonoBehaviour
{

    private GameObject[] _lights;

    void Awake()
    {
        // Get child objects
        _lights = new GameObject[transform.childCount - 1];
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            _lights[i] = transform.GetChild(i+1).gameObject;
        }
        
        // Set initial state
        setState(5);
    }

    void Update()
    {
        
    }

    public void setState(int state)
    {
        if (state == 2){
            _lights[0].SetActive(false);
            _lights[1].SetActive(false);
            _lights[2].SetActive(true);
        }
        else if (state == 3){
            _lights[0].SetActive(false);
            _lights[1].SetActive(true);
            _lights[2].SetActive(false);
        }
        else if (state == 4){
            _lights[0].SetActive(true);
            _lights[1].SetActive(false);
            _lights[2].SetActive(false);
        }
        else if (state == 5){
            _lights[0].SetActive(false);
            _lights[1].SetActive(false);
            _lights[2].SetActive(false);
        }
        else {
            Debug.Log("Error");
        }
    }
}
