using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicAgent : MonoBehaviour
{

    [SerializeField] NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent.SetDestination(new Vector3(0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            agent.destination = hit.point;
        }
    }
}
