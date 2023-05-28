using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public UserInput pi;
    public GameObject model;

    public float horizontalSpeed=20.0f;
    public float vecticalSpeed = 20.0f;
    public Image lockDot;
    public bool lockstate;

    private GameObject playerHandle;
    private GameObject cameraHandle;
    private float tempEulerX;
    private Quaternion tempModelRotation;
    private GameObject camera;
    private GameObject lockTarget;
    


    // Start is called before the first frame update
    void Awake()
    {
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
        UserInput[] pis = playerHandle.GetComponents<UserInput>();
        foreach (UserInput p in pis)
        {
            if (p.enabled)
            {
                pi = p;
                break;
            }
        }
        lockDot.enabled = false;
        model = playerHandle.GetComponent<AddController>().model;

        camera = Camera.main.gameObject;

    }
    private void Update()
    {
        if (lockTarget != null)
        {
            lockDot.rectTransform.position = Camera.main.WorldToScreenPoint(lockTarget.transform.position + new Vector3(0, 1f, 0));
            if (Vector3.Distance(model.transform.position, lockTarget.transform.position) > 15.0f)
            {
                lockDot.enabled=false;
                lockstate = false;
                lockTarget = null;
            }
        }

    }
    private void FixedUpdate()
    {
        if(lockTarget == null)
        {
            tempModelRotation = model.transform.rotation;
            playerHandle.transform.rotation *= Quaternion.Euler(0, pi.Jright * horizontalSpeed * Time.fixedDeltaTime, 0);
            tempEulerX += pi.Jup * vecticalSpeed * Time.fixedDeltaTime;
            tempEulerX = Mathf.Clamp(tempEulerX, -40, 30);
            cameraHandle.transform.localRotation = Quaternion.Euler(tempEulerX, 0, 0);
            model.transform.rotation = tempModelRotation;
        }
        else
        {
            Vector3 tempForward = lockTarget.transform.position - model.transform.position;
            tempForward.y = 0;
            playerHandle.transform.forward = tempForward;
        }
            
        camera.transform.position = Vector3.Lerp(camera.transform.position, transform.position, 0.2f);
        camera.transform.LookAt(cameraHandle.transform);
    }

    public void lockUnlock()
    {
        Vector3 modelOrigin1 = model.transform.position;
        Vector3 modelOrigin2 = modelOrigin1 + new Vector3(0, 1, 0);
        Vector3 boxCenter = modelOrigin2 + model.transform.forward * 5.0f;
        Collider[] colliders = Physics.OverlapBox(boxCenter, new Vector3(0.5f, 0.5f, 5f), model.transform.rotation, LayerMask.GetMask("Enemy"));
        if (colliders.Length > 0)
        {
            foreach (var collider in colliders)
            {
                if (collider.gameObject == lockTarget)
                {
                    lockTarget = null;
                    lockDot.enabled = false;
                    lockstate = false;
                }
                else
                {
                    lockTarget = collider.gameObject;
                    lockDot.enabled = true;
                    lockstate = true;
                }
                break;
            }
        }
        else
        {
            lockTarget = null;
            lockstate = false;
            lockDot.enabled = false;
        }
    }
}
