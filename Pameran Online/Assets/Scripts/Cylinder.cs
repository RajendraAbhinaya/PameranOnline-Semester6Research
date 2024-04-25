using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : MonoBehaviour
{
    public GameObject directionalLight;
    private float alpha = 1f;
    private Renderer cylinderRenderer;
    private bool fading = false;
    // Start is called before the first frame update
    void Start()
    {
        cylinderRenderer = this.gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(fading){
            alpha = Mathf.Lerp(alpha, 0f, 0.05f);
            cylinderRenderer.material.color = new Color(0f, 0f, 0f, alpha);
        }
    }

    public void Fade(){
        directionalLight.SetActive(true);
        fading = true;
        Invoke("DestroyCylinder", 0.5f);
    }

    void DestroyCylinder(){
        Destroy(this.gameObject);
    }
}
