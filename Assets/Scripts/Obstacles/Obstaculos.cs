using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculos : MonoBehaviour
{
     private float velocidad;//velicidadMinima,velocidadMaxima;
    [SerializeField] private float velocidad1 = 2f;//velicidadMinima,velocidadMaxima;
    [SerializeField] private float velocidad2 = 3f;//velicidadMinima,velocidadMaxima;
    [SerializeField] private float velocidad3 = 4f;//velicidadMinima,velocidadMaxima;



    private Rigidbody2D rb;
     private float cronometro = 0.0f;
    [SerializeField] private float tiempoTotalVar = 120f; // Tiempo total para llegar de A a B en segundos.
    private PezDeBarraDeProgreso barra;
    [Header("Num Obstaculo : 1=piedra, 2=rama")]

    [SerializeField] private int numObstaculo;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        barra = GameObject.Find("ManagerBarra").GetComponent<PezDeBarraDeProgreso>();

        int randomNum = Random.Range(1, 3); // Genera un número aleatorio entre 1 y 2

        if(numObstaculo == 2)
        {

            if (randomNum == 2)
            {
                 Vector3 newScale = transform.localScale;
                newScale.x *= -1; // Cambiar la escala de X al valor opuesto

                transform.localScale = newScale;        
             }   
        }



        
       // float numrandom = Random.Range(velicidadMinima, velocidadMaxima);
        cronometro = barra.devolverCronometro();

        setearVelocidad();
        rb.AddForce ( Vector2.down.normalized * velocidad, ForceMode2D.Impulse);
    }

     void Update()
    {
    }


     private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("RompeObstaculos"))
        {
            Destroy(gameObject);
        } 

    }
    private void setearVelocidad()
    {

        float tiempo1 = tiempoTotalVar * 0.3f;
        float tiempo2 = tiempoTotalVar * 0.6f;
        float tiempo3 = tiempoTotalVar * 1f;




            if (cronometro > 0f && cronometro < tiempo1)
            {
                velocidad = velocidad1;
            }
         
            if (cronometro > tiempo1 && cronometro < tiempo2)
            {
                velocidad = velocidad2;
            }
            if (cronometro > tiempo2 )
            {
                velocidad = velocidad3;
            }

    }
}

