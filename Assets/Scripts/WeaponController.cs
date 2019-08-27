using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {
    public bool fire;
    bool canDecrement;
    WaveSpawner waveS;
    AudioSource wSound;
	LayerMask ZombieMask;
	int ZombieDeadMask;
    public ParticleSystem muzzleFlash;
    public ParticleSystem fragment;

    public PouseManager pManager;

    // Use this for initialization
    void Start () {
        waveS = FindObjectOfType<WaveSpawner>();
        wSound = GetComponent<AudioSource>();
		//ZombieMask = LayerMask.GetMask ("Zombie");
		ZombieMask = (1 << 12) | (1 << 9);
		ZombieDeadMask = 13;

	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;


		if (Physics.Raycast(transform.position, transform.forward, out hit, 100, ZombieMask))
		{
			Debug.Log(hit.collider.name);
			Debug.DrawLine(transform.position, hit.point, Color.blue);
		}

        if (!pManager.isPouse)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                fire = true;
                FireWeapon();
                ShotSound();
                Flash();
            }

            if (Input.GetButtonUp("Fire1"))
            {
                fire = false;
            }

            // Se puder decrementar, decrementa o valor de inimigos vivos na cena.
            if (canDecrement)
            {
                waveS.livingEnemies--;
                canDecrement = false;
            }
        }
        
        
	}

    void FireWeapon()
    {
        RaycastHit hit;
		if(fire && Physics.Raycast(transform.position, transform.forward, out hit, 1000, ZombieMask))
        {            
            ZombieDeath(hit);
            Fragment(hit);
            Rigidbody rdb = hit.collider.gameObject.GetComponent<Rigidbody>();
            if (rdb)
            {
                rdb.AddForceAtPosition(transform.forward * 1000, hit.point);
            }
            Debug.DrawLine(transform.position, hit.point, Color.blue);
			//fire = false;
        }
        
    }

    void ShotSound()
    {
        wSound.pitch = 1f + Random.value * 0.2f;
        wSound.Play();
    }

    void Flash()
    {
        if (fire)
        {
            muzzleFlash.Emit(1);
        }
    }

    void Fragment(RaycastHit fhit)
    {
        fragment.transform.position = fhit.point;
        fragment.Play();
    }

    void ZombieDeath(RaycastHit zhit)
    {
		if (zhit.collider.CompareTag ("Enemy")) {

			Debug.Log ("Este é o zombie");
			zhit.collider.gameObject.layer = ZombieDeadMask;
			canDecrement = true;
			zhit.collider.SendMessageUpwards ("KillZombie");
		}
	}
}
