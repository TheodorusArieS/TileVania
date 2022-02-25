using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsBehavior : MonoBehaviour
{
    GameSession gameSession;
    [SerializeField] AudioClip clip;
    [SerializeField] int points=100;
    bool wasCollected;
    void Awake()
    {
        gameSession = FindObjectOfType<GameSession>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            gameSession.IncreaseCoin(points);
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
