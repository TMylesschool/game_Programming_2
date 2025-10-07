using System;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public float minSpeed = 20.0f;
    public float turnSpeed = 20.0f;
    public float randomFreq = 20.0f;
    public float randomForce = 20.0f;

    public float toOriginForce = 50.0f;
    public float toOriginRange = 100.0f;
    public float gravity = 20.0f;

    public float avoidanceRadius = 50.0f;
    public float avoidanceForce = 2.0f;

    public float followVelocity = 4.0f;
    public float followRadius = 40.0f;

    private Transform origin;
    private Vector3 velocity;
    private Vector3 normalizedVelocity;
    private Vector3 randomPush;
    private Vector3 originPush;
    private Transform[] objects;
    private Boid[] otherBoids;
    private Transform transformComponent;
    private float randomFreqInterval;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        randomFreqInterval = 1.0f / randomFreq;
        origin = transform.parent;
        transformComponent = transform;
        Component[] tempBoid = null;

        if (transform.parent)
        {
            tempBoid = transform.GetComponentsInChildren<Boid>();
        }

        objects = new Transform[tempBoid.Length];
        otherBoids = new Boid[tempBoid.Length];
        for (int i = 0; i < tempBoid.Length; i++)
        {
            objects[i] = tempBoid[i].transform;
            otherBoids[i] = (Boid)tempBoid[i];
        }

        transform.parent = null;

        StartCoroutine(UpdateRandom());
    }

    private string UpdateRandom()
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
