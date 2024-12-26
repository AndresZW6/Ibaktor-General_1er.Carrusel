using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control2D : MonoBehaviour
{
    public float velocidad;
    public bool atacando;
    public bool patada;
    public Animator animacion;

    private float Gravedad;
    private float Yposicion;
    private float YposicionPiso;
    public bool EstaEnPiso;
    public bool EstaSaltando;

    public int PosicionSalto;
    public float AlturaSalto;
    public float PotenciaSalto;
    public float Caida;

    public SpriteRenderer spr;
    private float delay;
    private int UpDown;

    void Start()
    {
        animacion = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
    }

    //Funcion control
    public void Movimiento()
    {
        //Si presiono la tecla flecha arriba o W movimiento hacia arriba
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) && !atacando && !EstaSaltando && EstaEnPiso)
        {
            transform.Translate(Vector3.up * velocidad * Time.deltaTime);
            animacion.SetBool("Walk", true);
        }
        else
        {
            animacion.SetBool("Walk", false);
        }

        //Si presiono la tecla flecha abajo o S movimiento hacia abajo
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) && !atacando && !EstaSaltando && EstaEnPiso)
        {
            transform.Translate(Vector3.up * -velocidad * Time.deltaTime);
            animacion.SetBool("Walk", true);
        }
        //else
        //{
        //    animacion.SetBool("Walk", false);
        //}

        //Si presiono la tecla flecha derecha o D movimiento hacia la derecha
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) && !atacando)
        {
            transform.Translate(Vector3.right * velocidad * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            animacion.SetBool("Walk", true);
        }
        //else
        //{
        //    animacion.SetBool("Walk", false);
        //}

        //Si presiono la tecla flecha izquierda o A movimiento hacia la izquierda
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) && !atacando)
        {
            transform.Translate(Vector3.right * velocidad * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            animacion.SetBool("Walk", true);
        }
        //else
        //{
        //    animacion.SetBool("Walk", false);
        //}
    }

    //Funcion de salto
    public void Saltar()
    {
        //Si pulso espacio, no esta saltando y esta en el piso salta
        if (Input.GetKeyDown(KeyCode.Space) && !EstaSaltando && EstaEnPiso)
        {
            YposicionPiso = transform.position.y;
            EstaSaltando = true;
            EstaEnPiso = false;
        }

        if (EstaSaltando)
        {
            switch (PosicionSalto)
            {
                case 0:
                    Gravedad = AlturaSalto;
                    PosicionSalto = 1;

                    break;

                case 1:
                    if (Gravedad > 0)
                    {
                        Gravedad -= PotenciaSalto * Time.deltaTime;
                    }
                    else
                    {
                        PosicionSalto = 2;
                    }

                    break;
            }
        }
    }

    void AjustarTransformacionY(float n)
    {
        transform.position = new Vector3(transform.position.x, n, transform.position.z);
    }

    public void DeteccionDelPiso()
    {
        if(!EstaSaltando && !atacando)
        {
            UpDown = 0;

            if (Yposicion == YposicionPiso)
            {
                EstaEnPiso = true;
            }
            animacion.SetBool("Jump", false);
        }
        else
        {
            animacion.SetBool("Jump", true);
        }

        if (EstaEnPiso)
        {
            Gravedad = 0;
            PosicionSalto = 0;
        }
        else
        {
            switch(PosicionSalto)
            {
                case 2:
                    Gravedad = 0;
                    PosicionSalto = 3;

                    break;

                case 3:
                    if(Yposicion >= YposicionPiso)
                    {
                        if (Gravedad > -10)
                        {
                            Gravedad -= AlturaSalto / Caida * Time.deltaTime;
                        }
                    }

                    else
                    {
                        EstaSaltando = false;
                        EstaEnPiso = true;
                        AjustarTransformacionY(YposicionPiso);

                        PosicionSalto = 0;
                    }

                    break;
               
            }
        }

        if (!EstaEnPiso && !patada)
        {
            if (transform.position.y > Yposicion)
            {
                animacion.SetFloat("gravedad", 1);
            }
            if (transform.position.y < Yposicion)
            {
                animacion.SetFloat("gravedad", 0);
                switch(UpDown)
                {
                    case 0:
                        animacion.Play("Base Layer.Jump", 0, 0);
                        UpDown ++;
                        break;
                }
            }

        }
        Yposicion = transform.position.y;
    }

    public void TerminarAnimacion()
    {
        atacando = false;
        patada = false;
    }

    public void Golpeando()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            delay = 1;
            if (!EstaSaltando)
            {
                if (!atacando)
                {
                    atacando = true;
                    animacion.SetTrigger("Hit");
                }
            }
            else
            {
                if(!patada)
                {
                    patada = true;
                    animacion.SetTrigger("Kick");
                }
            }
        }

        if (delay > 0)
        {
            spr.sortingOrder = 1;
            delay -= 2 * Time.deltaTime;
        }
        else
        {
            spr.sortingOrder = 0;
            delay = 0;
        }
    }

    void Update()
    {
        DeteccionDelPiso();
        Saltar();
        Golpeando();
    }

    void FixedUpdate()
    {
        Movimiento();
        transform.Translate(Vector3.up * Gravedad * Time.deltaTime);
    }
}
