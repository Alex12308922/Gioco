using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerController : MonoBehaviour 
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    
    [Header("Camera Settings")]
    public Camera mainCamera;
    
    // References
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }
    
    void Update()
    {
        // Raccogli l'input in Update per massima reattività
        moveDirection = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;
    }
    

    public Animator animatore;
    public int FacingDir = 1;
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if(horizontal > 0 && transform.localScale.x < 0 || 
            horizontal < 0 && transform.localScale.x > 0)
        {
            Flip();
        } 

        animatore.SetFloat("horizontal", Mathf.Abs(horizontal));
        animatore.SetFloat("vertical", Mathf.Abs(vertical));



        // Movimento diretto senza calcoli di smoothing che possono causare lag
        rb.linearVelocity = moveDirection * moveSpeed;
    }
    
    void Flip()
    {
        FacingDir *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    void LateUpdate()
    {
        // Segui immediatamente il player senza smoothing o lerp
        if (mainCamera != null)
        {
            // Mantieni solo l'offset Z per la telecamera 2D
            Vector3 newPosition = transform.position;
            newPosition.z = -10; // Valore standard per telecamere 2D
            mainCamera.transform.position = newPosition;
        }
    }
}