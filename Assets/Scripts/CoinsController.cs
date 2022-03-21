using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsController : MonoBehaviour
{

    public ParticleSystem collisionParticleSystem;
    public MeshRenderer sr;
    public bool once = true;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Coin collecte started!");
    }

    public void OnTriggerEnter(Collider Col)
    {
        if(Col.gameObject.tag == "Player" && once)
        {
            GlobalEventManager.SendOnCoinCollision();
            once = false;

            var em = collisionParticleSystem.emission;
            var dur = collisionParticleSystem.main.duration;
            em.enabled = true;
            collisionParticleSystem.Play();
            Destroy(sr);
            Invoke (nameof (DestroyObj), dur);
        }
    }

    void DestroyObj() {
        Destroy(gameObject);
    }
}    
