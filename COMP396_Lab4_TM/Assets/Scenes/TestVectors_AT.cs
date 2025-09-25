using Mono.Cecil.Cil;
using UnityEngine;

public class TestVectors_AT : MonoBehaviour
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
        print($"v1+v2={v1_plus_v2},v2+v1={v2_plus_v1}, diff={v1_plus_v2 - v2_plus_v1}");

        //Calculate: V1.k, k.v1, print results
        Vector3 v1_k = v1 * k;
        Vector3 k_v1 = k*v1;
        print($"v1*k={v1_k},k*v1={k_v1}, diff={v1_k - k_v1}");


        //TODO:
        //Calculate: |v1| (size of vector v1), |v2|
        float v1_size = v1.magnitude;
        float v2_size = v2.magnitude;
        float v1_size2 = Mathf.Sqrt(v1.x * v1.x + v1.y * v1.y + v1.z * v1.z);
        print($"|v1|=v1_size={v1_size}");
        print($"|v1|2==v1_size=2={v1_size2}");
        print($"|v2|={v2_size}");




        //Calculate: v1_hat (unit vector), v2_hat

        //Calculate: v1.v2 (dot product), v1_hat.v2_hat
        //Vector3.Dot(v1, v2);
        //Calculate: v1 x v2 (Cross product), v2 x v1, compare them
        //Vector3.Cross(v1, v2);
        //Calculate. Magnitude, Distance, 1-2 Lerps
        //
    }
    // Update is called once per frame
    void Update()
    {
        //
    }

}
