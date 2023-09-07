using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUpPowerUp : IPowerUp
{
    public void Use(GameObject currentGameObject)
    {
        VidaPlayer vidaPlayer = currentGameObject.GetComponent<VidaPlayer>();
        int vidas = vidaPlayer.PuntosDeVida;
        vidaPlayer.PuntosDeVida = vidas + 1;
    }
}
