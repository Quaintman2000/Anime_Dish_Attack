using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [Header("Movement:"),Tooltip("Set the player's movement joystick here.")]
    public Joystick joystick;
    [Tooltip("The max speed the player travels.")]
    public float speed = 10;
    // Sets the initial velocity to be zero.
    Vector3 velocityVector = Vector3.zero;
    [Tooltip("The maximum change in velocity. Acceleration.")]
    public float maxVelocityChange = 10f;
    // Stores the rigidBody.
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        // Grabs the Rigidbody.
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Grab the joystick's inputs.
        float _xMovementInput = joystick.Horizontal;
        float _zMovementInput = joystick.Vertical;

        // Calculate the velocity vectors.
        Vector3 _movementHorizontal = transform.right * _xMovementInput;
        Vector3 _movementVertical = transform.forward * _zMovementInput;

        // Calculate movement vector.
        Vector3 _movementVelocityVector = (_movementHorizontal + _movementVertical).normalized * speed;

        // Apply movement
        Move(_movementVelocityVector);
    }
    /// <summary>
    /// Sets the velocity vector to the set vector.
    /// </summary>
    /// <param name="vector">The vector to set the velocity vector.</param>
    void Move(Vector3 vector)
    {
        velocityVector = vector;
    }

    private void FixedUpdate()
    {
        // If the pawn's velocity is NOT zero.
        if(velocityVector != Vector3.zero)
        {
            // Get rigidbody's current velocity.
            Vector3 velocity = rb.velocity;
            Vector3 velocityChange = velocityVector - velocity;

            // Apply force by the amount of velocity change to achieve target velocity.
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0f;
            rb.AddForce(velocityChange, ForceMode.Acceleration);
        }
    }
}
