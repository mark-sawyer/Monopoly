using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SquareOffsets", menuName = "SquareOffsets")]
public class SquareOffsets : BaseTokenOffsets {
    #region properties
    public override Vector2 MajorOffset => majorOffset;
    protected override Vector2[] MinorOffsetOne {
        get => new Vector2[1] { new Vector2(0f, 0f) };
    }
    protected override Vector2[] MinorOffsetTwo {
        get {
            Vector2[] x = new Vector2[2] {
                new Vector2(-0.333f, 0.333f),
                new Vector2(0.333f, -0.333f)
            };
            return scale(x);
        }
    }
    protected override Vector2[] MinorOffsetThree {
        get {
            float mag = 0.6f;
            float points = 3f;
            Vector2[] x = new Vector2[3] {
                angleCoords(0f, points, mag),
                angleCoords(1f, points, mag),
                angleCoords(2f, points, mag)
            };
            Vector2[] scaled = scale(x);
            return translate(scaled, 0f, -0.8f);
        }
    }
    protected override Vector2[] MinorOffsetFour {
        get {
            Vector2[] x = new Vector2[4] {
                new Vector2(-0.5f, 0.5f),
                new Vector2(0.5f, 0.5f),
                new Vector2(-0.5f, -0.5f),
                new Vector2(0.5f, -0.5f)
            };
            return scale(x);
        }
    }
    protected override Vector2[] MinorOffsetFive {
        get {
            float mag = 0.65f;
            float points = 5f;
            Vector2[] x = new Vector2[5] {
                angleCoords(0f, points, mag),
                angleCoords(1f, points, mag),
                angleCoords(2f, points, mag),
                angleCoords(3f, points, mag),
                angleCoords(4f, points, mag)
            };
            Vector2[] scaled = scale(x);
            return translate(scaled, 0f, -0.47f);
        }
    }
    protected override Vector2[] MinorOffsetSix {
        get {
            float mag = 0.75f;
            float points = 6f;
            Vector2[] x = new Vector2[6] {
                angleCoords(5f, points, mag),
                angleCoords(0f, points, mag),
                angleCoords(1f, points, mag),
                angleCoords(4f, points, mag),
                new Vector2(0f, 0f),
                angleCoords(2f, points, mag)
            };
            Vector2[] scaled = scale(x, scalar, scalar * 1.3f);
            return translate(x, 0f, -1.35f);
        }
    }
    protected override Vector2[] MinorOffsetSeven {
        get {
            float mag = 0.75f;
            float points = 6f;
            Vector2[] x = new Vector2[7] {
                angleCoords(5f, points, mag),
                angleCoords(0f, points, mag),
                angleCoords(1f, points, mag),
                new Vector2(0f, 0f),
                angleCoords(4f, points, mag),
                angleCoords(3f, points, mag),
                angleCoords(2f, points, mag),
            };
            return scale(x);
        }
    }
    protected override Vector2[] MinorOffsetEight {
        get {
            float outer = 0.667f;
            float inner = 0.47f;
            Vector2[] x = new Vector2[8] {
                new Vector2(-outer, outer),
                new Vector2(0, inner),
                new Vector2(outer, outer),
                new Vector2(-inner, 0),
                new Vector2(inner, 0),
                new Vector2(-outer, -outer),
                new Vector2(0, -inner),
                new Vector2(outer, -outer)
            };
            return scale(x);
        }
    }
    #endregion properties
    [SerializeField] private Vector2 majorOffset;
    [SerializeField] private float scalar;



    #region private
    private Vector2[] scale(Vector2[] v, float xScalar, float yScalar) {
        for (int i = 0; i < v.Length; i++) {
            v[i] = new Vector2(v[i].x * xScalar, v[i].y * yScalar);
        }
        return v;
    }
    private Vector2[] scale(Vector2[] v) {
        for (int i = 0; i < v.Length; i++) {
            v[i] *= scalar;
        }
        return v;
    }
    private Vector2[] translate(Vector2[] v, float xAdj, float yAdj) {
        Vector2 translation = new Vector2(xAdj, yAdj);
        for (int i = 0; i < v.Length; i++) {
            v[i] += translation;
        }
        return v;
    }
    private Vector2 angleCoords(float i, float points, float magnitude) {
        return magnitude * new Vector2(Mathf.Sin((i / points) * 2 * Mathf.PI), Mathf.Cos((-i / points) * 2 * Mathf.PI));
    }
    #endregion
}
