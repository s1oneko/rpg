using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Vector3 deltaPosition;
    public GameObject model;
    public Animator animator;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        rb.position += deltaPosition * Time.fixedDeltaTime;
        deltaPosition = Vector3.zero;
    }
    public void OnUpdateRootMotion(object _deltaPosition)
    {
        deltaPosition += model.transform.forward * (float)_deltaPosition;
    }
    public void OnGroundEnter()
    {
        animator.SetInteger("skill_number", 0);
    }
}
