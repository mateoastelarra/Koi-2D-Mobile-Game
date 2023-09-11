using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    private static SFXManager instance;

    [SerializeField] FMODUnity.EventReference obstacleCrashSound;
    [SerializeField] FMODUnity.EventReference gongSound;
    [SerializeField] FMODUnity.EventReference UISound;
    [SerializeField] FMODUnity.EventReference drumSound;
    [SerializeField] FMODUnity.EventReference shieldSound;
    [SerializeField] FMODUnity.EventReference lifeUpSound;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public static SFXManager GetInstance()
    {
        return instance;
    }

    public void PlayCrashSound(GameObject gameObject)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(obstacleCrashSound, gameObject);
    }

    public void PlayGongSound(GameObject gameObject)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(gongSound, gameObject);
    }

    public void PlayDrumSound(GameObject gameObject)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(drumSound, gameObject);
    }

    public void PlayShieldSound(GameObject gameObject)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(shieldSound, gameObject);
    }

    public void PlayLifeUpSound(GameObject gameObject)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(lifeUpSound, gameObject);
    }

    public void PlayUISound(GameObject gameObject)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(UISound, gameObject);
    }
}
