using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarraProgreso : MonoBehaviour
{
    public float tiempoTotalVar = 5.0f; // Tiempo total para cambiar la escala de 0 a 1 en segundos.

    private float cronometro = 0.0f;

    void Update()
    {
        // Incrementa el cronómetro con el tiempo delta.
        cronometro += Time.deltaTime;

        // Calcula el valor de interpolación en función del cronómetro.
        float valorInterpolacion = Mathf.Clamp01(cronometro / tiempoTotalVar);

        // Usa la función Lerp para cambiar la escala en el eje X.
        Vector3 nuevaEscala = new Vector3(valorInterpolacion, transform.localScale.y, transform.localScale.z);
        transform.localScale = nuevaEscala;

        // Si deseas reiniciar la escala cuando llegue a 1, puedes hacerlo de manera similar al ejemplo anterior.
        if (cronometro >= tiempoTotalVar)
        {
            // Restablece el cronómetro y la escala en X para reiniciar el cambio de escala.
            cronometro = 0.0f;
            transform.localScale = new Vector3(0, transform.localScale.y, transform.localScale.z);
        }
    }
}