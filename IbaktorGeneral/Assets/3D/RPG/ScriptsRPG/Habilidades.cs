using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Habilidades : MonoBehaviour
{
    [Header("Habilidad Base")]
    public string NombreHabilidad;
    public float tiempoAnimacion;

    public bool AutoInfligido;

    public GameObject Efecto;

    protected Peleador emisor;
    protected Peleador receptor;

    private void Animate()
    {
        var go = Instantiate(this.Efecto, this.receptor.transform.position, Quaternion.identity);
        Destroy(go, this.tiempoAnimacion);
    }

    public void Ejecucion()
    {
        if (this.AutoInfligido)
        {
            this.receptor = this.emisor;
        }

        this.Animate();

        this.OnEjecucion();
    }

    public void AjustarEmisorReceptor(Peleador _emisor, Peleador _receptor)
    {
        this.emisor = _emisor;
        this.receptor = _receptor;
    }

protected abstract void OnEjecucion();
}

