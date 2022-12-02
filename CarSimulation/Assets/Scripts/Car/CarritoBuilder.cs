using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarritoBuilder : MonoBehaviour
{

    // CÓMO DETECTAR QUE OBJETO FUE CLICKEADO
    // https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnMouseDown.html

    [SerializeField]
    private CarroSO _datos;

    private GameObject _carritoInterno;

    // Start is called before the first frame update
    void Awake()
    {
        ActualizarCarrito();
    }

    private void ActualizarCarrito() {

        if(_carritoInterno != null)
            Destroy(_carritoInterno);

        // utilizando los datos construir carrito
        _carritoInterno = Instantiate<GameObject>(
            _datos.prefabDeModelo, 
            transform.position, 
            transform.rotation,
            transform
        );

        _carritoInterno.transform.localScale = new Vector3(
            _datos.escalaX, 
            _datos.escalaY, 
            _datos.escalaZ
        );

        _carritoInterno.transform.Rotate(0, _datos.rotateY, 0);
    }

    public void ActualizarCarrito(CarroSO nuevoCarro) {

        _datos = nuevoCarro;
        ActualizarCarrito();
    }

    private void OnMouseDown()
    {
        CameraManager.Instance.CarCamera(this.gameObject);
    }

}
