using System;
using System.Linq;
using UnityEngine;
public class Wire : CircuitPart, ICircuitNeighbour {
    public Battery connectedBattery;
    [SerializeField] private GameObject[] wireVisuals;
    private int nextVisualIndex;

    public override void AddSelfToGridSystem () {
        base.AddSelfToGridSystem ();

        SwapVisuals ();
        GetNeighboursToSwapVisuals ();
    }
    public void SwapVisuals () {
        CircuitPart[] neighbouringParts = this.m_gridSystem.GetNeighbouringParts (this.snapArea);

        // Use binary to check for different wire configurations
        string partsInBinary = (neighbouringParts[0] ? "1" : "0") + (neighbouringParts[1] ? "1" : "0") + (neighbouringParts[2] ? "1" : "0") + (neighbouringParts[3] ? "1" : "0");
        // Debug.Log (System.Convert.ToInt32 (partsInBinary, 2));
        nextVisualIndex = 0;
        switch (neighbouringParts.Where (x => x != null).Count ()) {
            case 0:
                nextVisualIndex = 0; // no connections
                break;
            case 1:
                switch (System.Convert.ToInt32 (partsInBinary, 2)) {
                    case 8:
                        nextVisualIndex = 1; // │
                        break;
                    case 2:
                        nextVisualIndex = 1; // │
                        break;
                    case 4:
                        nextVisualIndex = 2; // ─
                        break;
                    case 1:
                        nextVisualIndex = 2; // ─
                        break;
                }
                break;
            case 2:
                switch (System.Convert.ToInt32 (partsInBinary, 2)) {
                    case 5: // ─
                        nextVisualIndex = 2;
                        break;
                    case 10: // │
                        nextVisualIndex = 1;
                        break;
                    case 3: // ┐
                        nextVisualIndex = 3;
                        break;
                    case 12: //└
                        nextVisualIndex = 4;
                        break;
                    case 9: // ┘
                        nextVisualIndex = 5;
                        break;
                    case 6: // ┌
                        nextVisualIndex = 6;
                        break;
                }
                break;
            case 3:
                switch (System.Convert.ToInt32 (partsInBinary, 2)) {
                    case 14: // ├
                        nextVisualIndex = 7;
                        break;
                    case 7: // ┬
                        nextVisualIndex = 8;
                        break;
                    case 11: // ┤
                        nextVisualIndex = 9;
                        break;
                    case 13: // ┴
                        nextVisualIndex = 10;
                        break;
                }
                break;
            case 4:
                nextVisualIndex = 11; //┼
                break;
        }
        Destroy (this.visual);
        this.visual = Instantiate (wireVisuals[nextVisualIndex], this.transform);
    }

    private void GetNeighboursToSwapVisuals () {
        CircuitPart[] neighbouringParts = this.m_gridSystem.GetNeighbouringParts (this.snapArea).Where (x => x != null).ToArray ();
        for (int i = 0; i < neighbouringParts.Length; i++) {
            ICircuitNeighbour neighbour = neighbouringParts[i].GetComponent<ICircuitNeighbour> ();
            if (neighbour != null)
                neighbour.NeighbourAction ();
        }
        this.SwapVisuals ();
    }

    public void NeighbourAction () {
        this.SwapVisuals ();
    }

    [ContextMenu ("Next Wire Visual")]
    public void DebugSwitchVisuals () {
        nextVisualIndex = nextVisualIndex + 1 >= wireVisuals.Length? 0 : nextVisualIndex + 1;
        Destroy (this.visual);
        this.visual = Instantiate (wireVisuals[nextVisualIndex], this.transform);
    }

    public override void LinkToNextNode () {
        base.LinkToNextNode ();
        for(int i = 0; i < _From.Count; i++){
            if(_From[i].GetComponent<Wire>()){
                this.connectedBattery = ((Wire)_From[i]).connectedBattery;
                this.connectedBattery.connectedIndex++;
                this.connectedBattery.connectedPartsList.Add(this);
                return;
            }
            // if(_From[i].GetComponent<Turret>()){
            //     this.connectedBattery = ((Turret)_From[i]).connectedBattery;
            //     this.connectedBattery.connectedIndex++;
            //     this.connectedBattery.connectedPartsList.Add(this);
            //     return;
            // }
        }
        
    }
}