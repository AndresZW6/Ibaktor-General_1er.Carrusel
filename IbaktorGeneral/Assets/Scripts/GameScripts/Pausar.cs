using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausar : MonoBehaviour
{
    //variables declarando menu pausa y estado de pausa
    public GameObject menuPausa;
    public bool JuegoEnPausa = false;

    void Start()
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1;

    }
    // Update is called once per frame
    void Update()
    {
        //si presiono la tecla esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(JuegoEnPausa)
            {
                ReanudarJuego();
            }
            else
            {
                PausarJuego();
            }
        }
    }

    public void ReanudarJuego()
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1;
        JuegoEnPausa = false;
    }

    public void PausarJuego()
    {
        menuPausa.SetActive(true);
        Time.timeScale = 0;
        JuegoEnPausa = true;
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
        
    }
}
