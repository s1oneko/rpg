using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEditor;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent;
using static BehaviorDesigner.Runtime.BehaviorManager;

public class ChasePlayer : Action
{
    public SharedTransform target;
    public GameObject model;
    public float rotationSpeed = 100f;
    private float forward;
    private Transform transform;
    private Animator animator;

    public override void OnAwake()
    {
        transform = model.transform;
        animator = Owner.GetComponent<Animator>();
    }
    public override TaskStatus OnUpdate()
    {
        if (target.Value == null)
        {
            return TaskStatus.Failure;
        }
        if (Vector3.Distance(target.Value.position, transform.position) < 1f)
        {
            forward= 0f;
            animator.SetFloat("forward",0f);
            return TaskStatus.Success;
        }
        animator.SetFloat("forward", forward * Mathf.Lerp(animator.GetFloat("forward"), forward == 2.0f ? 2.0f : 1.0f, 0.5f));
        if (forward < 2.0f)
        {
            forward += 0.1f * Time.deltaTime;
        }
        else
        {
            forward = 2.0f;
        }
        return TaskStatus.Running;
    }

    public override void OnFixedUpdate()
    {
        Vector3 newPosition = Vector3.MoveTowards(transform.position, target.Value.position, forward * Time.fixedDeltaTime);
        transform.position = newPosition;

        Vector3 direction = (target.Value.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.fixedDeltaTime);
    }
}
