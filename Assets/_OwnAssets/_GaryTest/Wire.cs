using UnityEngine;
using System;
public class Wire : CircuitPart{
    private Wire _To;
    private Wire _From;

    [SerialzieField] private GameObject[] wireVisuals;
    
    private void swapVisuals(){
        //this.visual = wireVisuals[0];
    }
     
}