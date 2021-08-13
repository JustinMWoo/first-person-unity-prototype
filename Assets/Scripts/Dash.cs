using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public GameObject cameraObj;
    public GameObject UIOverlay;
    float timeStamp;
    float timeStamp2;
    float timer;
    float abilityCooldown = 0.5f;
    float abilityCooldown2 = 2.0f;
    CharacterController controller;

    int index = 0;
    int posMax = 5;
    Vector3?[] positions;
    bool moving = false;
    bool stepping = false;
    Rigidbody rb;
    Collider m_collider;
    int count = 1;

    private bool qPressed = false;
    private bool shiftPressed = false;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        positions = new Vector3?[posMax];
        rb = GetComponent<Rigidbody>();
        m_collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            qPressed = true;
        }
        else
        {
            qPressed = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            shiftPressed = true;
        }
        else
        {
            shiftPressed = false;
        }
    }
    private void FixedUpdate()
    {
        if (timer <= Time.time && !moving)
        {
            timer = Time.time + 1;
            if (index >= posMax)
            {
                index = 0;
            }
            positions[index++] = transform.position;
            //int count = 0;
            //foreach (var pos in positions)
            //{
            //    Debug.Log(count + ": " +pos);
            //    count++;
            //}

        }
        if (qPressed && timeStamp2 <= Time.time && !moving)
        {
            timeStamp2 = Time.time + abilityCooldown2;
            UIOverlay.SetActive(true);
            controller.enabled = false;
            rb.isKinematic = true;
            moving = true;
            index--;
        }

        if (moving && !stepping)
        {
            if (index < 0)
            {
                index = posMax - 1;
            }
            if (positions[index] == null)
            {
                moving = false;
                controller.enabled = true;
                rb.isKinematic = false;
                UIOverlay.SetActive(false);
                count = 1;
                return;
            }

            StartCoroutine(Step((Vector3)positions[index]));
            count++;
            positions[index] = null;
            index--;

        }



        if (shiftPressed && timeStamp <= Time.time)
        {
            timeStamp = Time.time + abilityCooldown;
            controller.enabled = false;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, cameraObj.transform.forward, out hit, 5f))
            {
                transform.position += cameraObj.transform.forward * hit.distance;
            }
            else
            {
                transform.position = transform.position + cameraObj.transform.forward * 5f;
            }
            controller.enabled = true;
        }
    }

    private IEnumerator Step(Vector3 position)
    {
        m_collider.enabled = false;
        Vector3 startPos = transform.position;
        stepping = true;
        float step = (15 * count / (startPos - position).magnitude) *Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            t += step;
            transform.position = Vector3.Lerp(startPos, position, t);
            yield return new WaitForFixedUpdate();
        }
        transform.position = position;
        stepping = false;
        m_collider.enabled = true;
    }
}
