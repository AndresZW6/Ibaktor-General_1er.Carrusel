using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivarColisionesPowerUp : MonoBehaviour
{
    //Declarar objeto a desaparecer
    public GameObject powerUp;

    //interaccion
    void OnTriggerEnter (Collider other)
    {
        //si el objeto que colisiona es el player
        if(other.CompareTag("Player"))
        {
            //desactivar mi power up
            powerUp.SetActive(false);
        }
    }
}
