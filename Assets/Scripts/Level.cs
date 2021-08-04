using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    // parameters
    [SerializeField] int numBreakableBlocks; // only serialized for debugging
    
    // cached reference
    SceneLoader sceneLoader;

    private void Start() {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void CountBreakableBlocks() {
        numBreakableBlocks++;
    }

    internal void BlockDestroyed() {
        numBreakableBlocks--;
        if (numBreakableBlocks == 0)
            sceneLoader.LoadNextScene();
    }
}
