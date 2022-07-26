using UnityEngine;
using UnityEngine.Tilemaps;

public class BackgroundParallax : MonoBehaviour
{
    private  float length, startPos;
    public float parallaxSpeed;

    public Camera cam;

    private void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<TilemapRenderer>().bounds.size.x;

        cam = Camera.main;
    }


    private void FixedUpdate()
    {
        float temp = cam.transform.position.x * (1 - parallaxSpeed);
        float distance = cam.transform.position.x * parallaxSpeed;

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if (temp > startPos + length) startPos += length;
        else if (temp < startPos - length) startPos -= length;
    }
}
