using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand : BossBasic
{
    [SerializeField]
    private float duration = 3f;
    private float nowTime = 0f;
    public override void OnAwake()
    {
        rb = model.GetComponent<Rigidbody>();
        animator = Owner.GetComponent<Animator>();
    }
    public override void OnStart()
    {
        nowTime = 0;
        animator.SetFloat("forward", 0f);
        animator.SetFloat("right", 0f);
    }
    public override TaskStatus OnUpdate()
    {
        nowTime += Time.deltaTime;
        if (nowTime>duration)
        {
            model.transform.localRotation*=Quaternion.Euler(0, -90, 0);
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }
}
