using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acciones : MonoBehaviour
{
    [Header("Acciones base")]
    public string nombreAccion;
    public float duracionAnimacion;

    public bool afectacionPropia;

    public GameObject efectosFX;

    protected Peleador emisor;
    protected Peleador receptor;

    private void Animar()
    {
        var adelante = Instantiate(this.efectosFX, this.receptor.transform.position, Quaternion.identity);
        Destroy(adelante, this.duracionAnimacion);
    }

    public void Run()
    {
    }

}
