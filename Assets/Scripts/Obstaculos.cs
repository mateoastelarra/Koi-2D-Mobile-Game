using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculos : MonoBehaviour
{
    [SerializeField] private float velocidad;//velicidadMinima,velocidadMaxima;
    private Rigidbody2D rb;

    [Header("Num Obstaculo : 1=piedra, 2=rama")]

    [SerializeField] private int numObstaculo;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();



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
        rb.AddForce ( Vector2.down.normalized * velocidad, ForceMode2D.Impulse);
    }

     private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("RompeObstaculos"))
        {
            Destroy(gameObject);
        } 

    }
}

