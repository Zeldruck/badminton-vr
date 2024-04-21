using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Debug = UnityEngine.Debug;

// Script permettant de frapper corectement le volant
public class hitVolant : MonoBehaviour
{
    public Rigidbody rb { get; private set; }

    private Vector3 initVelocity;
    private Vector3 initRotation;
    private Vector3 velocity;
    double t;
    Stopwatch watchHit;
    Boolean hit;
    private XRGrabInteractable interactable;

    public XRBaseController right_controller;
    public XRBaseController left_controller;

    public float duration = 0.3f;
    public float amplitude = 1f;

    [SerializeField] private TrailRenderer _trailRenderer;

    private void Awake()
    {
        interactable = GetComponent<XRGrabInteractable>();
        rb = gameObject.GetComponent<Rigidbody>();

        initVelocity = Vector3.zero;
        initRotation = Vector3.zero;
        t = 0;
        watchHit = new Stopwatch();
        watchHit.Start();
        hit = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //rb.isKinematic = false; // Reactive les collisions quand on touche quelque chose
        if (collision.gameObject.tag.Equals("raquette"))
        {
            sendHaptics();
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("raquette") == true && this.gameObject.tag.Equals("Shuttlecock") && (watchHit.ElapsedMilliseconds > 100 || !watchHit.IsRunning)) // On applique le changement de velocite que si l'objet a le tag raquette
        {
            ActivatePhysic();
        }

        else if (collision.gameObject.tag.Equals("raquette") == false || watchHit.ElapsedMilliseconds > 500) // Le temps est la a cause du flicker des collisions exit
        {
            rb.isKinematic = false; 
            hit = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (t < 0 && hit == true)
        {
            t += 1;
        }
        if(t ==0 && hit == true)
        {
            rb.isKinematic = true;
            watchHit.Reset(); // Lance un timeur pour eviter le flicker
            watchHit.Start();
        }

        if (rb.isKinematic == true && hit == true)
        {
            t += 1f / 60; // incremment du temps de la trajectoire
            double vxi = Mathf.Sqrt(initVelocity.x * initVelocity.x + initVelocity.z * initVelocity.z); // Vitesse horizontale
            double vyi = initVelocity.y; // vitesse verticale
            double vt = 4.5;
            double g = 9.81;

            double vx = vxi * vt * vt / (vxi * g * t + vt * vt);
            double vy = (vt + vyi) * Mathf.Exp((float)(-1 * t * g / vt)) - vt;

            velocity.x = (float)vx * initRotation.x;
            velocity.y = (float)vy;
            velocity.z = (float)vx * initRotation.z;

            //rb.rotation = Quaternion.LookRotation(-1*velocity);

            rb.MovePosition(transform.position + velocity / 60); // Avance d'une frame
        }

    }

    public void ActivatePhysic()
    {
        initVelocity = 2*rb.velocity;
        initRotation = initVelocity.normalized; // On prend la direction et la vitesse du volant
        hit = true;

        t = 0;

    }

    void sendHaptics()
    {
        if (right_controller != null)
        {
            right_controller.SendHapticImpulse(amplitude, duration);
        }
        else
        {
            Debug.Log("AAA");
        }
    }
}