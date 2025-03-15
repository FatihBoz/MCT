using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    [SerializeField]private Vector3 offset;
    [SerializeField]private float damping;
    
    public Transform target;
    private Vector3 vel=Vector3.zero;
    void Start()
    {
        
    }


    void Update()
    {
    Vector3 targetPosition=target.position+offset;
    transform.position=Vector3.SmoothDamp(transform.position,targetPosition,ref vel,damping);        
    }
}
