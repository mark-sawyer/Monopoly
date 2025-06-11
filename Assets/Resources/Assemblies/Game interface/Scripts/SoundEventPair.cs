using UnityEngine;

[CreateAssetMenu(menuName = "SoundEventPair")]
public class SoundEventPair : ScriptableObject {
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private GameEvent gameEvent;
    public AudioClip AudioClip => audioClip;
    public GameEvent GameEvent => gameEvent;
}
