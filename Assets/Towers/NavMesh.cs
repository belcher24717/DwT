using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMesh : MonoBehaviour
{

    public NavMeshAgent agent;
    public GameObject Destination;

	// Use this for initialization
	void Start ()
    {
        agent.destination = Destination.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
