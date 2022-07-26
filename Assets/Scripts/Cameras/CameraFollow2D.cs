using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraFollow2D : MonoBehaviour
{
    [SerializeField] float TimeOffset;

    public float LeftLimit, RightLimit, TopLimit, BottomLimit;

    [SerializeField] GameObject player;
    [SerializeField] Vector2 PosOffset;

    private void Update()
    {
        CameraFollow();
        CameraBounds();
    }

    private void CameraFollow()
    {
        Vector3 StartPos = transform.position;
        Vector3 EndPos = player.transform.position;

        EndPos.x += PosOffset.x;
        EndPos.y += PosOffset.y;
        EndPos.z = transform.position.z;

        transform.position = Vector3.Lerp(StartPos, EndPos, TimeOffset * Time.deltaTime);
    }

    private void CameraBounds()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, LeftLimit, RightLimit),
            Mathf.Clamp(transform.position.y, BottomLimit, TopLimit), transform.position.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(new Vector2(LeftLimit, TopLimit), new Vector2(RightLimit, TopLimit)); // Top Line
        Gizmos.DrawLine(new Vector2(LeftLimit, BottomLimit), new Vector2(RightLimit, BottomLimit)); // Bottom Line

        Gizmos.DrawLine(new Vector2(LeftLimit, BottomLimit), new Vector2(LeftLimit, TopLimit)); // Left Line
        Gizmos.DrawLine(new Vector2(RightLimit, TopLimit), new Vector2(RightLimit, BottomLimit)); // Right Line
    }
}
