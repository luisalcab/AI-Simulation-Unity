using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class TLManager : MonoBehaviour
{

    public static TLManager Instance {
        get;
        private set;
    }

    [SerializeField]
    private GameObject[] _trafficLights;

    void Awake()
    {
        if(Instance != null){
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void ActivarSemaforo(ListaCarro datos) {
        StartCoroutine(Semaforo(datos.steps, datos));
    }

    IEnumerator Semaforo(int steps, ListaCarro datos) {
        for (int i = 0; i < steps; i++)
        {
            for(int j = 0; j < _trafficLights.Length; j++){
                _trafficLights[j].GetComponent<LightsManager>().setState(datos.step[i].trafficLights[j].state);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
