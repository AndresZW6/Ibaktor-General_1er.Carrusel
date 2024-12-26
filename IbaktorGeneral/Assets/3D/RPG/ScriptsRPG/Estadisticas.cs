using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estadisticas : MonoBehaviour
{
    public float vida;
    public float vidaMaxima;

    public int nivel;
    public float ataque;
    public float defensa;
    public float energia;

    public Estadisticas(int nivel, float vidaMaxima, float ataque, float defensa, float energia)
    {
        this.nivel = nivel;

        this.vidaMaxima = vidaMaxima;
        this.vida = vidaMaxima;

        this.ataque = ataque;
        this.defensa = defensa;
        this.energia = energia;
    }

    public Estadisticas copia()
    {
        return new Estadisticas(this.nivel, this.vidaMaxima, this.ataque, this.defensa, this.energia);
    }
}
