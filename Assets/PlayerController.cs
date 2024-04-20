using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //player component refs
    Rigidbody rb;
    NavMeshAgent agent;

    //ui refs
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Image healthBarImage;
    [SerializeField] GameObject[] heartsUI;
    [SerializeField] GameObject gameEndPanel;
    [SerializeField] TMP_Text gameEndMessage;

    //player gameplay variables
    [SerializeField] int bulletSpeed = 600;
    int playerHealth = 3;
    bool isAlive;
    int kills;

    // Start is called before the first frame update
    void Start()
    {
        //sets player variables and refs. hides game end panel
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        isAlive = true;
        gameEndPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //handle player controls
        PlayerMouseMove();
        PlayerShoot();

        //check for game lost
        if (!isAlive)
        {
            //Game Over
            gameEndPanel.SetActive(true);
            gameEndMessage.text = "Game Over";
            gameObject.GetComponent<PlayerController>().enabled = false;
        }

        //check for game win. kills incremented when an enemy is killed
        if (kills >= 4)
        {
            //game win
            gameEndPanel.SetActive(true);
            gameEndMessage.text = "Victory!";
            gameObject.GetComponent<PlayerController>().enabled = false;
        }
    }

    //move player to mouse position on right click
    void PlayerMouseMove()
    {
        //build ray of mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(ray, out hit))
            {
                //set player move destination to mouse position
                agent.destination = hit.point;
            }
        }
    }

    //fire a bullet towards mouse position on left click
    void PlayerShoot()
    {
        //build position based on mouse position
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 30;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        //build bullet direction 
        Vector3 shootDirection = (mousePosition - transform.position).normalized;

        //instantiate bullet and add force on left click
        if (Input.GetMouseButtonDown(0))
        {
            GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody>().AddForce(shootDirection * bulletSpeed);
        }
    } 

    //called when collision with enemy
    public void DamagePlayer(int damage)
    {
        Debug.Log("Damage Player");

        //decrease healthbar
        healthBarImage.fillAmount -= .334f; ;

        //decrease player health variable
        playerHealth -= damage;

        //if player health has fallen below 1, end game
        if (playerHealth < 1)
        {
            isAlive = false;
        }
    }

    //called when enemy dies
    public void UpdateKillCounter()
    {
        kills += 1;
    }
}
