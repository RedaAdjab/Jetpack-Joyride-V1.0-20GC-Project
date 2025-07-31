using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private GameObject rocketPrefab;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private bool canSpawnObstacles = true;
    [SerializeField] private bool canSpawnRockets = true;
    [SerializeField] private bool canSpawnLasers = true;


    private int obstacleSpawnBounds = 4;
    private float obstacleTimer = 0f;
    private float obstacleSpawnFreq = 2f; //seconds
    private int minObstacleHeight = 1;
    private int maxObstacleHeight = 5;
    private float rocketTimer = 0f;
    private float rocketSpawnFreq = 10f; //seconds
    private int rocketSpawnBounds = 2;
    private float laserTimer = 0f;
    private float laserSpawnFreq = 0f; //seconds
    private float minLaserPawnTime = 40f; //seconds
    private float maxLaserPawnTime = 80f; //seconds
    private int laserSpawnBounds = 4;
    private int laserSpawnPercnt = 80;

    private GameManager gameManager;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        gameManager = GameManager.Instance;

        laserSpawnFreq = Random.Range(minLaserPawnTime, maxLaserPawnTime);
    }

    private void Update()
    {
        if (gameManager.GameIsPlaying())
        {
            float difficultyFactor = Mathf.Clamp01(gameManager.GetScore() / 500f);
            obstacleTimer += Time.deltaTime;
            rocketTimer += Time.deltaTime;
            laserTimer += Time.deltaTime;
            if (obstacleTimer >= obstacleSpawnFreq && canSpawnObstacles)
            {
                float obstacleSpeed = Mathf.Lerp(5f, 10f, difficultyFactor);
                SpawnObstacle(obstacleSpeed);
                obstacleTimer = 0;
            }
            if (rocketTimer >= rocketSpawnFreq && canSpawnRockets)
            {
                float rocketSpeed = Mathf.Lerp(10f, 20f, difficultyFactor);
                SpawnRocket(rocketSpeed);
                SpawnRocket(rocketSpeed);
                rocketTimer = 0;
            }
            if (laserTimer >= laserSpawnFreq && canSpawnLasers)
            {
                SetSpawnPermissions(false, true, false);
                SpawnLaser();
                laserSpawnFreq = Random.Range(minLaserPawnTime, maxLaserPawnTime);
                laserTimer = 0f;
            }
        }
    }

    private void SpawnObstacle(float speed)
    {
        int newObstacleHeight = Random.Range(minObstacleHeight, maxObstacleHeight + 1);

        float spawnOffset = (newObstacleHeight + 1) % 2; //if newObstacleHeight is even => spawnOffset = 0c
        spawnOffset /= 2;
        float boundsOffset = spawnOffset + (newObstacleHeight - 1) / 2;
        float spwnPointY = Random.Range(-obstacleSpawnBounds, obstacleSpawnBounds + 1);
        if (spwnPointY > 0)
        {
            spwnPointY -= boundsOffset;
        }
        else
        {
            spwnPointY += boundsOffset;
        }

        Vector3 spawnPoint = transform.position + new Vector3(0, spwnPointY, 0);
        GameObject obstacle = Instantiate(obstaclePrefab, spawnPoint, obstaclePrefab.transform.rotation);

        int newObstacleWidth = (int)obstacle.transform.localScale.x;
        if (newObstacleHeight == 1)
        {
            newObstacleWidth = maxObstacleHeight;
            if (spwnPointY == 0)
            {
                obstacle.GetComponent<ObstacleController>().AddSpin();
            }
        }
        obstacle.transform.localScale = new Vector3(newObstacleWidth, newObstacleHeight, obstacle.transform.localScale.z);
        obstacle.GetComponent<ObstacleController>().SetSpeed(speed);
    }

    private void SpawnRocket(float speed)
    {
        float spwnPointY = Random.Range(-rocketSpawnBounds, rocketSpawnBounds + 1) * 2;
        Vector3 spawnPoint = transform.position + new Vector3(0f, spwnPointY, 0f);
        GameObject rocket = Instantiate(rocketPrefab, spawnPoint, rocketPrefab.transform.rotation);
        rocket.GetComponent<RocketController>().SetSpeed(speed);
    }

    private void SpawnLaser()
    {
        Instantiate(laserPrefab, new(0f, laserSpawnBounds, 0f), laserPrefab.transform.rotation);
        Instantiate(laserPrefab, new(0f, -laserSpawnBounds, 0f), laserPrefab.transform.rotation);

        if (Random.Range(0, 101) < laserSpawnPercnt)
        {
            Instantiate(laserPrefab, new(0f, laserSpawnBounds - 1, 0f), laserPrefab.transform.rotation);
            if (Random.Range(0, 101) < (laserSpawnPercnt / 2))
            {
                Instantiate(laserPrefab, new(0f, laserSpawnBounds - 2, 0f), laserPrefab.transform.rotation);
            }
        }
        if (Random.Range(0, 101) < laserSpawnPercnt)
        {
            Instantiate(laserPrefab, new(0f, -laserSpawnBounds + 1, 0f), laserPrefab.transform.rotation);
            if (Random.Range(0, 101) < (laserSpawnPercnt / 2))
            {
                Instantiate(laserPrefab, new(0f, -laserSpawnBounds + 2, 0f), laserPrefab.transform.rotation);
            }
        }
    }

    public void SetSpawnPermissions(bool canSpawnObstacles, bool canSpawnRockets, bool canSpawnLasers)
    {
        this.canSpawnObstacles = canSpawnObstacles;
        this.canSpawnRockets = canSpawnRockets;
        this.canSpawnLasers = canSpawnLasers;
    }
}
