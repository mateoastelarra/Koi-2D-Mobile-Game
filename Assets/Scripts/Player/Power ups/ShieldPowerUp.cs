using System;
using System.Threading.Tasks;
using UnityEngine;

public class ShieldPowerUp : IPowerUp
{
    public void Use(GameObject currentGameObject)
    {
        ActiveShield(currentGameObject);
    }

    async void ActiveShield(GameObject gameObject)
    {
        Transform shieldSprite = gameObject.transform.Find("ShieldSprite");

        gameObject.GetComponent<VidaPlayer>().Inmune = true;
        shieldSprite.gameObject.SetActive(true);
        SFXManager.GetInstance().PlayShieldSound(gameObject);

        await Task.Delay(TimeSpan.FromSeconds(5));

        shieldSprite.gameObject.SetActive(false);

        await Task.Delay(TimeSpan.FromSeconds(0.2f));

        gameObject.GetComponent<VidaPlayer>().Inmune = false;
        

    }
}
