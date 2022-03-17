using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class GameManager : MonoBehaviour
{

    [SerializeField] private PlayerInputManager PM;
    [SerializeField] private GameObject[] ScoreObjects = new GameObject[3];
    [SerializeField] private GameObject[] PowerUps = new GameObject[3];
    public enum ScoreObjectNames { BOOK, WATER, APPLE };
    public enum PowerUpNames { SPEED_BOOST, RESISTANCE, BALL };
    public int TotalScoreObjects = 0;
    public int TotalPowerUps = 0;
    static int MaxScoreObjects = 15;
    static int MaxPowerUps = 5;
    private Vector2[] Spawnlocations = new Vector2[MaxScoreObjects + MaxPowerUps];
    private PlayerInput pi;

    //gets the players that are currently in game and assigns them player numbers
    // to be used later have UI elements show which player is which
    void GetPlayers()
    {
        PM.DisableJoining();
        for (int i = 0; i < PlayerInput.all.Count; i++)
        {
            pi = PlayerInput.GetPlayerByIndex(i);
            pi.name = "Player" + i + 1;
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
            }
            else if (tmp > 25 && tmp <= 75)
            {
                ObjToSpawn = ScoreObjects[((int)ScoreObjectNames.APPLE)];
            }
            else
            {
                ObjToSpawn = ScoreObjects[(int)ScoreObjectNames.WATER];
            }


            Vector2 LocationTospawn = new Vector2(Random.Range(-16.6f, 17.6f), Random.Range(-11.5f, 11.6f));
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
    }

    IEnumerator SpawnPowerUps()
    {
        yield return new WaitForSeconds(10f);
        while (TotalPowerUps < MaxPowerUps)
        {

            GameObject PowerUpToSpawn;
            int tmp = Random.Range(0, 99);
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
            Vector2 LocationTospawn = new Vector2(Random.Range(-16.6f, 17.6f), Random.Range(-11.5f, 11.6f));
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
            break;
        }
        yield return new WaitForSeconds(15f);
    }
    IEnumerator GlobalTimer()
    {
        yield return new WaitForSeconds(1f);
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
    private void Start()
    {
        //Invoke("GetPlayers", 5);
        StartCoroutine(SpawnScoreObjects());
        StartCoroutine(SpawnPowerUps());
    }
}
       
        

   
