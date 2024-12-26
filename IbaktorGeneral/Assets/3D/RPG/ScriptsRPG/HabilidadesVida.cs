using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HabilidadesVidaTipos
{
    EstadisticaBase, fijo, porcentaje
}

public class HabilidadesVida : Habilidades
{
    [Header("Modificador Vida")]
    public float cantidad;

    public HabilidadesVidaTipos ModificadorTipo;

    protected override void OnEjecucion()
    {
        float dano = this.ObtenerModificacion();

        this.receptor.ModificadorVida(cantidad);
    }

    public float ObtenerModificacion()
    {
        switch (this.ModificadorTipo)
        {
            case HabilidadesVidaTipos.EstadisticaBase:

                Estadisticas emisorEstadisticas = this.emisor.ObtenerEstadisticasActuales();
                Estadisticas receptorEstadisticas = this.receptor.ObtenerEstadisticasActuales();

                float rawDano = (((2 * emisorEstadisticas.nivel) / 5) + 2) * this.cantidad * (emisorEstadisticas.ataque / receptorEstadisticas.defensa);

                return (rawDano / 50) + 2;

            case HabilidadesVidaTipos.fijo:
                return this.cantidad;

            case HabilidadesVidaTipos.porcentaje:
                Estadisticas rEstadisticas = this.receptor.ObtenerEstadisticasActuales();

                return rEstadisticas.vidaMaxima * this.cantidad;
        }

        throw new System.InvalidOperationException("HabilidadesVida:: ObtenerDano. inalcanzable");
    }
}
