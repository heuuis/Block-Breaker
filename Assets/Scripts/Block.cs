using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] AudioClip breakSound;

    // cached references
    Level level;
    GameSession status;

    private void Start() {
        level = FindObjectOfType<Level>();
        level.CountBreakableBlocks();

        status = FindObjectOfType<GameSession>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        DestroyBlock();
    }

    private void DestroyBlock() {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        level.BlockDestroyed();
        status.AddToScore();
        Destroy(gameObject);
    }
}
