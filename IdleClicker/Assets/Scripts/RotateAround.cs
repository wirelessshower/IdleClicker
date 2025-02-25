using UnityEngine;

public class RotateAround : MonoBehaviour
{
    private float speed = 3f;
    void Update()
    {
        transform.Rotate(new Vector3(0,0,1)  * Time.deltaTime * speed);
    }
}
