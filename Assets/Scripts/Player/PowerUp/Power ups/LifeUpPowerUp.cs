using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUpPowerUp : IPowerUp
{
    public void Use(GameObject currentGameObject)
    {
        VidaPlayer vidaPlayer = currentGameObject.GetComponent<VidaPlayer>();
        int vidas = vidaPlayer.Lives;

        if (vidas < vidaPlayer.MaxLives)
        {
            vidaPlayer.Lives = vidas + 1;
            vidaPlayer.UpdateLivesImages();
        }

        SFXManager.GetInstance().PlayLifeUpSound(currentGameObject);
    }
}
