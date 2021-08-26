using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollVertical : MonoBehaviour
{
    [Tooltip("Game unit per second")]
    [SerializeField] float scrollRate = 0.2f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(0f,scrollRate * Time.deltaTime));
    }
}
