using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    
    public float turnSpeed = 4.0f;
    public float moveSpeed = 2.0f;
    
    public float minTurnAngle = -90.0f;
    public float maxTurnAngle = 90.0f;
    private float rotX;

    private float range = 100;

    public RaycastHit[] hits;

    //public transform gun_pos;


    void MouseAiming ()
    {
        // get the mouse inputs
        float y = Input.GetAxis("Mouse X") * turnSpeed;
        rotX += Input.GetAxis("Mouse Y") * turnSpeed;
    
        // clamp the vertical rotation
        rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);
    
        // rotate the camera
        transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y + y, 0);
    }
    
    void KeyboardMovement ()
    {
        Vector3 dir = new Vector3(0, 0, 0);
    
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");
    
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }

    void FireLaser()
    {
        Vector3 gun_pos = transform.position + new Vector3(0.35f,-0.25f,1f);

        Ray ray = new Ray(gun_pos, transform.forward);
        hits = Physics.RaycastAll(ray);
        
        Vector3 forward = transform.TransformDirection(Vector3.forward) * range;
        Debug.DrawRay(gun_pos, forward, Color.red);
        
        foreach(RaycastHit obj in hits)
        {
            Destroy(obj.transform.gameObject);
        }
    }

    void Start()
    {
        
    }
    
    void Update ()
    {
        MouseAiming();
        //KeyboardMovement();

        
    }



 

}
