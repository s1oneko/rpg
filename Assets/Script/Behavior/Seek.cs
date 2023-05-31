using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Seek : BossBasic
{
    [SerializeField]
    private float duration = 7f;
    [SerializeField]
    private float walkspeed = 1f;
    private float nowTime = 0f;

    private void Seeking()
    {
        nowTime = 0;
        animator.SetFloat("forward", 1f);
        animator.SetFloat("right", 1f);
    }
    public override void OnAwake()
    {
        rb = model.GetComponent<Rigidbody>();
        animator = Owner.GetComponent<Animator>();
    }
    public override void OnStart()
    {
        Seeking();
    }
    public override void OnFixedUpdate()
    {
        nowTime += Time.fixedDeltaTime;
        if (nowTime <= duration)
        {
            rb.velocity = (model.transform.forward+model.transform.right) * walkspeed;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
    public override TaskStatus OnUpdate()
    {
        if (nowTime > duration)
        { 
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }

}
