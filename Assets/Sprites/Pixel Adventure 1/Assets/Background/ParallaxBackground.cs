using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private float startPos, length;
    public GameObject cam;
    public float parallaxEffect; // Speed at which the background should move relative to the camera
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Calculate the distance that the background should move based on camera movement
        float distance = (cam.transform.position.x * parallaxEffect);
        float movement = cam.transform.position.x * (1 - parallaxEffect);

        // Move the background
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        // Infinite scrolling logic: reposition the background when it's out of view
        if (movement > startPos + length)
        {
            startPos += length;
        }
        else if (movement < startPos - length)
        {
            startPos -= length;
        }
    }
}
