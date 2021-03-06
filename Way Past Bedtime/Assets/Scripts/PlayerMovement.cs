using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controls the player's movement
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb2d;
    [SerializeField] float speed = 5;
    Animator anim;
    Vector2 controlVector;
    public bool canMove = true;
    AudioSource audiosrc;

Camera cam;

    public void OnPlayerMovement(InputAction.CallbackContext context)
    {
        controlVector = context.ReadValue<Vector2>();
    }

    private void Start()
    {
        controlVector = Vector2.zero;
        rb2d = GetComponent<Rigidbody2D>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        anim = GetComponent<Animator>();
        audiosrc = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (canMove)
        {
            if (rb2d.velocity.magnitude < .1f)
            {
                anim.SetBool("Moving", false);
            }
            else
            {
                anim.SetBool("Moving", true);
                anim.SetFloat("Horizontal", rb2d.velocity.x);
                anim.SetFloat("Vertical", rb2d.velocity.y);
            }
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            rb2d.velocity = controlVector * speed;
        }
    }

    public void StepSound()
    {
        AudioManager.PlaySoundEffect("tommystep",audiosrc);
    }

}
