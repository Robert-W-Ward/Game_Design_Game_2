using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreObject : MonoBehaviour
{

    private Collider2D collider;
    private float timeremaining = 5;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    IEnumerator CollectItem()
    {
        Debug.Log("Collecting:" + this.gameObject.name);
        yield return new WaitForSeconds(3);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(timeremaining > 0)
            {
                Debug.Log("Collecting"+this.gameObject.name+" Time Remaining:"+timeremaining);

                timeremaining -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Collected" + this.gameObject.name);
                Destroy(this.gameObject);
            }
        }

        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            timeremaining = 5;
        }
    }
}
