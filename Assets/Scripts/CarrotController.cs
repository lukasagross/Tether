using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotController : MonoBehaviour {

    private Transform[] carrotSpawns;
    private Transform nextSpawn;
    private int spawnerCount;
    private GameMode gm;
    
    


    void Awake()
    {
        gm = FindObjectOfType<GameMode>();

        if (gm.currentMode == GameMode.Mode.carrots) {
            spawnerCount = transform.childCount;
        carrotSpawns = new Transform[spawnerCount];
        for (int i = 0; i < spawnerCount; i++)
        {
            carrotSpawns[i] = transform.GetChild(i);
        }

        StartCoroutine(SpawnCarrots(5));
        }
	}
	
	private IEnumerator SpawnCarrots(float timeToNextSpawn)
    {
        yield return new WaitForSeconds(timeToNextSpawn);
        
        nextSpawn = transform.GetChild(Random.Range(0, spawnerCount));
        Object c = Resources.Load("Prefabs/Carrot");
        Instantiate(c, nextSpawn.position, Quaternion.Euler(new Vector3(0, 0, 0)), nextSpawn);

        if (gm.currentMode == GameMode.Mode.carrots)
        {
            StartCoroutine(SpawnCarrots(5));
        }
    }
}
