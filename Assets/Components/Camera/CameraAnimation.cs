using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraAnimation : MonoBehaviour
{
    public float speed = 1f;

    [SerializeField] private CameraPoint startingPoint;

    private CameraPoint target;

    private void Awake()
    {
        if (startingPoint == null)
        {
            Debug.LogError("CameraAnimation: StartingPoint atanmad�! L�tfen sahnede bir CameraPoint ata.");
            return;
        }

        target = startingPoint;
        transform.position = target.transform.position;
    }

    private void Update()
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);

        if (Vector3.Distance(transform.position, target.transform.position) < 0.05f)
        {
            target = target.GetNextPoint();
        }
    }
}
