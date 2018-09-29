using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

    public enum STATUS { START, END, ONGOING, NOTHING}
    public STATUS waveStatus;

    bool start;
    bool end;
    bool ongoing;

    int waveCount = 0;
    int resetCount = 0;
	
    public Zombie zombie;
    public GameObject startOne;
    public GameObject startTwo;
    public GameObject startThree;
    public GameObject startFour;
    public AIArray aiArray;

    Vector3 startPosOne;
    Quaternion startRotOne;
    Vector3 startPosTwo;
    Quaternion startRotTwo;
    Vector3 startPosThree;
    Quaternion startRotThree;
    Vector3 startPosFour;
    Quaternion startRotFour;
    Zombie spawnZombie;
    // Use this for initialization
    void Start () {
        startPosOne = startOne.transform.position;
        startRotOne = startOne.transform.rotation;
        startPosTwo = startTwo.transform.position;
        startRotTwo = startTwo.transform.rotation;
        startPosThree = startThree.transform.position;
        startRotThree = startThree.transform.rotation;
        startPosFour = startFour.transform.position;
        startRotFour = startFour.transform.rotation;

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
                if (waveCount <= 3 && aiArray)
                    for (int i = 0; i < waveCount * 5; i++)
                    {
                    Spawn();
                    }
                ongoing = true;
                break;
        }
    }

    public void StartWave()
    {
        waveCount++;
        waveStatus = STATUS.ONGOING;
    }

    public void EndWave()
    {

    }

    public void Spawn()
    {
        if(waveCount <= 3 && aiArray.enemyList.Count < waveCount * 5)
        {
            spawnZombie = Instantiate(zombie, startPosTwo, startRotTwo);
            spawnZombie.destinationObj = FindObjectOfType<Core>().gameObject;
            //Debug.Log(spawnZombie.destinationObj.transform.position);
        }

        if (!aiArray.enemyList.Contains(spawnZombie as EnemyAI))
            aiArray.enemyList.Add(spawnZombie as EnemyAI);
    }
}
