using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;
using static UnityEngine.GraphicsBuffer;

// Script permettant de frapper corectement le volant
public class hitVolant : MonoBehaviour
{

    public float coefVelocite = 2;
    private Vector3 initVelocity;
    private Vector3 velocity;
    private Vector3 posHit;
    double t;
  
    // Start is called before the first frame update
    void Start()
    {
        initVelocity = Vector3.zero;
        posHit = Vector3.zero;
        t = 0;


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("raquette") == true) // On applique le changement de velocite que si l'objet a le tag raquette
        {
            initVelocity.x = 1f; initVelocity.y = 1f; initVelocity.z = 1f;
            posHit = this.transform.position;
            t = 0;

        }
    }

    // Update is called once per frame
    void Update()
    {


        if (posHit != Vector3.zero)
        {
            Rigidbody rb = transform.GetComponent<Rigidbody>();
            t += 0.001;
            double vxi = initVelocity.x;
            double vyi = initVelocity.y;
            double vt = 4.5;
            double g = 9.81;


            double vx = vxi * vt*vt/ (vxi * g * t + vt*vt);
            double temp = 1 + (vyi / vt) * Mathf.Tan((float) (t * g / vt));
            double vy = vyi - vt * Mathf.Tan((float)(t * g / vt));
            vy /= temp;

            velocity.x = (float) vx;
            velocity.y = (float) vy;
            velocity.z = 0;

            rb.MovePosition(transform.position + velocity/100);


        }
        //this.transform.Translate(velocity/60);
    }
}
