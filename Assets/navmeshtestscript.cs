using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navmeshtestscript : MonoBehaviour
{
    public NavMeshAgent Agent;
    public GameObject Destination;

    // Use this for initialization
    void Start()
    {
        //Agent = this.gameObject.GetComponent<NavMeshAgent>();
        Agent.destination = Destination.transform.position;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
