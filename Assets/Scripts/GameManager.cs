using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class GameManager : MonoBehaviour
{

    [SerializeField] private PlayerInputManager PM;
    private PlayerInput p1, p2, p3, p4;
    void getPlayers()
    {
        PM.DisableJoining();
        Debug.Log("test");
        p1 = PlayerInput.GetPlayerByIndex(0);
        p1.name = "Player1";
        p2 = PlayerInput.GetPlayerByIndex(1);
        p2.name = "Player2";
        p3 = PlayerInput.GetPlayerByIndex(2);
        p3.name = "Player3";
        p4 = PlayerInput.GetPlayerByIndex(3);
        p4.name= "Player4";
    }
    private void Awake()
    {
       
    }
    // Start is called before the first frame update
    void Start()
    {
        Invoke("getPlayers", 5);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
