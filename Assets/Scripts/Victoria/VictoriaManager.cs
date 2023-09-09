using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoriaManager : MonoBehaviour
{
       [SerializeField] private GameObject pez;
       [SerializeField] private GameObject dragon;
       [SerializeField] private GameObject humo;
       private float tiempoPasado;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LerpPosition(new Vector3(0,-9f,0),new Vector3(0,-2f,0f),1,pez,2));
        StartCoroutine(AgrandarDespuesDeDelay(1,new Vector3(0,0,3),new Vector3(2f,2f,3),humo,4));
        StartCoroutine(CambiarTransparenciaDespuesDeDelay(2,humo.GetComponent<SpriteRenderer>(),5f));
        StartCoroutine(ActivarYdesactivar(pez,dragon,5f));
        StartCoroutine(LerpPosition(new Vector3(0,-2f,0),new Vector3(0,11,0f),4,dragon,7));
        StartCoroutine(cargarEscena(12,"Main Menu"));

    }
private IEnumerator cargarEscena(float delay,string NombreEscena)
    {

       
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene (NombreEscena);


    }
 IEnumerator ActivarYdesactivar(GameObject objetoDesactivar, GameObject objetoActivar,float delayDuration )
    {
        yield return new WaitForSeconds(delayDuration);
        objetoDesactivar.SetActive(false);
        objetoActivar.SetActive(true);

    }



     IEnumerator LerpPosition(Vector3 startPosition, Vector3 targetPosition, float lerpDuration, GameObject objeto,float delayDuration )
    {
        yield return new WaitForSeconds(delayDuration);
        float timeElapsed = 0f;
        while (timeElapsed < lerpDuration)
        {
            objeto.transform.position = Vector3.Lerp(startPosition, targetPosition, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        objeto.transform.position = targetPosition;
    }
private IEnumerator AgrandarDespuesDeDelay(float tiempoEnAgrandar,Vector3 escalaInicial,Vector3 escalaFinal,GameObject objeto,float delay)
    {

       
        yield return new WaitForSeconds(delay);

         tiempoPasado = 0f;

        while (tiempoPasado < tiempoEnAgrandar)
        {
            tiempoPasado += Time.deltaTime;

            // Calcula el valor de interpolación (entre 0 y 1) basado en el tiempo transcurrido y el tiempo total.
            float t = Mathf.Clamp01(tiempoPasado / tiempoEnAgrandar);

            // Interpola gradualmente entre la escala inicial y la escala final.
            objeto.transform.localScale = Vector3.Lerp(escalaInicial, escalaFinal, t);

            yield return null;
        }

    }

     private IEnumerator CambiarTransparenciaDespuesDeDelay(float tiempoEnDesvanecer, SpriteRenderer spriteRenderer,float delay)
    {
        yield return new WaitForSeconds(delay);

        float tiempoPasado = 0f;
        Color colorInicial = spriteRenderer.color;
        Color colorTransparente = new Color(colorInicial.r, colorInicial.g, colorInicial.b, 0f);

        while (tiempoPasado < tiempoEnDesvanecer)
        {
            tiempoPasado += Time.deltaTime;
            float t = Mathf.Clamp01(tiempoPasado / tiempoEnDesvanecer);
            spriteRenderer.color = Color.Lerp(colorInicial, colorTransparente, t);
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
