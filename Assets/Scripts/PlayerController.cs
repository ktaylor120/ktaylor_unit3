using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rbPlayer;
    public float gravityModifer;
    public float jumpForce;
    private bool isGrounded = true;

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifer;
        //Same //Physics.gravity = Physics.gravity * gravityModifer;
    }

    // Update is called once per frame
    void Update()
    {
        bool spaceDown = Input.GetKey(KeyCode.Space);
        if(spaceDown && isGrounded)
        {
            rbPlayer.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;

    }
}
