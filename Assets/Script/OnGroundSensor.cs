using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundSensor : MonoBehaviour
{
    public CapsuleCollider capsuleCollider;
    private Vector3 point1;
    private Vector3 point2;
    private float radius;
    // Start is called before the first frame update
    void Awake()
    {
        radius=capsuleCollider.radius;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        point1 = transform.position + transform.up * (radius-0.05f);
        point2 = transform.position + transform.up * (capsuleCollider.height-0.05f) - transform.up * radius;
        Collider[] outputColliders = Physics.OverlapCapsule(point1, point2, radius,LayerMask.GetMask("Ground"));
        if (outputColliders.Length!=0)
        {
            SendMessageUpwards("isGround");
        }
        else
        {
            SendMessageUpwards("isNotGround");
        }
    }
}
