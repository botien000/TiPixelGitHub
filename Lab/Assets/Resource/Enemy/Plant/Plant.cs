using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
   
    enum State
    {
        Idle,Attack,Hit
    }
    [SerializeField] private List<Bean> inactive;
    [SerializeField] private Bean beanPrefab;
    [SerializeField] private Transform posSwn;

    private Enemy enemy;
    private Rigidbody2D rgbody;
    private BoxCollider2D boxStand;

    private void OnEnable()
    {
        if(inactive == null)
        {
            inactive = new List<Bean>();
        }
        if (enemy == null)
        {
            enemy = GetComponent<Enemy>();
        }
        if (rgbody == null && boxStand == null)
        {
            rgbody = GetComponent<Rigidbody2D>();
            boxStand = GetComponentInChildren<BoxCollider2D>();
        }
        enemy.GetRg_Colli(rgbody, null);
    }
    // Update is called once per frame
    void Update()
    {
        enemy.PlayAnimation((int)State.Attack);

    }
    private void Fire()
    {
        if(inactive.Count > 0)
        {
            Bean bean = inactive[0];
            inactive.RemoveAt(0);
            bean.gameObject.SetActive(true);
            bean.Instantiate(this);
        }
        else
        {
            Bean bean = Instantiate(beanPrefab, posSwn.position, Quaternion.identity);
            bean.Instantiate(this);
        }
    }
    public void RemoveBean(Bean bean)
    {
        bean.gameObject.SetActive(false);
        inactive.Add(bean);
    }
}
