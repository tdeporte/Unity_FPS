using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody  rigibBodyComponent;

    //Manual movement
    CharacterController characterController;

    public float c_speed = 1.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;

    //Automatic movement
    public Vector3 destination;
    private int repel_force = 20;
    public float distance = 10f;


    void go_to(Vector3 to){
        Vector3 position = rigibBodyComponent.position;
        Vector3 dir = (to - position).normalized;
            //Debug.Log(Vector3.Distance(position, destination));

            Vector3 forward = transform.TransformDirection(Vector3.forward) * distance;
            Vector3 left = transform.TransformDirection(Vector3.left) * distance;
            //Vector3 left = transform.position + new Vector3(-1.5f, 0f, 0f);
            Vector3 right = transform.TransformDirection(Vector3.right) * distance;
            //Vector3 right = transform.position + new Vector3(1.5f, 0f, 0f);


            Vector3[] rays = {forward,left,right};
            
            for(int i =0;i<rays.Length;i++){
                Debug.DrawRay(transform.position, rays[i], Color.red);
            }
            
            RaycastHit hit;

            if (Physics.Raycast(transform.position, forward, out hit,distance) ||
                Physics.Raycast(transform.position, left,    out hit,distance) || 
                Physics.Raycast(transform.position, right,   out hit,distance) ){

                dir += hit.normal * repel_force; // repel_force is force to repel by
            }
            // rotation
            Quaternion  rot = Quaternion.LookRotation (dir);
            transform.rotation = Quaternion.Slerp (transform.rotation, rot, Time.deltaTime);
            //position
            transform.position += transform.forward * (2 * Time.deltaTime); // 20 is speed
    }

    // Start is called before the first frame update
    void Start()
    {
        rigibBodyComponent = gameObject.GetComponent<Rigidbody>() ;
        destination = new Vector3(-8f,1f,-30f); 
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    

    void Update()
    {
        
        /*
        Vector3 position = rigibBodyComponent.position;
        if(Vector3.Distance(position, destination)>0.5){
            //go_to(destination);
        }
        */    

        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection *= c_speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
    }

    void FixedUpdate () {
        
    }

    
}
