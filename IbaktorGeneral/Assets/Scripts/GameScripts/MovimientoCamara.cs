using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCamara : MonoBehaviour
{
    //Variable para controlar la velocidad de la camara
    public float sensibilidadMouse = 300;

    //Variable para rotar en el eje x la camara
    public float rotacionX = 0;
    //Variable para rotar al jugador
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        //Mandar a llamar las Inputs de la camara de Unity
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadMouse * Time.deltaTime;

        //Asignando valor de rotacion Y/en el personaje
        rotacionX -= mouseY;

        //Limite rotacion vertical
        rotacionX = Mathf.Clamp(rotacionX, -10, 10);

        //Aplicar rotacion eje X
        transform.localRotation = Quaternion.Euler(rotacionX,0,0);

        //Rotacion eje Y
        player.Rotate(Vector3.up * mouseX);
    }
}
