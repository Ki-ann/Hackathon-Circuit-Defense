using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextUpdater : MonoBehaviour
{
    [SerializeField] private IntVariable money;
    private TextMeshPro TMPro;

    private void Start()
    {
        TMPro = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        TMPro.text = "$" + money.Value;
    }
}
