using UnityEngine;

public class RocketController : MonoBehaviour
{
    private float moveSpeed = 10f;

    private void Update()
    {
        MoveLeft();
    }

    private void MoveLeft()
    {
        transform.position = transform.position + moveSpeed * Time.deltaTime * Vector3.left;
    }

    public void SetSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }
}
