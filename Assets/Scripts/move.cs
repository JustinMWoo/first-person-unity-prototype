using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    float horizontalSpeed = 2;
    float verticalSpeed = 2;
    float moveSpeed = 10;
    float jumpSpeed = 10;
    //float airSpeed = 0.5f;
    float gravity = 20;
    CharacterController controller;
    Vector3 moveDirection;


    public GameObject cameraObj;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (jump)
        //{
        //    transform.position += cameraObj.transform.forward * Time.deltaTime * moveSpeed * Input.GetAxis("Vertical");

        //    transform.position += cameraObj.transform.right * Time.deltaTime * moveSpeed * Input.GetAxis("Horizontal");
        //}
        //else
        //{
        //    transform.position += cameraObj.transform.forward * Time.deltaTime * moveSpeed * airSpeed * Input.GetAxis("Vertical");

        //    transform.position += cameraObj.transform.right * Time.deltaTime * moveSpeed * airSpeed * Input.GetAxis("Horizontal");
        //}


        float h = horizontalSpeed * Input.GetAxis("Mouse X");
        float v = verticalSpeed * -Input.GetAxis("Mouse Y");
        transform.Rotate(0, h, 0);

        cameraObj.transform.Rotate(v, h, 0);

        float z = cameraObj.transform.eulerAngles.z;
        cameraObj.transform.Rotate(0, 0, -z);

        //if (Input.GetKey(KeyCode.Space) && jump)
        //{
        //    jump = false;
        //    rb.AddForce(transform.up * jumpForce);
        //}

    }
    private void FixedUpdate()
    {

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = cameraObj.transform.TransformDirection(moveDirection);
            moveDirection *= moveSpeed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        //else
        //{
        //    moveDirection = new Vector3(Mathf.Clamp(moveDirection.x + Input.GetAxis("Horizontal") * airSpeed * moveSpeed * Time.fixedDeltaTime, -10, 10), moveDirection.y, Mathf.Clamp(moveDirection.x + Input.GetAxis("Vertical") * airSpeed * moveSpeed * Time.fixedDeltaTime, -10, 10));
        //    moveDirection = cameraObj.transform.TransformDirection(moveDirection);
        //}
        moveDirection.y -= gravity * Time.fixedDeltaTime;

        if (controller.enabled)
        {
            controller.Move(moveDirection * Time.fixedDeltaTime);
        }
    }
}
