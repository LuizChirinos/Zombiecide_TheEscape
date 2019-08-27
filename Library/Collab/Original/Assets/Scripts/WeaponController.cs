using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {
    public bool fire;
    public bool canDecrement;
    WaveSpawner waveS;
    public AudioSource weaponSound;

	// Use this for initialization
	void Start () {
        waveS = FindObjectOfType<WaveSpawner>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            fire = true;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            fire = false;
        }
        FireWeapon();
        
        // Se puder decrementar, decrementa o valor de inimigos vivos na cena.
        if (canDecrement)
        {
            waveS.livingEnemies --;
            ShotSound();
            canDecrement = false;
        }
        
	}

    void FireWeapon()
    {
        RaycastHit hit;
        if(fire && Physics.Raycast(transform.position, transform.forward, out hit, 100))
        {            
            ZombieDeath(hit);
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
        if (fire)
        {
            weaponSound.pitch = 1f + Random.value * 0.2f;
            weaponSound.Play();
        }
    }

    void ZombieDeath(RaycastHit zhit)
    {
        //Debug.Log(zhit.collider.name);
        if (zhit.collider.CompareTag("Enemy"))
        {            
            Debug.Log("Este é o zombie");
            canDecrement = true;
            zhit.collider.SendMessageUpwards("KillZombie");
        }
    }
}
