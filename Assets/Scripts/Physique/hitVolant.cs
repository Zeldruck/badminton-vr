using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;
using static UnityEngine.GraphicsBuffer;
using Debug = UnityEngine.Debug;

// Script permettant de frapper corectement le volant
public class hitVolant : MonoBehaviour
{

    public float coefVelocite = 2;
    private Vector3 initVelocity;
    private Vector3 initRotation;
    private Vector3 velocity;
    private Vector3 posHit;
    double t;
    Stopwatch watchHit;
  
    // Start is called before the first frame update
    void Start()
    {
        initVelocity = Vector3.zero;
        posHit = Vector3.zero;
        initRotation = Vector3.zero;
        t = 0;
        watchHit = new Stopwatch();


    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = transform.GetComponent<Rigidbody>();
        //Debug.Log("O");
        //Debug.Log(other.gameObject);

        if (other.gameObject.tag.Equals("raquette") == true) // && (watchHit.ElapsedMilliseconds > 100 || !watchHit.IsRunning)) // On applique le changement de velocite que si l'objet a le tag raquette
        {
            //initVelocity.x = 10f; initVelocity.y = 10f; initVelocity.z = 0f;
            t = 0;
            rb.isKinematic = true;
            initRotation = other.transform.rotation.eulerAngles;
            initVelocity = 2 * other.transform.GetComponent<Rigidbody>().velocity;
            //Debug.Log(initRotation);
            watchHit.Start();
            /*
            Debug.Log("A");

            Debug.Log(initVelocity);

            Debug.Log("B");

            Debug.Log(transform.position);


            Debug.DrawRay(transform.position, initVelocity, Color.red, 30f);
            */
        }
        else
        {
            Debug.Log("W");
            rb.isKinematic = false;
            watchHit.Reset();
            //rb.isKinematic = false;
        }


    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = transform.GetComponent<Rigidbody>();
        if (collision.gameObject.tag.Equals("raquette") == true && (watchHit.ElapsedMilliseconds > 100 || !watchHit.IsRunning)) // On applique le changement de velocite que si l'objet a le tag raquette
        {
            //initVelocity.x = 10f; initVelocity.y = 10f; initVelocity.z = 0f;
            t = 0;
            rb.isKinematic = true;
            initRotation = collision.GetContact(0).normal;
            Debug.Log(initRotation);
            initVelocity = 2*collision.rigidbody.GetPointVelocity(collision.GetContact(0).point);
            watchHit.Start();
            Debug.Log("A");

            Debug.Log(initVelocity);

            Debug.Log("B");

            Debug.Log(transform.position);


            Debug.DrawRay(transform.position, initVelocity, Color.red, 30f);

        }
        else
        {
            watchHit.Reset();
            rb.isKinematic = false;
        }

    }
    */

    // Update is called once per frame
    void Update()
    {

        Rigidbody rb = transform.GetComponent<Rigidbody>();
        if (rb.isKinematic == true)
        {
            
            t += 1f/60;
            Debug.Log(t);
            double vxi = Mathf.Sqrt(initVelocity.x * initVelocity.x + initVelocity.z* initVelocity.z);
            double vyi = initVelocity.y;
            double vt = 4.5;
            double g = 9.81;

            double vx = vxi * vt*vt/ (vxi * g * t + vt*vt);
            double vy = (vt + vyi) * Mathf.Exp((float) (-1 * t* g / vt)) - vt;

            velocity.x = (float) vx * Mathf.Cos(initRotation.x);
            velocity.y = (float) vy;
            velocity.z = (float) vx * Mathf.Cos(initRotation.z);


            rb.MovePosition(transform.position + velocity/60);


        }
    }
}
