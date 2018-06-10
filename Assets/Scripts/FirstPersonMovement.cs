using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FirstPersonMovement : MonoBehaviour {

    private float speed = 25f;

    private float horz = 0f;
    private float vert = 0f;
    private Rigidbody mRigidbody = null;


    private void Awake()
    {
        mRigidbody = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate() {

        transform.forward = Camera.main.transform.forward;
        transform.rotation = new Quaternion(0f, transform.rotation.y, 0f, transform.rotation.w);

        horz = Input.GetAxisRaw("Horizontal");
        vert = Input.GetAxisRaw("Vertical");

        Vector3 forward = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);
        Vector3 right = Vector3.ProjectOnPlane(Camera.main.transform.right, Vector3.up);

        Vector3 movement = forward * vert;
        movement += (right * horz) / 2;
        movement = movement.normalized * Time.fixedDeltaTime * speed;

        mRigidbody.MovePosition(transform.position + movement);

        transform.position += movement;



    }

}
