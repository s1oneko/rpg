using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddController : MonoBehaviour
{
    public GameObject model;
    public UserInput pi;
    public float walkSpeed = 1.0f;
    public float runMuti = 2.0f;
    public float jumpVelocity = 3.8f;
    public float rollVelocity = 5.0f;


    [SerializeField]
    private Animator animator;
    private Rigidbody rb;
    private Vector3 planarVec;
    private Vector3 thrustVec;
    private Vector3 deltaPosition;

    private bool lockPlanar=false;
    private bool canMove=true;

    void Awake()
    {
        animator=model.GetComponent<Animator>();
        UserInput[]pis=GetComponents<UserInput>();
        foreach(UserInput p in pis)
        {
            if (p.enabled)
            {
                pi = p;
                break;
            }
        }
        rb=GetComponent<Rigidbody>();
        animator.SetBool("isEquiped", pi.isEquiped);
    }

    // Update is called once per frame
    void Update()
    {
        if (!lockPlanar)
        {
            planarVec = pi.Dmag * model.transform.forward * walkSpeed * (pi.run ? runMuti : 1.0f) * (pi.isEquiped ? 0.5f : 1f);
        }
        animator.SetFloat("forward", pi.Dmag * Mathf.Lerp(animator.GetFloat("forward"), pi.run ? 2.0f : 1.0f, 0.5f));//平滑起跑
        if (pi.jump)
        {
            animator.SetTrigger("jump");
        }        
        if (pi.lattack)
        {
            if(!pi.isEquiped) 
            {
                animator.SetTrigger("lattack");
            }
            else
            {
                SwitchWeapon();
            }
        }
        if (pi.defence)
        {
            animator.SetTrigger("defence");
        }
        if (pi.rattack)
        {
            if (pi.isEquiped)
            {
                animator.SetTrigger("rattack");
            }
            else
            {
                SwitchWeapon();
            }
        }

        if (pi.Dmag > 0.1f)
        {
            Vector3 targetForward = Vector3.Slerp(model.transform.forward, pi.Dvec,0.1f);//平滑转身
            model.transform.forward = targetForward;
        }
    }
    void FixedUpdate()
    {
        rb.position += deltaPosition*Time.fixedDeltaTime;
        if (canMove)
        {
            rb.velocity = new Vector3(planarVec.x, rb.velocity.y, planarVec.z) + thrustVec;//y向只有原来速度和冲力
        }
        thrustVec = Vector3.zero;
        deltaPosition = Vector3.zero;
    }
    public void SwitchWeapon()
    {
        pi.isEquiped = !pi.isEquiped;
        animator.SetTrigger("switchWeapon");
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
        canMove = true;
        pi.inputEnabled = true;
        lockPlanar = false;
    }
    public void OnGroundExit()
    {
        pi.inputEnabled = false;
        lockPlanar = true;
    }
    public void OnIdleEnter()
    {
        canMove = true;
        pi.inputEnabled = true;
        lockPlanar = false;
    }
    public void OnIdleExit()
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
    public void OnUpdateRootMotion(object _deltaPosition)
    {
        deltaPosition +=model.transform.forward*(float)_deltaPosition;
    }
    public void ClearVelocity()
    {
        canMove = false;
    }
    public void EnableVelocity()
    {
        canMove = true;
    }
    public void OnDefenceEnter() 
    {
        animator.SetBool("isEquiped", pi.isEquiped);
        animator.ResetTrigger("defence");
    }
    public void OnDefenceExit()
    {
        animator.ResetTrigger("defence");
    }
}
