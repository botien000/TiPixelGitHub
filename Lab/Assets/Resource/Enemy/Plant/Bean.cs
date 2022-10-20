using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bean : MonoBehaviour
{
    [SerializeField] private float speedFire;
    [SerializeField] private bool dirObj;

    private Plant curPlant;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + speedFire * (dirObj ? 1 : -1), transform.position.y, transform.position.z);

    }
    public void Instantiate(Plant plant)
    {
        curPlant = plant;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("Player"))
        {
            curPlant.RemoveBean(this);
        }
    }
}
