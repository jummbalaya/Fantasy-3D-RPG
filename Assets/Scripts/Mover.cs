using System;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    private Transform target;
    NavMeshAgent navMeshAgent;
    private Animator animator;
    private Ray ray;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MoveToCoursor();
        }
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        Vector3 velocity = navMeshAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        animator.SetFloat("forwardSpeed", speed);

    }

    private void MoveToCoursor()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);

        if (hasHit)
        {
            MoveToTarget(hit.point);
        }
    }

    private void MoveToTarget(Vector3 target)
    {
        navMeshAgent.destination = target;
    }
}
