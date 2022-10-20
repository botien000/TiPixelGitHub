using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBird : MonoBehaviour
{
    enum State
    {
        Fly,Hit
    }
    [SerializeField] private float speedFly;
    [SerializeField] private bool dirObj;

    private Rigidbody2D rgbody;
    private Enemy enemy;
    // Start is called before the first frame update
    private void OnEnable()
    {
        if(enemy == null)
        {
            enemy = GetComponent<Enemy>();
        }
        if(rgbody == null)
        {
            rgbody = GetComponent<Rigidbody2D>();
        }
        enemy.GetRg_Colli(rgbody, null);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.IsHitted)
        {
            enemy.PlayAnimation((int)State.Hit);
            return;
        }
        Move();
    }
    private void Move()
    {
        transform.position = new Vector3(transform.position.x + speedFly * (dirObj ? 1 : -1), transform.position.y, transform.position.z);
        enemy.PlayAnimation((int)State.Fly);
    }
}
