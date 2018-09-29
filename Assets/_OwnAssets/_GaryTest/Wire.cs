using System;
using System.Linq;
using UnityEngine;
public class Wire : CircuitPart {
    [SerializeField] private GameObject[] wireVisuals;

    public override void AddSelfToGridSystem () {
        base.AddSelfToGridSystem ();

        SwapVisuals ();
    }
    public void SwapVisuals () {
        CircuitPart[] neighbouringParts = this.m_GridSystem.GetNeighbouringParts (transform.position);

        // Use binary to check for different wire configurations
        string partsInBinary = (neighbouringParts[0] ? "1" : "0") + (neighbouringParts[1] ? "1" : "0") + (neighbouringParts[2] ? "1" : "0") + (neighbouringParts[3] ? "1" : "0");

        switch (neighbouringParts.Where (x => x != null).Count ()) {
            case 0:
                this.visual = wireVisuals[0]; // no connections
                break;
            case 1:
                this.visual = wireVisuals[1]; // │
                this.visual.transform.LookAt (neighbouringParts.Where (x => x != null).FirstOrDefault ().transform);
                break;
            case 2:
                switch (System.Convert.ToInt32 (partsInBinary, 2)) {
                    case 5: // ─
                        this.visual = wireVisuals[2];
                        break;
                    case 10: // │
                        this.visual = wireVisuals[3];
                        break;
                    case 3: // ┐
                        this.visual = wireVisuals[4];
                        break;
                    case 12: //└
                        this.visual = wireVisuals[5];
                        break;
                    case 9: // ┘
                        this.visual = wireVisuals[6];
                        break;
                    case 6: // ┌
                        this.visual = wireVisuals[7];
                        break;
                }
                break;
            case 3:
                switch (System.Convert.ToInt32 (partsInBinary, 2)) {
                    case 14: // ├
                        this.visual = wireVisuals[8];
                        break;
                    case 7: // ┬
                        this.visual = wireVisuals[9];
                        break;
                    case 11: // ┤
                        this.visual = wireVisuals[10];
                        break;
                    case 13: // ┴
                        this.visual = wireVisuals[11];
                        break;
                }
                break;
            case 4:
                this.visual = wireVisuals[12];//┼
                break;
        }
    }

    private void GetNeighboursToSwapVisuals(){
        CircuitPart[] neighbouringParts = this.m_GridSystem.GetNeighbouringParts (transform.position).Where(x=> x != null).ToArray();
        for(int i = 0; i < neighbouringParts.Length; i++){
            Wire wireComponent = neighbouringParts[i].GetComponent<Wire>();
            if(wireComponent)
                wireComponent.SwapVisuals();
        }
    }
}