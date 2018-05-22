using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]

public class CorrectNavMeshAgentScript : MonoBehaviour
{

    private NavMeshAgent agent;
    private Rigidbody rigidBody;
    private bool isDestinationSet;
    public GameObject Destination;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rigidBody = GetComponent<Rigidbody>();
        isDestinationSet = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionStay(Collision collision)
    {
        if (!isDestinationSet && agent?.isOnNavMesh == true && Destination != null)
        {
            isDestinationSet = true;
            rigidBody.isKinematic = true;
            rigidBody.useGravity = false;
            agent.destination = Destination.transform.position;
        }
    }
}
