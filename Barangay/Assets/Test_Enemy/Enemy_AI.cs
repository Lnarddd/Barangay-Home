using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_AI : MonoBehaviour
{
    public NavMeshAgent agent;
    [SerializeField] private Transform PlayerTarget;
    private int current_waypoint, last_waypoint;
    public int waypoint;
    private bool isWaiting, isChasing, isSeen;
    public float WaitingTimer, ChaseTimer;
    private float WaitingTimerLeft, ChaseTimerLeft;
    RaycastHit hit;

    //this is the timer for the enemy to watch the area before moving to next waypoint
    private void Update() {
        if( WaitingTimerLeft > 0){
            WaitingTimerLeft -= 1 * Time.deltaTime;
        }
        else if(isWaiting == true && isChasing == false){
            isWaiting = false;
            patrol();
        }

        //ignore everything and chase the player when seen
        if(Physics.Raycast(transform.position, (PlayerTarget.position - transform.position), out hit, Mathf.Infinity) && isSeen == true){
            if(hit.transform.tag == "Player"){
                ChaseTimerLeft = ChaseTimer;
                WaitingTimerLeft = 0;
                isWaiting = false;
                isChasing = true;
            }
        }

        //this is how many seconds the object would follow the player, if the player left its sight it would wait for a few seconds before returning to wander
        if(ChaseTimerLeft > 0){
            ChaseTimerLeft -= 1 * Time.deltaTime;
            agent.destination = PlayerTarget.position;
            Debug.Log(ChaseTimerLeft);
        }
        else if(isChasing == true){
            WaitingTimerLeft = WaitingTimer;
            isWaiting = true;
            isChasing = false;
        }

    }

    //start random pathing
    private void Start() {
        current_waypoint = Random.Range(1,waypoint+1);
        patrol();
    }
    
    //setup different waypoints this can be edited
    void patrol(){
        if(current_waypoint == last_waypoint){
            current_waypoint = Random.Range(1,waypoint+1);
            patrol();
        }
        else{
            switch(current_waypoint){
            case 1:
                Debug.Log(current_waypoint);
                last_waypoint = current_waypoint;
                Transform waypoint_1 = GameObject.Find("Waypoint_1").transform;
                agent.destination = waypoint_1.position;
                break;
            case 2:
                Debug.Log(current_waypoint);
                last_waypoint = current_waypoint;
                Transform waypoint_2 = GameObject.Find("Waypoint_2").transform;
                agent.destination = waypoint_2.position;
                break;
            case 3:
                Debug.Log(current_waypoint);
                last_waypoint = current_waypoint;
                Transform waypoint_3 = GameObject.Find("Waypoint_3").transform;
                agent.destination = waypoint_3.position;
                break;
            case 4:
                Debug.Log(current_waypoint);
                last_waypoint = current_waypoint;
                Transform waypoint_4 = GameObject.Find("Waypoint_4").transform;
                agent.destination = waypoint_4.position;
                break;
            case 5:
                Debug.Log(current_waypoint);
                last_waypoint = current_waypoint;
                Transform waypoint_5 = GameObject.Find("Waypoint_5").transform;
                agent.destination = waypoint_5.position;
                break;
            }
        }
    }

    //checks if the waypoint was hit the second if is to check if player enters the object view
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Waypoint" &&  isChasing == false){
            current_waypoint = Random.Range(1,waypoint+1);
            WaitingTimerLeft = WaitingTimer;
            isWaiting = true;
        }  

        if(other.gameObject.tag == "Player"){
            isSeen = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player"){
            isSeen = false;
        }
    }
}