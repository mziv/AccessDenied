using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public float speed = 6.0F;
    public float rotationSpeed = 60.0f;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    private Map map;

    private void Start()
    {
        map = FindObjectOfType<Map>();
    }

    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        moveDirection = new Vector3(0, 0 ,0);

        processCheckMapRequest();

        if (controller.isGrounded)
        {
            //FPS version
            if (Input.GetKey("w"))
            {
                moveDirection = gameObject.transform.forward;
            }else if (Input.GetKey("s"))
            {
                moveDirection = -gameObject.transform.forward;
            }

            if (Input.GetKey("a"))
            {
                transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);
            }else if (Input.GetKey("d"))
            {
                transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            }

            //TPS version
            /*
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            if (moveDirection != Vector3.zero)
            {
                Quaternion qTo = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, qTo, Time.deltaTime * rotationSpeed);

            }*/
                moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
        }
 
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
    
    void processCheckMapRequest()
    {
        if (Input.GetKeyUp("m") && !map.mapIsOpen())
        {
            map.openMap();
        }
        else if(Input.GetKeyUp("m") && map.mapIsOpen())
        {
            map.closeMap();
        }
    }
}
