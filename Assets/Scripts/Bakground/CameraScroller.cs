using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroller : MonoBehaviour
{
    public float scrollingSpeed = 1;
    private void Start()
    {
        transform.position = new Vector3 (0, 0, -10);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(0, scrollingSpeed * Time.deltaTime, 0);
    }
}
