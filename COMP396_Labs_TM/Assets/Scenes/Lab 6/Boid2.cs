using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow;

public class Boid2 : MonoBehaviour
{
    internal FlockController2 controller;
    private new Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        controller =
        (FlockController2)GameObject.FindGameObjectWithTag("FlockController2").GetComponent("FlockController2");
    }

    void Update()
    {
        if (controller)
        {
            Vector3 relativePos = Steer() * Time.deltaTime;
            if (relativePos != Vector3.zero)
                rigidbody.linearVelocity = relativePos;

            // enforce minimum and maximum speeds for the boids
            float speed = rigidbody.linearVelocity.magnitude;

            if (speed > controller.maxVelocity)
            {
                rigidbody.linearVelocity = rigidbody.linearVelocity.normalized *
                controller.maxVelocity;
            }
            else if (speed < controller.minVelocity)
            {
                rigidbody.linearVelocity = rigidbody.linearVelocity.normalized *
                controller.minVelocity;
            }
        }
    }

    private Vector3 Steer()
    {
        Vector3 center = controller.flockCenter - transform.localPosition; // cohesion
        Vector3 velocity = controller.flockVelocity - rigidbody.linearVelocity; // alignement
        Vector3 follow = controller.target.localPosition - transform.localPosition; // follow leader
        Vector3 separation = Vector3.zero;

        foreach (Boid2 boid2 in controller.flockList)
        {
            if (boid2 != this)
            {
                Vector3 relativePos = transform.localPosition - boid2.transform.localPosition;
                separation += relativePos.normalized;
            }
        }

        // randomize: [0,1)*2-1 => [-1,1)
        Vector3 randomize = new Vector3(Random.value * 2 - 1, Random.value * 2 - 1, Random.value * 2 - 1);
        randomize.Normalize();

        return (
            controller.centerWeight * center // cohesion
            + controller.velocityWeight * velocity // alignment
            + controller.separationWeight * separation // separation
            + controller.followWeight * follow // following the leader
            + controller.randomizeWeight * randomize // random
        );
    }
}
