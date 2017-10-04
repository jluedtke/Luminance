using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour {

    public float speed;
    private NavMeshAgent nva;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private Camera mainCamera;


    private void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        nva = GetComponent<NavMeshAgent>();
        Physics.IgnoreLayerCollision(8, 15);
    }

    // Update is called once per frame
    void Update () {
     
        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        moveVelocity = moveInput * speed;

        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up * 10, Vector3.zero);

        float rayLength;

        if(groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.red);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }

    }

    private void FixedUpdate()
    {
        nva.velocity = moveVelocity;
    }
}
