using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelHabilidadesPlayer : MonoBehaviour
{
    public GameObject[] botonesHabilidades;
    public Text[] etiquetasBotonesHabilidades;

    void Awake()
    {
        this.Ocultar();

        foreach(var boton in this.botonesHabilidades)
        {
            boton.SetActive(false);
        }
    }

    public void ConfigurarBotones(int Entrada, string nombreHabilidad)
    {
        this.botonesHabilidades[Entrada].SetActive(true);
        this.etiquetasBotonesHabilidades[Entrada].text = nombreHabilidad;
    }

    public void Mostrar()
    {
        this.gameObject.SetActive(true);
    }

    public void Ocultar()
    {
        this.gameObject.SetActive(false);
    }
}
