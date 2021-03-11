using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    public Text fps_display;

    private float frameCount = 0.0f;
    private float dt = 0.0f;
    private float fps = 0.0f;
    private float updateRate = 4.0f;  // 4 updates per sec.
     
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        frameCount++;
        dt += Time.deltaTime;
        if (dt > 1.0/updateRate)
        {
            fps = frameCount / dt ;
            frameCount = 0;
            dt -= 1.0f/updateRate;
        }

        fps_display.text = "FPS: " + fps.ToString();
    }
}
