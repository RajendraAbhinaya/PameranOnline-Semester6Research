using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public GameObject waypoints;
    public AudioClip footsteps;
    private UnityEngine.AI.NavMeshAgent agent;
    private List<Transform> destinations = new List<Transform>();
    private Animator animator;
    private int destinationAmount;
    private Vector3 targetDestination;
    private bool destinationSet = false;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = this.GetComponent<Animator>();
        audioSource = this.GetComponent<AudioSource>();

        destinationAmount = waypoints.transform.childCount;
        for(int i=0; i<destinationAmount; i++){
            destinations.Add(waypoints.transform.GetChild(i));
        }
        NewDestination();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = (targetDestination - transform.position).magnitude;
        if(distance < 1f && destinationSet){
            Invoke("NewDestination", Random.Range(3f, 8f));
            animator.SetBool("Walk", false);
            destinationSet = false;
            StopAllCoroutines();
        }
    }

    void NewDestination(){
        targetDestination = destinations[Random.Range(0, destinationAmount)].position;
        agent.SetDestination(targetDestination);
        animator.SetBool("Walk", true);
        destinationSet = true;
        StartCoroutine(Footsetps());
    }

    IEnumerator Footsetps(){
        while(true){
            audioSource.PlayOneShot(footsteps, 0.5f);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
