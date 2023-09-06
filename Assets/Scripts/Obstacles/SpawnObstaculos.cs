using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstaculos : MonoBehaviour
{
    [Header("Tiempo Para Spawnear")]

    [SerializeField] private  float TiempoParaSpawnMin ;//, limiteYArrBrocoli,LimiteYabajoBrocoli,poisicionXBrocoli,limiteXizqFrutilla,limiteXderFrutilla,posicionYfutilla;
    [SerializeField] private  float TiempoParaSpawnMax;
   
    [Header("GameObjets de obstaculos(Prefabs)")]

    [SerializeField] private  GameObject piedraRandom;
    [SerializeField] private  GameObject piedraDoble;
    [SerializeField] private  GameObject piedraMedio;
    [SerializeField] private  GameObject rama;



    [Header("Posiciones")]

    [SerializeField] private  float posicionYparaObstaculos;
    [SerializeField] private  float limiteXizquierdo;
    [SerializeField] private  float limiteXderecho;


    private  Vector2 spawnPosition;
    private  GameObject obstaculoElegido;
    private  float tiempoPasado;
    private  float TiempoParaSpawn;

    private Transform camTransform;
    private float camYPosition;
    private int numAnt;
    public int randomNumParaPiedra;
    void Start()
    {
        // Obtener el transform de la cámara
        camTransform = Camera.main.transform;

        TiempoParaSpawn = Random.Range(TiempoParaSpawnMin, TiempoParaSpawnMax);
    }
  
    void Update()
    {
        // Actualizar la posición del eje Y de la cámara
        camYPosition = camTransform.position.y;


        tiempoPasado += Time.deltaTime;
        if (tiempoPasado > TiempoParaSpawn)
        {
            Spawnear();
            TiempoParaSpawn = Random.Range(TiempoParaSpawnMin, TiempoParaSpawnMax);

        }
    }
    public void Spawnear()
    {
        tiempoPasado = 0;
  
         randomNumParaPiedra = Random.Range(1, 6); // aca va (1, numero de obstaculos +1)
        if (numAnt ==  randomNumParaPiedra){
            // esto es para que no salgan muchas veces seguidas el de la piedra del medio o el de las dos pidras por que queda medio mal
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

        //Instanciar

         GameObject nuevoObstaculo = Instantiate(obstaculoElegido, spawnPosition, Quaternion.identity);
    
        // Establecer el objeto padre
        nuevoObstaculo.transform.SetParent(transform);

    }
}