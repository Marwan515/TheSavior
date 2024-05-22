using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

       
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        rb.MovePosition(transform.position + movement * moveSpeed * Time.fixedDeltaTime);

        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);

        if (animator != null)
        {
            if (movement.magnitude > 0)
            {
                animator.SetBool("IsWalking", true);
            }
            else
            {
                animator.SetBool("IsWalking", false);
            }
        }

    }

}
