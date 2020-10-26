using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    private void Awake()
    {
        if(Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    [SerializeField] private KeyCode leftPrimaryKey = KeyCode.A;
    [SerializeField] private KeyCode leftSecondaryKey = KeyCode.LeftArrow;
    [SerializeField] private KeyCode rightPrimaryKey = KeyCode.D;
    [SerializeField] private KeyCode rightSecondaryKey = KeyCode.RightArrow;
    [SerializeField] private KeyCode boostPrimaryKey = KeyCode.Space;

    public bool Left 
    { 
        get;
        private set;
    }
    public bool Right
    {
        get; 
        private set;
    }
    public bool Boost
    {
        get;
        private set;
    }

    public int Tilt
    {
        get
        {
            if (Left && Right)
                return 0;
            if (Left)
                return -1;
            if (Right)
                return 1;
            return 0;
        }
    }

    private void Update()
    {
        Left = Input.GetKey(leftPrimaryKey) || Input.GetKey(leftSecondaryKey);
        Right = Input.GetKey(rightPrimaryKey) || Input.GetKey(rightSecondaryKey);
        Boost = Input.GetKey(boostPrimaryKey);
    }
}
