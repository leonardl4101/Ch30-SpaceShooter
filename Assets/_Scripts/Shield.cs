﻿using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {
	public float  rotationsPerSecond = 0.1f;
	public bool __________________;
	public int  levelShown = 0;

	// Update is called once per frame
	void Update () {
		int currLevel = Mathf.FloorToInt (Hero.S.shieldLevel);

		if (levelShown != currLevel) {
						levelShown = currLevel;
						Material mat = this.GetComponent<Renderer>().material;
						mat.mainTextureOffset = new Vector2 (0.2f * levelShown, 0);
				}

		float rZ = (rotationsPerSecond * Time.time * 360) % 360f;
		transform.rotation = Quaternion.Euler (0, 0, rZ);
	
	}
}
