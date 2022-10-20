using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Animator animator;
    private int curState;
    private Collider2D colider;
    private bool isHitted;
    private Rigidbody2D rgbody;
    public bool IsHitted { get => isHitted; set => isHitted = value; }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetRg_Colli(Rigidbody2D rigidbody,BoxCollider2D box)
    {
        colider = box;
        rgbody = rigidbody;
    }
    public void PlayAnimation(int state)
    {
        if (curState == state)
            return;
        animator.SetInteger("State", state);
        animator.SetTrigger("Change");
        curState = state;
    }
    public void Hit()
    {
        IsHitted = true;
        rgbody.bodyType = RigidbodyType2D.Dynamic;
        if(colider != null)
        colider.isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FirePlayer"))
        {
            Hit();
        }
    }
}
