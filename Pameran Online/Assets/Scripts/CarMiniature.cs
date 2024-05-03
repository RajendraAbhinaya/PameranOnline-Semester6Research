using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMiniature : MonoBehaviour
{
    public Car car;
    public AudioClip collisionAudio;
    private float despawnTimer;
    private AudioSource audioSource;
    private Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        rigidBody = this.GetComponent<Rigidbody>();
        despawnTimer = Time.time + 60f;
        StartCoroutine(CheckDespawn());
    }

    // Update is called once per frame
    IEnumerator CheckDespawn(){
        while(true){
            if(Time.time > despawnTimer){
                DestroyMiniature();
            }
            yield return new WaitForSeconds(5f);
        }
    }

    public void SetDespawnTimer(){
        despawnTimer = Time.time + 60f;
    }

    public void DestroyMiniature(){
        Debug.Log("Destroyed " + this.gameObject.name);
        Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision col){
        float velocity = Mathf.Clamp(Vector3.Magnitude(rigidBody.velocity) / 5f, 0.1f, 1f);
        audioSource.PlayOneShot(collisionAudio, velocity);
        Debug.Log(velocity);
    }
}
