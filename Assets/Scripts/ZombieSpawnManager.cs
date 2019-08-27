using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnManager : MonoBehaviour {
    public GameObject zombieC;
    public Transform[] spawnPoints;
    int spawnIndex;
    float rIndex;
    [SerializeField]
    float counter;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        counter += Time.deltaTime;
        if(counter >= 5)
        {
            SpawnFunction();
            counter = 0;
        }

	}

    void SpawnFunction()
    {
        rIndex = Random.Range(0f, 4f);
        spawnIndex = (int)rIndex;
        Instantiate(zombieC, spawnPoints[spawnIndex].position, Quaternion.identity);
    }


}
