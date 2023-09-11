using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VidaPlayer : MonoBehaviour
{
    [SerializeField] private  int puntosDeVidaMaximos;
    [SerializeField] private  int puntosDeVida;
    [SerializeField] private  Text textoUIvida;

    [Header("variables para effecto 'blink'")]
     private  SpriteRenderer spriteRenderer;
     private Material originalMaterial;
    [SerializeField] private Material MaterialBlink;
    [SerializeField] private float timerBlink;
    [SerializeField] private float tiempoInmune;
    [SerializeField] private float tiempoEntreBlinks;
    [SerializeField] private bool inmune;
    [SerializeField] private bool hasShield;

    [Header("Imagenes de vida de koi")]
    [SerializeField] private GameObject vida1;
    [SerializeField] private GameObject vida2;
    [SerializeField] private GameObject vida3;
    [SerializeField] private GameObject vida4;
    [SerializeField] private GameObject vida5;
    

    public int PuntosDeVida { get => puntosDeVida; set => puntosDeVida = value; }
    public bool Inmune { get => inmune; set => inmune = value; }
    public bool HasShield { get => hasShield; set => hasShield = value; }



    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {

        if(puntosDeVida > puntosDeVidaMaximos)
        {
            puntosDeVida = puntosDeVidaMaximos;
        }
        timerBlink += Time.deltaTime;
       
        Blink();
         
        AtualizarImagenDeVida();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Obstaculo"))
        {
            if (Inmune == false)
            {
                //es el timer para el blink
                //timer = 0;
                StartCoroutine(BeInmune());
                SFXManager.GetInstance().PlayCrashSound(gameObject);
                PuntosDeVida -=1;

                if (PuntosDeVida == 0)
                {
                    //ReiniciarEscena();
                    StartCoroutine("VolverAlMenuPrincipal");
                }
            }
        } 

    }


      private void Blink()
    {
        

        if (Inmune)
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
        yield return new WaitForSeconds(1);
        SFXManager.GetInstance().PlayDrumSound(gameObject);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }

    private IEnumerator BeInmune()
    {
        inmune = true;

        yield return new WaitForSeconds(tiempoInmune);
        
        if (!HasShield)
        {
            inmune = false;
        }
        
    }

    public void AtualizarImagenDeVida()
    {
        switch (PuntosDeVida)
        {
        case 0:
            vida1.SetActive(false); 
            vida2.SetActive(false); 
            vida3.SetActive(false); 
            vida4.SetActive(false); 
            vida5.SetActive(false); 

            break;

        case 1:
            vida1.SetActive(true); 
            vida2.SetActive(false); 
            vida3.SetActive(false); 
            vida4.SetActive(false); 
            vida5.SetActive(false); 

            break;

           
        case 2:
            vida1.SetActive(true); 
            vida2.SetActive(true); 
            vida3.SetActive(false); 
            vida4.SetActive(false); 
            vida5.SetActive(false); 

            break;

           
        case 3:
            vida1.SetActive(true); 
            vida2.SetActive(true); 
            vida3.SetActive(true); 
            vida4.SetActive(false); 
            vida5.SetActive(false); 

            break;

           
         case 4:
            vida1.SetActive(true); 
            vida2.SetActive(true); 
            vida3.SetActive(true); 
            vida4.SetActive(true); 
            vida5.SetActive(false); 

            break;

           
        case 5:
            vida1.SetActive(true); 
            vida2.SetActive(true); 
            vida3.SetActive(true); 
            vida4.SetActive(true); 
            vida5.SetActive(true); 

            break;


        }

    }

    
}
