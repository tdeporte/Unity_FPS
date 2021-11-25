using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public float gravity = 5;
    public float translation;
    public float rotation;
    public float speed = 1.0f;
    public float rotationSpeed = 0.001f;
    private Rigidbody  rigibBodyComponent;

    //SAUT
    public float jumpForce = 7f;
    public bool isGrounded;

    public Vector3 destination;


    // Start is called before the first frame update
    void Start()
    {
        rigibBodyComponent = gameObject.GetComponent<Rigidbody>() ;
        destination = new Vector3(1f,0.5f,-15f); 
    }

    // Update is called once per frame
    
    void Update()
    {
        
        
    }

    void FixedUpdate () {
        float horizontal = Input.GetAxis ("Horizontal");
        float vertical = Input.GetAxis ("Vertical");
        //rigibBodyComponent.velocity = new Vector3 (horizontal * speed, rigibBodyComponent.velocity.y, vertical * speed);
        
        Vector3 position = rigibBodyComponent.position;

        var fwd = transform.TransformDirection (Vector3.forward);
        RaycastHit hit;

        if (!Physics.Raycast(transform.position, fwd, out hit,2)){
            Debug.Log("Found an object - distance: " + hit.distance);
            float step =  speed * Time.deltaTime; // calculate distance to move
            rigibBodyComponent.position = Vector3.MoveTowards(position, destination, step);
        }
        
    }

    
}
