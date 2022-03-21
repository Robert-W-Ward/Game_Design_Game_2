using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{   
    
    private Vector2 movementInput;
    private Vector2 AimDir;
    [SerializeField] public float speed = 5;
    private float oldspeed;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] public string character;
    [SerializeField] private string[] PowerUpStorage;
    [SerializeField] public float score;
    [SerializeField] public GameObject Anchor;
    [SerializeField] private GameObject AimArrow;
    [SerializeField] public GameObject Projectile;
    [SerializeField] public GameObject TextObj;
    [SerializeField] public GameObject SelectedPowerUp;
    [SerializeField] private GameObject resistField;
    private GameManager gameManager;
    private SpriteRenderer itemrenderer;
    private Inventory inventory;
    private TextMesh textMesh;
    public bool PlayerReady = false;
    public float multiplier = 1;
    public float collectSpd = 1;
    public int invenidx =0;
    public bool canLosePoints= true;
    public string selectedPowerUp;
    public int PlayerPowerUpCount;
    private void Awake()
    {
        invenidx = 0;
        Anchor.SetActive(false);
        textMesh = TextObj.GetComponent<TextMesh>();
        inventory = SelectedPowerUp.GetComponent<Inventory>();
        itemrenderer = SelectedPowerUp.GetComponent<SpriteRenderer>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        if (character == "Pete")
        {
            speed = 6.5f;
            oldspeed = speed;
            PowerUpStorage = new string[2];
            collectSpd = 1;
            multiplier = 1;
        }
        else if(character == "Shelby")
        {
            speed = 5;
            oldspeed = speed;
            PowerUpStorage = new string[3];
            multiplier = 1;
            collectSpd = 1;
        }
        else if(character == "Mary")
        {
            speed = 3.75f;
            oldspeed = speed;
            PowerUpStorage = new string[2];
            multiplier = 1.5f;
            collectSpd=1;
        }else if(character == "Jeff")
        {
            speed = 5;
            oldspeed = speed;
            PowerUpStorage = new string[2];
            multiplier =1f;
            collectSpd = 2;
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
        if (context.started&&PlayerReady==false&&gameManager.PlayersReady==false)
        {
            Destroy(gameObject);
        }
    }
    public void OnReady(InputAction.CallbackContext context)
    {
        PlayerReady = !PlayerReady;
    }
    public void OnUsePowerUp(InputAction.CallbackContext context)
    {
        if (context.started)
        {

            if (selectedPowerUp == "Ball")
            {
                
                Debug.Log("ball thrown");
                throwball();
                PowerUpStorage[invenidx] = "";
                PlayerPowerUpCount--;
                itemrenderer.sprite = null;
                selectedPowerUp = "";
                Anchor.SetActive(false);
            }
            else if (selectedPowerUp == "Speed_Boost")
            {
                Debug.Log("speed boost used");
                PowerUpStorage[invenidx] = "";
                selectedPowerUp = "";
                itemrenderer.sprite = null;
                PlayerPowerUpCount--;
                if(PlayerPowerUpCount <0)
                    PlayerPowerUpCount = 0;
                speed = speed * 1.5f;
                collectSpd += 1;
        
                StartCoroutine(Countdown(5));
                


            }
            else if(selectedPowerUp == "Resistance")
            {
                Debug.Log("resistance used");
                PowerUpStorage[invenidx] = "";
                itemrenderer.sprite = null;
                selectedPowerUp = "";
                PlayerPowerUpCount--;
                canLosePoints = false;
                resistField.SetActive(true);
                StartCoroutine(Countdown(5));
                
            }
            else
            {
                Debug.Log("No Powerup selected");
            }
        }
       
    }
    public void OnSelectNextPowerUp(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            invenidx = (invenidx+1) % PowerUpStorage.Length;
            selectedPowerUp = PowerUpStorage[invenidx];
            if(selectedPowerUp == null) 
                itemrenderer.sprite = null;
            if(selectedPowerUp == "Ball")
            {
                Anchor.SetActive(true);
                itemrenderer.sprite = inventory.Items[2];
            }else if(selectedPowerUp == "Speed_Boost")
            {
                Anchor.SetActive(false);
                itemrenderer.sprite = inventory.Items[1];
            }else if(selectedPowerUp=="Resistance")
            {
                Anchor.SetActive(false);
                itemrenderer.sprite = inventory.Items[0];
            }
        }
    }
    public void OnSelectPrevPowerUp(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Anchor.SetActive(false);
            invenidx = (invenidx - 1) % PowerUpStorage.Length;
            if (invenidx < 0)
                invenidx = PowerUpStorage.Length-1;
            selectedPowerUp = PowerUpStorage[invenidx];
            if (selectedPowerUp == "Ball")
                Anchor.SetActive(true);
        }
    }
    public void OnAim(InputAction.CallbackContext context)
    {

        AimDir = context.ReadValue<Vector2>();
           
    }
    public void OnRestart(InputAction.CallbackContext context)
    {
        if (context.started && gameManager.Restart == true)
        {
            
            PlayerReady = false;
            RestartGame();
        }
    }
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * new Vector2(movementInput.x, movementInput.y));
        if(transform.position.x > 17.6f || transform.position.x < -16.6f || transform.position.y> 11.6f || transform.position.y< -11.5f)
        {

            Vector2 tmpPos = transform.position;
            tmpPos.x = Mathf.Clamp(transform.position.x, -16.6f, 17.6f);
            tmpPos.y = Mathf.Clamp(transform.position.y, -11.5f, 11.6f);
            transform.position = tmpPos;
        }
        
        textMesh.text = textMesh.text + " "+ score.ToString();

        float angleA = Mathf.Atan2(AimDir.x, AimDir.y)*Mathf.Rad2Deg;
        Anchor.transform.rotation = Quaternion.Euler(0f,0f,-angleA );
        
    }
    public string[] GetPlayerPowerUps()
    {
        return PowerUpStorage;
    }
    public IEnumerator Countdown(int time)
    {
        while (time > 0)
        {
            Debug.Log(time--);
            yield return new WaitForSeconds(1);
        }

        collectSpd -= 1;
        speed = oldspeed ;
        canLosePoints = true;
        resistField.SetActive(false);

        selectedPowerUp = "";
        yield break;
    }
    private void throwball()
    {
       var SpawnPos = new Vector3(AimArrow.transform.position.x+AimDir.x,AimArrow.transform.position.y+AimDir.y,AimArrow.transform.position.z);
       var _tmp = Instantiate(Projectile,SpawnPos, Anchor.transform.rotation);
       _tmp.transform.rotation = Anchor.transform.rotation;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}