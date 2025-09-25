using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BigOhExercise : MonoBehaviour
{
    public int NumberOfElements = 1000;
    // Use this for initialization
    void Start()
    {
        //int[] numbers = new int[NumberOfElements];
        DateTime t1 = DateTime.Now;
        ArrayList al = new ArrayList();
        for (int i = 0; i < NumberOfElements; i++)
        {
            al.Add(UnityEngine.Random.Range(0, NumberOfElements));
        }
        DateTime t2 = DateTime.Now;
        al.Sort();
        DateTime t3 = DateTime.Now;
        Debug.Log("t1.sec:" + t1.Second);
        Debug.Log("t2.sec:" + t2.Second);
        Debug.Log("t3.sec:" + t3.Second);
        Debug.Log("t1.ms:" + t1.Millisecond);
        Debug.Log("t2.ms:" + t2.Millisecond);
        Debug.Log("t3.ms:" + t3.Millisecond);
        Debug.Log("t2-t1:" + t2.Subtract(t1));
        Debug.Log("t3-t2:" + t3.Subtract(t2));
    }
    // Update is called once per frame
    void Update()
    {
    }
}
