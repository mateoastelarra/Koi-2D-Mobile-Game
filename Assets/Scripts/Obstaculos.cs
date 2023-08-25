using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculos : MonoBehaviour
{
    [SerializeField] private float velicidadMinima,velocidadMaxima;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        float numrandom = Random.Range(velicidadMinima, velocidadMaxima);
        rb.AddForce ( Vector2.down.normalized * numrandom, ForceMode2D.Impulse);
    }

     private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("RompeObstaculos"))
        {
            Destroy(gameObject);
        } 

    }
}

