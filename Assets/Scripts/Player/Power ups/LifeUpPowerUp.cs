using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUpPowerUp : IPowerUp
{
    public void Use(GameObject currentGameObject)
    {
        VidaPlayer vidaPlayer = currentGameObject.GetComponent<VidaPlayer>();
        int vidas = vidaPlayer.PuntosDeVida;
        if (vidas < 5)
        {
            vidaPlayer.PuntosDeVida = vidas + 1;
        }
        SFXManager.GetInstance().PlayLifeUpSound(currentGameObject);
    }
}
