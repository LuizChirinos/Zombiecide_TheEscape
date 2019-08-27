using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieControl : MonoBehaviour {
	Animator anim;
	Rigidbody[] rdbs;
	public CharacterController zCharCtrl;
    public WaveSpawner wSpawner;
	public bool walking;
	public GameObject player;
    public GameObject ways;
    public Transform[] wayPoints;
    int indexround = 1;
	public float speed = 1.5f;
    public NavMeshAgent nAgent;

    
    public bool isDead;

    public enum Zstate
    {
        Patrol,
        Berserk,
        Attack,
        Dead
    }
    public Zstate currentState = Zstate.Berserk;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rdbs = GetComponentsInChildren<Rigidbody> ();
		//walking = true;
		foreach (Rigidbody rdb in rdbs) {
			rdb.isKinematic = true;
		}
		zCharCtrl = GetComponent<CharacterController> ();
        player = GameObject.FindGameObjectWithTag("Player");
        wSpawner = FindObjectOfType<WaveSpawner>();
        /*wayPoints = ways.GetComponentsInChildren<Transform>();
        indexround = Random.Range (1, wayPoints.Length);
        nAgent.SetDestination (wayPoints[indexround].position);*/
	}

    private void Update()
    {
        /*if (isDead)
        {
            wSpawner.livingEnemies--;
            isDead = false;
        }*/
    }

    void FixedUpdate()
    {
        switch (currentState)
        {
            case (Zstate.Patrol):
                 Patrol(); 
                 break;
            case (Zstate.Attack):
                 Attack();
                 break;
            case (Zstate.Berserk):
                 Berserk();
                 break;
            case (Zstate.Dead):
                 Dead();
                 break;
        }


        /*
        if (zCharCtrl.enabled)
        {
            if (player)
            {
                Berserk();
            }
            else
            {
                Patrol();
            }
            zCharCtrl.SimpleMove(transform.forward);
        }*/
    }

    void Dead()
    {
        
    }

    /// <summary>
    /// Desativa os componentes necessários para simular a morte do Zombie.
    /// </summary>
    public void KillZombie (){
		anim.enabled = false;
		zCharCtrl.enabled = false;
		foreach (Rigidbody rdb in rdbs) {
			rdb.isKinematic = false;
		}
        nAgent.enabled = false;
        anim.enabled = false;        
        currentState = Zstate.Dead;
        if (gameObject.CompareTag("Enemy2"))
        {
            anim.SetBool("dead", true);
        }
    }

    /// <summary>
    /// Faz com que o Zombie patrulhe uma área determinada pelos "Way Points".
    /// </summary>
    void Patrol()
    {
        Vector3 dir = wayPoints[indexround].transform.position - transform.position;
        //transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir, Vector3.up), Time.deltaTime);
        if(dir.magnitude < 2)
        {
            indexround = Random.Range(1, wayPoints.Length);
            nAgent.SetDestination(wayPoints[indexround].position);


            /*
            indexround++;
            if(indexround >= wayPoints.Length)
            {
                indexround = 1;
            }*/
        }
    }

    /// <summary>
    /// Faz o Zombie perseguir o jogador.
    /// </summary>
	void Berserk(){
        nAgent.SetDestination(player.transform.position);
        nAgent.speed = 2.2f;
        if (gameObject.CompareTag("Enemy2"))
        {
            nAgent.speed = 3.5f;
        }
		Vector3 dir = player.transform.position - transform.position;
        if(dir.magnitude < 2.5f)
        {
            currentState = Zstate.Attack;
        }        
        anim.SetBool("attack", false);
		//transform.rotation = Quaternion.LookRotation (dir, Vector3.up);
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir, Vector3.up), Time.deltaTime);
	}

    void Attack()
    {
        Vector3 dir = player.transform.position - transform.position;
        if(dir.magnitude > 2)
        {
            currentState = Zstate.Berserk;
        }        
        anim.SetBool("attack", true);        
    }

	void MoveZ (){
		Vector3 dir = player.transform.position - transform.position;
		Vector3 movement = new Vector3 (dir.x, 0f, dir.z);
		transform.position += movement.normalized * speed * Time.deltaTime;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            currentState = Zstate.Berserk;
        }
    }
}
