using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rbPlayer;
    public float gravityModifer;
    public float jumpForce;
    private bool isGrounded = true;
    public bool gameOver = false;

    private Animator animPlayer;
    public ParticleSystem expSystem;
    public ParticleSystem dirtSystem;

    public AudioClip jumpSound;
    public AudioClip crashSound;

    private AudioSource asPalyer;

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifer;
        //Same //Physics.gravity = Physics.gravity * gravityModifer;

        animPlayer = GetComponent<Animator>();
        
        asPalyer = GetComponent<AudioSource>(); 
    }

    void Update()
    {
        bool spaceDown = Input.GetKey(KeyCode.Space);
        if(spaceDown && isGrounded && !gameOver)
        {
            rbPlayer.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            animPlayer.SetTrigger("Jump_trig");
            dirtSystem.Stop();
            asPalyer.PlayOneShot(jumpSound, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            dirtSystem.Play();
        }
        else if(collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over!");
            gameOver = true;
            animPlayer.SetBool("Death_b", true);
            animPlayer.SetInteger("DeathType_int", 1);
            expSystem.Play();
            dirtSystem.Stop();
            asPalyer.PlayOneShot(crashSound, 1.0f);

        }
    }
}
