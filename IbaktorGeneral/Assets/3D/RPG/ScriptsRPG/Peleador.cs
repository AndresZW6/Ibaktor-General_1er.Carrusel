using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Peleador : MonoBehaviour
{
	public string IdNombre;
	public PanelEstatus panelEstatus;

	public GestorCombate gestorCombate;

	protected Estadisticas estadisticas;

	protected Habilidades[] habilidades;

	public bool EstaVivo
    {
		get => this.estadisticas.vida > 0;
    }

	protected virtual void Start()
	{
		this.panelEstatus.AjustarEstadisticas(this.IdNombre, this.estadisticas);

		this.habilidades = this.GetComponentsInChildren<Habilidades>();
	}

	public void ModificadorVida(float cantidad)
	{
		this.estadisticas.vida = Mathf.Clamp(this.estadisticas.vida + cantidad, 0f, this.estadisticas.vidaMaxima);

		this.estadisticas.vida = Mathf.Round(this.estadisticas.vida);

		this.panelEstatus.AjustarVida(this.estadisticas.vida, this.estadisticas.vidaMaxima);
	}

	public Estadisticas ObtenerEstadisticasActuales()
    {
		return this.estadisticas;
    }

	public abstract void IniciarTurno();
}
