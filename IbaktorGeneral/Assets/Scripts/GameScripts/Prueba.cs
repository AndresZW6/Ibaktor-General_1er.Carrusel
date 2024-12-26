using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IBAKTOR
{

public class Prueba : MonoBehaviour
{   
    //variables colisiones
    public GameObject pressF, buscarCubo, buscarEsfera,desactivarEsfera, aparicionCubo, aparicionCapsula, aparicionEsfera;

    void Start()
    {
        pressF.SetActive(false);
        aparicionCubo.SetActive(true);
        aparicionCapsula.SetActive(false);
        aparicionEsfera.SetActive(true);
        buscarCubo.SetActive(true);
        buscarEsfera.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "TriggerCubo")
        {
            Debug.Log("Estas entrando al cubo");
            buscarCubo.SetActive(false);
            aparicionCapsula.SetActive(true);
        }

        if(other.name == "TriggerEsfera")
        {
            Debug.Log("Estas entrando a la esfera");
            SceneManager.LoadScene("EscenaJuego3D_02");
        }

        if(other.name == "TriggerCapsula")
        {
            Debug.Log("Estas entrando a la capsula");
            pressF.SetActive(true);
        }

        if(other.name == "rapidez")
        {
                velocidadCaminado = velocidadCaminado * 2;
                StartCoroutine(TemporizadorVelocidadCaminado());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.name == "TriggerCubo")
        {
            Debug.Log("Estas saliendo del cubo");
        }

        if(other.name == "TriggerEsfera")
        {
            Debug.Log("Estas saliendo de la esfera");
        }

        if(other.name == "TriggerCapsula")
        {
            Debug.Log("Estas saliendo de la capsula");
            pressF.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.name == "TriggerCubo")
        {
            Debug.Log("Estas dentro del cubo");
        }

        if(other.name == "TriggerEsfera")
        {
            Debug.Log("Estas dentro de la esfera");
        }

        if(other.name == "TriggerCapsula")
        {
            Debug.Log("Estas dentro de la capsula");
            if(Input.GetKey(KeyCode.F))
            {
                aparicionEsfera.SetActive(true);
                aparicionCapsula.SetActive(false);
                pressF.SetActive(false);
            }
        }
    }

    IEnumerator TemporizadorVelocidadCaminado()
     {
            yield return new WaitForSeconds(10);
            velocidadCaminado = velocidadCaminado / 2;
     }
    // Update is called once per frame
    void Update()
    {
        KeyboardInputs();
    }

    //funcion de interaccion con el piso

    void OnCollisionEnter(Collision otro_Objeto)
    {
        //Comprobando que el objeto con  el que colisiona el jugador es una superficie

        //si otro_Objeto contiene el tag Superficie
        if(otro_Objeto.gameObject.CompareTag("Superficie"))
        {
            //Reseteamos la variable
            estoy_saltando = false;
        }

    }
    //------Variables--------
    public Transform cuerpoPersonaje;




    //Variable para controlar la velocidad de caminado
    [Range(0,5)]
    public float velocidadCaminado = 2;

    //Variable para controlar la fuerza de salto
    [Range(1,10)]
    public float fuerzaSalto = 5;
    public Rigidbody controladorFisicas;
    bool estoy_saltando = false;

    public EstadosDeAnimacion controlador_animaciones;

    //------Funciones------
    void KeyboardInputs()
    {
        //creando desplazamientos

        //Si presiono la tecla W o si presiono la flecha arriba
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            //Primero Realizamos la accion de mover al personaje hacia adelante
            Vector3 personaje_adelante = cuerpoPersonaje.forward;
            Walk(personaje_adelante);
            //Mandamos a llamar la animacion
            if (estoy_saltando == false)
            {
                controlador_animaciones.HacerAnimacion(Estados_Animacion.Caminado_Adelante);
            }
        }

        //Si presiono la tecla S o si presiono la flecha atras
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            //Primero realizamos la accion de mover al personaje hacia atras
            Vector3 personaje_atras = cuerpoPersonaje.forward * -1;
            Walk(personaje_atras);
            //Mandamos a llamar la animacion
            if (estoy_saltando == false)
            {
                controlador_animaciones.HacerAnimacion(Estados_Animacion.Caminado_Atras);
            }
        }

        //Si presiono la tecla D o si presiono la flecha derecha
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            //Primero realizamos la accion de mover al personaje a la derecha
            Vector3 personaje_derecha = cuerpoPersonaje.right;
            Walk(personaje_derecha);
            //Mandamos a llamar la animacion
            if (estoy_saltando == false)
            {
                controlador_animaciones.HacerAnimacion(Estados_Animacion.Caminado_Derecha);
            }
        }
        
        //Si presiono la tecla A o si presiono la flecha izquierda
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 personaje_izquierda = cuerpoPersonaje.right * -1;
            Walk(personaje_izquierda);
            //Mandamos a llamar la animacion
            //if (estoy_saltando == false)
            //{
                controlador_animaciones.HacerAnimacion(Estados_Animacion.Caminado_Izquierda);
            //}

        }

        //IDLE
        else
        {
            //mandar a llamar la animacion
            if (estoy_saltando == false)
            {
                controlador_animaciones.HacerAnimacion(Estados_Animacion.Idle);
            }
        }

        //Si pulso la barra espaciadora
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Si mi personaje no se encuentra saltando ejecuta el salto
            if (estoy_saltando == false)
            {
                Jump();
                //Mandar a llamar la animacion
                controlador_animaciones.HacerAnimacion(Estados_Animacion.Salto);
            }
        }

    }

    //Declarando el modo de uso del input mover
    void Walk(Vector3 direccion)
    {
        Vector3 posicion_actual = cuerpoPersonaje.position;
        cuerpoPersonaje.position = posicion_actual + (direccion * velocidadCaminado * Time.deltaTime);
    }

    //Declarando el modo de uso del input saltar
    void Jump()
    {
        Vector3 arriba = Vector3.up;
        Vector3 impulso_salto = arriba * fuerzaSalto;

        controladorFisicas.velocity = impulso_salto;

        estoy_saltando = true;
    }

}

}