using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public UserInput pi;
    public GameObject model;

    public float horizontalSpeed=20.0f;
    public float vecticalSpeed = 20.0f;

    private GameObject playerHandle;
    private GameObject cameraHandle;
    private float tempEulerX;
    private Quaternion tempModelRotation;
    private GameObject camera;

    private Quaternion lastRotation;
    private Quaternion lastLocalRotation;
    private Vector3 lastPosition;

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
        model = playerHandle.GetComponent<AddController>().model;

        lastPosition = model.transform.position;
        lastLocalRotation = cameraHandle.transform.localRotation;
        lastRotation = transform.rotation;

        camera = Camera.main.gameObject;

    }
    private void FixedUpdate()
    {
        tempModelRotation = model.transform.rotation;
        playerHandle.transform.rotation *= Quaternion.Euler(0, pi.Jright * horizontalSpeed * Time.fixedDeltaTime, 0);
        tempEulerX += pi.Jup * vecticalSpeed * Time.fixedDeltaTime;
        tempEulerX = Mathf.Clamp(tempEulerX, -40, 30);
        cameraHandle.transform.localRotation = Quaternion.Euler(tempEulerX, 0, 0);
            
        camera.transform.position = Vector3.Lerp(camera.transform.position, transform.position, 0.2f);
        camera.transform.LookAt(cameraHandle.transform);
        model.transform.rotation = tempModelRotation;
    }
}
