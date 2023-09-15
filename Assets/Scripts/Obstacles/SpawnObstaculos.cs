using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstaculos : MonoBehaviour
{
    [SerializeField] private float[] timeBetweenSpawnForEachPhase;
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private float xLeftBound;
    [SerializeField] private float xRightBound;
    [SerializeField] private PezDeBarraDeProgreso progressBar;

    private float timeBetweenSpawns;
    private int velocityPhase;
    private  float elapsedTime;
    private int prevObstacleNum;
    
    void Start()
    {
        timeBetweenSpawns = timeBetweenSpawnForEachPhase[0];
    }
  
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > timeBetweenSpawns)
        {
            SpawnObstacle();
        }
    }

    private void OnEnable()
    {
        progressBar.OnChangePhase += ChangeSpawnTime;
    }

    private void OnDisable()
    {
        progressBar.OnChangePhase -= ChangeSpawnTime;
    }

    void ChangeSpawnTime(int phase)
    {
        velocityPhase = phase;
        if (phase < timeBetweenSpawnForEachPhase.Length)
            timeBetweenSpawns = timeBetweenSpawnForEachPhase[phase];
    }

    void SpawnObstacle()
    {
        elapsedTime = 0;

        int obstacleNum = Random.Range(0, obstacles.Length);

        if (prevObstacleNum == obstacleNum)
        {
            obstacleNum = Random.Range(0, obstacles.Length);
        }

        prevObstacleNum = obstacleNum;

        GameObject obstacleToSpawn = obstacles[obstacleNum];

        Vector2 spawnPosition;

        if (obstacleToSpawn.name == "roca Random")
        {
            float posX = Random.Range(xLeftBound, xRightBound);
            spawnPosition = new Vector2(posX, transform.position.y);
        }
        else
        {
            spawnPosition = new Vector2(0, transform.position.y);
        }

        GameObject newObstacle = Instantiate(obstacleToSpawn, spawnPosition, Quaternion.identity);

        newObstacle.GetComponent<Obstaculos>().SetVelocity(velocityPhase);

        newObstacle.transform.SetParent(transform);
    }

}
