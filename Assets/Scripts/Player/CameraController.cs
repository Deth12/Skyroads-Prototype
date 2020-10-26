using UnityEngine;

public enum FOVMode
{    
    fov_default, 
    fov_boost
}

public class CameraController : MonoBehaviour
{
    private Camera cam;
    private Transform target;
    
    [SerializeField] private float followSpeed = 10f;

    [Header("FOV Settings")] 
    [SerializeField] private float fovSpeed = 10f;
    [SerializeField] private float defaultFOV = 60f;
    [SerializeField] private float sprintFOV = 70f;

    private float targetFOV;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        cam = GetComponentInChildren<Camera>();
        targetFOV = defaultFOV;
    }

    private void Update()
    {
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, fovSpeed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * followSpeed);
    }

    public void ChangeFOV(FOVMode f)
    {
        switch (f)
        {
            case FOVMode.fov_default:
                targetFOV = defaultFOV;
                break;
            case FOVMode.fov_boost:
                targetFOV = sprintFOV;
                break;
        }
    }
}
