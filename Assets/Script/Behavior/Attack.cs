using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEditor;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent;
using static BehaviorDesigner.Runtime.BehaviorManager;


public class Attack : Action 
{
    private Animator animator;
    public override void OnAwake()
    {
        animator = Owner.GetComponent<Animator>();
    }
    public override TaskStatus OnUpdate()
    {
        animator.SetTrigger("attack");
        return TaskStatus.Success;
    }
}
