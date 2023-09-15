using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerUp : MonoBehaviour
{
    [Header("Tiempo Para Spawnear")]

    [SerializeField] private  float TiempoParaSpawnMin ;//, limiteYArrBrocoli,LimiteYabajoBrocoli,poisicionXBrocoli,limiteXizqFrutilla,limiteXderFrutilla,posicionYfutilla;
    [SerializeField] private  float TiempoParaSpawnMax;
   
    [Header("GameObjets de powerup(Prefabs)")]

    [SerializeField] private  GameObject vidaExtra;
    [SerializeField] private  GameObject Escudito;
    

    [Header("Posiciones")]

    [SerializeField] private  float posicionYparaPowerUp;
    [SerializeField] private  float limiteXizquierdo;
    [SerializeField] private  float limiteXderecho;


    private  Vector2 spawnPosition;
    private  GameObject powerUpElegido;
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
  
         randomNumParaPiedra = Random.Range(1, 3); // aca va (1, numero de cantidad de powerups +1)
     
        switch (randomNumParaPiedra)
        {
           
            case 1:
                powerUpElegido = vidaExtra.gameObject;
                spawnPosition = new Vector2( Random.Range(limiteXizquierdo, limiteXderecho),posicionYparaPowerUp + camYPosition);
                break;
            case 2:
                powerUpElegido = Escudito.gameObject;
                spawnPosition = new Vector2( Random.Range(limiteXizquierdo, limiteXderecho),posicionYparaPowerUp + camYPosition);
                break;
           
        }

        GameObject nuevoPowerUp = Instantiate(powerUpElegido, spawnPosition, Quaternion.identity);
    
        
        nuevoPowerUp.transform.SetParent(transform);

    }
}
