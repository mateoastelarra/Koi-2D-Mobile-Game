using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
    
public class PezDeBarraDeProgreso : MonoBehaviour
{
        [SerializeField] private Transform puntoA;
        [SerializeField] private Transform puntoB;
        [SerializeField] private float tiempoTotalVar = 5.0f; // Tiempo total para llegar de A a B en segundos.
        [SerializeField] private float escalaFinal;

        [SerializeField] private GameObject pezBarra;
        [SerializeField] private GameObject barraAzul;
        [SerializeField] private float cronometro = 0.0f;

    void Update()
    {
        // Incrementa el cronómetro con el tiempo delta.
        cronometro += Time.deltaTime;

        // Calcula el valor de interpolación en función del cronómetro.
        float valorInterpolacion = Mathf.Clamp01(cronometro / tiempoTotalVar);

        // Usa la función Lerp para mover el objeto.
        pezBarra.transform.position = Vector3.Lerp(puntoA.position, puntoB.position, valorInterpolacion);


        // Calcula la escala actual utilizando Lerp.
        float escalaActual = Mathf.Lerp(0.0f, escalaFinal, valorInterpolacion);

        // Aplica la escala actual al objeto en el eje X.
        Vector3 nuevaEscala = new Vector3(escalaActual, transform.localScale.y, transform.localScale.z);
        barraAzul.transform.localScale = nuevaEscala;
        // Si deseas que el objeto regrese a A cuando llega a B, puedes usar algo como esto:
        if (cronometro >= tiempoTotalVar)
        {
            // Restablece el cronómetro y el valor de interpolación para reiniciar el movimiento.
          //  cronometro = 0.0f;
          //  barraAzul.transform.localScale = new Vector3(0, transform.localScale.y, transform.localScale.z);
                SceneManager.LoadScene("Victoria");
                PlayerPrefs.SetInt("Progress", 3);

        }
        else if (cronometro >= 2 * tiempoTotalVar / 3f)
        {
            Debug.Log("hola");
            if (PlayerPrefs.GetInt("Progress") < 2)
            {
                PlayerPrefs.SetInt("Progress", 2);
            }
        }
        else if (cronometro >=  tiempoTotalVar / 3f)
        {
            if (PlayerPrefs.GetInt("Progress") < 1)
            {
                PlayerPrefs.SetInt("Progress", 1);
            }
        }
    }
    public float devolverCronometro(){
        return cronometro;
    }
}