using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // configuration parameters
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] int maxHits;
    [SerializeField] Sprite[] hitSprites;

    // cached references
    Level level;
    GameSession session;

    // state variables
    [SerializeField] int timesHit; // serialized for debugging

    private void Start() {
        level = FindObjectOfType<Level>();
        session = FindObjectOfType<GameSession>();

        CountBreakableBlocks();
    }

    private void CountBreakableBlocks() {
        if (CompareTag("Breakable")) {
            level.CountBreakableBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (CompareTag("Breakable")) {
            HandleHit();
        }
    }

    private void HandleHit() {
        timesHit++;
        if (timesHit >= maxHits) {
            HandleDestroyBlock();
        }
        else {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite() {
        int spriteIndex = timesHit - 1;
        GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
    }

    private void HandleDestroyBlock() {
        session.AddToScore();
        level.BlockDestroyed();
        DestroyBlock();
    }

    private void DestroyBlock() {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        TriggerSparklesVFX();
        Destroy(gameObject);
    }

    private void TriggerSparklesVFX() {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 2.0f);
    }
}
