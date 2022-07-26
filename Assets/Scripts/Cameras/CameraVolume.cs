using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVolume : MonoBehaviour
{
    [SerializeField] CameraFollow2D cam;
    [SerializeField] float LeftLimit, RightLimit, TopLimit, BottomLimit;


    private void OnCollisionEnter2D(Collision2D other)
    {
        print(other.gameObject.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            cam.LeftLimit = LeftLimit;
            cam.RightLimit = RightLimit;
            cam.TopLimit = TopLimit;
            cam.BottomLimit = BottomLimit;
        }
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            cam.LeftLimit = LeftLimit;
            cam.RightLimit = RightLimit;
            cam.TopLimit = TopLimit;
            cam.BottomLimit = BottomLimit;
        }
    }
}
