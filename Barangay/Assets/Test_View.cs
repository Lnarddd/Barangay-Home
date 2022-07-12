using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_View : MonoBehaviour
{
    RaycastHit hit;
    [SerializeField] private Transform PlayerTarget;
    bool search;
    // Update is called once per frame
    void Update()
    {
        if(search == true){
            if(Physics.Raycast(transform.position, (PlayerTarget.position - transform.position), out hit, Mathf.Infinity)){
                if(hit.transform.tag == "Player"){
                Debug.Log("hit");
                }
        }
        }
    }

    private void OnTriggerEnter(Collider other) {
        search = true;
    }

    private void OnTriggerExit(Collider other) {
        search = false;
    }
}
