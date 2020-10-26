using UnityEngine;

public class Obstacle : MonoBehaviour, IMovingObject
{
    [SerializeField] private float rotateSpeed = 1f;

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.Rotate(0, rotateSpeed, 0, Space.Self);
        transform.Translate(Vector3.back * GameManager.Instance.Speed * Time.deltaTime, Space.World);
    }
}
