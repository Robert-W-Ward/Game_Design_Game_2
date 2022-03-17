using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{   
    
    private Vector2 movementInput;
    [SerializeField] public float speed = 5;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] public string character;
    [SerializeField] private string[] PowerUpStorage;
    [SerializeField] public float score;
    public int invenidx ;
    private void Awake()
    {
        invenidx = 0;
        if(character == "Pete")
        {
            speed = 6.5f;
        }
        else if(character == "Shelby")
        {
            PowerUpStorage = new string[3];
        }
        else
        {
            PowerUpStorage = new string[2];
        }
        
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        if(movementInput.x <0)
        {
            spriteRenderer.flipX =true; 
        }
        else if(movementInput.x > 0)
        {
            spriteRenderer.flipX=false;
        }
    }
    

    public void OnDropOut(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Destroy(gameObject);
        }
    }    
   
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * new Vector2(movementInput.x, movementInput.y));
        if(transform.position.x >5 ||transform.position.y > 5 ||transform.position.x<-5||transform.position.y<-5)
        {

            Vector2 tmpPos = transform.position;
            tmpPos.x = Mathf.Clamp(transform.position.x, -16.6f, 17.6f);
            tmpPos.y = Mathf.Clamp(transform.position.y, -11.5f, 11.6f);
            transform.position = tmpPos;
        }
        
        
       
    }
    public string[] GetPlayerPowerUps()
    {
        return PowerUpStorage;
    }
}