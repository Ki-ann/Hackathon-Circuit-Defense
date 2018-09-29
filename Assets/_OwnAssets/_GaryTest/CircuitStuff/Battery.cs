using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Battery : CircuitPart , ICircuitNeighbour{
    
    public List<CircuitPart> connectedPartsList = new List<CircuitPart> ();
    public int connectedIndex = 0;

    public override void Start () {
        base.Start ();
        Debug.Log (_Positive);
    }
    public override void AddSelfToGridSystem () {
        base.AddSelfToGridSystem ();
        GetNeighboursToSwapVisuals ();
        CheckNeighboursForConnection();
    }

    private void GetNeighboursToSwapVisuals () {
        CircuitPart[] neighbouringParts = this.m_gridSystem.GetNeighbouringParts (this.snapArea).Where (x => x != null).ToArray ();
        for (int i = 0; i < neighbouringParts.Length; i++) {
            ICircuitNeighbour neighbour = neighbouringParts[i].GetComponent<ICircuitNeighbour> ();
            if(neighbour != null)
                neighbour.NeighbourAction();
        }
    }
    private void CheckNeighboursForConnection () {
        CircuitPart[] neighbouringParts = this.m_gridSystem.GetNeighbouringParts (this.snapArea);
        int partIndex = -1;
        switch (_Positive) {
            case poles.UP:
                if (neighbouringParts[0]) {
                    partIndex = 0;
                }
                break;
            case poles.DOWN:
                if (neighbouringParts[1]) {
                    partIndex = 1;
                } 
                break;
            case poles.LEFT:
                if (neighbouringParts[2]) {
                    partIndex = 2;
                }
                break;
            case poles.RIGHT:
                if (neighbouringParts[3]) {
                    partIndex = 3;
                }
                break;
            default:
                partIndex = -1;
                PositivePart = null;
                break;
        }
        if (partIndex >= 0) {
            PositivePart = neighbouringParts[0];
        } else {
            PositivePart = null;
        }

        partIndex = -1;
        switch (_Negative) {
            case poles.UP:
                if (neighbouringParts[0]) {
                    partIndex = 0;
                }
                break;
            case poles.DOWN:
                if (neighbouringParts[1]) {
                    partIndex = 1;
                } 
                break;
            case poles.LEFT:
                if (neighbouringParts[2]) {
                    partIndex = 2;
                }
                break;
            case poles.RIGHT:
                if (neighbouringParts[3]) {
                    partIndex = 3;
                }
                break;
            default:
                partIndex = -1;
                NegativePart = null;
                break;
        }
        if (partIndex >= 0) {
            NegativePart = neighbouringParts[0];
        } else {
            NegativePart = null;
        }
    }

    public void NeighbourAction()
    {
        SetupConnections();
    }

    private void SetupConnections(){
        if(PositivePart && NegativePart){
            
        }
    }

    public override void LinkToNextNode(){
        base.LinkToNextNode();
    }
}