using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] AudioClip coinPickUpSFX;
    [SerializeField] int pointsToAdd = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<GameSession>().AddToScore(pointsToAdd);
        AudioSource.PlayClipAtPoint(coinPickUpSFX, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
