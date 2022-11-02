using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public Transform target;
    public float speed;
    public float rotSpeed;
    Rigidbody rb;
    Vector3 dir;
    Quaternion rot;
    public GameObject ExpoSounder;
    GameObject cam;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GameObject.Find("CameraRocket");
        cam.transform.SetParent(this.transform);
        
        //cam.transform.rotation = Quaternion.Euler(27, 0, 0);
    }

    private void Update()
    {
        cam.transform.rotation = transform.rotation;
        cam.transform.localPosition = new Vector3(0, 3, -1.2f);
        dir = target.position - transform.position;

        rot = Quaternion.LookRotation(dir);
    }
    void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;
        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation,rot,rotSpeed*Time.deltaTime));
    }
    private void OnCollisionEnter(Collision other)
    {
        Instantiate(ExpoSounder, transform.position, Quaternion.identity);
        cam.transform.SetParent(null);
        if (other.gameObject.layer == 6)
        {
           
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
}
