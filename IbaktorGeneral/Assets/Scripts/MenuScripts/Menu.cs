using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public CanvasGroup MenuCanvas;
	public CanvasGroup OpcionesCanvas;

	void Start()
	{
		MenuCanvas.gameObject.SetActive(true);
		OpcionesCanvas.gameObject.SetActive(false);
	}

	public void Play()
	{
		SceneManager.LoadScene("EscenaJuego3D");
	}

	public void Opciones()
	{
		MenuCanvas.gameObject.SetActive(false);
		OpcionesCanvas.gameObject.SetActive(true);
	}

	public void Return()
	{
		OpcionesCanvas.gameObject.SetActive(false);
		MenuCanvas.gameObject.SetActive(true);
	}

	public void Exit()
	{
		Application.Quit();
	}
}
