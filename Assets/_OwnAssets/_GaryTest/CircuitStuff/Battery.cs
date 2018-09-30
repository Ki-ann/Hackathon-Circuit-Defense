using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Battery : CircuitPart, ICircuitNeighbour {
    public struct VoltagePath {
        public List<CircuitPart> path;
        public float charge;
    }
    public List<VoltagePath> goodPaths = new List<VoltagePath> ();
    public GameObject VoltageBall;

    private bool startChecking;
    [ContextMenu("Send Ball")]
    public override void Start () {
        base.Start ();
        this.connectedBattery = this;
        StartCoroutine (voltageCheckerCoroutine ());
    }
    public override void Update () {
        base.Update ();

        if (goodPaths.Any ()) {
            foreach (VoltagePath path in goodPaths) {
                for (int i = 0; i < path.path.Count; i++) {
                    path.path[i].Charge.ChargeLevelChange (path.charge);
                    path.path[i].isConnected = true;
                }
            }
        }

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
            startChecking = true;
            this.LinkToNextNode ();
        } else {
            startChecking = false;
        }
    }

    public override void LinkToNextNode () {
        CheckNeighboursForConnection ();
        if (PositivePart != null) {
            if (PositivePart.GetComponent<CircuitPart> ()) {
                if (!this._To.Contains (PositivePart)) this._To.Add (PositivePart);
                ((CircuitPart) PositivePart).connectedBattery = this;
            }
        }
        if (NegativePart != null) {
            if (NegativePart.GetComponent<CircuitPart> ()) {
                //flip negative side hack
                if (!this._From.Contains (NegativePart)) this._From.Add (NegativePart);
                // NegativePart._From = new List<CircuitPart> (NegativePart._To);
                // NegativePart._To.Clear ();
                // NegativePart._To.Add (this);
                //((CircuitPart) NegativePart).connectedBattery = this;
            }
        }
    }
    
    IEnumerator voltageCheckerCoroutine () {
        while (true) {
            if (startChecking) {
                
                StartCoroutine(SendBall());
                yield return new WaitForSeconds(7f);
            } else
                yield return new WaitForFixedUpdate ();
        }
    }

    IEnumerator SendBall(){
        VoltageSphere ball = Instantiate (VoltageBall, this.visual.transform.position, Quaternion.identity).GetComponent<VoltageSphere> ();
                ball.originalBattery = this;
                ball.charge.ChargeLevelChange (this.Charge.MaxCharge);
                ball.previousPart = (CircuitPart) this;
                ball.nextPart = PositivePart;
                yield return new WaitUntil (() => ball == null);
    }
}