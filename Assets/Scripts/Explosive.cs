using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    private bool done = false;
    private float delay = 3;
    private float timeStamp;
    public ParticleSystem ps;
    private void Start()
    {
        timeStamp = Time.time + delay;
        Destroy(gameObject, delay + 1);
    }

    private void Update()
    {
        if (timeStamp <= Time.time && !done)
        {
            done = true;
            ps.Play();
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            Collider[] colliders = Physics.OverlapSphere(transform.position, 25f);

            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("pushable"))
                {
                    collider.gameObject.GetComponent<Rigidbody>().AddExplosionForce(1000, transform.position, 25);
                }
            }
        }
    }
}
