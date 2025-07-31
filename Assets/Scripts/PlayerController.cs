using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const string OBSTACLE = "Obstacle";

    [SerializeField] private bool immune = false;

    private float jumpSpeed = 1500;
    private Rigidbody2D rb;
    private GameManager gameManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
    }


    private void Update()
    {
        if (gameManager.GameIsPlaying())
        {
            if (Input.GetKey(KeyCode.Space))
            {
                rb.AddForce(jumpSpeed * Time.deltaTime * Vector2.up, ForceMode2D.Force);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!immune && gameManager.GameIsPlaying() && other.gameObject.CompareTag(OBSTACLE))
        {
            gameManager.ChangeGameState(GameManager.GameState.GameOver);
            float pushForce = 2.5f;
            rb.AddForce(pushForce * Vector2.right, ForceMode2D.Impulse);
        }
    }
}
