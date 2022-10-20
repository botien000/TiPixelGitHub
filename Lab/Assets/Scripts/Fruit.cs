using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private Transform posMove;
    [SerializeField] private float speed;

    private bool hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!hit)
            return;
        transform.position = Vector3.MoveTowards(transform.position, posMove.position, speed);
        if(transform.position == posMove.position)
        {
            Destroy(gameObject);
            UIManager.instance.Score();
        }
    }
    public void Hit()
    {
        hit = true;
    }

}
