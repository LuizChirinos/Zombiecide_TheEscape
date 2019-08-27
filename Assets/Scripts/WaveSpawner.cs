using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

    [System.Serializable]
	public class Wave
    {
        public string name;
        public Transform zombie;
        public int amount;
        public float rate;
    }

    [Header("Ondas existentes no jogo")]
    public Wave[] waves;
    private int nextWave = 0;

    [Header("Spawn Points dos zombies")]
    public Transform[] spawnPoints;

    public float timeBetweenWaves = 10f;
    public float waveCountDown;

    private float searchCountdown = 1f;

    [Header("Quantidade de inimigos vivos")]
    public int livingEnemies;

    public Text countFeedBack;
    string countText;
    public GameObject completeWaveFeedback;

    public enum SpawnStates
    {
        Spawning,
        Waiting,
        Counting
    }

    private SpawnStates state = SpawnStates.Counting;

    private void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("Nenhuma referência de Spawn Points");
        }

        waveCountDown = timeBetweenWaves;
    }

    private void Update()
    {
        countText = "Próxima Onda: " + (int)waveCountDown;
        countFeedBack.text = countText;
        if (state == SpawnStates.Waiting)
        {
            if (!ZombieIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountDown <= 0)
        {
            if(state != SpawnStates.Spawning)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Onda Completada");

        state = SpawnStates.Counting;
        waveCountDown = timeBetweenWaves;
        completeWaveFeedback.SetActive(true);

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("Completou todas as ondas - Looping...");
        }
        else
        {
            nextWave++;
        }
        StartCoroutine(CompleteWaveOff());
    }

    bool ZombieIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0)
        {
            searchCountdown = 1f;

            // Se a quantidade de inimigos for meno ou igual a zero.
            if(livingEnemies <= 0)
            {
                return false;
            }

            /*for(int i = 0; i < GameObject.FindGameObjectsWithTag("Enemy").Length; i++)
            {
                if(GameObject.FindGameObjectsWithTag("Enemy")[i].GetComponent<ZombieControl>().currentState == ZombieControl.Zstate.Dead)
                {
                    livingEnemies--;
                }
            }*/
            
        // Se não tiver nenhum GameObject de inimigo na cena.
            /*if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }*/
        }        
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawnando Onda: " + _wave.name);
        state = SpawnStates.Spawning;

        for (int i = 0; i < _wave.amount; i++)
        {
            SpawnZombie(_wave.zombie);
            livingEnemies++;
            yield return new WaitForSeconds(1f / _wave.rate);
        }
        
        state = SpawnStates.Waiting;        

        yield break;
    }

    IEnumerator CompleteWaveOff()
    {
        yield return new WaitForSeconds(2f);

        completeWaveFeedback.SetActive(false);

        yield break;
    }

    void SpawnZombie(Transform _zombie)
    {
        Debug.Log("Spawnando inimigo:" + _zombie.name);        
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_zombie, _sp.position, _sp.rotation);
    }
}
