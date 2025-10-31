using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    public ParticleSystem deathParticleSystem;
    public float forceMultipler = 12f;
    public float maximumVelocity = 5f;
    private Rigidbody rb;
    private CinemachineImpulseSource cinemachineImpulseSource;
    public GameObject mainVCam;
    public GameObject zoomVCam;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void Update()
    {
        if (GameManager.Instance == null)
        {
            return;
        }
        var x = Input.GetAxis("Horizontal");
        if (rb.velocity.magnitude <= maximumVelocity)
        {
            rb.AddForce(new Vector3(x * forceMultipler * Time.deltaTime, 0, 0));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            GameManager.GameOver();
            Destroy(gameObject);
            Instantiate(deathParticleSystem, transform.position, Quaternion.identity);
            cinemachineImpulseSource.GenerateImpulse();
            mainVCam.SetActive(false);
            zoomVCam.SetActive(true);
        }
    }
}
