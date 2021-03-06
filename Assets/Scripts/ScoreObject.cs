using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreObject : MonoBehaviour
{

    private Collider2D collider;
    private float timeremaining = 5;
    private Animator animator;
    private GameManager gamemanager;
    [SerializeField] string ScoreObjName;
    Player p;
    // Start is called before the first frame update
    private void Awake()
    {

       gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
       animator = gameObject.GetComponent<Animator>();
       
    }
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {

           
            if (timeremaining > 0)
            {             

                timeremaining -= Time.deltaTime;
                animator.speed = (2/timeremaining);
            }
            else
            {          
                gamemanager.RemoveScoreObj(this.gameObject);
                p = other.gameObject.GetComponentInParent<Player>();
                
                if(this.ScoreObjName == "Book")
                {
                    p.score += Mathf.Round(10*p.multiplier);
                }else if(this.ScoreObjName == "Apple")
                {
                    p.score += Mathf.Round(25*p.multiplier);
                }
                else
                {
                    p.score += Mathf.Round(50*p.multiplier);
                }             
                
                Destroy(this.gameObject);
            }
        }

        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if(collision.gameObject.tag == "Player")
        {                  
            
            timeremaining = 5;
            animator.speed = 1;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            p = collision.gameObject.GetComponent<Player>();
            if (p.character == "Jeff")
            {
                timeremaining = 3;
            }
            else
            {
                timeremaining = 5;
            }
        }
    }
}
