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
    [SerializeField] private GameObject[] lives;

    public int PuntosDeVida { get => puntosDeVida; set => puntosDeVida = value; }
    public bool Inmune { get => inmune; set => inmune = value; }
    public bool HasShield { get => hasShield; set => hasShield = value; }
    public int PuntosDeVidaMaximos { get => puntosDeVidaMaximos;}

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
        UpdateLivesImages();
    }

    void Update()
    {
        timerBlink += Time.deltaTime;
       
        Blink();
    }


    private void OnTriggerEnter2D(Collider2D other)
    { 
        if (other.CompareTag("Obstaculo"))
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        if (Inmune == false)
        {
            StartCoroutine(BeInmune());
            SFXManager.GetInstance().PlayCrashSound(gameObject);
            PuntosDeVida -= 1;
            UpdateLivesImages();

            if (PuntosDeVida == 0)
            {
                StartCoroutine("VolverAlMenuPrincipal");
            }
        }
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

    public void UpdateLivesImages()
    {
        for (int i = 0; i < puntosDeVidaMaximos; i++)
        {
            lives[i].SetActive(i < puntosDeVida);
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

}
