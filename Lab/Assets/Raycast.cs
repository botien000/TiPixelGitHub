using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    [SerializeField] private float speedMove;

    int dir = 1;// Hướng
    float _distance = 5;// Khoảng cách va chạm

    void Update()
    {
        transform.position += Vector3.right * speedMove * Time.deltaTime * dir;
    }
    void FixedUpdate()
    {
        if(Physics2D.Raycast(transform.position,transform.right, _distance))
        {
            Debug.Log("Hit something");
            // FLip đối tượng
            Direction();
        }
    }
    void Direction()
    {
        if (transform.rotation.y == 1 || transform.rotation.y == -1)
        {
            dir = -1;
            transform.rotation = Quaternion.AngleAxis(0, Vector2.zero);
        }
        else
        {
            dir = 1;
            transform.rotation = Quaternion.AngleAxis(180, Vector2.up);
        }
    }
}
