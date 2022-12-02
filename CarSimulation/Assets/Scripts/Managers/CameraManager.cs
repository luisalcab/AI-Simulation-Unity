using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    [SerializeField]
    public Camera[] _camaras;
    private int _camaraActiva;

    [SerializeField]
    private GameObject[] _ui;

    public GameObject _carrito;

    public static CameraManager Instance {
        get;
        private set;
    }

    // Start is called before the first frame update
    void Awake()
    {
        if(Instance != null){
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start() 
    {
        HabilitarCamara(_camaras.Length - 2);
        _camaraActiva = 1;
    }

    private void HabilitarCamara(int camaraAHabilitar) {

        if(camaraAHabilitar < 0 || camaraAHabilitar >= _camaras.Length){
            throw new System.Exception("INDICE DE CÁMARA A HABILITAR FUERA DE RANGO");
        }

        if(_camaras == null){
            throw new System.Exception("ARREGLO DE CAMARAS NULO");
        }

        for(int i = 0; i < _camaras.Length; i++){

            if(i == camaraAHabilitar){
                _camaras[i].gameObject.SetActive(true);
            } else {
                _camaras[i].gameObject.SetActive(false);
            }
        }
    }

    public void IntercambiarCamara() {

        if (_ui[0].activeSelf == false) {
            _ui[0].SetActive(true);
            _ui[1].SetActive(false);
        }

        // moverme a siguiente cámara
        _camaraActiva++;

        // módulo para asegurarnos que no se exceda del tamaño
        _camaraActiva %= _camaras.Length - 2;

        HabilitarCamara(_camaraActiva);
    }

    public void CarCamera(GameObject carro) {
        _carrito = new GameObject();
        _carrito.name = carro.name;
        _camaraActiva = 1;
        _camaras[_camaras.Length - 1].gameObject.transform.parent = carro.transform;
        _camaras[_camaras.Length - 1].gameObject.transform.localPosition = new Vector3(0, 2, 1);
        _camaras[_camaras.Length - 1].gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        HabilitarCamara(_camaras.Length - 1);
    }
}
