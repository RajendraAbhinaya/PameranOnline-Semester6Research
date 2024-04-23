using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMiniature : MonoBehaviour
{
    public Car car;
    private float despawnTimer;
    // Start is called before the first frame update
    void Start()
    {
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
}
