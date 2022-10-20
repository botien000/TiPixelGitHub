using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    enum State
    {
        Idle, Run, Hit
    }
    [SerializeField] private Transform noGroundPos;
    [SerializeField] private Transform WallPos;
    [SerializeField] private float speedWalk;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask wallMask;


    private Enemy enemy;
    private int dirObj;
    private Rigidbody2D rgbody;
    private BoxCollider2D boxStand;
    private void OnEnable()
    {
        if (enemy == null)
        {
            enemy = GetComponent<Enemy>();
        }
        if (rgbody == null && boxStand == null)
        {
            rgbody = GetComponent<Rigidbody2D>();
            boxStand = GetComponentInChildren<BoxCollider2D>();
        }
        dirObj = -1;
        enemy.GetRg_Colli(rgbody, boxStand);
    }
    void FixedUpdate()
    {
        if (enemy.IsHitted)
        {
            enemy.PlayAnimation((int)State.Hit);
            return;
        }
        if (CheckGround_Wall())
        {
            if (transform.rotation.y == 1 || transform.rotation.y == -1)
            {
                dirObj = -1;
                transform.rotation = Quaternion.AngleAxis(0, Vector2.zero);
            }
            else
            {
                dirObj = 1;
                transform.rotation = Quaternion.AngleAxis(180, Vector2.up);
            }
        }
        rgbody.velocity = new Vector2(speedWalk * dirObj, rgbody.velocity.y);
        enemy.PlayAnimation((int)State.Run);
    }

    private void OnDrawGizmos()
    {
        Vector2 from = new Vector2(noGroundPos.transform.position.x, noGroundPos.transform.position.y);
        Vector2 to = from;
        to.y -= 0.5f;
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(from, to);

        Vector2 fromHor = new Vector2(WallPos.transform.position.x, WallPos.transform.position.y);
        Vector2 toHor = fromHor;
        toHor.x -= 0.8f;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(fromHor, toHor);
    }
    private bool CheckGround_Wall()
    {
        Vector2 from = new Vector2(noGroundPos.transform.position.x, noGroundPos.transform.position.y);
        Vector2 to = from;
        to.y -= 0.5f;
        Vector2 fromHor = new Vector2(WallPos.transform.position.x, WallPos.transform.position.y);
        Vector2 toHor = fromHor;
        toHor.x += 0.5f;
        if ((!Physics2D.Linecast(from, to, wallMask) && !Physics2D.Linecast(from, to, groundMask)) || Physics2D.Linecast(fromHor, toHor, wallMask))
        {
            return true;
        }
        return false;
    }
}
