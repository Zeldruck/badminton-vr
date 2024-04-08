using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shuttle_basic_spawner : MonoBehaviour
{
    public Transform direction;
    public GameObject shuttle;
    public float cool_down = 10.0f;
    public float speed = 100.0f;
    public int max_count = 2;

    Queue<GameObject> stack = new Queue<GameObject>();
    
    float timer = 0;

    void Start()
    {
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        Spawn();
    }

    void Spawn()
    {
        if (stack.Count > max_count)
        {
            GameObject obj = stack.Dequeue();
            DestroyImmediate(obj);
        }

        if (timer >= cool_down)
        {
            GameObject obj = Instantiate(shuttle, this.transform.position, Quaternion.identity);
            Rigidbody  trs = obj.GetComponent<Rigidbody>();

            Vector3 dir = (this.direction.position - this.transform.position).normalized;

            trs.velocity = dir * speed;
            stack.Enqueue(obj);
            timer = 0;
        }
    }
}
