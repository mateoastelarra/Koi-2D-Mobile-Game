using UnityEngine;

public class Obstaculos : MonoBehaviour
{
    private float velocity;
    [SerializeField] private float[] velocityForEachPhase;

    private Rigidbody2D rb;

    [Header("Num Obstaculo : 1=piedra, 2=rama")]
    [SerializeField] private int numObstaculo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        int randomNum = Random.Range(1, 3);

        if (numObstaculo == 2)
        {
            if (randomNum == 2)
            {
                Vector3 newScale = transform.localScale;
                newScale.x *= -1;

                transform.localScale = newScale;
            }
        }

        rb.AddForce(Vector2.down.normalized * velocity, ForceMode2D.Impulse);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("RompeObstaculos"))
        {
            Destroy(gameObject);
        }

    }

    public void SetVelocity(int phase)
    {
        velocity = velocityForEachPhase[phase];
    }

}

