using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ParticleSystem deathParticleSystem;
    public float forceMultipler = 12f;
    public float maximumVelocity = 5f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!GameManager.IsRunning())
        {
            return;
        }
        var x = Input.GetAxis("Horizontal");
        if (rb.velocity.magnitude <= maximumVelocity)
        {
            rb.AddForce(new Vector3(x * forceMultipler * Time.deltaTime, 0, 0));
        }
    }

    public void OnEnable()
    {
        gameObject.SetActive(true);
        gameObject.transform.position = new Vector3(0, gameObject.transform.position.y, gameObject.transform.position.z);
        rb.velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            GameManager.Stop();
            Instantiate(deathParticleSystem, transform.position, Quaternion.identity);
        }
    }
}
