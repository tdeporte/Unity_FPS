using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public float gravity = 5;
    public float translation;
    public float rotation;
    public float c_speed = 0.1f;
    public float rotationSpeed = 100f;
    private Rigidbody  rigibBodyComponent;

    //SAUT
    public float jumpForce = 7f;
    public bool isGrounded;

    public Vector3 destination;

    private float distance = 2f;

    private int repel_force = 20;


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
        destination = new Vector3(-8f,1f,-20f); 
    }

    // Update is called once per frame
    

    void Update()
    {
        Vector3 position = rigibBodyComponent.position;
        if(Vector3.Distance(position, destination)>0.5){
            //go_to(destination);
        }
        
    }

    void FixedUpdate () {
        
    }

    
}
