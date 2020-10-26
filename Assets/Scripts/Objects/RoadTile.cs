using UnityEngine;

public class RoadTile : MonoBehaviour, IMovingObject
{
    private void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.Translate(Vector3.back * GameManager.Instance.Speed * Time.deltaTime);
    }
}
