using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 
using System;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    [SerializeField] private PlayerInputManager PM;
    [SerializeField] private GameObject[] ScoreObjects = new GameObject[3];
    [SerializeField] private GameObject[] PowerUps = new GameObject[3];
    [SerializeField] public bool PlayersReady = false;
    [SerializeField] private TextMesh tm;
    [SerializeField] private GameObject RestartScreen;
    private MeshRenderer Mr;
    public enum ScoreObjectNames { BOOK =0,APPLE= 1,WATER=2 };
    public enum PowerUpNames { SPEED_BOOST, RESISTANCE, BALL };
    [SerializeField] public int TotalScoreObjects = 0;
    public static int TotalPowerUps = 0;
    static int MaxScoreObjects = 15;
    static int MaxPowerUps = 5;
    private Vector2[] Spawnlocations = new Vector2[MaxScoreObjects + MaxPowerUps];
    private PlayerInput pi;
    public GameObject[] PlayerObjects = new GameObject[4];
    public Player[] PlayerScripts = new Player[4];
    private bool GameStarted = false;
    public bool Restart = false;
    //gets the players that are currently in game and assigns them player numbers
    // to be used later have UI elements show which player is which
    void GetPlayers()
    {
        
        PlayerObjects = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < PlayerInput.all.Count; i++)
        {
            pi = PlayerInput.GetPlayerByIndex(i);
            pi.name = "Player" + (i + 1);
            pi.GetComponentInChildren<TextMesh>().text = "Player" +(i + 1);
            PlayerScripts[i] = PlayerObjects[i].GetComponent<Player>();
        }
    }
    


    IEnumerator SpawnScoreObjects()
    {
        while (true)
        {
            if (TotalScoreObjects < MaxScoreObjects)
            {
                GameObject ObjToSpawn = null;
                int tmp = UnityEngine.Random.Range(0, 99);
                if (tmp <= 25)
                {
                    ObjToSpawn = ScoreObjects[(int)ScoreObjectNames.APPLE];
                }
                else if (tmp > 25 && tmp <= 79)
                {
                    ObjToSpawn = ScoreObjects[(int)ScoreObjectNames.BOOK];
                }
                else if (tmp > 80)
                {
                    ObjToSpawn = ScoreObjects[(int)ScoreObjectNames.WATER];
                }


                Vector2 LocationTospawn = new Vector2(UnityEngine.Random.Range(-16.6f, 17.6f), UnityEngine.Random.Range(-11.5f, 11.6f));
                for (int i = 0; i < Spawnlocations.Length; ++i)
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
            else yield return new WaitForSeconds(3f);             
        }
    }
    IEnumerator SpawnPowerUps()
    {            
        while (true)
        {
            if (TotalPowerUps < MaxPowerUps)
            {
                GameObject PowerUpToSpawn;
                int tmp = UnityEngine.Random.Range(0, 99);
                if (tmp <= 25)
                {
                    PowerUpToSpawn = PowerUps[(int)PowerUpNames.BALL];
                }
                else if (tmp > 25 && tmp <= 75)
                {
                    PowerUpToSpawn = PowerUps[(int)PowerUpNames.SPEED_BOOST];
                }
                else
                {
                    PowerUpToSpawn = PowerUps[(int)PowerUpNames.RESISTANCE];
                }
                Vector2 LocationTospawn = new Vector2(UnityEngine.Random.Range(-16.6f, 17.6f), UnityEngine.Random.Range(-11.5f, 11.6f));
                for (int i = 0; i < Spawnlocations.Length; ++i)
                {
                    if (LocationTospawn != Spawnlocations[i])
                    {
                        Spawnlocations[i] = LocationTospawn;
                        Instantiate(PowerUpToSpawn, LocationTospawn, Quaternion.identity);
                        TotalPowerUps++;
                        break;
                    }
                    else continue;
                }
                yield return new WaitForSeconds(10f);
            }
            else yield return new WaitForSeconds(10f);             
        }
    }
    IEnumerator GlobalTimer(int time)
    {
        while (time > 0)
        {
            time--;
            TimeSpan timespan =TimeSpan.FromSeconds(time);
            string timestr = timespan.ToString("mm\\:ss");
            tm.text = "Time remaining: " + timestr;
            yield return new WaitForSeconds(1);
        }
        if(time == 0)
        {
            
            tm.text = "";
            for(int i = 0;i< PlayerInput.all.Count; ++i)
            {
                tm.text += PlayerObjects[i].name+":"+PlayerScripts[i].score+"\n";
                
            }
            
            tm.text += "\nWinner is: "+DetermineWinner(PlayerScripts);
            StopAllCoroutines();
            RestartScreen.SetActive(true);
            Restart = true;
        }
    }

    public void RemoveScoreObj(GameObject obj)
    {
        TotalScoreObjects--;
        for (int i = 0; i < Spawnlocations.Length; ++i)
        {
            if (Spawnlocations[i] == new Vector2(obj.transform.position.x, obj.transform.position.y))
            {
                Spawnlocations[i] = new Vector2(0, 0);
            }
        }
    }
    public void RemovePowerUp(GameObject obj)
    {
        TotalPowerUps--;
        for (int i = 0; i < Spawnlocations.Length; ++i)
        {
            if (Spawnlocations[i] == new Vector2(obj.transform.position.x, obj.transform.position.y))
            {
                Spawnlocations[i] = new Vector2(0, 0);
            }
        }
    }
    private void Awake()
    {
        Mr = gameObject.GetComponent<MeshRenderer>();
        Mr.sortingLayerName = "UI";
        Mr.sortingOrder = 0;
       
        RestartScreen.SetActive(false);
    }
    void Update()
    {
        GetPlayers();
        switch (PlayerInput.all.Count)
        {
            case 0:              
                break;
            case 1:       
                break;
            case 2:
                if (PlayerScripts[0].PlayerReady == true && PlayerScripts[1].PlayerReady == true)
                {
                    PlayersReady = true;
                    
                }
                break;
            case 3:
                if (PlayerScripts[0].PlayerReady == true && PlayerScripts[1].PlayerReady == true && PlayerScripts[2].PlayerReady == true)
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
        if (PlayersReady == true && GameStarted == false)
        {
            PM.DisableJoining();
            StartCoroutine(SpawnScoreObjects());
            StartCoroutine(SpawnPowerUps());
            StartCoroutine(GlobalTimer(180));
            GameStarted = true;
        }
    }
    string DetermineWinner(Player[] players)
    {
        string winner = "";
        winner = players[0].name;
        for(int i = 0; i < PlayerInput.all.Count; i++)
        {
            if (players[i].score > players[0].score)
            {
                winner = players[i].name;
            }

        }
        if(winner == players[0].name)
        {
            if(players[0].score == 0)
            {
                winner = "No one! There are no ties!";
            }
        }
        return winner;
    }
}



