using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelEstatus : MonoBehaviour
{
	public Text nombreEntidad;
	public Text nivelEntidad;

	public Slider SliderVida;
	public Image BarraVida;
	public Text vidaEntidad;

	public void AjustarEstadisticas(string nombre, Estadisticas estadiscas)
	{
		this.nombreEntidad.text = nombre;

		this.nivelEntidad.text = $"LVL. {estadiscas.nivel}";
		this.AjustarVida(estadiscas.vida, estadiscas.vidaMaxima);	
	}

	public void AjustarVida (float vida, float vidaMaxima)
	{
		this.vidaEntidad.text = $"{Mathf.RoundToInt(vida)} / {Mathf.RoundToInt(vidaMaxima)}";
		float percentage = vida/vidaMaxima;

		this.SliderVida.value = percentage;

		if(percentage < 0.4f)
		{
			this.BarraVida.color = Color.red;
		}

		if(percentage > 0.4f)
			{
				this.BarraVida.color = Color.green;
			}
	}
}
