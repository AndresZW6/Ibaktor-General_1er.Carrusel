using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeleadorEnemigo : Peleador
{
	void Awake()
	{
		this.estadisticas = new Estadisticas(20, 50, 40, 30, 60);
	}

	public override void IniciarTurno()
	{
		StartCoroutine(this.IA());
	}

	IEnumerator IA()
    {
		yield return new WaitForSeconds(1f);

		Habilidades habilidad = this.habilidades[Random.Range(0, this.habilidades.Length)];

		habilidad.AjustarEmisorReceptor(this, this.gestorCombate.GetPeleadorOpuesto());

		this.gestorCombate.EnHabilidadPeleador(habilidad);
	}
}
