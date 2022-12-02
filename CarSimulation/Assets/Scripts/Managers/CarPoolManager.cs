using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class CarPoolManager : MonoBehaviour
{

    public static CarPoolManager Instance {
        get;
        private set;
    }

    [SerializeField]
    private GameObject _carritoOriginal;

    [SerializeField]
    public int _tamanioPool;
    private Queue<GameObject> _pool;

    void Awake() {

        if(Instance != null){
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if(_carritoOriginal == null){
            throw new System.Exception("CAR POOL MANAGER: ES NECESARIO UN CARRO");
        }

        _pool = new Queue<GameObject>();

        // creamos objetos y agregamos al pool
        for(int i = 0; i < _tamanioPool; i++){

            // método para crear objetos
            GameObject actual = Instantiate<GameObject>(_carritoOriginal);
            actual.SetActive(false);
            _pool.Enqueue(actual);
        }
    }


    public GameObject Activar(Vector3 posicion, int id) {

        // evitamos error - no hay objetos en pool
        if(_pool.Count == 0){
            Debug.LogError("TE QUEDASTE SIN OBJETOS");
            return null;
        }

        // obtengo objeto de pool
        GameObject actual = _pool.Dequeue();

        actual.SetActive(true);
        actual.transform.position = posicion;
        actual.name = "Carrito " + id;
        
        return actual;
    }

    public void Desactivar(GameObject objetoADesactivar){
        if(CameraManager.Instance._camaras[CameraManager.Instance._camaras.Length - 1].gameObject.activeSelf){
            if (objetoADesactivar.name == CameraManager.Instance._carrito.name){
                CameraManager.Instance.IntercambiarCamara();
            }
        }
        objetoADesactivar.SetActive(false);
        _pool.Enqueue(objetoADesactivar);
    }
}
