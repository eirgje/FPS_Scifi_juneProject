using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class FirstPersonMovement : MonoBehaviour {

    private float speed = 25f;


    private float horz = 0f;
    private float vert = 0f;
    private Rigidbody mRigidbody = null;


    //Jump
    private float jetPackThrust = 1500f;
    private float durationOfJetPack = 2f;
    private float currentJetPackTime = 0f;
    private Image JetpackFill = null;
    private bool jetpackDelayed = false;
    private bool havePlayerReleasedJetpack = true;


    private void Awake()
    {
        mRigidbody = GetComponent<Rigidbody>();
        JetpackFill = GameObject.Find("Game_GUI").transform.GetChild(0).GetChild(0).GetComponent<Image>();

        currentJetPackTime = durationOfJetPack;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate() {

        Movement();

        JetPack();
    }

    private void Movement()
    {

        transform.forward = Camera.main.transform.forward;
        transform.rotation = new Quaternion(0f, transform.rotation.y, 0f, transform.rotation.w);

        horz = Input.GetAxisRaw("Horizontal");
        vert = Input.GetAxisRaw("Vertical");

        Vector3 forward = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);
        Vector3 right = Vector3.ProjectOnPlane(Camera.main.transform.right, Vector3.up);

        Vector3 movement = forward * vert;
        movement += (right * horz) / 2;
        movement = movement.normalized * Time.fixedDeltaTime * speed;
        if (!GroundCheck())
        {
            movement /= 4;
        }

        movement = new Vector3(movement.x, -20f * Time.deltaTime, movement.z);

            mRigidbody.MovePosition(transform.position + movement);

        transform.position += movement;
    }

    private void JetPack()
    {
        if (!jetpackDelayed)
        {
            if (havePlayerReleasedJetpack)
            {
                if (!Input.GetButton("Jump"))
                {
                    if (currentJetPackTime < durationOfJetPack)
                    {
                        currentJetPackTime += Time.fixedDeltaTime;
                    }
                }
                else if (Input.GetButton("Jump") && currentJetPackTime > 0f)
                {
                    mRigidbody.velocity = new Vector3(mRigidbody.velocity.x, jetPackThrust * Time.deltaTime, mRigidbody.velocity.z);

                    currentJetPackTime -= Time.fixedDeltaTime;

                    if (currentJetPackTime < 0.05f)
                        StartCoroutine(WaitForJetpackRecovery(1f));
                }

                JetpackFill.fillAmount = currentJetPackTime / durationOfJetPack;
            }
            else
            {
                if (!Input.GetButton("Jump"))
                {
                    havePlayerReleasedJetpack = true;
                }
            }
        }
    }

    private IEnumerator WaitForJetpackRecovery(float duration)
    {
        jetpackDelayed = true;

        if(currentJetPackTime < 0.05f)
            havePlayerReleasedJetpack = false;

        yield return new WaitForSeconds(duration);
        jetpackDelayed = false;
    }

    private bool GroundCheck()
    {
        RaycastHit hit;
        Ray groundCheck = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(groundCheck, out hit, 0.5f))
        {
            return true;
        }

        return false;
    }
}
