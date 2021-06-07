using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    // configuration parameters
    [SerializeField] float xMin = 1f, xMax = 15f;
    [SerializeField] float screenWidthUnits = 16f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mousePosX = screenWidthUnits * Input.mousePosition.x / Screen.width;
        float paddlePosX = Mathf.Clamp(mousePosX, xMin, xMax);       
        Vector2 paddlePos = new Vector2(paddlePosX, transform.position.y);
        transform.position = paddlePos;
    }
}
