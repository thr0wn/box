using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    Vector3 rotation;

    void Start()
    {
	var xRotation = Random.Range(0.2f, 1f);
	rotation = new Vector3(xRotation, 0, 0);
    }

    void Update()
    {
	transform.Rotate(rotation);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
	if (!collision.gameObject.CompareTag("Hazard"))    {
	  Destroy(gameObject);
	}
    }
}
