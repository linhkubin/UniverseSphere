using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] Transform child;
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] float speed = 5f;

    // Update is called once per frame
    void Update()
    {
        UpdateDirect();
        UpdateMove();
    }

    public void UpdateDirect()
    {
        if (JoystickControl.direct.sqrMagnitude > 0)
        {
            Vector3 direct = JoystickControl.direct;
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, direct);
            child.localRotation = rot;
        }
    }

    public void UpdateMove()
    {
        if (JoystickControl.direct.sqrMagnitude > 0)
        {
            Vector3 newPoint = child.position + child.forward * speed * Time.deltaTime;
            (transform.position, transform.up) = GetPoint(newPoint, 1f);
        }
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
