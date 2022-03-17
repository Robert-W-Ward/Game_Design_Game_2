using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    float timeremaining = 5;
    Player p;
    [SerializeField] public string PowerUpName;
    [SerializeField] private GameManager gamemanger;
    // Start is called before the first frame update
    private void Awake()
    {
       gamemanger = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {

            if (timeremaining > 0)
            {
                Debug.Log("Collecting" + this.gameObject.name + " Time Remaining:" + timeremaining);

                timeremaining -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Collected" + this.gameObject.name);
                if(p.invenidx != p.GetPlayerPowerUps().Length)
                {
                    p.GetPlayerPowerUps()[p.invenidx] = this.PowerUpName;
                    p.invenidx++;
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

            timeremaining = 5;
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
