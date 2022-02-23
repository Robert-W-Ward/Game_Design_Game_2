using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{   
    
    private Vector2 movementInput;
    [SerializeField] private float speed = 5;
   
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
    

    public void OnDropOut(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Destroy(gameObject);
        }
    }    
    public void OnDropIn(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            
            gameObject.SetActive(true); 
        }
    }
    
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * new Vector2(movementInput.x, movementInput.y));
        if(transform.position.x >5 ||transform.position.y > 5 ||transform.position.x<-5||transform.position.y<-5)
        {

            Vector2 tmpPos = transform.position;
            tmpPos.x = Mathf.Clamp(transform.position.x, -5, 5);
            tmpPos.y = Mathf.Clamp(transform.position.y, -5, 5);
            transform.position = tmpPos;
        }
        
       
    }
}