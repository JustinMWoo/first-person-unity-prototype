using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float speed = 30;
    float force = 1000;
    public bool jump = false;
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
        Destroy(gameObject, 10);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("pushable"))
        {
            other.GetComponent<Rigidbody>().AddForce(transform.forward * force);
            if (jump)
            {
                other.GetComponent<Rigidbody>().AddForce(transform.up * force);
            }
        }

        Destroy(gameObject);
    }
}
