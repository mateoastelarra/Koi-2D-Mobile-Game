using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpRunner : MonoBehaviour
{
    [SerializeField] IPowerUp currentPowerUp = new LifeUpPowerUp();

    public void UsePowerUp()
    {
        currentPowerUp.Use(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {  
        bool isPowerUp = true;
        switch (collision.transform.tag)
        {
            case "LifeUp":
                currentPowerUp = new LifeUpPowerUp();
                break;
            case "Shield":
                currentPowerUp = new ShieldPowerUp();
                break;
            default:
                isPowerUp = false;
                break;
        }

        if (isPowerUp)
        {
            UsePowerUp();
            Destroy(collision.gameObject);
        }
    }
}
