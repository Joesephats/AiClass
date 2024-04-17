using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    NavMeshAgent agent;

    GameObject playerObject;

    bool isAlive;
    bool hasLineOfSight;
    [SerializeField] int enemyPatrolRange = 5;

    [SerializeField] int health = 3;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        isAlive = true;
        hasLineOfSight = false;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, playerObject.transform.position - transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                hasLineOfSight = true;
            }
            else
            {
                hasLineOfSight = false;
            }
        }

        if ((playerObject.transform.position - transform.position).magnitude < 10 && hasLineOfSight)
        {
            agent.speed = 6.5f;
            agent.SetDestination(playerObject.transform.position);
        }
        else
        {
            agent.speed = 4;
            StartCoroutine(Wander());
        }

        if (!isAlive)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().DamagePlayer(1);
            StartCoroutine(AttackLag());
        }
    }

    IEnumerator Wander()
    {
        if (agent.remainingDistance < 0.5f)
        {
            //Debug.Log("Calculating new position");
            Vector3 patrolPos = transform.position;
            float randomX = Random.Range(-enemyPatrolRange, enemyPatrolRange);
            float randomZ = Random.Range(-enemyPatrolRange, enemyPatrolRange);
            while (randomX > -1.5 && randomX < 1.5)
            {
                randomX = Random.Range(-enemyPatrolRange, enemyPatrolRange);
            }
            while (randomZ > 1.5 && randomZ < 1.5)
            {
                randomZ =   Random.Range(-enemyPatrolRange, enemyPatrolRange);
            }

            patrolPos.x += randomX;
            patrolPos.z += randomZ;
            patrolPos.y = 0;
            //Debug.Log("Done Calculating");

            //Debug.Log("waiting");
            yield return new WaitForSeconds(1);

            //Debug.Log("Set destination");
            agent.SetDestination(patrolPos);
            StopAllCoroutines();
        }
    }
    IEnumerator AttackLag()
    {
        yield return new WaitForSeconds(1);
    }

    public void DamageEnemy(int damage)
    {
        health -= damage;

        if (health < 1)
        {
            isAlive = false;
        }
    }
}
