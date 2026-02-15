using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private DataManager dataManager;
    private float elapsedTime = 0f;
    private int score = 0;
    private float scoreMultiplier = 10f;
    private float thrustForce = 1f;
    private float maxSpeed = 5f;
    private Rigidbody2D rb;
    [SerializeField] private GameObject BoosterFlame;
    [SerializeField] private GameObject BordersParent;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI HighScoreText;
    public GameObject ExplosionEffects;
    public GameObject RestartButton;
    public InputAction moveForward;
    public InputAction lookPosition;


    void Awake()
    {
        dataManager = FindFirstObjectByType<DataManager>();
        HighScoreText.gameObject.SetActive(false);
        BordersParent.gameObject.SetActive(true);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveForward.Enable();
        lookPosition.Enable();
        rb = GetComponent<Rigidbody2D>();
        RestartButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
        MovePlayer();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        Instantiate(ExplosionEffects, transform.position, Quaternion.identity);
        SavePreviousData();
        ShowData();
        Destroy(gameObject);
        RestartButton.SetActive(true);
        BordersParent.gameObject.SetActive(false);
    }
    void UpdateScore()
    {
        elapsedTime += Time.deltaTime;
        score = Mathf.FloorToInt(elapsedTime * scoreMultiplier);
        scoreText.text = score.ToString();
    }
    void MovePlayer()
    {
        if (moveForward.IsPressed())
        {
            //calculate direction of mouse
            Vector3 positionMouse = Camera.main.ScreenToWorldPoint(lookPosition.ReadValue<Vector2>());
            Vector2 direction = transform.position - positionMouse;
            direction = direction.normalized;

            //move in the direction of mouse
            transform.up = -direction;
            rb.AddForce(transform.up * thrustForce);
            if(rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        }
        if (moveForward.WasPressedThisFrame())
        {
            BoosterFlame.SetActive(true);
        }
        else if(moveForward.WasReleasedThisFrame())
        {
            BoosterFlame.SetActive(false);
        }
    }
    void ShowData()
    {
        if(dataManager != null)
        {   
            var userData =dataManager.LoadData();
            if(userData == null) return;
            HighScoreText.text = "Best Score : " + userData.highscore;
            HighScoreText.gameObject.SetActive(true);
        }
    }
    void SavePreviousData()
    {
        if(dataManager != null)
        {
            var previousData = dataManager.LoadData();
            if(previousData == null || int.Parse(previousData.highscore) < score)
            {
                dataManager.SaveData(score.ToString());
            }      
        }   
    }
}
