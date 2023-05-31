using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inVision : Conditional
{
    [SerializeField]
    private Transform detectionCenter;
    [SerializeField]
    private float detectionRange;
    [SerializeField]
    private LayerMask isTarget;
    [SerializeField]
    private LayerMask barricade;
    [SerializeField, Header("Target")]
    private Transform currentTarget;

    Collider[] colliderTarget = new Collider[10];

    private bool View()
    {
        int targetCount = Physics.OverlapSphereNonAlloc(detectionCenter.position, detectionRange, colliderTarget, isTarget);
        if(targetCount > 0)
        {
            if (!Physics.Raycast((transform.root.position + transform.root.up * 0.5f), (colliderTarget[0].transform.position-transform.root.position).normalized,out var hit, detectionRange, barricade))
            {
                if(Vector3.Dot((colliderTarget[0].transform.position - transform.root.position).normalized,transform.root.forward)>0.35f){
                    currentTarget = colliderTarget[0].transform;
                    return true;
                }
            }
        }
        return false;
    }

    public override TaskStatus OnUpdate()
    {
        if (View())
        {
            GlobalVariables.Instance.GetVariable("target").SetValue(currentTarget);
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }

/*    public override void OnDrawGizmos()
    {
#if UNITY_EDITOR
        var oldColor = UnityEditor.Handles.color;
        var color = Color.yellow;
        color.a = 0.1f;
        UnityEditor.Handles.color = color;

        float cosineThreshold = 0.35f;
        float angleInDegrees = Mathf.Acos(cosineThreshold) * (180.0f / Mathf.PI);
        float halfFOV = angleInDegrees * 0.5f;

        UnityEditor.Handles.DrawSolidArc(transform.root.position, transform.root.up, Quaternion.AngleAxis(-halfFOV, transform.root.up) * transform.root.forward, angleInDegrees, detectionRange);

        UnityEditor.Handles.color = oldColor;
#endif
    }*/



}
