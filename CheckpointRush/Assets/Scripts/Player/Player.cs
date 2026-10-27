using UnityEngine;

public class Player : MonoBehaviour
{

    [Header ("Vehicle Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 90f;
    [Header ("Keys Controls")]
    [SerializeField] private KeyCode accelerateKey = KeyCode.Z;
    [SerializeField] private KeyCode reverseKey = KeyCode.S;
    [SerializeField] private KeyCode leftKey = KeyCode.Q;
    [SerializeField] private KeyCode rightKey = KeyCode.D;

    private Rigidbody rb;

    private GameManager gameManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get components
        rb = GetComponent<Rigidbody>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(!gameManager.IsGameOver)
        {
            Movement();
        }
    }

    private void Movement()
    {
        float moveInput = 0f;
        float turnInput = 0f;

        // Get Input
        if (Input.GetKey(accelerateKey))
        {
            moveInput += 1f;
        }

        if (Input.GetKey(reverseKey))
        {
            moveInput -= 1f;
        }

        if (Input.GetKey(leftKey))
        {
            turnInput -= 1f;
        }

        if (Input.GetKey(rightKey))
        {
            turnInput += 1f;
        }

        // Move forword or backward
        Vector3 movement = transform.forward * moveInput * speed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + movement);

        // Rotate car
        float rotation = turnInput * rotationSpeed * Time.fixedDeltaTime;

        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, rotation, 0f));
    }
}
