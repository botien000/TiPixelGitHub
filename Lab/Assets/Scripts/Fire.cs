using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private GameObject vfx;
    [SerializeField] private float speedRoll;
    [SerializeField] private float duration;
    private Rigidbody2D rgbody2D;
    private float dirX;
    void Awake()
    {
        rgbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        duration -= Time.fixedDeltaTime;
        if(duration <= 0)
        {
            Destroy(gameObject);
        }
        rgbody2D.velocity = new Vector2(speedRoll * dirX, rgbody2D.velocity.y);
        //rgbody2D.AddForce(new Vector2(speedRoll * Time.fixedDeltaTime, 0), ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Conduit"))
        {
            Destroy(gameObject);
            Destroy(Instantiate(vfx, transform.position, Quaternion.identity), 0.5f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            Destroy(gameObject);
            Destroy(Instantiate(vfx, transform.position, Quaternion.identity), 0.5f);
        }
    }
    public void SetDir(float x)
    {
        dirX = x;
    }

}
