using UnityEngine;

public class SpawnPowerUp : MonoBehaviour
{
    [Header("Spawning Time")]

    [SerializeField] private  float minSpawnTime ;
    [SerializeField] private  float maxSpawnTime;

    [SerializeField] private GameObject[] powerUps;
    
    [Header("Positions")]

    [SerializeField] private  float leftXBound;
    [SerializeField] private  float rightXBound;

    private  float elapsedTime;
    private  float timeToSpawn;
    
    void Start()
    { 
        timeToSpawn = Random.Range(minSpawnTime, maxSpawnTime);
    }
  
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > timeToSpawn)
        {
            Spawn();
            timeToSpawn = Random.Range(minSpawnTime, maxSpawnTime);

        }
    }

    void Spawn()
    {
        elapsedTime = 0;

        int randomNumberToChoosePowerUp = Random.Range(0, powerUps.Length);

        GameObject chosenPowerUp = powerUps[randomNumberToChoosePowerUp];

        Vector2 spawnPosition = new Vector2( Random.Range(leftXBound, rightXBound), transform.position.y); 

        GameObject nuevoPowerUp = Instantiate(chosenPowerUp, spawnPosition, Quaternion.identity);
    
        nuevoPowerUp.transform.SetParent(transform);
    }
}
