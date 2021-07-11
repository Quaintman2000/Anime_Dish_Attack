using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Joystick joystick;
    public Rigidbody rb;
    public float speed;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        joystick = GetComponentInChildren<Joystick>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Grab the joystick's inputs.
        float _xMovementInput = joystick.Horizontal;
        float _zMovementInput = joystick.Vertical;


        if (_xMovementInput != 0 || _zMovementInput != 0)
        {
            // Set rotate the player to look in the joystick direction
            Quaternion goalRotation = Quaternion.LookRotation(new Vector3(x: _xMovementInput, y: 0, z: _zMovementInput), upwards: transform.up);
            transform.rotation = goalRotation;
        }

        rb.velocity = new Vector3(_xMovementInput, 0, _zMovementInput) * speed;

        anim.SetFloat("Speed", (rb.velocity.magnitude/speed));
    }
}
