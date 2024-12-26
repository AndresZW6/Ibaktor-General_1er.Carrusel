using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangoEnemigo : MonoBehaviour
{
    //animator igual al del script del EnemigoZombi
    public Animator animacion;
    //mandamos a llamar el codigo del EnemigoZombi
    public EnemigoZombi enemigo;

    //condicion de colision
    void OnTriggerEnter(Collider deteccion)
    {
        //Si el collider deteccion interactua con un objeto con un tag  "player" realiza el ataque
        if(deteccion.CompareTag("Player"))
        {
            animacion.SetBool("walk", false);
            animacion.SetBool("run", false);

            animacion.SetBool("attack", true);

            enemigo.atacando = true;

            //desactivamso el colisionador para que no nos genere ataques extras
            GetComponent<CapsuleCollider>().enabled = false;        
        }
    }


}
