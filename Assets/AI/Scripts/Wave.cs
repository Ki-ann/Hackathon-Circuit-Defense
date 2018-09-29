using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

    public enum STATUS { START, END, ONGOING}
    public STATUS waveStatus;

    bool start;
    bool end;
    bool ongoing;

    int waveCount = 0;
    int resetCount = 0;
	
    
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void waveAlgo(STATUS waveStatus)
    {
        switch (waveStatus)
        {
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
                ongoing = true;
                break;
        }
    }

    public void StartWave()
    {

    }

    public void EndWave()
    {

    }

    public void Spawn()
    {

    }
}
