using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    public float x = 5f;
    public float y = 5f;
    public float z = 5f;
    public float duration = 5f;
    private void OnDisable()
    {
        transform.localScale = new Vector3(x, y, z);
        transform.GetChild(0).gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        
    }

    IEnumerator distroy()
    {
        yield return new WaitForSecondsRealtime(z);
    }
}
