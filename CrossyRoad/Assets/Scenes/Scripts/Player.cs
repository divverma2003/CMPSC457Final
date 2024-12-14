using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private TerrainGenerator terrainGenerator;
    [SerializeField] private UIManager uiManager;

    private Animator animator;
    private Rigidbody rb;
    private bool isHopping;
    private int score;
    private int coinCount;

    private bool onLog;
    private Transform currentLog;
    private Vector3 lastLogPosition;

    private void Start()
    {
        score = 0;
        coinCount = 0;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        score++;
        uiManager.UpdateScore(score);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && !isHopping)
        {
            float zDifference = 0f;

            // Ensure player's z position alignment
            if (Mathf.Abs(transform.position.z % 1) > 0.01f)
            {
                zDifference = Mathf.Round(transform.position.z) - transform.position.z;
            }

            MoveCharacter(new Vector3(1, 0, zDifference));
        }
        else if (Input.GetKeyDown(KeyCode.A) && !isHopping)
        {
            float xDifference = 0f;

            // Ensure player's x position alignment
            if (Mathf.Abs(transform.position.x % 1) > 0.01f)
            {
                xDifference = Mathf.Round(transform.position.x) - transform.position.x;
            }

            MoveCharacter(new Vector3(xDifference, 0, 1));
        }
        else if (Input.GetKeyDown(KeyCode.D) && !isHopping)
        {
            float xDifference = 0f;

            // Ensure player's x position alignment
            if (Mathf.Abs(transform.position.x % 1) > 0.01f)
            {
                xDifference = Mathf.Round(transform.position.x) - transform.position.x;
            }

            MoveCharacter(new Vector3(xDifference, 0, -1));
        }
    }

    public void FinishHop()
    {
        animator.ResetTrigger("hop");
        isHopping = false;
    }

    public void StartHop()
    {
        animator.ResetTrigger("hop");
        animator.SetTrigger("hop");
        isHopping = true;
    }

    private void MoveCharacter(Vector3 difference)
    {
        StartHop();

        float roundedX = RoundToNearestHalf(rb.position.x);
        float constrainedY = Mathf.Max(rb.position.y, 0.7f);
        float roundedZ = RoundToNearestHalf(rb.position.z);

        rb.position = new Vector3(roundedX, constrainedY, roundedZ);
        Vector3 newPosition = rb.position + difference;
        rb.MovePosition(newPosition);

        terrainGenerator.SpawnTerrain(false, newPosition);
    }

    float RoundToNearestHalf(float value)
    {
        return Mathf.Round(value * 2f) / 2f;
    }

    private void OnDestroy()
    {
        if (uiManager != null)
        {
            uiManager.ShowGameOver();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Log log = collision.collider.GetComponent<Log>();
        if (log != null)
        {
            onLog = true;
            currentLog = log.transform;
            lastLogPosition = currentLog.position;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Log log = collision.collider.GetComponent<Log>();
        if (log != null && currentLog == log.transform)
        {
            onLog = false;
            currentLog = null;
        }
    }

    // Allow the player to stay on the log
    private void LateUpdate()
    {
        // If currently on a log, move the player according to the log's movement
        if (onLog && currentLog != null)
        {
            Vector3 logDelta = currentLog.position - lastLogPosition;
            rb.MovePosition(rb.position + logDelta);
            lastLogPosition = currentLog.position;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Coin>())
        {
            other.gameObject.SetActive(false);
            coinCount++;
            uiManager.UpdateCount(coinCount);
        }
    }
}
