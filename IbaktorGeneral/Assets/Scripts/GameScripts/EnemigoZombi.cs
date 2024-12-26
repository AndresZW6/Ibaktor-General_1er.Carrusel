using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemigoZombi : MonoBehaviour
{
	//variables, no deteccion del jugador
	public int esperando;
	public float tiempo; //tiempo entre estados de animacion de espera (idle y caminado)
	public Animator animacion; //animaciones mandadas a llamar sin base de datos
	public Quaternion angulo; //rotacion del enemigo para generar un movimiento mas libre
	public float grado; // grado del angulo

	//jugador y variable de ataque para usarse a cierta distancia
	public GameObject jugador;
	public bool atacando;

	//vision del jugador (script RangoEnemigo) forzar que el npc mire al jugador al atacar
	public RangoEnemigo Rango;

	//Variable de velocidad
	public float speed;

	//Declaramos un NavMesh
	public NavMeshAgent navegacion;

	//variable para realizar el ataque
	public float distanciaAtaque;
	public float rangoVision;

	//Iniamos el componente de animacion y la busqueda del jugador
	void Start()
	{
		animacion = GetComponent<Animator>();
		jugador = GameObject.Find("Player");
	}

	//funcion de estado de espera y deteccion del jugador

	public void Comportamiento_ZombiF()
	{
		//si la distancia entre la posicion del enemigo y el jugador es mayor a 5
		if(Vector3.Distance(transform.position, jugador.transform.position) > rangoVision)
		{
		navegacion.enabled = false;

		//al no detectar al jugador la animación de correr no inicia
		animacion.SetBool("run", false);

		//Hacemos correr el tiempo para dos estados de animacion
		tiempo += 1 * Time.deltaTime;
		//si tiempo es mayor o igual a 4
		if (tiempo >= 4)
		{
			//El estado de espera sacara un numero entre 0 y 1 (el maximo 1 hay que sumarle 1 para que la funcion ranger respete el rango de 0 a 1)
			esperando = Random.Range(0,2);
			//tiempo inicia y se reinicia en 0
			tiempo = 0;
		}

		//tenemos un "menu de situaciones"
		switch(esperando)
		{
			//El enemigo esta quieto
			case 0:
			animacion.SetBool("walk", false);
			break;

			//direccion a desplazarse
			case 1:
			grado = Random.Range(0, 360);
			angulo = Quaternion.Euler(0, grado, 0);

			//incremento en esperando para pasar al caso 2
			esperando ++;
			
			break;

			case 2:
			//rotacion del enemigo = al angulo establecido en el caso 1
			transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
			//desplazamiento hacia adelante
			transform.Translate(Vector3.forward * speed * Time.deltaTime);
			//activar animacion de caminado
			animacion.SetBool("walk", true);

			break;
		}
		}
		//deteccion del jugador y comenzara a seguirlo
		else
		{
			//variable igual a la posicion del target menos la posicion del enemigo
			var lookPos = jugador.transform.position - transform.position;
			//el enemigo mire al objetivo en este caso al Player
			lookPos.y = 0;
			var rotation = Quaternion.LookRotation(lookPos);

			//agente de navegacion activada
			navegacion.enabled = true;
			//destino de la navegacion hacia el jugador
			navegacion.SetDestination(jugador.transform.position);

			//si la distancia entre el enemigo y el jugador es mayor a la ditancia de ataque y atacando sea falso el enemigo nos seguira
			if (Vector3.Distance(transform.position, jugador.transform.position) > distanciaAtaque && !atacando)
			{
				//animacion de caminado se desactivara al detectar al jugador
				animacion.SetBool("walk", false);

				//animacion de correr se activara al detectar al jugador
				animacion.SetBool("run", true);
			}
			//si el enemigo se encuentra a menos de 0.05 de distancia ()
			else
			{
				if(!atacando)
				{
					//mandamos a llamar al scrpt de rotacion para que el enemigo se fuerce a ver al objetivo para atacarlo
					transform.rotation = Quaternion.RotateTowards(transform.rotation,rotation,2);

					//animaciones de caminado y correr desactivadas
					animacion.SetBool("walk", false);
					animacion.SetBool("run", false);
				}
			}
		}
		if (atacando)
		{
			navegacion.enabled = false;
		}
	}

	//funcion para desactivar la animacion de ataque y la variable atacando (reset)
	public void Final_Anim()
	{
		//condicion para cancelar la animacion de ataque
		if(Vector3.Distance(transform.position, jugador.transform.position) > distanciaAtaque + 0.2f)
		{
			animacion.SetBool("attack", false);
		}
		atacando = false;

		//activar colisionador para buscar al player y forzar que lo vea al atacar
		Rango.GetComponent<CapsuleCollider>().enabled = true;
	}



	void Update()
	{
		//mandar a llamar la funcion para que se repita cada frame de nuestro videojuego teniendo variaciones en su estado de espera
		Comportamiento_ZombiF();
	}
}
