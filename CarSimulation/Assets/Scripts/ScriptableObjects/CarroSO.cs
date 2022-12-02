using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Carrito", menuName = "ScriptableObjects/ScriptableObjectsCars/Carrito", order = 1)]
public class CarroSO : ScriptableObject
{
    public float escalaX;
    public float escalaY;
    public float escalaZ;
    public float rotateY;
    public GameObject prefabDeModelo;
}
