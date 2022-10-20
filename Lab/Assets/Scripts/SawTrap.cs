using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawTrap : MonoBehaviour
{
    [SerializeField] private Transform posPlayerComing;
    [SerializeField] private LayerMask playerMask;

    private Rigidbody2D rgbody2D;
    // Start is called before the first frame update
    void Start()
    {
        rgbody2D = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 from = new Vector2(posPlayerComing.transform.position.x, posPlayerComing.transform.position.y);
        Vector2 to = from;
        to.x += 8;
        if (Physics2D.Linecast(from, to, playerMask))
        {
            rgbody2D.velocity = new Vector2(6, rgbody2D.velocity.y);
        }
    }
    private void OnDrawGizmos()
    {
        Vector2 from = new Vector2(posPlayerComing.transform.position.x, posPlayerComing.transform.position.y);
        Vector2 to = from;
        to.x += 8;
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(from, to);
    }

}
