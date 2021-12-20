using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalingPlatform : MonoBehaviour
{

	Rigidbody2D rb;
	Vector2 initialPosition;
	bool platformMovingBack;

	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		initialPosition = transform.position;
	}

	void Update()
	{
		if (platformMovingBack)
			transform.position = Vector2.MoveTowards(transform.position, initialPosition, 20f * Time.deltaTime);

		if (transform.position.y == initialPosition.y)
			platformMovingBack = false;
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.name.Equals("Player") && !platformMovingBack)
		{
			Invoke("DropPlatform", 0.5f);
		}
	}

	void DropPlatform()
	{
		rb.isKinematic = false;
		Invoke("GetPlatformBack", 1f);
	}

	void GetPlatformBack()
	{
		rb.velocity = Vector2.zero;
		rb.isKinematic = true;
		platformMovingBack = true;
	}

}
