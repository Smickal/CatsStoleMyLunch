using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float cloudSpeed;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector2.left * cloudSpeed * 0.001f);
    }
}
