using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEditor;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent;
using static BehaviorDesigner.Runtime.BehaviorManager;

public class ChasePlayer : Action
{
    public SharedTransform target;
    [SerializeField]
    private float rotationSpeed = 100f;
    [SerializeField]
    private float chaseSpeed =1.6f;
    [SerializeField]
    private GameObject model;

    private float forward;
    private Transform transform;
    private Animator animator;
    private Rigidbody rb;
    private Vector3 direction;

    public override void OnAwake()
    {
        transform = model.transform;
        animator = Owner.GetComponent<Animator>();
        rb = model.GetComponent<Rigidbody>();
    }
    public override void OnStart()
    {
        target= (SharedTransform)GlobalVariables.Instance.GetVariable("target");
    }
    public override TaskStatus OnUpdate()
    {
        if (target.Value == null)
        {
            return TaskStatus.Failure;
        }
        if (Vector3.Distance(target.Value.position, transform.position) < 1.2f) //arrived
        {
            forward= 0f;
            animator.SetFloat("forward",0f);
            rb.velocity = Vector3.zero;
            return TaskStatus.Success;
        }
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("ground"))
        {
            return TaskStatus.Running;
        }           
        animator.SetFloat("forward", forward * Mathf.Lerp(animator.GetFloat("forward"), forward == 2.0f ? 2.0f : 1.0f, 0.5f));//animation
        if (forward < 2.0f)
        {
            forward += 0.5f * Time.deltaTime;
        }
        else
        {
            forward = 2.0f;
        }
        return TaskStatus.Running;
    }

    public override void OnFixedUpdate()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("ground"))
        {
            return;
        }
        direction = (target.Value.position - transform.position).normalized;
        rb.velocity = new Vector3(direction.x*forward*chaseSpeed, rb.velocity.y, direction.z*forward*chaseSpeed);
        direction.y = 0f;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.fixedDeltaTime);
    }
}
