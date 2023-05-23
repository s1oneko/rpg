using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class PlayerInSight : Conditional
{
    public SharedGameObject targetObject;
    public SharedFloat fieldOfViewAngle = 90;
    public SharedFloat viewDistance = 1000;
    public SharedGameObject returnedObject;

    public override TaskStatus OnUpdate()
    {
        returnedObject.Value = WithinSight(targetObject.Value, fieldOfViewAngle.Value, viewDistance.Value);
        if (returnedObject.Value != null)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }

    /// <summary>
    /// Determines if the targetObject is within sight of the transform.
    /// </summary>
    private GameObject WithinSight(GameObject targetObject, float fieldOfViewAngle, float viewDistance)
    {
        if (targetObject == null)
        {
            return null;
        }

        var direction = targetObject.transform.position - transform.position;
        direction.y = 0;
        var angle = Vector3.Angle(direction, transform.forward);
        if (direction.magnitude < viewDistance && angle < fieldOfViewAngle * 0.5f)
        {
            // The hit agent needs to be within view of the current agent
            if (LineOfSight(targetObject))
            {
                return targetObject; // return the target object meaning it is within sight
            }
        }
        return null;
    }

    /// <summary>
    /// Returns true if the target object is within the line of sight.
    /// </summary>
    private bool LineOfSight(GameObject targetObject)
    {
        RaycastHit hit;
        if (Physics.Linecast(transform.position, targetObject.transform.position, out hit))
        {
            if (hit.transform.IsChildOf(targetObject.transform) || targetObject.transform.IsChildOf(hit.transform))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Draws the line of sight representation
    /// </summary>
    public override void OnDrawGizmos()
    {
#if UNITY_EDITOR
        var oldColor = UnityEditor.Handles.color;
        var color = Color.yellow;
        color.a = 0.1f;
        UnityEditor.Handles.color = color;
        var halfFOV = fieldOfViewAngle.Value * 0.5f;
        var beginDirection = Quaternion.AngleAxis(-halfFOV, Vector3.up) * Owner.transform.forward;
        UnityEditor.Handles.DrawSolidArc(Owner.transform.position, Owner.transform.up, beginDirection, fieldOfViewAngle.Value, viewDistance.Value);
        UnityEditor.Handles.color = oldColor;
#endif
    }
}


