using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private float speed;
    [SerializeField]private bool running;
    [SerializeField]private bool grounded;
    [SerializeField]private float gravity = -9.81f;
    [SerializeField]private float jumpHeight = 1.0f;
    private CharacterController cc;
    private Vector3 velocity;


    private void Start() {
        cc = GetComponent<CharacterController>();
    }

    private void Update() {
        grounded = cc.isGrounded;
        if (grounded && velocity.y < 0) {
            velocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        cc.Move(move * Time.deltaTime * speed);

        if (move != Vector3.zero) {
            gameObject.transform.forward = move;
        }

        if (Input.GetButtonDown("Jump") && grounded) {
            velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
    }
}
