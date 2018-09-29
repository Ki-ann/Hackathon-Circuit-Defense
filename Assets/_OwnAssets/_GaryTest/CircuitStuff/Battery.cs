using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Battery : CircuitPart, ICircuitNeighbour {
    public List<CircuitPart> path = new List<CircuitPart> ();
    public GameObject VoltageBall;
    public override void Start () {
        base.Start ();
        this.connectedBattery = this;
    }
    public override void Update () {
        base.Update ();
    }
    public override void AddSelfToGridSystem () {
        base.AddSelfToGridSystem ();
        GetNeighboursToDoAction ();

    }

    private void CheckNeighboursForConnection () {
        CircuitPart[] neighbouringParts = this.m_gridSystem.GetNeighbouringParts (this.snapArea);
        int partIndex = -1;
        switch (_Positive) {
            case poles.NONE:
                partIndex = -1;
                PositivePart = null;
                break;
            case poles.UP:
                if (neighbouringParts[0]) {
                    partIndex = 0;
                }
                break;
            case poles.RIGHT:
                if (neighbouringParts[1]) {
                    partIndex = 1;
                }
                break;
            case poles.DOWN:
                if (neighbouringParts[2]) {
                    partIndex = 2;
                }
                break;
            case poles.LEFT:
                if (neighbouringParts[3]) {
                    partIndex = 3;
                }
                break;

        }
        if (partIndex >= 0) {
            PositivePart = neighbouringParts[partIndex];
        } else {
            PositivePart = null;
        }

        partIndex = -1;
        switch (_Negative) {
            case poles.NONE:
                partIndex = -1;
                NegativePart = null;
                break;
            case poles.UP:
                if (neighbouringParts[0]) {
                    partIndex = 0;
                }
                break;
            case poles.RIGHT:
                if (neighbouringParts[1]) {
                    partIndex = 1;
                }
                break;
            case poles.DOWN:
                if (neighbouringParts[2]) {
                    partIndex = 2;
                }
                break;
            case poles.LEFT:
                if (neighbouringParts[3]) {
                    partIndex = 3;
                }
                break;
        }
        if (partIndex >= 0) {
            NegativePart = neighbouringParts[partIndex];
        } else {
            NegativePart = null;
        }
    }

    public override void NeighbourAction () {
        CheckNeighboursForConnection ();
        SetupConnections ();
    }

    private void SetupConnections () {
        if (PositivePart && NegativePart) {
            _From.Add (NegativePart);
            _To.Add (PositivePart);
            this.LinkToNextNode ();
        }
    }

    public override void LinkToNextNode () {
        CheckNeighboursForConnection ();
        if (PositivePart != null) {
            if (PositivePart.GetComponent<Wire> ())
                if (!this._To.Contains (PositivePart)) this._To.Add (PositivePart);
            ((Wire) PositivePart).connectedBattery = this;
        }
        if (NegativePart != null) {
            if (NegativePart.GetComponent<Wire> ())
                if (!this._From.Contains (NegativePart)) this._From.Add (NegativePart);
            ((Wire) NegativePart).connectedBattery = this;
        }
    }

    public void EstablishedBatteryConnection (CircuitPart lastPart) {

        foreach (CircuitPart part in path) {
            part.isConnected = true;
        }

        Debug.Log ("We are connected");
    }
}