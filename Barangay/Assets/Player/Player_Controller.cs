using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;

    //player movement stats
    private float speed = 5f;   
    private float gravity = -9.81f;
    private float energy = 5f;
    private float exhaustion = 3f;
    private float exhaustionTimerLeft;

    Vector3 velocity;
    
    //checker for wall or ground
    private bool isGrounded, isRunning, isCrouching, isPinned;
    public Transform groundCheck, ceilingCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public Text energy_text;

    // Update is called once per frame
    void Update()
    {
        //movement and gravity 
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isPinned = Physics.CheckSphere(ceilingCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0){ 
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        //running trigger
        if(Input.GetButton("Jump") && 0 < (Mathf.Abs(x) + Mathf.Abs(z)) && isCrouching == false && energy > 0){
            speed = 10f;
            isRunning = true;
            animator.SetBool("isRunning", isRunning);
            energy -= 1 * Time.deltaTime;
            exhaustionTimerLeft = exhaustion;
        }
        else if(isRunning == true){
            isRunning = false;
            speed = 5f;
            animator.SetBool("isRunning", isRunning);
        }   

        //energy replenish
        if(energy < 5 && isRunning == false){
            if(energy < 1 && exhaustionTimerLeft > 0 ){
                exhaustionTimerLeft -= 1 * Time.deltaTime;
            }
            else{
                energy += 1 * Time.deltaTime;
            }
        }

        //crouching trigger
        if(Input.GetButton("Crouch") | isPinned ){
            isRunning = false;
            animator.SetBool("isRunning", isRunning);
            controller.height = 0.3f;
            isCrouching = true;
            speed = 2f;
        }
        else if(isCrouching == true){
            isCrouching = false;
            speed = 5f;
            controller.height = 2;
        }

        //animation
        animator.SetFloat("Walking", Mathf.Abs(x) + Mathf.Abs(z));

        //UI and shit that I don't care
        energy_text.text = energy.ToString();
    }
}
