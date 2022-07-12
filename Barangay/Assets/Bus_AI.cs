using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Bus_AI : MonoBehaviour
{
    [SerializeField] private Transform Target;
    public NavMeshAgent agent;
    
    void Start()
    {
        agent.destination = Target.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
