using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class GameManager : MonoBehaviour
{

    [SerializeField] private PlayerInputManager PM;
    [SerializeField] private GameObject[] ScoreObjects = new GameObject[3];
    public enum ScoreObjectNames { BOOK,WATER,APPLE};
    int TotalScoreObjects =0;
    static int MaxScoreObjects = 15;
    private Vector2[] Spawnlocations = new Vector2[MaxScoreObjects];
    private PlayerInput pi;

    //gets the players that are currently in game and assigns them player numbers
    // to be used later have UI elements show which player is which
    void GetPlayers()
    {
        PM.DisableJoining();
        for(int i = 0; i < PlayerInput.all.Count; i++)
        {
            pi =PlayerInput.GetPlayerByIndex(i);
            pi.name = "Player" + i+1;
        }
    }
    
    IEnumerator SpawnScoreObjects()
    {
        while (TotalScoreObjects < MaxScoreObjects)
        {
            GameObject ObjToSpawn;
            int tmp = Random.RandomRange(0, 99);
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
