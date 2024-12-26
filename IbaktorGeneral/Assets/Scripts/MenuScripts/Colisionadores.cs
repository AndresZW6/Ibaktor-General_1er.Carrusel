using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colisionadores : MonoBehaviour
{
    public GameObject Cubo;
    public GameObject Esfera;
    public GameObject Capsula;

    // Start is called before the first frame update
    void Start()
    {
        Esfera.gameObject.SetActive(false);
        Capsula.gameObject.SetActive(false);
        Cubo.gameObject.SetActive(true);
    }

    void OnTriggerEnter(Collision Jugador) 
    {
        if (Jugador.gameObject.CompareTag("Player"))
        {
            Esfera.gameObject.SetActive(true);
        }
    }
}
