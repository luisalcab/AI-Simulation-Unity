using System.Diagnostics;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Debug = UnityEngine.Debug;
using UnityEngine.Events;
using System;
using SimpleJSON;

[Serializable]
public class RequestPositions : UnityEvent<ListaCarro> {}

public class RequestManager : MonoBehaviour
{

    [SerializeField]
    private RequestPositions _requestPositions;
    private IEnumerator _enumeratorCorrutina;
    private Coroutine _corrutina;
    [SerializeField]
    private string _url = "https://server-traffic.herokuapp.com";

    void Start(){
        _enumeratorCorrutina = Request(); 
        _corrutina = StartCoroutine(_enumeratorCorrutina);
    }

    IEnumerator Request() {
        UnityWebRequest www = UnityWebRequest.Get(_url + "/" + CarPoolManager.Instance._tamanioPool + "&[0,1,2,3]");

        yield return www.SendWebRequest();

        ListaCarro listaCarro = new ListaCarro();

        if(www.result != UnityWebRequest.Result.Success){
            Debug.LogError(www.error);
        } else {

            // Parsear la respuesta
            JSONNode json = JSON.Parse(www.downloadHandler.text);

            // Asignar el valor de totalCars
            listaCarro.totalCars = json[0].AsInt;
            listaCarro.steps = json[1][0].Count;

            listaCarro.step = new Step[listaCarro.steps];

            for (int i = 0; i < listaCarro.steps; i++) {
                listaCarro.step[i] = new Step();
                listaCarro.step[i].cars = new Carro[listaCarro.totalCars];
                for (int j = 0; j < listaCarro.totalCars; j++) {
                    listaCarro.step[i].cars[j] = new Carro();
                    listaCarro.step[i].cars[j].x = json[2][j][i][1].AsInt;
                    listaCarro.step[i].cars[j].y = json[2][j][i][0].AsInt;
                    listaCarro.step[i].cars[j].activated = json[2][j][i][2].AsInt;
                }
                listaCarro.step[i].trafficLights = new TrafficLight[json[1].Count];
                for (int j = 0; j < json[1].Count; j++) {
                    listaCarro.step[i].trafficLights[j] = new TrafficLight();
                    listaCarro.step[i].trafficLights[j].state = json[1][j][i][0].AsInt;
                }
            }

            _requestPositions?.Invoke(listaCarro);
        }
    }
}