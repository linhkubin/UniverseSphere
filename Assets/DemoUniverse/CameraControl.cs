using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()   
    {
        Vector3 newPoint = target.position + target.forward * offset.z;
        (transform.position, transform.up) = GetPoint(newPoint, offset.y);
        transform.up = target.up;
        Debug.DrawLine(transform.position, Vector3.zero);
    }

    public (Vector3, Vector3) GetPoint(Vector3 newPoint, float height)
    {
        RaycastHit hit;
        Vector3 origin = (newPoint - Vector3.zero).normalized;

        if (Physics.Raycast(origin * 100, Vector3.zero - origin, out hit, 90, groundLayerMask))
        {
            return (hit.point + origin * height, origin);
        }
        return (Vector3.zero, Vector3.up);
    }

}
