using UnityEngine;

[CreateAssetMenu(fileName = "New PanelSprites", menuName = "PanelSprites")]
public class PanelSprites : ScriptableObject {
    public Sprite CornerSprite { get => cornerSprite; }
    public Sprite EdgeSprite { get => edgeSprite; }
    public Sprite CentreSprite { get => centreSprite; }
    [SerializeField] private Sprite cornerSprite;
    [SerializeField] private Sprite edgeSprite;
    [SerializeField] private Sprite centreSprite;
}
