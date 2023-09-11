using UnityEngine;

public class Obstaculos : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private float velocidad1 = 2f;
    [SerializeField] private float velocidad2 = 3f;
    [SerializeField] private float velocidad3 = 4f;



    
    private Rigidbody2D rb;
    private float cronometro = 0.0f;
    private float tiempoTotalVar = 120f; // Tiempo total para llegar de A a B en segundos.
    private PezDeBarraDeProgreso barra;

    [Header("Num Obstaculo : 1=piedra, 2=rama")]

    [SerializeField] private int numObstaculo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        barra = GameObject.Find("ManagerBarra").GetComponent<PezDeBarraDeProgreso>();

        int randomNum = Random.Range(1, 3); 

        if(numObstaculo == 2)
        {

            if (randomNum == 2)
            {
                Vector3 newScale = transform.localScale;
                newScale.x *= -1; 

                transform.localScale = newScale;        
             }   
        }

        cronometro = barra.devolverCronometro();

        setearVelocidad();

        rb.AddForce ( Vector2.down.normalized * velocidad, ForceMode2D.Impulse);
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

        float tiempo1 = tiempoTotalVar * 0.33333333333f;
        float tiempo2 = tiempoTotalVar * 0.66666666666f;
        //float tiempo3 = tiempoTotalVar;

        if (cronometro > 0f && cronometro < tiempo1)
        {
            velocidad = velocidad1;
        }
        else if (cronometro < tiempo2)
        {
            velocidad = velocidad2;
        }
        else
        {
            velocidad = velocidad3;
        }

    }
}

