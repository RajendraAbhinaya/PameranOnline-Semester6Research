using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialIcon : MonoBehaviour
{
    public GameObject icon;
    public GameObject particles;
    public bool destroyOnEnter;
    public float height;
    public float verticalSpeed;
    public float rotationSpeed;
    private float startingY;
    // Start is called before the first frame update
    void Start()
    {
        startingY = icon.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        icon.transform.position = new Vector3(icon.transform.position.x, startingY + Mathf.Sin(Time.time*verticalSpeed)*height, icon.transform.position.z);
        icon.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    void OnTriggerEnter(Collider col){
        if(col.gameObject.tag == "Player"){
            if(destroyOnEnter){
                DestroyIcon();
            }
            icon.SetActive(false);
        }
    }

    void OnTriggerExit(Collider col){
        if(col.gameObject.tag == "Player"){
            icon.SetActive(true);
        }
    }

    public void DestroyIcon(){
        GameObject spawnedParticles = Instantiate(particles, this.transform.position + new Vector3(0f, 1.5f, 0f), Quaternion.Euler(-90f, 0f, 0f));
        Destroy(spawnedParticles, 6f);
        Destroy(this.gameObject);
    }
}
