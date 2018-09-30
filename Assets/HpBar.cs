using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour {

	[SerializeField] Image image;
	[SerializeField] Color color;
	public BillboardItem billboard;

	// void Start()
	// {
	// 	if(billboard)
	// 	billboard = GetComponent<BillboardItem>();
	// 	billboard.faceTarget = 
	// }

	// Update is called once per frame
	void Update () {
		image.color = color;
	}

	public void UpdateHPBar (float currentHp, float maxHp) {
		image.fillAmount = currentHp / maxHp;
	}
}