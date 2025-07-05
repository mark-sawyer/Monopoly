using UnityEngine;

[CreateAssetMenu(menuName = "RandomAudioClip")]
internal class RandomAudioClip : ScriptableObject {
    [SerializeField] private AudioClip[] audioClips;

    public AudioClip getRandom() {
        int index = Random.Range(0, audioClips.Length);
        return audioClips[index];
    }
}
