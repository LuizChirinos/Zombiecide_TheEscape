using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {
    public bool fire;
    bool canDecrement;
    WaveSpawner waveS;

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
