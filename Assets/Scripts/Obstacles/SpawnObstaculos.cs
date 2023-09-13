using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstaculos : MonoBehaviour
{
    private float timeBetweenSpawns;
    [SerializeField] private float[] timeBetweenSpawnForEachPhase;
   
    [Header("GameObjets de obstaculos(Prefabs)")]

    [SerializeField] private  GameObject piedraRandom;
    [SerializeField] private  GameObject piedraDoble;
    [SerializeField] private  GameObject piedraMedio;
    [SerializeField] private  GameObject rama;

    [Header("Posiciones")]

    [SerializeField] private  float posicionYparaObstaculos;
    [SerializeField] private  float limiteXizquierdo;
    [SerializeField] private  float limiteXderecho;

    [SerializeField] private PezDeBarraDeProgreso barra;
    private  Vector2 spawnPosition;
    private  GameObject obstaculoElegido;
    private  float tiempoPasado;
    

    private Transform camTransform;
    private float camYPosition;
    private int numAnt;
    public int randomNumParaPiedra;

    private float cronometro = 0.0f;
    //private PezDeBarraDeProgreso barra;
    private float tiempoTotalVar = 120f; // Tiempo total para llegar de A a B en segundos.


    void Start()
    {
        camTransform = Camera.main.transform;

        //barra = GameObject.Find("ManagerBarra").GetComponent<PezDeBarraDeProgreso>();
        //SetearTiempoSpawn();
        timeBetweenSpawns = timeBetweenSpawnForEachPhase[0];

    }
  
    void Update()
    {
        cronometro = barra.GetTimer();

        // Actualizar la posición del eje Y de la cámara
        camYPosition = camTransform.position.y;


        tiempoPasado += Time.deltaTime;
        if (tiempoPasado > timeBetweenSpawns)
        {
            Spawnear();
            //SetearTiempoSpawn();
        }
    }

    private void OnEnable()
    {
        barra.OnChangePhase += ChangeSpawnTime;
    }

    void ChangeSpawnTime(int phase)
    {
        Debug.Log("cambiando velocidad " + phase);
        timeBetweenSpawns = timeBetweenSpawnForEachPhase[phase];
    }

    public void Spawnear()
    {
        tiempoPasado = 0;
  
        randomNumParaPiedra = Random.Range(1, 6); // aca va (1, numero de obstaculos +1)

        if (numAnt ==  randomNumParaPiedra){
            // esto es para que no salgan muchas veces seguidas el de la piedra del medio o el de las dos piedras por que queda medio mal
             randomNumParaPiedra = Random.Range(1, 6);
        }

        switch (randomNumParaPiedra)
        {
           
            case 1:
                obstaculoElegido = piedraRandom.gameObject;
                spawnPosition = new Vector2( Random.Range(limiteXizquierdo, limiteXderecho),posicionYparaObstaculos + camYPosition);
                break;
            case 2:
                obstaculoElegido = piedraDoble.gameObject;
                spawnPosition = new Vector2( 0,posicionYparaObstaculos + camYPosition);
                break;
            case 3:
                obstaculoElegido = piedraMedio.gameObject;
                spawnPosition = new Vector2( 0,posicionYparaObstaculos + camYPosition);
                break;
             case 4:
                obstaculoElegido = rama.gameObject;
                spawnPosition = new Vector2( 0,posicionYparaObstaculos + camYPosition);
                break;    
             case 5:
                obstaculoElegido = rama.gameObject;
                spawnPosition = new Vector2( 0,posicionYparaObstaculos + camYPosition);
                break; 
                     
        }

        numAnt =  randomNumParaPiedra;

        GameObject nuevoObstaculo = Instantiate(obstaculoElegido, spawnPosition, Quaternion.identity);
    
        // Establecer el objeto padre
        nuevoObstaculo.transform.SetParent(transform);

    }


}
