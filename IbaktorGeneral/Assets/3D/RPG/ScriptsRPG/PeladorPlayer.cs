using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeladorPlayer : Peleador
{
	[Header("UI")]
	public PanelHabilidadesPlayer panelHabilidades;

	void Awake()
	{
		this.estadisticas = new Estadisticas(21, 60, 50, 45, 20);
	}

	public override void IniciarTurno()
	{
		this.panelHabilidades.Mostrar();

		for (int i = 0; i < this.habilidades.Length; i++)
        {
			this.panelHabilidades.ConfigurarBotones(i, this.habilidades[i].NombreHabilidad);
        }
	}

	public void EjecutarHabilidades(int Entrada)
	{
		this.panelHabilidades.Ocultar();

		Habilidades habilidad = this.habilidades[Entrada];

		habilidad.AjustarEmisorReceptor(this, this.gestorCombate.GetPeleadorOpuesto());

		this.gestorCombate.EnHabilidadPeleador(habilidad);
	}
}
