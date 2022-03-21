using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int speed = 40;
    private Collider2D collider;
    private Player p;
    private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {      
        if (collision.gameObject.tag == "Player")
        {
            p = collision.gameObject.GetComponent<Player>();
            if(p.canLosePoints == true)
                p.score = p.score -= 10;
            Destroy(this.gameObject);
        }     
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, 1*speed*Time.deltaTime, 0));
        if(transform.position.x > 17.6f || transform.position.x < -16.6f || transform.position.y > 11.6f || transform.position.y < -11.5f)
        {
            Destroy(this.gameObject);
        }
    }
}
