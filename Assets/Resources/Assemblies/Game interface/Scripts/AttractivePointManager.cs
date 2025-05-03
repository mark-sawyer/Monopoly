using UnityEngine;

public class AttractivePointManager {
    #region offsets
    private Vector2[][] otherOffsets = {
        new Vector2[1] { new Vector2(3.7f, -6f) },
        new Vector2[2] { new Vector2(3.7f, -3.7f), new Vector2(3.7f, -8.3f) },
        new Vector2[3] {
            new Vector2(3.7f, -2.7f),
            new Vector2(3.7f, -6f),
            new Vector2(3.7f, -9.3f)
        },
        new Vector2[4] {
            new Vector2(3.7f, -2.1f),
            new Vector2(3.7f, -4.7f),
            new Vector2(3.7f, -7.3f),
            new Vector2(3.7f, -9.9f)
        },
        new Vector2[5] {
            new Vector2(2.2f, -2.2f),
            new Vector2(5.2f, -4.1f),
            new Vector2(2.2f, -6.0f),
            new Vector2(5.2f, -7.9f),
            new Vector2(2.2f, -9.8f)
        },
        new Vector2[6] {
            new Vector2(2.2f, -2.3f),
            new Vector2(5.2f, -2.3f),
            new Vector2(2.2f, -6.0f),
            new Vector2(5.2f, -6.0f),
            new Vector2(2.2f, -9.7f),
            new Vector2(5.2f, -9.7f)
        },
        new Vector2[7] {
            new Vector2(2.3f, -1.8f),
            new Vector2(5.1f, -3.2f),
            new Vector2(2.3f, -4.6f),
            new Vector2(5.1f, -6.0f),
            new Vector2(2.3f, -7.4f),
            new Vector2(5.1f, -8.8f),
            new Vector2(2.3f, -10.2f)
        },
        new Vector2[8] {
            new Vector2(2.3f, -1.94f),
            new Vector2(5.1f, -1.94f),
            new Vector2(2.3f, -4.67f),
            new Vector2(5.1f, -4.67f),
            new Vector2(2.3f, -7.33f),
            new Vector2(5.1f, -7.33f),
            new Vector2(2.3f, -10.00f),
            new Vector2(5.1f, -10.00f)
        }
    };
    private Vector2[][] cornerOffsets = {
        new Vector2[1] { new Vector2(6f, -6f) },
        new Vector2[2] { new Vector2(3.9f, -3.9f), new Vector2(8.1f, -8.1f) },
        new Vector2[3] {
            new Vector2(6.00f, -3.40f),
            new Vector2(8.25f, -7.30f),
            new Vector2(3.75f, -7.30f)
        },
        new Vector2[4] {
            new Vector2(3.5f, -3.5f),
            new Vector2(8.5f, -3.5f),
            new Vector2(3.5f, -8.5f),
            new Vector2(8.5f, -8.5f)
        },
        new Vector2[5] {
            new Vector2(2.48f, -4.86f),
            new Vector2(6.00f, -2.30f),
            new Vector2(9.52f, -4.86f),
            new Vector2(3.83f, -8.99f),
            new Vector2(8.17f, -8.99f)
        },
        new Vector2[6] {
            new Vector2(3.5f, -2.5f),
            new Vector2(8.5f, -2.5f),
            new Vector2(3.5f, -6.0f),
            new Vector2(8.5f, -6.0f),
            new Vector2(3.5f, -9.5f),
            new Vector2(8.5f, -9.5f)
        },
        new Vector2[7] {
            new Vector2(2.7f, -4.1f),
            new Vector2(6.0f, -2.2f),
            new Vector2(9.3f, -4.1f),
            new Vector2(6.0f, -6.0f),
            new Vector2(2.7f, -7.9f),
            new Vector2(6.0f, -9.8f),
            new Vector2(9.3f, -7.9f)
        },
        new Vector2[8] {
            new Vector2(2.5f, -2.5f),
            new Vector2(6.0f, -3.5f),
            new Vector2(9.5f, -2.5f),
            new Vector2(3.5f, -6.0f),
            new Vector2(8.5f, -6.0f),
            new Vector2(2.5f, -9.5f),
            new Vector2(6.0f, -8.5f),
            new Vector2(9.5f, -9.5f)
        }
    };
    private Vector2[][] visitingOffsets = {
        new Vector2[1] { new Vector2(10.23f, -10.23f) },
        new Vector2[2] { new Vector2(4.38f, -10.23f), new Vector2(10.23f, -4.38f) },
        new Vector2[3] {
            new Vector2(4f, -10.23f),
            new Vector2(10.23f, -10.23f),
            new Vector2(10.23f, -4f)
        },
        new Vector2[4] {
            new Vector2(3f, -10.23f),
            new Vector2(7.2f, -10.23f),
            new Vector2(10.23f, -7.2f),
            new Vector2(10.23f, -3f)
        },
        new Vector2[5] {
            new Vector2(2.5f, -10.23f),
            new Vector2(6.365f, -10.23f),
            new Vector2(10.23f, -10.23f),
            new Vector2(10.23f, -6.365f),
            new Vector2(10.23f, -2.5f)
        },
        new Vector2[6] {
            new Vector2(2.3f, -10.23f),
            new Vector2(5.3f, -10.23f),
            new Vector2(8.3f, -10.23f),
            new Vector2(10.23f, -8.3f),
            new Vector2(10.23f, -5.3f),
            new Vector2(10.23f, -2.3f)
        },
        new Vector2[7] {
            new Vector2(1.77f, -10.23f),
            new Vector2(4.59f, -10.23f),
            new Vector2(7.41f, -10.23f),
            new Vector2(10.23f, -10.23f),
            new Vector2(10.23f, -7.41f),
            new Vector2(10.23f, -4.59f),
            new Vector2(10.23f, -1.77f)
        },
        new Vector2[8] {
            new Vector2(1.7f, -10.23f),
            new Vector2(4.07f, -10.23f),
            new Vector2(6.43f, -10.23f),
            new Vector2(8.8f, -10.23f),
            new Vector2(10.23f, -8.8f),
            new Vector2(10.23f, -6.43f),
            new Vector2(10.23f, -4.07f),
            new Vector2(10.23f, -1.7f)
        }
    };
    #endregion



    public AttractivePoint getAttractivePoint(Transform spaceTransform, SpaceType spaceType, int tokensOnSpace, int tokenOrder) {
        Vector2 offset = new Vector2();
        switch (spaceType) {
            case SpaceType.ESTATE: offset = new Vector2(1f, 1.2125f) * otherOffsets[tokensOnSpace - 1][tokenOrder - 1]; break;
            case SpaceType.OTHER: offset = otherOffsets[tokensOnSpace - 1][tokenOrder - 1]; break;
            case SpaceType.CORNER: offset = cornerOffsets[tokensOnSpace - 1][tokenOrder - 1]; break;
            case SpaceType.VISITING: offset = visitingOffsets[tokensOnSpace - 1][tokenOrder - 1]; break;
            case SpaceType.JAIL: offset = 0.73f * cornerOffsets[tokensOnSpace - 1][tokenOrder - 1]; break;
        }
        Vector2 worldPosition = spaceTransform.TransformPoint(offset);
        return new AttractivePoint(
            worldPosition.x,
            worldPosition.y
        );
    }
}
