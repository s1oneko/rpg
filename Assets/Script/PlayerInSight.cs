using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class PlayerInSight : Conditional
{
    public SharedTransform target;
    public float fieldOfViewAngle = 45.0f;
    public float viewDistance = 10.0f;

    public override TaskStatus OnUpdate()
    {
        if (target.Value == null)
        {
            return TaskStatus.Failure;
        }

        Vector3 direction = target.Value.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);

        if (angle < fieldOfViewAngle * 0.5f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction.normalized, out hit, viewDistance) && hit.transform == target.Value)
            {
                return TaskStatus.Success;
            }
        }

        return TaskStatus.Running;
    }
}


