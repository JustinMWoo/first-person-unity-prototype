using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject projectile;
    public GameObject projectile2;
    public GameObject projSpawn;

    public GameObject ability;

    public GameObject explosive;

    float abilityCooldown = 5;
    float timeStamp;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject proj = Instantiate(projectile, projSpawn.transform.position, transform.rotation);
        }

        if (Input.GetMouseButtonDown(1))
        {
            GameObject proj = Instantiate(projectile2, projSpawn.transform.position, transform.rotation);
        }

        if (Input.GetKeyDown(KeyCode.E) && timeStamp <= Time.time)
        {
            timeStamp = Time.time + abilityCooldown;
            GameObject proj = Instantiate(ability, projSpawn.transform.position, transform.rotation);
            proj.GetComponent<Rigidbody>().AddForce(proj.transform.forward * 1000);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject proj = Instantiate(explosive, projSpawn.transform.position, transform.rotation);
            proj.GetComponent<Rigidbody>().AddForce(proj.transform.forward * 1000);
        }
    }
}
