using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "NewCallDataScriptableObject", menuName = "Call Data")]
public class CallDataScriptableObject : ScriptableObject
{
    public Sprite externalImage;
    public Sprite internalImage;
    public AudioClip internalAudio;
    public int price;
    public bool isOpen;
}