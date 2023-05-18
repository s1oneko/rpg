using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddController : MonoBehaviour
{
    public GameObject model;
    public PlayerInput pi;
    public float walkSpeed = 1.0f;
    public float runMuti = 2.0f;
    public float jumpVelocity = 3.8f;
    public float rollVelocity = 5.0f;


    [SerializeField]
    private Animator animator;
    private Rigidbody rb;
    private Vector3 planarVec;
    private Vector3 thrustVec;

    private bool lockPlanar=false;

    void Awake()
    {
        animator=model.GetComponent<Animator>();
        pi=GetComponent<PlayerInput>();
        rb=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("forward",pi.Dmag* Mathf.Lerp(animator.GetFloat("forward"), pi.run ? 2.0f : 1.0f, 0.5f));//平滑起跑
        if (pi.jump)
        {
            animator.SetTrigger("jump");
        }
        if (pi.attack && animator.GetCurrentAnimatorStateInfo(animator.GetLayerIndex("Base Layer")).fullPathHash == Animator.StringToHash("Base Layer.ground")&&!animator.IsInTransition(animator.GetLayerIndex("Base Layer")))
        {
            animator.SetTrigger("attack");
        }
        if (pi.Dmag > 0.1f)
        {
            Vector3 targetForward = Vector3.Slerp(model.transform.forward, pi.Dvec,0.1f);//平滑转身
            model.transform.forward = targetForward;
        }
        if (!lockPlanar)
        {
            planarVec = pi.Dmag * model.transform.forward * walkSpeed * (pi.run ? runMuti : 1.0f);
        }
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector3(planarVec.x, rb.velocity.y, planarVec.z) + thrustVec;//y向只有原来速度和冲力
        thrustVec = Vector3.zero;
    }
    /// <summary>
    /// Message processing
    /// </summary>
    public void OnJumpEnter()
    {
        thrustVec=new Vector3(0,jumpVelocity,0);
    }
    public void isGround()
    {
        animator.SetBool("isGround", true);
    }
    public void isNotGround()
    {
        animator.SetBool("isGround", false);
        if(rb.velocity.sqrMagnitude > 50.0f )
        {
            animator.SetTrigger("roll");
        }
    }
    public void OnGroundEnter()
    {
        pi.inputEnabled = true;
        lockPlanar = false;
    }
    public void OnGroundExit()
    {
        pi.inputEnabled = false;
        lockPlanar = true;
    }
    public void OnRollUpdate()
    {
        thrustVec = new Vector3(0, animator.GetFloat("rollHeight"), 0) + model.transform.forward* animator.GetFloat("jumpVelocity");
    }
    public void OnJabUpdate()
    {
        thrustVec = model.transform.forward * animator.GetFloat("jumpVelocity");
    }
    public void OnAttack_1h_AEnter()
    {
        animator.SetLayerWeight(1, 1.0f);//attack为第一层
    }
    public void OnAttack_1h_AUpdate()
    {
        thrustVec = model.transform.forward * animator.GetFloat("attack_1h_AVelocity");
    }
    public void OnAttackIdleEnter()
    {
        pi.inputEnabled = true;
        animator.SetLayerWeight(1, 0);//attack为第一层
    }
    public void OnAttackIdleExit()
    {
        pi.inputEnabled = false;
    }
}
