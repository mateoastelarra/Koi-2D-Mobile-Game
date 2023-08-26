using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VidaPlayer : MonoBehaviour
{       
    [SerializeField] private  int puntosDeVida;
    [SerializeField] private  Text textoUIvida;
    [Header("variables para effecto 'blink'")]
     private  SpriteRenderer spriteRenderer;
     private Material originalMaterial;
    [SerializeField] private Material MaterialBlink;
    [SerializeField] private float timerBlink;
    [SerializeField] private float timer;
    [SerializeField] private float tiempoInmune;
    [SerializeField] private float tiempoEntreBlinks;
    [SerializeField] private bool inmune;

//    [SerializeField] private float tiempoInmune;



    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        timerBlink += Time.deltaTime;
        timer += Time.deltaTime;
        if (timer < tiempoInmune)
        {
            inmune = true;
        }
        else
        {
            inmune = false;
        }
        Blink();
         textoUIvida.text = puntosDeVida.ToString();
   
    }


        private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Obstaculo"))
        {
            if (inmune == false)
            {
            //es el timer para el blink
            timer = 0;

            puntosDeVida -=1;
            if (puntosDeVida == 0)
            {
                StartCoroutine("VolverAlMenuPrincipal");
            }
            }
        } 

    }


    //comenzamos con lo del blink...


      private void Blink()
    {
        

        if (inmune)
        {
            if (timerBlink >= tiempoEntreBlinks)
            {
                if (spriteRenderer.material == originalMaterial)
                {
                    spriteRenderer.material = MaterialBlink;
                }
                else
                {
                    spriteRenderer.material = originalMaterial;
                }

                timerBlink = 0;
            }
            else
            {
                timerBlink += Time.deltaTime;
            }
        }
        else
        {
            spriteRenderer.material = originalMaterial;
        }
    }
    public void ReiniciarEscena()
    {
        Debug.Log("reinciar");
        // Obtenemos el nombre de la escena actual
        string nombreEscenaActual = SceneManager.GetActiveScene().name;

        // Cargamos la misma escena para reiniciarla
        SceneManager.LoadScene(nombreEscenaActual);
    
    }

    public IEnumerator VolverAlMenuPrincipal()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }
}
