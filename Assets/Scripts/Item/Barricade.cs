using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    public float x = 5f;
    public float y = 5f;
    public float z = 5f;
    private void OnDisable()
    {
        transform.localScale = new Vector3(x, y, z);
    }
}
