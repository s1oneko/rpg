using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;


public class BossBasic : Action
{
    [SerializeField]
    protected GameObject model;
    [SerializeField]
    protected Animator animator;
    [SerializeField]
    protected Rigidbody rb;    
}
