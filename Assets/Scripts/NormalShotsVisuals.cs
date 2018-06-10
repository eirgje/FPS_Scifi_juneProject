using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalShotsVisuals : MonoBehaviour {

    private ParticleSystem mNormalShots = null;
    private ParticleSystem splatterParticle = null;
    List<ParticleCollisionEvent> collisionEvents;


    private void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
        mNormalShots = GetComponent<ParticleSystem>();
        splatterParticle = transform.GetChild(0).GetComponent<ParticleSystem>();
    }
    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(mNormalShots, other, collisionEvents);
        print("event found");
        for (int i = 0; i < collisionEvents.Count; i++)
        {

            EmitAtLocation(collisionEvents[i]);
        }
    }

    private void EmitAtLocation(ParticleCollisionEvent particleCollisionEvent)
    {
        splatterParticle.transform.position = particleCollisionEvent.intersection;
        splatterParticle.transform.rotation = Quaternion.LookRotation(particleCollisionEvent.normal);
        print(particleCollisionEvent.intersection);
        splatterParticle.Emit(5);
    }
}
