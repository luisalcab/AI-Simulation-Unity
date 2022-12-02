using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class CarDataManager : MonoBehaviour
{

    private Carro[] _listaDeCarros;
    private GameObject[] _carrosGO;
    private Vector3[] _direcciones;

    [SerializeField]
    private CarroSO[] _carritosScriptableObjects;

    void Start()
    {
        _listaDeCarros = new Carro[CarPoolManager.Instance._tamanioPool];

        for (int i = 0; i < _listaDeCarros.Length; i++)
        {
            _listaDeCarros[i] = new Carro();
            _listaDeCarros[i].x = 0;
            _listaDeCarros[i].y = 0;
            _listaDeCarros[i].activated = 0;
        }

        _carrosGO = new GameObject[_listaDeCarros.Length];

        for(int i = 0; i < _listaDeCarros.Length; i++)
        {
            _carrosGO[i] = CarPoolManager.Instance.Activar(new Vector3(0, 0, 0), i);
            // actualizar scriptable object de carrito random
            _carrosGO[i].GetComponent<CarritoBuilder>().ActualizarCarrito(_carritosScriptableObjects[Random.Range(0, _carritosScriptableObjects.Length)]);
        }
    }

    void Update()
    {
        if(_direcciones != null){
            for (int i = 0; i < _carrosGO.Length; i++)
            {
                // Rotar carrito
                if (_direcciones[i].normalized != Vector3.zero)
                {
                    _carrosGO[i].transform.rotation = Quaternion.LookRotation(_direcciones[i].normalized);
                }

                // Mover carrito
                _carrosGO[i].transform.position += _direcciones[i] * Time.deltaTime * 200;
            }
        }
    }

    private void PosicionarCarros() {
        // nuevo centro
        Vector3 centro = new Vector3(3050, 0, 3050);

        for(int i = 0; i < _listaDeCarros.Length; i++) 
        {
            _carrosGO[i].transform.position = new Vector3(
                ((_listaDeCarros[i].x * 100)) - centro.x,
                0,
                ((_listaDeCarros[i].y * 100)) - centro.z
            );
        }
    }

    public void EscucharPosiciones(ListaCarro datos) {
        CameraManager.Instance.IntercambiarCamara();
        _direcciones = new Vector3[datos.totalCars];
        for(int i = 0; i < _direcciones.Length; i++) {
            _direcciones[i] = new Vector3();
        }
        StartCoroutine(MoverCarros(datos));
    }

    IEnumerator MoverCarros(ListaCarro datos) {
        for (int i = 0; i < datos.steps; i++)
        {
            for(int j = 0; j < datos.totalCars; j++){
                _listaDeCarros[j].x = datos.step[i].cars[j].x;
                _listaDeCarros[j].y = datos.step[i].cars[j].y;
                _listaDeCarros[j].activated = datos.step[i].cars[j].activated;
                if(_listaDeCarros[j].activated == 0){
                    CarPoolManager.Instance.Desactivar(_carrosGO[j]);
                }
            }

            PosicionarCarros();

            for(int j = 0; j < _direcciones.Length; j++){
                if (i < datos.steps - 1)
                {
                    _direcciones[j] = new Vector3(
                        datos.step[i + 1].cars[j].x - datos.step[i].cars[j].x,
                        0,
                        datos.step[i + 1].cars[j].y - datos.step[i].cars[j].y
                    );
                } else {
                    _direcciones[j] = new Vector3(
                        0,
                        0,
                        0
                    );
                }
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}
