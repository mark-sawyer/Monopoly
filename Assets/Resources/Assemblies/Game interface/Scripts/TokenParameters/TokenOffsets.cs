using UnityEngine;

[CreateAssetMenu(fileName = "New TokenOffsets", menuName = "TokenOffsets")]
public class TokenOffsets : BaseTokenOffsets {
    #region properties
    public override Vector2 MajorOffset => majorOffset;
    protected override Vector2[] MinorOffsetOne => minorOffsetOne;
    protected override Vector2[] MinorOffsetTwo => minorOffsetTwo;
    protected override Vector2[] MinorOffsetThree => minorOffsetThree;
    protected override Vector2[] MinorOffsetFour => minorOffsetFour;
    protected override Vector2[] MinorOffsetFive => minorOffsetFive;
    protected override Vector2[] MinorOffsetSix => minorOffsetSix;
    protected override Vector2[] MinorOffsetSeven => minorOffsetSeven;
    protected override Vector2[] MinorOffsetEight => minorOffsetEight;
    #endregion
    [SerializeField] private Vector2 majorOffset;
    [SerializeField] private Vector2[] minorOffsetOne;
    [SerializeField] private Vector2[] minorOffsetTwo;
    [SerializeField] private Vector2[] minorOffsetThree;
    [SerializeField] private Vector2[] minorOffsetFour;
    [SerializeField] private Vector2[] minorOffsetFive;
    [SerializeField] private Vector2[] minorOffsetSix;
    [SerializeField] private Vector2[] minorOffsetSeven;
    [SerializeField] private Vector2[] minorOffsetEight;
}