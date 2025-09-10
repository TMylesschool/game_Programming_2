using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Vector3 v1, v2, v3;
    float k;
    // Start is called before the first frame update
    void Start()
    {
        v1 = new Vector3(1, 2, 3);
        v2 = new Vector3(2, -1, 1);
        k = 1.5f;

        Vector3 v1_plus_v2 = v1 + v2;
        Vector3 v2_plus_v1 = v2 + v1;
        print($"v1_plus_v2={v1_plus_v2}, v2_plus_v1={v2_plus_v1}, diff={v1_plus_v2 - v2_plus_v1}");

        // Scalar Multiplication
        Vector3 v1_times_k = v1 * k;
        Vector3 k_times_v1 = k * v1;
        Debug.Log($"v1 * k = {v1_times_k}, k * v1 = {k_times_v1}");

        // Unit Vectors
        Vector3 v1_hat = v1.normalized;
        Vector3 v2_hat = v2.normalized;
        Debug.Log($"v1_hat = {v1_hat}, v2_hat = {v2_hat}");

        // Dot Product
        float dot_v1_v2 = Vector3.Dot(v1, v2);
        float dot_hat = Vector3.Dot(v1_hat, v2_hat);
        Debug.Log($"Dot product v1.v2 = {dot_v1_v2}, v1_hat.v2_hat = {dot_hat}");

        // Cross Product
        Vector3 cross_v1_v2 = Vector3.Cross(v1, v2);
        Vector3 cross_v2_v1 = Vector3.Cross(v2, v1);
        Debug.Log($"Cross v1 x v2 = {cross_v1_v2}, v2 x v1 = {cross_v2_v1}");

        // --- Required Usages ---

        // 1. Vector3.Angle
        float angle = Vector3.Angle(v1, v2);
        Debug.Log($"Angle between v1 and v2 = {angle} degrees");

        // 2. Vector3.Distance
        float dist = Vector3.Distance(v1, v2);
        Debug.Log($"Distance between v1 and v2 = {dist}");

        // 3. Vector3.Normalize
        Vector3 v3 = new Vector3(3, 4, 0);
        Vector3 v3_normalized = Vector3.Normalize(v3);
        Debug.Log($"Normalized v3 = {v3_normalized}");

        // 4. Vector3.SqrMagnitude
        float sqrMag_v1 = v1.sqrMagnitude;
        Debug.Log($"SqrMagnitude of v1 = {sqrMag_v1}");

        // 5. Vector3.Scale
        Vector3 scaled = Vector3.Scale(v1, v2); // Element-wise multiplication
        Debug.Log($"Element-wise scaled vector (v1 Scale v2) = {scaled}");

        // 6. Vector3.ClampMagnitude
        Vector3 clamped = Vector3.ClampMagnitude(v1, 2.0f);
        Debug.Log($"v1 clamped to magnitude 2 = {clamped}");
    }

    // Update is called once per frame
    void Update()
    {
        //
    }
}