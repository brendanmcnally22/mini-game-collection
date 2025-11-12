using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resettingmeteors : MonoBehaviour
{
    public Vector2 resetPosition = new Vector2(0f, 8f);
    public float fallSpeed = 2f;

    void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("meteorReset"))
        {
            Vector3 pos = transform.position;
            pos.y = resetPosition.y;
            transform.position = pos;
        }
    }
}