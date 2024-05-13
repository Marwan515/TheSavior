using UnityEngine;

public class TwoDimensionalAnimationControl : MonoBehaviour
{
    public Animator animator;
    float velocityX = 0.0f;
    float velocityZ = 0.0f;
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    public float maximumWalkVelocity = 0.5f;
    public float maximumRunVelocity = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Search the game object this script is attached to and get the animator component
        animator = GetComponent<Animator>();
    }

    // handles acceleration and deceleration
    void ChangeVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool backPressed, bool runPressed, float currentMaximumVelocity)
    {
        // if player presses forward, increase velocity in z direction
        if (forwardPressed && velocityZ < currentMaximumVelocity)
        {
            velocityZ += Time.deltaTime * acceleration;
        }
        // if player presses back, increase velocity in z direction
        if (backPressed && velocityZ > -currentMaximumVelocity)
        {
            velocityZ -= Time.deltaTime * acceleration;
        }
        // increase velocity in left direction
        if (leftPressed && velocityX > -currentMaximumVelocity)
        {
            velocityX -= Time.deltaTime * acceleration;
        }
        // increase velocity in right direction
        if (rightPressed && velocityX < currentMaximumVelocity)
        {
            velocityX += Time.deltaTime * acceleration;
        }

        // decrease velocityZ
        if (!forwardPressed && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }
       // decrease velocityZ
        if (!backPressed && velocityZ < 0.0f)
        {
            velocityZ += Time.deltaTime * deceleration;
        }
        // increase velocityX if left not pressed and velocityX < 0
        if (!leftPressed && velocityX < 0.0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }
        // decrease velocityX if right not pressed and velocityX > 0
        if (!rightPressed && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * deceleration;
        }
    }

    void LockOrChangeVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool backPressed, bool runPressed, float currentMaximumVelocity)
    {
        // reset velocityZ
        if (!forwardPressed && !backPressed && velocityZ != 0.0f && velocityZ > -0.05f && velocityZ < 0.05f)
        {
            velocityZ = 0.0f;
        }
        
        // reset velocityX
        if (!leftPressed && !rightPressed && velocityX != 0.0f && velocityX > -0.05f && velocityX < 0.05f)
        {
            velocityX = 0.0f;
        }

        // lock forward 
        if (forwardPressed && runPressed && velocityZ > currentMaximumVelocity)
        {
            velocityZ = currentMaximumVelocity;
        }
        // decelerate to maximum walking velocity
        else if (forwardPressed && velocityZ > currentMaximumVelocity)
        {
            velocityZ -= Time.deltaTime * deceleration;
            // round to the current maximum velocity if within offset
            if (velocityZ > currentMaximumVelocity && velocityZ < (currentMaximumVelocity + 0.05f))
            {
                velocityZ = currentMaximumVelocity;
            }
        }
        else if (forwardPressed && velocityZ < currentMaximumVelocity && velocityZ > (currentMaximumVelocity - 0.05f))
        {
            velocityZ = currentMaximumVelocity;
        }
        // lock backward
        if (backPressed && runPressed && velocityZ < -currentMaximumVelocity)
        {
            velocityZ = -currentMaximumVelocity;
        }
        // decelerate
        else if (backPressed && velocityZ < -currentMaximumVelocity)
        {
            velocityZ += Time.deltaTime * deceleration;
            // round to the current maximum velocity if within offset
            if (velocityZ < -currentMaximumVelocity && velocityZ > (-currentMaximumVelocity - 0.05f))
            {
                velocityZ = -currentMaximumVelocity;
            }
        }
        else if (backPressed && velocityZ > -currentMaximumVelocity && velocityZ < (-currentMaximumVelocity + 0.05f))
        {
            velocityZ = -currentMaximumVelocity;
        }
         // lock left
        if (leftPressed && runPressed && velocityX < -currentMaximumVelocity)
        {
            velocityX = -currentMaximumVelocity;
        }
        // decelerate to maximum walking velocity
        else if (leftPressed && velocityX < -currentMaximumVelocity)
        {
            velocityX += Time.deltaTime * deceleration;
            // round to the current maximum velocity if within offset
            if (velocityX < -currentMaximumVelocity && velocityX > (-currentMaximumVelocity - 0.05f))
            {
                velocityX = -currentMaximumVelocity;
            }
        }
        // round to the current maximum velocity
        else if (leftPressed && velocityX > -currentMaximumVelocity && velocityX < (-currentMaximumVelocity + 0.05f))
        {
            velocityX = -currentMaximumVelocity;
        }
         // lock right
        if (rightPressed && runPressed && velocityX > currentMaximumVelocity)
        {
            velocityX = currentMaximumVelocity;
        }
        // decelerate to maximum walking velocity
        else if (rightPressed && velocityX > currentMaximumVelocity)
        {
            velocityX -= Time.deltaTime * deceleration;
            // round to the current maximum velocity if within offset
            if (velocityX > currentMaximumVelocity && velocityX < (currentMaximumVelocity + 0.05f))
            {
                velocityX = currentMaximumVelocity;
            }
        }
        // round to the current maximum velocity
        else if (rightPressed && velocityX < currentMaximumVelocity && velocityX > (currentMaximumVelocity - 0.05f))
        {
            velocityX = currentMaximumVelocity;
        }
    }
    // Update is called once per frame
    void Update()
    {
        // Input will be true if player is pressing on the passed in key parameter
        // get key input from the player
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool backPressed = Input.GetKey(KeyCode.S);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);


        // set current maxVelocity
        float currentMaximumVelocity = runPressed ? maximumRunVelocity : maximumWalkVelocity;

        ChangeVelocity(forwardPressed, leftPressed, rightPressed, backPressed, runPressed, currentMaximumVelocity);
        LockOrChangeVelocity(forwardPressed, leftPressed, rightPressed, backPressed, runPressed, currentMaximumVelocity);

        // set the parameters to our local variable values
        animator.SetFloat("Yaxis", velocityZ);
        animator.SetFloat("Xaxis", velocityX);
    }
}
