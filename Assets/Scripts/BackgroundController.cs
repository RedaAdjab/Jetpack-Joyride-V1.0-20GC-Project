using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private float worldMoveSpeed = 3f;
    private float bgSizeX = 57.6f;
    private float bgRestPoint = -103;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        if (gameManager.GameIsPlaying())
        {
            transform.position = transform.position + worldMoveSpeed * Time.deltaTime * Vector3.left;
            if (transform.position.x <= bgRestPoint)
            {
                transform.position = transform.position + new Vector3(bgSizeX, 0f, 0f);
            }
        }
    }

    public float GetWorldMoveSpeed()
    {
        return worldMoveSpeed;
    }

    public void SetWorldMoveSpeed(float worldMoveSpeed)
    {
        this.worldMoveSpeed = worldMoveSpeed;
    }
}
