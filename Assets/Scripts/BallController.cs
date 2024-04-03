using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    public float moveSpeed = 0;
    public Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        Launch();
        
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Paddle")
        {
            rb.velocity = new Vector3(rb.velocity.x + 0.25f, 0f, -rb.velocity.z + 0.25f);
        }
        if (other.gameObject.tag == "Wall")
        {
            if (rb.velocity.z < 0.25f)
            {
                rb.velocity = new Vector3(-rb.velocity.x, 0f, rb.velocity.z * 1.1f);
            }
            else
            {
                rb.velocity = new Vector3(-rb.velocity.x, 0f, rb.velocity.z);
            }
        }
        if (other.gameObject.tag == "Goal")
        {
            rb.velocity = new Vector3(0f, 0f, 0f);
            transform.localPosition = new Vector3(0f, 0.3f, 0f);
            Invoke(nameof(Launch), 2);
        }
    }

    

    private void Launch()
    {
        float x = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);

        if(z < 0.25) 
        { 
            z = z * 6;
        }
        if(z == 0)
        {
            z = 0.5f;
        }
        if (x == 0)
        {
            x = 0.5f;
        }

        Vector3 direction = new Vector3(x, 0f, z);
        direction.Normalize();
        rb.velocity = direction * moveSpeed;
    }
}
