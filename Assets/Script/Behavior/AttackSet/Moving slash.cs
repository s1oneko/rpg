using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class Movingslash : BossAttack
{
    public override void OnAwake()
    {
        animator = Owner.GetComponent<Animator>();
        rb = model.GetComponent<Rigidbody>();
    }
    public override void OnStart()
    {
        take_move(3);
    }
    public override TaskStatus OnUpdate()
    {
        return hitOrMiss();
    }
}
