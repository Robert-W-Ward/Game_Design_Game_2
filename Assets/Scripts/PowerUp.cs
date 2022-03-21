using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    float timeremaining = 5;
    Player p;
    [SerializeField] public string PowerUpName;
    private GameManager gamemanger;
    private Animator animator;
    // Start is called before the first frame update
    private void Awake()
    {
       gamemanger = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
       animator = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {

            if (timeremaining > 0)
            {               
                timeremaining -= Time.deltaTime;
                animator.speed = (2 / timeremaining);
            }
            else
            {
               
                if(p.PlayerPowerUpCount != p.GetPlayerPowerUps().Length)
                {
                    var tmp = p.GetPlayerPowerUps();
                    for (int i = 0; i < tmp.Length; i++)
                    {
                        if (tmp[i] == null||tmp[i]=="")
                        {
                            tmp[i] = this.PowerUpName;
                            break;
                        }
                    }
                    p.PlayerPowerUpCount++;
                    gamemanger.RemovePowerUp(this.gameObject);
                    Destroy(this.gameObject);
                }
            }
        }


    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {

            timeremaining = 4;
            animator.speed = 1;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            p = collision.gameObject.GetComponent<Player>();
            switch (p.collectSpd)
            {
                case 1:
                    timeremaining = 4;
                    break;
                case 2:
                    timeremaining = 2;
                    break;
                case 3:
                    timeremaining = 1;
                    break;


            }
        }
    }
    
}
