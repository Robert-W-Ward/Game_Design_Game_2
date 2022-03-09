using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{   
    
    private Vector2 movementInput;
    [SerializeField] private float speed = 5;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public bool PlayerReady = false;
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
    public void OnReady(InputAction.CallbackContext context)
    {
        PlayerReady = true;
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
}