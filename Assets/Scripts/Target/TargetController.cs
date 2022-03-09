using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetController : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject pathways;
    public Animator animator;
    public float xRange = 2f;
    public float zRange = 2f;
    public Vector3 destinationSelected;
    
    private int previousSelected;
    private int nextDestination;

    private void Start()
    {
        pathways = GameObject.Find("Pathways");
        animator.SetBool("Run", true);
        destinationSelected = pathways.transform.GetChild(0).position;
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckPoint(0))
        {
            nextDestination = Random.Range(1, 3);
            destinationSelected = pathways.transform.GetChild(nextDestination).position;
        }
        if (CheckPoint(2))
        {
            destinationSelected = pathways.transform.GetChild(3).position;
        }
        if (CheckPoint(1))
        {
            destinationSelected = pathways.transform.GetChild(4).position;
        }
        if (CheckPoint(3))
        {
            destinationSelected = pathways.transform.GetChild(6).position;
        }
        if (CheckPoint(4))
        {
            destinationSelected = pathways.transform.GetChild(5).position;
        }
        if (CheckPoint(5))
        {
            nextDestination = Random.Range(1, 3);
            if (nextDestination == 1 && previousSelected != 6)
            {
                previousSelected = 5;
                destinationSelected = pathways.transform.GetChild(6).position;
            }
            else
                destinationSelected = pathways.transform.GetChild(8).position;
        }
        if (CheckPoint(6))
        {
            nextDestination = Random.Range(1, 3);
            if (nextDestination == 1 && previousSelected != 5)
            {
                previousSelected = 6;
                destinationSelected = pathways.transform.GetChild(5).position;
            }
            else
                destinationSelected = pathways.transform.GetChild(7).position;
        }
        if (CheckPoint(7))
        {
            destinationSelected = pathways.transform.GetChild(10).position;
        }
        if (CheckPoint(8))
        {
            destinationSelected = pathways.transform.GetChild(9).position;
        }
        if (CheckPoint(9))
        {
            destinationSelected = pathways.transform.GetChild(11).position;
        }
        if (CheckPoint(10))
        {
            destinationSelected = pathways.transform.GetChild(11).position;
        }
        if (CheckPoint(11))
        {
            destinationSelected = pathways.transform.GetChild(12).position;
        }
        

        if(destinationSelected != null)
            agent.SetDestination(destinationSelected);

        if (CheckPoint(12))
        {
            Destroy(gameObject);
        }
    }

    private bool CheckPoint(int childIndex)
    {
        return transform.position.x >= destinationSelected.x - xRange && transform.position.x <= destinationSelected.x + xRange && transform.position.z >= destinationSelected.z - zRange && transform.position.z <= destinationSelected.z + zRange && destinationSelected == pathways.transform.GetChild(childIndex).position;
    }
}
