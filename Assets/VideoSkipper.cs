using UnityEngine.Video;
using UnityEngine;

public class VideoSkipper : MonoBehaviour
{
    public VideoPlayer player;
    public void Skip()
    {
        player.Pause();
    }
}
