using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public FMODUnity.EventReference obstacleCrashSound;
    public FMOD.Studio.EventInstance instance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Instancia");
            instance = FMODUnity.RuntimeManager.CreateInstance(obstacleCrashSound);
            instance.start();
            instance.release();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Obstaculo"))
        {
            Debug.Log("boing");
            

        }

    }
}
