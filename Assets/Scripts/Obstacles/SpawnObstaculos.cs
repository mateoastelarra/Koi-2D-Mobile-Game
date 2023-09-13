using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstaculos : MonoBehaviour
{
    [Header("changes for each phase")]
    [SerializeField] private float[] timeBetweenSpawnForEachPhase;
    private float timeBetweenSpawns;
    private int velocityPhase;

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
    private  float elapsedTime;
    

    private Transform camTransform;
    //private float camYPosition;
    private int numAnt;
    public int randomNumParaPiedra;

    void Start()
    {
        camTransform = Camera.main.transform;

        timeBetweenSpawns = timeBetweenSpawnForEachPhase[0];
    }
  
    void Update()
    {
        // Update cam y position
        //camYPosition = camTransform.position.y;

        elapsedTime += Time.deltaTime;
        if (elapsedTime > timeBetweenSpawns)
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
        velocityPhase = phase;
    }

    public void Spawnear()
    {
        elapsedTime = 0;
  
        randomNumParaPiedra = Random.Range(1, 6); // aca va (1, numero de obstaculos +1)

        if (numAnt ==  randomNumParaPiedra){
            // esto es para que no salgan muchas veces seguidas el de la piedra del medio o el de las dos piedras por que queda medio mal
             randomNumParaPiedra = Random.Range(1, 6);
        }

        switch (randomNumParaPiedra)
        {
           
            case 1:
                obstaculoElegido = piedraRandom.gameObject;
                spawnPosition = new Vector2( Random.Range(limiteXizquierdo, limiteXderecho),posicionYparaObstaculos + camTransform.position.y);
                break;
            case 2:
                obstaculoElegido = piedraDoble.gameObject;
                spawnPosition = new Vector2( 0,posicionYparaObstaculos + camTransform.position.y);
                break;
            case 3:
                obstaculoElegido = piedraMedio.gameObject;
                spawnPosition = new Vector2( 0,posicionYparaObstaculos + camTransform.position.y);
                break;
             case 4:
                obstaculoElegido = rama.gameObject;
                spawnPosition = new Vector2( 0,posicionYparaObstaculos + camTransform.position.y);
                break;    
             case 5:
                obstaculoElegido = rama.gameObject;
                spawnPosition = new Vector2( 0,posicionYparaObstaculos + camTransform.position.y);
                break; 
                     
        }

        numAnt =  randomNumParaPiedra;

        GameObject newObstacle = Instantiate(obstaculoElegido, spawnPosition, Quaternion.identity);

        newObstacle.GetComponent<Obstaculos>().SetVelocity(velocityPhase);
    
        newObstacle.transform.SetParent(transform);

    }


}
