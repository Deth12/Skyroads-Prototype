using UnityEngine;

public class ShipController : MonoBehaviour
{
    private CharacterController controller;

    [Header("References")] 
    [SerializeField] private CameraController cam = null;
    
    [Header("Movement")]
    [SerializeField] private float strafeSpeed = 2f;

    [Header("Control Bounds")]
    [SerializeField] private float maxX = 3;
    [SerializeField] private float minX = -3;
    
    [Header("Tilting")]
    [SerializeField] private float maxTiltAngle = 45f;
    [SerializeField] private float tiltSpeed = 10f;

    [Header("FX")] 
    [SerializeField] private ParticleSystem enginesFX = null;
    [SerializeField] private ParticleSystem boostFX = null;

    [Header("SFX")] 
    [SerializeField] private AudioClip boostSFX = null;
    [SerializeField] private AudioClip deathSFX = null;

    private bool _isBoostEnabled;
    public bool IsBoostEnabled
    {
        get => _isBoostEnabled;
        set
        {
            QualityManager.Instance.SmoothToggleCA(value);
            _isBoostEnabled = value;
            if (value)
            {
                AudioManager.Instance.PlayOneShot(boostSFX, 1f);
                boostFX.Play();
            }
            else
                boostFX.Stop();
        }
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameStarted)
            return;

        HandleMovement();
        HandleRotation();
        HandleFX();
    }

    private void HandleMovement()
    {
        HandleInput();
    }

    private void HandleRotation()
    {
        Quaternion targetRotation = 
            Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, maxTiltAngle * -InputManager.Instance.Tilt);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * tiltSpeed);
    }

    private void HandleInput()
    {
        if(InputManager.Instance.Left)
            Move(Vector3.left);
        if(InputManager.Instance.Right)
            Move(Vector3.right);
        cam.ChangeFOV(InputManager.Instance.Boost ? FOVMode.fov_boost : FOVMode.fov_default);
    }

    private void HandleFX()
    {
        if (InputManager.Instance.Boost && !IsBoostEnabled)
            IsBoostEnabled = true;
        if (!InputManager.Instance.Boost && IsBoostEnabled)
            IsBoostEnabled = false;
    }

    private void Move(Vector3 direction)
    {
        if (direction == Vector3.left)
            if (transform.position.x > minX)
                controller.Move((direction) * (strafeSpeed * Time.deltaTime));

        if (direction == Vector3.right)
            if (transform.position.x < maxX)
                controller.Move((direction) * (strafeSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Obstacle>())
        {
            DestroyPlayer();
            GameManager.Instance.GameOver();
        }
    }

    private void DestroyPlayer()
    {
        IsBoostEnabled = false;
        enginesFX.Stop();
        AudioManager.Instance.PlayOneShot(deathSFX, 1f);
        LeanTween.rotateAround(gameObject, Vector3.forward, 180f, .5f);
        LeanTween.moveY(gameObject, 0.3f, .5f);
        PoolManager.Instance.GetObject("Explosion", transform.position, Quaternion.identity);
        PoolManager.Instance.GetObject("Smoke", transform.position, Quaternion.identity);
    }
}
