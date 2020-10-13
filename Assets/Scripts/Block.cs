using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    // config
    [SerializeField] private AudioClip breakSound;
    [SerializeField] private GameObject blockSparklesVFX;
    [SerializeField] private Sprite[] hitSprites;
    [SerializeField] private int blockPoints;

    private Level _level;
    
    // state
    [SerializeField] private int timesHit; // Serialized only for DEBUG purposes
    
    private void Start()
    {
        if (CompareTag("Breakable"))
        {
            _level = FindObjectOfType<Level>();
            _level.CountBreakableBlocks();
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CompareTag("Breakable"))
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        timesHit++;
        var maxHits = hitSprites.Length + 1;
        
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.Log("Block sprite is missing from array " + gameObject.name);
        }
    }

    private void DestroyBlock()
    {
        FindObjectOfType<GameSession>().AddToScore(blockPoints);
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        Destroy(gameObject);
        _level.BlockDestroyed();
        TriggerSparklesVFX();
    }

    private void TriggerSparklesVFX()
    {
        var sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }
}
