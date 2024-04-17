using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{

    Rigidbody rb;
    NavMeshAgent agent;

    [SerializeField] GameObject bulletPrefab;

    [SerializeField] GameObject[] heartsUI;

    [SerializeField] int bulletSpeed = 600;

    int playerHealth = 3;
    bool isAlive;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMouseMove();
        PlayerShoot();

        if (!isAlive)
        {
            //Game Over
        }
    }

    void PlayerMouseMove()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(ray, out hit))
            {
                agent.destination = hit.point;
            }
        }
    }

    void PlayerShoot()
    {

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 30;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //mousePosition.y = 0.5f;
        //Debug.Log(mousePosition);

        Vector3 shootDirection = (mousePosition - transform.position).normalized;

        if (Input.GetMouseButtonDown(0))
        {
            GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody>().AddForce(shootDirection * bulletSpeed);
        }
    } 

    public void DamagePlayer(int damage)
    {
        Debug.Log("Damage Player");

        heartsUI[playerHealth].SetActive(false);

        playerHealth -= damage;

        if (playerHealth < 1)
        {
            isAlive = false;
        }
    }
}
