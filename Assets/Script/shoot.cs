using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    private float range = 100f;

    int destroyable; 
    int layer_mask;

    public RaycastHit[] hits;

    void FireLaser()
    {
        Vector3 gun_pos = transform.position + new Vector3(0.2f,0.04f,0.75f);
        //Vector3 forward = transform.TransformDirection(Vector3.left) * range;
        Vector3 left = transform.TransformDirection(Vector3.left);
        RaycastHit hit;

        if (Physics.Raycast(gun_pos, left, out hit, range, layer_mask)) 
        {
            Debug.DrawRay(transform.position, left, Color.yellow);
            Debug.Log("Did Hit");
            Destroy(hit.transform.gameObject);
        }
        else 
        {
            Debug.DrawRay(transform.position, left, Color.white);
            Debug.Log("Did not Hit");
        } 

        /*Ray ray = new Ray(gun_pos, transform.right * -1);
        hits = Physics.RaycastAll(ray);
        
        Vector3 forward = transform.TransformDirection(Vector3.left) * range;
        Debug.DrawRay(transform.position, forward, Color.blue);
        
        foreach(RaycastHit obj in hits)
        {
            Destroy(obj.transform.gameObject);
        }*/
    }
    // Start is called before the first frame update
    void Start()
    {
        destroyable = 8;
        layer_mask = 1 << destroyable; 
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Vector3 gun_pos = transform.position + new Vector3(0.5f,0.04f,0f);
        Vector3 forward = transform.TransformDirection(Vector3.left) * range;
        Debug.DrawRay(transform.position, forward, Color.blue);
        */
        if(Input.GetMouseButtonDown(0))
        {
            //Debug.Log("pew");
            FireLaser();
        }
        
    }
}
