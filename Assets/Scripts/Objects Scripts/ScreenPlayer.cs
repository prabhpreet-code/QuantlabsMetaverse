using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenPlayer : MonoBehaviour
{
    private GameObject screen;
    
    private void Awake()
    {
        GetComponent<UnityEngine.Video.VideoPlayer>().url = System.IO.Path.Combine(Application.streamingAssetsPath, "SampleVideo.mp4");
    }
}
