﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

    public enum STATUS { START, END, ONGOING, NEXT, STANDBY }
    public STATUS waveStatus;
    public Zombie zombie;

    [SerializeField] int waveCount = 0;
    [SerializeField] float timer = 1.5f;
    [SerializeField] float resetTimer = 0.5f;
    [SerializeField] float timeB4WaveStart = 30f;
    int resetCount = 0;
    [SerializeField] int totalToSpawn;
    [SerializeField] int spawnCount;

    Vector3 spawnPos;
    Quaternion spawnRot;
    Zombie spawnedZombie;
    // Use this for initialization
    void Start () { }

    // Update is called once per frame
    void Update () {
        //Debug.Log(spawnZombie);
        WaveAlgo (waveStatus);

        //test
        if (Input.GetKeyDown(KeyCode.C))
         {
             ShortenWaveWaitTime();
         }
    }

    void ShortenWaveWaitTime () {
        timer = 2.0f;
    }

    public void WaveAlgo (STATUS waveStatus) {
        switch (waveStatus) {
            case STATUS.STANDBY:
                StandBy ();
                break;
            case STATUS.START:
                StartWave ();
                break;
            case STATUS.END:
                EndWave ();
                break;
            case STATUS.ONGOING:
                Spawn ();
                Ongoing ();
                break;
            case STATUS.NEXT:
                NextWave ();
                break;
        }
    }

    public void StandBy () {
        timer = timeB4WaveStart;
        waveStatus = STATUS.START;
    }
    public void StartWave () {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            spawnCount = 0;
            totalToSpawn = waveCount * waveCount;
            timer = 1.5f;
            waveStatus = STATUS.ONGOING;
        }
    }

    public void EndWave () {
        for (int i = 0; i < AIArray.Instance.enemyList.Count; i++) {
            if (AIArray.Instance.enemyList[i] != null) {
                DestroyImmediate (AIArray.Instance.enemyList[i].gameObject);
                AIArray.Instance.enemyList.RemoveAt (i);
            } else
                AIArray.Instance.enemyList.RemoveAt (i);
        }

        if (AIArray.Instance.enemyList.Count == 0)
            waveStatus = STATUS.NEXT;
    }

    public void NextWave () {
        waveCount += 1;
        waveStatus = STATUS.STANDBY;
    }

    public void Ongoing () {
        for (int i = 0; i < AIArray.Instance.enemyList.Count; i++) {
            if (AIArray.Instance.enemyList[i] == null) {
                AIArray.Instance.enemyList.RemoveAt (i);
                totalToSpawn--;
                spawnCount--;
            }
            if (AIArray.Instance.enemyList.Count == 0)
                waveStatus = STATUS.END;
        }
    }
    public void Spawn () {
        if (totalToSpawn - spawnCount > 0) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                spawnPos = SpawnArray.Instance.spawnPointObjList[Random.Range (0, waveCount)].transform.position;
                spawnRot = SpawnArray.Instance.spawnPointObjList[Random.Range (0, waveCount)].transform.rotation;
                spawnedZombie = Instantiate (zombie, spawnPos, spawnRot);
                spawnedZombie.TypeOfZombie(Random.Range(1, 3)); //1 is normal zombie, 2 is fast zombie, 3 is tanky zombie
                spawnedZombie.destinationObj = FindObjectOfType<Core> ().gameObject;

                if (!AIArray.Instance.enemyList.Contains (spawnedZombie as EnemyAI))
                    AIArray.Instance.enemyList.Add (spawnedZombie as EnemyAI);

                spawnCount++;
                timer = resetTimer;
            }
        }
    }
}