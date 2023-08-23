using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startPosition;
    public Camera mainCamera;
    public float parallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float relativePos = mainCamera.transform.position.y * (1 - parallaxEffect);

        float distance = mainCamera.transform.position.y * parallaxEffect;

        transform.position = new Vector3(transform.position.x, 
                                         startPosition + distance, 
                                         transform.position.z);
        
        if (relativePos > startPosition + length) startPosition += length;
        
    }
}