using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdHealthBar : MonoBehaviour
{
	public BirdHealth birdHealth;
	public Slider slider;

	void Start()
	{
		slider.maxValue = birdHealth.health;
	}

	// Update is called once per frame
	void Update()
	{
		slider.value = birdHealth.health;
	}
}
