using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstaculos : MonoBehaviour
{
    [Header("Tiempo Para Spawnear")]

 //   [SerializeField] private  float TiempoParaSpawnMin ;//, limiteYArrBrocoli,LimiteYabajoBrocoli,poisicionXBrocoli,limiteXizqFrutilla,limiteXderFrutilla,posicionYfutilla;
 //   [SerializeField] private  float TiempoParaSpawnMax;
       private  float TiempoParaSpawn;
      [SerializeField] private  float TiempoParaSpawn1;
      [SerializeField] private  float TiempoParaSpawn2;
      [SerializeField] private  float TiempoParaSpawn3;

       
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
    //private  float TiempoParaSpawn;

    private Transform camTransform;
    private float camYPosition;
    private int numAnt;
    public int randomNumParaPiedra;


    private float cronometro = 0.0f;
    private PezDeBarraDeProgreso barra;
    private float tiempoTotalVar = 120f; // Tiempo total para llegar de A a B en segundos.


    void Start()
    {
        // Obtener el transform de la cámara
        camTransform = Camera.main.transform;

        //TiempoParaSpawn = Random.Range(TiempoParaSpawnMin, TiempoParaSpawnMax);
        barra = GameObject.Find("ManagerBarra").GetComponent<PezDeBarraDeProgreso>();
        SetearTiempoSpawn();

    }
  
    void Update()
    {
        cronometro = barra.devolverCronometro();

        // Actualizar la posición del eje Y de la cámara
        camYPosition = camTransform.position.y;


        tiempoPasado += Time.deltaTime;
        if (tiempoPasado > TiempoParaSpawn)
        {
            Spawnear();
            SetearTiempoSpawn();
        }
    }


    public void SetearTiempoSpawn()
    {
        float tiempo1 = tiempoTotalVar * 0.33333333333f;
        float tiempo2 = tiempoTotalVar * 0.66666666666f;




        if (cronometro > 0f && cronometro < tiempo1)
        {
            TiempoParaSpawn = TiempoParaSpawn1;
        }
         
        if (cronometro > tiempo1 && cronometro < tiempo2)
        {            
            TiempoParaSpawn = TiempoParaSpawn2;

        }
        if (cronometro > tiempo2 )
        {
            TiempoParaSpawn = TiempoParaSpawn3;

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

        GameObject nuevoObstaculo = Instantiate(obstaculoElegido, spawnPosition, Quaternion.identity);
    
        // Establecer el objeto padre
        nuevoObstaculo.transform.SetParent(transform);

    }


}
