using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.VFX;
using static UnityEngine.GraphicsBuffer;
using Debug = UnityEngine.Debug;

// Script permettant de frapper corectement le volant
public class hitVolant : MonoBehaviour
{

    public float coefVelocite = 2;
    private float initVelocity;
    private Vector3 initRotation;
    private Vector3 velocity;
    private Vector3 posHit;
    double t;
    Stopwatch watchHit;
  
    // Start is called before the first frame update
    void Start()
    {
        initVelocity = 0;
        posHit = Vector3.zero;
        initRotation = Vector3.zero;
        t = 0;
        watchHit = new Stopwatch();


    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = transform.GetComponent<Rigidbody>();

        //if (collision.gameObject.tag.Equals("raquette") == false) // && (watchHit.ElapsedMilliseconds > 100 || !watchHit.IsRunning)) // On applique le changement de velocite que si l'objet a le tag raquette
        //{
            rb.isKinematic = false;
        Debug.Log("A");
        watchHit.Start();
        initVelocity = 0;

        //}
    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag.Equals("raquette") == true && 2 * collision.transform.GetComponent<Rigidbody>().velocity.magnitude >= initVelocity)
        {
            initVelocity = 2*collision.transform.GetComponent<Rigidbody>().velocity.magnitude;
            Debug.Log("C");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Rigidbody rb = transform.GetComponent<Rigidbody>();

        if (collision.gameObject.tag.Equals("raquette") == true ) // && (watchHit.ElapsedMilliseconds > 100 || !watchHit.IsRunning)) // On applique le changement de velocite que si l'objet a le tag raquette
        {
            if(watchHit.ElapsedMilliseconds >= 10)
            {
                //initVelocity.x = 10f; initVelocity.y = 10f; initVelocity.z = 0f;

                initRotation = collision.GetContact(0).normal;
  

                float oldVelocity = rb.velocity.magnitude;
                Collider col = transform.GetComponent<Collider>();



                t = 0;
                rb.isKinematic = true;
                Debug.Log("B");

                watchHit.Reset();


            }



        }

        else
        {
            Debug.Log("W");
            rb.isKinematic = false;
            initVelocity = 0;
            //rb.isKinematic = false;
        }
    }

    /*
    public void OnTriggerExit(Collider other)
    {
        Rigidbody rb = transform.GetComponent<Rigidbody>();

        if (other.gameObject.tag.Equals("raquette") == true) // && (watchHit.ElapsedMilliseconds > 100 || !watchHit.IsRunning)) // On applique le changement de velocite que si l'objet a le tag raquette
        {
            //initVelocity.x = 10f; initVelocity.y = 10f; initVelocity.z = 0f;

            initRotation = other.transform.rotation.eulerAngles;
            initVelocity = 3 * other.transform.GetComponent<Rigidbody>().velocity;
            Collider col = transform.GetComponent<Collider>();

            if (initVelocity.magnitude >= 3 && (initVelocity.magnitude >= other.transform.GetComponent<Rigidbody>().velocity.magnitude)) // Si trop lent, physique de base
            {
                t = 0;
                rb.isKinematic = true;
                Debug.Log("B");
                watchHit.Start();
            }
            else
            {
                col.isTrigger = false;
            }

            /*
            Debug.Log("A");

            Debug.Log(initVelocity);

            Debug.Log("B");

            Debug.Log(transform.position);

                        Debug.Log("W");

            Debug.DrawRay(transform.position, initVelocity, Color.red, 30f);
            
        }
        else
        {
            Debug.Log("W");
            rb.isKinematic = false;
            watchHit.Reset();
            //rb.isKinematic = false;
        }
    }
        */

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

        Collider col = transform.GetComponent<Collider>();


        Rigidbody rb = transform.GetComponent<Rigidbody>();
        if (rb.isKinematic == true)
        {
            t += 1f/600;
            double vxi = Mathf.Abs(initVelocity * Mathf.Sin(Mathf.Deg2Rad * initRotation.y));
            double vyi = initVelocity * Mathf.Cos(Mathf.Deg2Rad * initRotation.y);
            double vt = 4.5;
            double g = 9.81;

            double vx = vxi * vt*vt/ (vxi * g * t + vt*vt);
            double vy = (vt + vyi) * Mathf.Exp((float) (-1 * t* g / vt)) - vt;

            velocity.x = (float) vx * Mathf.Cos(Mathf.Deg2Rad * initRotation.x);
            velocity.y = (float) vy;
            velocity.z = (float) vx * Mathf.Cos(Mathf.Deg2Rad * initRotation.z);

            Debug.Log("Velo");
            Debug.Log(initRotation);
            rb.MovePosition(transform.position + velocity/600);


        }
    }
}
