using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSwitcher : MonoBehaviour
{
    
    [SerializeField] public GameObject[] PlayerList = new GameObject[4];
    [SerializeField]private PlayerInputManager manager;
    int i = 0;
    
    public void AddPlayer(PlayerInput input)
    { 
       
        i =(i+1)%4;
        manager.playerPrefab = PlayerList[i];
        
   
    }

   

}
