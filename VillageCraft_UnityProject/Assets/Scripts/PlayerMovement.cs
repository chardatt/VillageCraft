using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField]private float playerSpeed;
    [SerializeField]private float walkSpeed;
    [SerializeField]private float runningSpeed;
    [SerializeField][Range(0f, 1f)]private float crouchSpeed;

    [Header("Jump")]
    [SerializeField]private float gravity;
    [SerializeField]private float jumpHeight;

    [Header("Crouch")]
    [SerializeField]private float crouchHeight;
    [SerializeField]private float standHeight;

    [Header("Check States")]
    [SerializeField]private bool isRunning;
    [SerializeField]private bool isGrounded;
    [SerializeField]private bool isCrouching;


    private Transform render;
    private CharacterController cc;
    private Vector3 velocity;


    private void Start() {
        render = GetComponentInChildren<Transform>();
        cc = GetComponent<CharacterController>();
    }

    private void Update() {

        #region MOVING
        //Déplacement
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        cc.Move(move * Time.deltaTime * playerSpeed);
        if (move != Vector3.zero) {
            gameObject.transform.forward = move;
        }
        #endregion

        #region SPRINT
        //Sprint
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.JoystickButton8)) {
            isRunning = !isRunning;
        }
        
        if (isRunning) {
            playerSpeed = runningSpeed;
        }
        else playerSpeed = walkSpeed;
        #endregion

        #region JUMP
        //Jump
        if (Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
        //Check si le joueur touche le sol ou pas
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 0.1f))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.green);
                isGrounded = true;
            }
            else isGrounded = false;

        if (isGrounded && velocity.y < 0) {
            velocity.y = 0f;
        }
        #endregion

        #region  GRAVITY
        //Gravity
        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
        #endregion

        isCrouching = Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.LeftControl);
    }

    private void FixedUpdate() {
        var desiredHeight = isCrouching ? crouchHeight : standHeight;

        if (cc.height != desiredHeight){
            AdjustHeight(desiredHeight);
        }
    }

    private void AdjustHeight(float height){
        float center = height / 2;

        cc.height = Mathf.Lerp(cc.height, height, crouchSpeed);
        cc.center = Vector3.Lerp(cc.center, new Vector3(0, center, 0), crouchSpeed);
    }
}
