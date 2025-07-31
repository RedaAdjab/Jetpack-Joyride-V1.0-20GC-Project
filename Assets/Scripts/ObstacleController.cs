using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float spinSpeed = 10f;
    private bool canSpin = false;

    private void Update()
    {
        MoveLeft();
        if (canSpin)
        {
            Spin();
        }
    }

    private void MoveLeft()
    {
        transform.position = transform.position + moveSpeed * Time.deltaTime * Vector3.left;
    }

    private void Spin()
    {
        transform.Rotate(Vector3.back, spinSpeed * Time.deltaTime);
    }

    public void AddSpin()
    {
        canSpin = true;
    }

    public void SetSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }
}
