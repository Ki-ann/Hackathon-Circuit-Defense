using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

    public enum STATUS { START, END, ONGOING, NEXT, NOTHING}
    public STATUS waveStatus;
    public Zombie zombie;

    bool start;
    bool end;
    bool ongoing;

    [SerializeField] int waveCount = 0;
    [SerializeField] float timer = 1.5f;
    [SerializeField] float resetTimer = 0.5f;
    int resetCount = 0;
    [SerializeField] int totalToSpawn;
    [SerializeField] int spawnCount;

    Vector3 spawnPos;
    Quaternion spawnRot;
    Zombie spawnedZombie;
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(spawnZombie);
        WaveAlgo(waveStatus);
	}

    public void WaveAlgo(STATUS waveStatus)
    {
        switch (waveStatus)
        {
            case STATUS.NOTHING:
                break;
            case STATUS.START:
                start = true;
                end = false;
                StartWave();
                break;
            case STATUS.END:
                start = false;
                end = true;
                ongoing = false;
                EndWave();
                break;
            case STATUS.ONGOING:
                Spawn();
                Ongoing();
                ongoing = true;
                break;
            case STATUS.NEXT:
                NextWave();
                break;
        }
    }

    public void StartWave()
    {
        waveStatus = STATUS.ONGOING;
        spawnCount = 0;
        totalToSpawn = waveCount * waveCount;
    }

    public void EndWave()
    {
        for (int i = 0; i < AIArray.Instance.enemyList.Count; i++)
        {
            if (AIArray.Instance.enemyList[i] != null)
            {
                DestroyImmediate(AIArray.Instance.enemyList[i].gameObject);
                AIArray.Instance.enemyList.RemoveAt(i);
            }
            else
                AIArray.Instance.enemyList.RemoveAt(i);
        }

        if(AIArray.Instance.enemyList.Count == 0)
            waveStatus = STATUS.NEXT;
    }

    public void NextWave()
    {
        waveCount += 1;
        waveStatus = STATUS.START;
    }

    public void Ongoing()
    {
        for (int i = 0; i < AIArray.Instance.enemyList.Count; i++)
        {
            if (AIArray.Instance.enemyList[i] == null)
            {
                AIArray.Instance.enemyList.RemoveAt(i);
                totalToSpawn--;
                spawnCount--;
            }
            if (AIArray.Instance.enemyList.Count == 0)
                waveStatus = STATUS.END;
        }
    }
    public void Spawn()
    {
        if(totalToSpawn - spawnCount > 0)
        {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    spawnPos = SpawnArray.Instance.spawnPointObjList[Random.Range(0, waveCount)].transform.position;
                    spawnRot = SpawnArray.Instance.spawnPointObjList[Random.Range(0, waveCount)].transform.rotation;
                    spawnedZombie = Instantiate(zombie, spawnPos, spawnRot);
                    spawnedZombie.destinationObj = FindObjectOfType<Core>().gameObject;

                    if (!AIArray.Instance.enemyList.Contains(spawnedZombie as EnemyAI))
                        AIArray.Instance.enemyList.Add(spawnedZombie as EnemyAI);

                    spawnCount++;
                    timer = resetTimer;
                }
            }
        }
        //if(AIArray.Instance.enemyList.Count < waveCount * waveCount)
        //{
        //    //timer -= Time.deltaTime;
        //    for (int i = 0; i < waveCount * waveCount; i++)
        //    {
        //        spawnPos = SpawnArray.Instance.spawnPointObjList[Random.Range(0, waveCount)].transform.position;
        //        spawnRot = SpawnArray.Instance.spawnPointObjList[Random.Range(0, waveCount)].transform.rotation;

        //      //  if (timer <= 0)
        //        //{
        //            spawnedZombie = Instantiate(zombie, spawnPos, spawnRot);
        //            spawnedZombie.destinationObj = FindObjectOfType<Core>().gameObject;
        //            timer = 1.5f;
        //        //}
        //        if (!AIArray.Instance.enemyList.Contains(spawnedZombie as EnemyAI))
        //            AIArray.Instance.enemyList.Add(spawnedZombie as EnemyAI);
        //        //Debug.Log(spawnZombie.destinationObj.transform.position);
        //    }
        //}
    
}
