using UnityEngine;

[CreateAssetMenu(menuName = "SoundEventPair/SoundGroupAndEvent")]
public class SoundGroupAndEvent : SoundEventPair {
    [SerializeField] private AudioClip[] audioClips;
    public override AudioClip AudioClip => audioClips[Random.Range(0, audioClips.Length)];
}
