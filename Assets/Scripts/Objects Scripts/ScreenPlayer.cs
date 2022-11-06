using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Video;

public class ScreenPlayer : MonoBehaviour
{
    [SerializeField] private VideoClip video;

    private void Awake()
    {
        GetComponent<UnityEngine.Video.VideoPlayer>().url = System.IO.Path.Combine(Application.streamingAssetsPath, "SampleVideo.mp4");
    }

}
