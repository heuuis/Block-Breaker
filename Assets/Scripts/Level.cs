using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] int numBreakableBlocks; // only serialized for debugging
    [SerializeField] SceneLoader sceneLoader;

    public void CountBreakableBlocks() {
        numBreakableBlocks++;
    }

    // Update is called once per frame
    void Update()
    {
        if (numBreakableBlocks == 0)
            sceneLoader.LoadNextScene();
    }

    internal void ReduceBreakableBlocks() {
        numBreakableBlocks--;
    }
}
