using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class GameManager : MonoBehaviour
{
    //Object that managers PlayerInput components
    [SerializeField] private PlayerInputManager PM;
    [SerializeField] private GameObject[] ScoreObjects = new GameObject[3];
    public enum ScoreObjectNames { BOOK, WATER, APPLE };
    int TotalScoreObjects = 0;
    static int MaxScoreObjects = 15;
    private Vector2[] Spawnlocations = new Vector2[MaxScoreObjects];
    //Component that controls inputs 
    private PlayerInput pi;
    private GameObject[] PlayerObjects= new GameObject[4];
    private Player[] PlayerScripts = new Player[4];
    private bool PlayersReady=false;

    //gets the players that are currently in game and assigns them player numbers
    // to be used later have UI elements show which player is which (might become deprecated)
    void GetPlayers()
    {
        PM.DisableJoining();
        PlayerObjects = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < PlayerInput.all.Count; i++)
        { 
            pi = PlayerInput.GetPlayerByIndex(i);
            pi.name = "Player" + i + 1;
            PlayerScripts[i] = PlayerObjects[i].GetComponent<Player>(); 
        }
    }
    void WaitForPlayers()
    {
        while (!PlayersReady)
        {
            switch (PlayerInput.all.Count)
            {
                case 0:    
                    break;
                case 1:
                    Debug.LogError("Not enough players to continue");
                    break;
                case 2:
                    if(PlayerScripts[0].PlayerReady ==true && PlayerScripts[1].PlayerReady == true)
                    {
                        PlayersReady = true;
                    }
                    break;
                case 3:
                    if (PlayerScripts[0].PlayerReady == true && PlayerScripts[1].PlayerReady == true&&PlayerScripts[2].PlayerReady==true)
                    {
                        PlayersReady = true;
                    }
                    break;
                case 4:
                    if (PlayerScripts[0].PlayerReady == true && PlayerScripts[1].PlayerReady == true && PlayerScripts[2].PlayerReady == true && PlayerScripts[3].PlayerReady == true)
                    {
                        PlayersReady = true;
                    }
                    break;
                default:
                    Debug.LogError("Too many players something is wrong!");
                    break;

            }
                
                    
        }
    }
    IEnumerator SpawnScoreObjects()
    {
        while (TotalScoreObjects < MaxScoreObjects)
        {
            GameObject ObjToSpawn;
            int tmp = Random.Range(0, 99);
            if (tmp <= 25)
            {
                ObjToSpawn = ScoreObjects[(int)ScoreObjectNames.BOOK];
            }else if (tmp > 25 && tmp <= 75)
            {
                ObjToSpawn = ScoreObjects[((int)ScoreObjectNames.APPLE)];
            }
            else
            {
                ObjToSpawn = ScoreObjects[(int) ScoreObjectNames.WATER];
            }


            Vector2 LocationTospawn = new Vector2(Random.Range(-16.6f, 17.6f), Random.Range(-11.5f, 11.6f));
            for(int i = 0; i < Spawnlocations.Length; ++i)
            {
                if (LocationTospawn != Spawnlocations[i])
                {
                    Spawnlocations[i] = LocationTospawn;
                    Instantiate(ObjToSpawn, LocationTospawn, Quaternion.identity);
                    TotalScoreObjects++;
                    break;
                }
                else continue;
                
            }
            yield return new WaitForSeconds(3f);
        }
    }
    private void Awake()
    {
       
    }
    // Start is called before the first frame update
    void Start()
    {
       
        Invoke("GetPlayers", 5);
        StartCoroutine(SpawnScoreObjects());

    }

    // Update is called once per frame
    void Update()
    {
    }
}
