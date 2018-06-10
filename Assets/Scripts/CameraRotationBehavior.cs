using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationBehavior : MonoBehaviour {

    private Transform playerTransform = null;
    private float cameraSpeed = 35f;

    [SerializeField]
    private Vector3 headOffset = new Vector3(0.25f, 1, 0);

    // Use this for initialization
    void Start () {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = playerTransform.position + headOffset;
        transform.RotateAround(transform.position, Vector3.up, Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * cameraSpeed);
    }

}
