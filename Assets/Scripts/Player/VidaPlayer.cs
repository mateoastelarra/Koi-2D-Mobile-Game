using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaPlayer : MonoBehaviour
{
    [Header("Player lives")]
    [SerializeField] private  int maxLives;
    [SerializeField] private  int lives;
    [SerializeField] private GameObject[] livesUI;

    [Header("variables para effecto 'blink'")]
    [SerializeField] SpriteRenderer koiSpriteRenderer;
    [SerializeField] private Material MaterialBlink;
    [SerializeField] private float timeImmune;
    [SerializeField] private float timeBetweenBlinks;
    [SerializeField] private bool immune;
    [SerializeField] private bool hasShield;

    private float timerBlink;
    private Material originalMaterial;

    public int Lives { get => lives; set => lives = value; }
    public bool Immune { get => immune; set => immune = value; }
    public bool HasShield { get => hasShield; set => hasShield = value; }
    public int MaxLives { get => maxLives;}

    void Start()
    {
        originalMaterial = koiSpriteRenderer.material;

        timerBlink = 0;

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
        if (Immune == false)
        {
            StartCoroutine(BeInmune());
            SFXManager.GetInstance().PlayCrashSound(gameObject);
            Lives -= 1;
            UpdateLivesImages();

            if (Lives == 0)
            {
                StartCoroutine("GoToMainMenu");
            }
        }
    }

    public IEnumerator GoToMainMenu()
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
        immune = true;

        yield return new WaitForSeconds(timeImmune);

        if (!HasShield)
        {
            immune = false;
        }

    }

    public void UpdateLivesImages()
    {
        for (int i = 0; i < maxLives; i++)
        {
            livesUI[i].SetActive(i < lives);
        }
    }

    private void Blink()
    {
        
        if (Immune)
        {
            if (timerBlink >= timeBetweenBlinks)
            {
                if (koiSpriteRenderer.material == originalMaterial)
                {
                    koiSpriteRenderer.material = MaterialBlink;
                }
                else
                {
                    koiSpriteRenderer.material = originalMaterial;
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
            koiSpriteRenderer.material = originalMaterial;
        }
    }

}
