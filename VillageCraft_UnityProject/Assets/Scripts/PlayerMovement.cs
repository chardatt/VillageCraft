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
    [SerializeField]private float crouchingSpeed;
    [SerializeField]private float slidingSpeed;
    [SerializeField]private float rollSpeed;

    [Header("Jump")]
    [SerializeField]private float jumpHeight;
    [SerializeField]private float gravity;

    [Header("Crouch")]
    [SerializeField]private float crouchHeight;
    [SerializeField]private float standHeight;
    [SerializeField][Range(0f, 1f)]private float crouchSpeed;

    [Header("Crouch")]
    [SerializeField]private float rollCD;
    [SerializeField]private float rollTime;

    [Header("Check States")]
    [SerializeField]private bool isMoving;
    [SerializeField]private bool isGrounded;
    [SerializeField]private bool isRunning;
    [SerializeField]private bool isCrouching;
    [SerializeField]private bool isConstantlyCrouching;
    [SerializeField]private bool isSliding;
    [SerializeField]private bool isRolling;

    [Header("Camera")]
    [SerializeField]private GameObject playerCam;

    private Vector3 move;
    private Transform render;
    private CharacterController cc;
    private Vector3 velocity;

    
    private void Start() {
        isMoving = true;

        render = GetComponentInChildren<Transform>();
        cc = GetComponent<CharacterController>();
    }

    private void Update() {

        #region MOVING
        //Déplacement
        if (isMoving)
        {
            move = playerCam.transform.forward * Input.GetAxis("Vertical") + playerCam.transform.right * Input.GetAxis("Horizontal");
        }

        move.y = 0;
        cc.Move(move * Time.deltaTime * playerSpeed);
        if (move != Vector3.zero) 
        {
            gameObject.transform.forward = move;
        }        
        #endregion

        #region SPEEDS
        //Sprint
        if (Input.GetButtonDown("Run"))
        {
            isRunning = !isRunning;
        }
        
        if (isRunning) 
        {
            playerSpeed = runningSpeed;                 //vitesse de course
        }
        else if (isCrouching || isConstantlyCrouching)  //vitesse quand crouch
        {
            playerSpeed = crouchingSpeed;
        }
        else if (isRolling) 
        {
            playerSpeed = rollSpeed;
        }
        else 
        {
            playerSpeed = walkSpeed;                    //vitesse de marche
        }
        #endregion

        #region JUMP
        //Jump
        if (Input.GetButtonDown("Jump") && isGrounded) 
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
        //Check si le joueur touche le sol ou pas
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 0.1f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.red);
            isGrounded = true;
        }
        else 
        {
            isGrounded = false;
        } 

        if (isGrounded && velocity.y < 0) 
        {
            velocity.y = 0f;
        }
        #endregion

        #region GRAVITY
        //Gravity
        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
        #endregion

        #region CROUCH
        
        if (Input.GetButtonDown("Crouch") && !isRunning) 
        {
            isConstantlyCrouching = !isConstantlyCrouching;
        }
        if (Input.GetButton("Constantly Crouch") && !isRunning) 
        {
            isCrouching = true;
        }
        else 
        {
            isCrouching = false;
        }
        
        //Empêcher le collider de se remettre en position initial s'il reste sous un plafond trop bas
        RaycastHit hitTop;
        if (Physics.Raycast(transform.position + Vector3.up, transform.TransformDirection(Vector3.up), out hitTop, 1f))
        {
            Debug.DrawRay(transform.position + Vector3.up, transform.TransformDirection(Vector3.up) * hitTop.distance, Color.yellow);
            Debug.Log("je ne peux pas me relever");
            isCrouching = true;
        }
        #endregion

        #region HEIGHT
        //choisis la bonne hauteur de collider en fonction de son état
        var desiredHeight = isCrouching || isConstantlyCrouching || isSliding ? crouchHeight : standHeight;

        if (cc.height != desiredHeight)
        {
            AdjustHeight(desiredHeight);
        }
        #endregion

        #region SLIDE
        if(Input.GetButtonDown("Slide") && isRunning)
        {
            move = playerCam.transform.forward * Input.GetAxis("Vertical") + playerCam.transform.right * Input.GetAxis("Horizontal");
        }
        if(Input.GetButton("Slide") && isRunning)
        {
            Debug.Log("je slide");
            isSliding = true;
            isMoving = false;
            playerSpeed = slidingSpeed;
            slidingSpeed -= slidingSpeed * Time.deltaTime;
            
        }
        else if (Input.GetButtonUp("Slide") && isSliding)
        {
            isSliding = false;
            isMoving = true;
            slidingSpeed = runningSpeed;
        }
        #endregion

        #region ROLL
        if(Input.GetButtonDown("Roll") && rollCD <= 0)
        {
            move = playerCam.transform.forward * Input.GetAxis("Vertical") + playerCam.transform.right * Input.GetAxis("Horizontal");

            isMoving = false;
            isRolling = true;

            StartCoroutine(EndRoll());
        }
        else 
        {
            rollCD -= Time.deltaTime;
        }
        #endregion
    }

    #region FONCTIONS
    private IEnumerator EndRoll()
    {
        yield return new WaitForSeconds(rollTime);
        isMoving = true;
        isRolling = false;
        rollCD = 3;
    }

    private void AdjustHeight(float height)
    {
        float center = height / 2;
        cc.height = Mathf.Lerp(cc.height, height, crouchSpeed);
        cc.center = Vector3.Lerp(cc.center, new Vector3(0, center, 0), crouchSpeed);
    }

    
    #endregion
}
