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

    [SerializeField] private GameObject azul;
    [SerializeField] private GameObject peces;
        
    [SerializeField] private GameObject pezBarra;
    [SerializeField] private GameObject barraAzul;
        
    [SerializeField] private float cronometro = 0.0f;
        

    void Update()
    {
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
             PlayerPrefs.SetInt("Progress", 3);

            victoria();

        }
        else if (cronometro >= 2 * tiempoTotalVar / 3f)
        {
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

    public void victoria()
    {
        peces.SetActive(false);
        StartCoroutine(CambiarTransparenciaDespuesDeDelay(2,azul.GetComponent<SpriteRenderer>(),0f));
        StartCoroutine(cargarEscena(2,"Victoria"));

    }


    private IEnumerator cargarEscena(float delay,string NombreEscena)
    {

        Debug.Log("llamado a cargar escena1");

        yield return new WaitForSeconds(delay);
        
        Debug.Log("llamado a cargar escena2");

        SceneManager.LoadScene (NombreEscena);
    }

    private IEnumerator CambiarTransparenciaDespuesDeDelay(float tiempoEnDesvanecer, SpriteRenderer spriteRenderer,float delay)
    {
        Debug.Log("llamado a cambiar transparencia1");

        yield return new WaitForSeconds(delay);
        
        Debug.Log("llamado a cambiar transparencia2");

        float tiempoPasado = 0f;
        Color colorInicial = spriteRenderer.color;
        Color colorTransparente = new Color(colorInicial.r, colorInicial.g, colorInicial.b, 1f);

        while (tiempoPasado < tiempoEnDesvanecer)
        {
            tiempoPasado += Time.deltaTime;
            float t = Mathf.Clamp01(tiempoPasado / tiempoEnDesvanecer);
            spriteRenderer.color = Color.Lerp(colorInicial, colorTransparente, t);
            yield return null;
        }
    }



}