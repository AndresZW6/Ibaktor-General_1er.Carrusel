using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EstadoCombate
{
    Esperando_por_peleador,
    Peleador_accion,
    Revision_Victoria,
    Siguiente_turno
}

public class GestorCombate : MonoBehaviour
{
    public Peleador[] peleadores;
    private int peleadorLista;

    private bool CombateActivo;

    private EstadoCombate estadoCombate;

    private Habilidades habilidadActualDelPeleador;

    // Start is called before the first frame update
    void Start()
    {
        LogPanel.Write("Comienza el combate.");

        foreach(var peleador in this.peleadores)
        {
            peleador.gestorCombate = this;
        }

        this.estadoCombate = EstadoCombate.Siguiente_turno;

        this.peleadorLista = -1;
        this.CombateActivo = true;
        StartCoroutine(this.LoopCombate()); 
    }

    IEnumerator LoopCombate()
    {
        while (this.CombateActivo)
        {
            switch (this.estadoCombate)
            {
                case EstadoCombate.Esperando_por_peleador:
                    yield return null;
                    break;

                case EstadoCombate.Peleador_accion:
                    LogPanel.Write($"{this.peleadores[this.peleadorLista].IdNombre} uso {habilidadActualDelPeleador.NombreHabilidad}.");

                    yield return null;

                    habilidadActualDelPeleador.Ejecucion();

                    yield return new WaitForSeconds(habilidadActualDelPeleador.tiempoAnimacion);
                    this.estadoCombate = EstadoCombate.Revision_Victoria;

                    habilidadActualDelPeleador = null;
                    break;

                case EstadoCombate.Revision_Victoria:
                    foreach (var peleador in this.peleadores)
                    {
                        if (peleador.EstaVivo == false)
                        {
                            this.CombateActivo = false;

                            LogPanel.Write("Haz ganado el combate");
                        }
                        else
                        {
                            this.estadoCombate = EstadoCombate.Siguiente_turno;
                        }
                    }

                    yield return null;
                    break;
                 
                case EstadoCombate.Siguiente_turno:
                    yield return new WaitForSeconds(1f);
                    this.peleadorLista = (this.peleadorLista + 1) % this.peleadores.Length;

                    var TurnoActual = this.peleadores[this.peleadorLista];

                    LogPanel.Write($"turno de {TurnoActual.IdNombre}.");
                    TurnoActual.IniciarTurno();

                    this.estadoCombate = EstadoCombate.Esperando_por_peleador;

                    break;
            }
        }
    }

    public Peleador GetPeleadorOpuesto()
    {
        if(this.peleadorLista == 0)
        {
            return this.peleadores[1];
        }
        else
        {
            return this.peleadores[0];
        }
    }

    public void EnHabilidadPeleador(Habilidades habilidad)
    {
        this.habilidadActualDelPeleador = habilidad;
        this.estadoCombate = EstadoCombate.Peleador_accion;
    }
}
