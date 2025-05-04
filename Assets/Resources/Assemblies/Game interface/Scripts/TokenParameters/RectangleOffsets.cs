using UnityEngine;

[CreateAssetMenu(fileName = "New RectangleOffsets", menuName = "RectangleOffsets")]
public class RectangleOffsets : BaseTokenOffsets {
    #region properties
    protected override Vector2 MajorOffset => majorOffset;
    protected override Vector2[] MinorOffsetOne {
        get => new Vector2[1] { new Vector2(0f, 0f) };
    }
    protected override Vector2[] MinorOffsetTwo {
        get {
            Vector2[] x = new Vector2[2] {
                new Vector2(0f, 0.5f),
                new Vector2(0f, -0.5f)
            };
            return scale(x);
        }
    }
    protected override Vector2[] MinorOffsetThree {
        get {
            Vector2[] x = new Vector2[3] {
                new Vector2(0f, 0.667f),
                new Vector2(0f, 0f),
                new Vector2(0f, -0.667f)
            };
            return scale(x);
        }
    }
    protected override Vector2[] MinorOffsetFour {
        get {
            Vector2[] x = new Vector2[4] {
                new Vector2(-0.5f, 0.667f),
                new Vector2(0.5f, 0.222f),
                new Vector2(-0.5f, -0.222f),
                new Vector2(0.5f, -0.667f)
            };
            return scale(x);
        }
    }
    protected override Vector2[] MinorOffsetFive {
        get {
            Vector2[] x = new Vector2[5] {
                new Vector2(-0.5f, 0.667f),
                new Vector2(0.5f,  0.333f),
                new Vector2(-0.5f, 0f),
                new Vector2(0.5f,  -0.333f),
                new Vector2(-0.5f, -0.667f)
            };
            return scale(x);
        }
    }
    protected override Vector2[] MinorOffsetSix {
        get {
            Vector2[] x = new Vector2[6] {
                new Vector2(-0.5f, 0.667f),
                new Vector2(0.5f, 0.667f),
                new Vector2(-0.5f, 0f),
                new Vector2(0.5f, 0f),
                new Vector2(-0.5f, -0.667f),
                new Vector2(0.5f, -0.667f)
            };
            return scale(x);
        }
    }
    protected override Vector2[] MinorOffsetSeven {
        get {
            Vector2[] x = new Vector2[7] {
                new Vector2(-0.5f, 0.75f),
                new Vector2(0.5f,  0.5f),
                new Vector2(-0.5f, 0.25f),
                new Vector2(0.5f,  0f),
                new Vector2(-0.5f, -0.25f),
                new Vector2(0.5f,  -0.5f),
                new Vector2(-0.5f, -0.75f)
            };
            return scale(x);
        }
    }
    protected override Vector2[] MinorOffsetEight {
        get {
            Vector2[] x = new Vector2[8] {
                new Vector2(-0.5f, 0.75f),
                new Vector2(0.5f, 0.75f),
                new Vector2(-0.5f, 0.25f),
                new Vector2(0.5f, 0.25f),
                new Vector2(-0.5f, -0.25f),
                new Vector2(0.5f, -0.25f),
                new Vector2(-0.5f, -0.75f),
                new Vector2(0.5f, -0.75f)
            };
            return scale(x);
        }
    }
    #endregion properties
    [SerializeField] private Vector2 majorOffset;
    [SerializeField] private float xScalar;
    [SerializeField] private float yScalar;



    private Vector2[] scale(Vector2[] v) {
        for (int i = 0; i < v.Length; i++) {
            v[i] = new Vector2(v[i].x * xScalar, v[i].y * yScalar);
        }
        return v;
    }
}
