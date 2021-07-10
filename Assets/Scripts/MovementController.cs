using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Joystick joystick;
    public float speed = 10;
    Vector3 velocityVector = Vector3.zero;

    public float maxVelocityChange = 10f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
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
    void Move(Vector3 vector)
    {
        velocityVector = vector;
    }

    private void FixedUpdate()
    {
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
