using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// Script permettant de frapper corectement le volant
public class hitVolant : MonoBehaviour
{

    public float coefVelocite = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("raquette") == true) // On applique le changement de velocite que si l'objet a le tag raquette
        {
            Rigidbody rb = transform.GetComponent<Rigidbody>();
            rb.velocity *= coefVelocite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
