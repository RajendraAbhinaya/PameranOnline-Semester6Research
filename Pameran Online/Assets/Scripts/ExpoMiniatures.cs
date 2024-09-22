using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpoMiniatures : MonoBehaviour
{
    public AnimationCurve selectWiggle;
    public float animationLength;
    private Vector3 startingPosition;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    public void ExpoSelected(){
        StopAllCoroutines();
        StartCoroutine(ExpoAnimation());
    }

    IEnumerator ExpoAnimation(){
        for(float i = 0.0f; i <= animationLength; i += 0.02f){
            transform.position = startingPosition + new Vector3(0f, selectWiggle.Evaluate(i), 0f);
            yield return new WaitForSeconds(0.02f);
        }
    }
}
