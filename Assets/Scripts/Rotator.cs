using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Transform target = null;
    [SerializeField] private float rotationSpeed = 10.0f;
    private Vector3 point;
    
   private void Start () 
   {
        point = target.transform.position;
        transform.LookAt(point);
   }
   
    private void Update ()
    {
        transform.RotateAround (point,Vector3.up, Time.deltaTime * rotationSpeed);
    }
}
