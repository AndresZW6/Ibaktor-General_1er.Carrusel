using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IBAKTOR
{

    public enum Estados_Animacion
    {
        Idle = 0,
        Caminado_Adelante = 1,
        Caminado_Atras = 2,
        Caminado_Izquierda = 3,
        Caminado_Derecha = 4,
        Correr = 5,
        Salto = 6,
        Muerte = 7,
        Golpeado = 8,
    }

public class EstadosDeAnimacion : MonoBehaviour
{
    public Animator control_anim;
    public string parametro_estado_animacion = "Estado";

    //Funcion que reproduce animaciones
    public void HacerAnimacion (Estados_Animacion animacion)
    {
        int anim = (int)animacion;
        //Indicar cual parametro de Estado va a tener mi accion
        control_anim.SetInteger (parametro_estado_animacion, anim);
    }
}
}
