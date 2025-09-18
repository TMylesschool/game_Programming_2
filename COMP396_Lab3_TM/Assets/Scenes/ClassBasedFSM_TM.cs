using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Demonstrating the use of the StateMachine class
/// Seek_Waypoint,
/// Chase,
/// Attack,
/// Evade,
/// Patrol,
/// Investigate
/// </summary>
public class ClassBasedFSM : MonoBehaviour
{
    public GameObject player;
    public float distanceToChase = 10; // chase if <=10 m away
    public bool InFront;
    public float distanceToAttack = 2; // attack if <=2 m away
    public float FOV = 60; // 60 deg Field Of View
    public float speed = 1f;
    public bool StrongerThanPlayer = true;

    private float FOV_in_RAD;
    public float csCosFOV_2;

    public float Deg2Rad(float deg) { return deg / 180f * Mathf.PI; }
    public float Rad2Deg(float rad) { return rad * 180 / Mathf.PI; }

    StateMachine stateMachine;

    void Start()
    {
        // setup FOV helper
        csCosFOV_2 = Mathf.Cos(Deg2Rad(FOV) / 2f);

        stateMachine = new StateMachine();

        // --- Seek_Waypoint ---
        var seekWaypoint = stateMachine.CreateState("Seek_Waypoint");
        seekWaypoint.onEnter = delegate { Debug.Log("In Seek_Waypoint.onEnter"); };
        seekWaypoint.onStay = delegate {
            Vector3 playerHeading = player.transform.position - this.transform.position;
            float distanceToPlayer = playerHeading.magnitude;
            Vector3 directionToPlayer = playerHeading.normalized;
            bool InFront = (Vector3.Dot(this.transform.forward, directionToPlayer) >= csCosFOV_2);

            if (InFront)
            {
                if (distanceToPlayer <= distanceToAttack)
                {
                    stateMachine.TransitionTo("Attack");
                }
                else if (distanceToPlayer <= distanceToChase)
                {
                    if (this.StrongerThanPlayer)
                        stateMachine.TransitionTo("Chase");
                    else
                        stateMachine.TransitionTo("Evade");
                }
            }
        };
        seekWaypoint.onExit = delegate { Debug.Log("In Seek_Waypoint.onExit"); };

        // --- Chase ---
        var chase = stateMachine.CreateState("Chase");
        chase.onEnter = delegate { Debug.Log("In Chase.onEnter"); };
        chase.onStay = delegate {
            Vector3 E = this.transform.position;
            Vector3 P = player.transform.position;
            Vector3 Heading = P - E;
            Vector3 HeadingDir = Heading.normalized;
            this.transform.position += HeadingDir * speed * Time.deltaTime;

            if (!this.StrongerThanPlayer)
                stateMachine.TransitionTo("Evade");
        };
        chase.onExit = delegate { Debug.Log("In Chase.onExit"); };

        // --- Attack ---
        var attack = stateMachine.CreateState("Attack");
        attack.onEnter = delegate { Debug.Log("In Attack.onEnter"); };
        attack.onStay = delegate { /* attack behaviour here */ };
        attack.onExit = delegate { Debug.Log("In Attack.onExit"); };

        // --- Evade ---
        var evade = stateMachine.CreateState("Evade");
        evade.onEnter = delegate { Debug.Log("In Evade.onEnter"); };
        evade.onStay = delegate {
            Vector3 E = this.transform.position;
            Vector3 P = player.transform.position;
            Vector3 Heading = E - P;
            Vector3 HeadingDir = Heading.normalized;
            this.transform.position += HeadingDir * speed * Time.deltaTime;

            if (this.StrongerThanPlayer)
                stateMachine.TransitionTo("Chase");
        };
        evade.onExit = delegate { Debug.Log("In Evade.onExit"); };

        // --- Patrol ---
        var patrol = stateMachine.CreateState("Patrol");
        patrol.onEnter = delegate { Debug.Log("In Patrol.onEnter"); };
        patrol.onStay = delegate {
            // Example: move forward constantly as a simple patrol
            this.transform.position += this.transform.forward * speed * Time.deltaTime;

            // Check for player
            Vector3 playerHeading = player.transform.position - this.transform.position;
            float distanceToPlayer = playerHeading.magnitude;
            if (distanceToPlayer <= distanceToChase)
                stateMachine.TransitionTo("Seek_Waypoint");
        };
        patrol.onExit = delegate { Debug.Log("In Patrol.onExit"); };

        // --- Investigate ---
        var investigate = stateMachine.CreateState("Investigate");
        investigate.onEnter = delegate { Debug.Log("In Investigate.onEnter"); };
        investigate.onStay = delegate {
            // Rotate in place like searching
            this.transform.Rotate(Vector3.up, 30f * Time.deltaTime);

            // After some time (or condition), go back to patrol
            if (Random.value < 0.01f)
                stateMachine.TransitionTo("Patrol");
        };
        investigate.onExit = delegate { Debug.Log("In Investigate.onExit"); };
    }

    void Update()
    {
        Debug.Log("StrongerThanPlayer=" + StrongerThanPlayer);
        stateMachine.Update();
    }
}
