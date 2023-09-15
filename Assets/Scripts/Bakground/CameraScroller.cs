using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroller : MonoBehaviour
{
    [SerializeField] PezDeBarraDeProgreso[] progressBar;
    public float[] scrollingSpeedForEachPhase;
    
    private float scrollingSpeed;

    public float ScrollingSpeed { get => scrollingSpeed;}

    private void Awake()
    {
        scrollingSpeed = scrollingSpeedForEachPhase[0];
    }

    private void Start()
    {
        transform.position = new Vector3 (0, 0, -10);  
    }

    private void OnEnable()
    {
        if (progressBar[0] != null)
            progressBar[0].OnChangePhase += ChangeScrollingSpeed;
    }

    private void OnDisable()
    {
        if (progressBar[0] != null)
            progressBar[0].OnChangePhase -= ChangeScrollingSpeed;
    }

    void ChangeScrollingSpeed(int phase)
    {
        if (phase < scrollingSpeedForEachPhase.Length)
            scrollingSpeed = scrollingSpeedForEachPhase[phase];   
    }
  
    void FixedUpdate()
    {
        transform.Translate(0, ScrollingSpeed * Time.deltaTime, 0);
    }
}
