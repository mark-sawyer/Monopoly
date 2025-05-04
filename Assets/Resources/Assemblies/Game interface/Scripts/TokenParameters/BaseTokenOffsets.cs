using UnityEngine;

public abstract class BaseTokenOffsets : ScriptableObject {
    protected abstract Vector2 MajorOffset { get; }
    protected abstract Vector2[] MinorOffsetOne { get; }
    protected abstract Vector2[] MinorOffsetTwo  { get; }
    protected abstract Vector2[] MinorOffsetThree  { get; }
    protected abstract Vector2[] MinorOffsetFour  { get; }
    protected abstract Vector2[] MinorOffsetFive  { get; }
    protected abstract Vector2[] MinorOffsetSix  { get; }
    protected abstract Vector2[] MinorOffsetSeven  { get; }
    protected abstract Vector2[] MinorOffsetEight  { get; }



    public Vector2 getTotalOffset(int playersOnSpace, int order) {
        return MajorOffset + getMinorOffsets(playersOnSpace)[order];
    }
    private Vector2[] getMinorOffsets(int playersOnSpace) {
        switch (playersOnSpace) {
            case 1: return MinorOffsetOne;
            case 2: return MinorOffsetTwo;
            case 3: return MinorOffsetThree;
            case 4: return MinorOffsetFour;
            case 5: return MinorOffsetFive;
            case 6: return MinorOffsetSix;
            case 7: return MinorOffsetSeven;
            default: return MinorOffsetEight;
        }
    }
}
