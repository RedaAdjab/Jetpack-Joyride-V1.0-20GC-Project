using UnityEngine;

public class LaserController : MonoBehaviour
{
    [SerializeField] private GameObject laser;

    private float spawnDelayTimer = 5f;
    private float lifeTime = 5f;
    private bool isActivated = false;
    private float timer = 0f;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnDelayTimer && !isActivated)
        {
            laser.SetActive(true);
            isActivated = true;
            timer = 0;
        }

        if (timer >= lifeTime || gameManager.GameIsGameOver())
        {
            SpawnManager.Instance.SetSpawnPermissions(true, true, true);
            Destroy(gameObject);
        }
    }
}
