using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPoint : MonoBehaviour
{
    [SerializeField] private CameraPoint nextPoint;

    public CameraPoint GetNextPoint()
    {
        return nextPoint;   
    }
}
