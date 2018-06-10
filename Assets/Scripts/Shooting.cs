using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Shooting : MonoBehaviour {

    private ParticleSystem mNormalShots = null;
    private PlayableDirector gunAnimation = null;



    private void Start()
    {
        mNormalShots = Camera.main.transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>();
        gunAnimation = mNormalShots.transform.parent.GetComponent<PlayableDirector>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            gunAnimation.Play();
        else if (Input.GetButtonUp("Fire1"))
            gunAnimation.Stop();

        if (Input.GetButton("Fire1"))
        {
            mNormalShots.Emit(1);

        }

    }



}
