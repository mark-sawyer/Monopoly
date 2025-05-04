using System;
using System.Collections.Generic;
using UnityEngine;

public class AttractivePointManager {
    #region offsets
    private static Vector2[][] otherOffsets = {
        new Vector2[1] {
            new Vector2(0f, 0f)
        },
        new Vector2[2] {
            new Vector2(0f, 2.3f),
            new Vector2(0f, -2.3f)
        },
        new Vector2[3] {
            new Vector2(0f, 3.3f),
            new Vector2(0f, 0f),
            new Vector2(0f, -3.3f)
        },
        new Vector2[4] {
            new Vector2(0f, 3.9f),
            new Vector2(0f, 1.3f),
            new Vector2(0f, -1.3f),
            new Vector2(0f, -3.9f)
        },
        new Vector2[5] {
            new Vector2(-1.5f, 3.8f),
            new Vector2(1.5f, 1.9f),
            new Vector2(-1.5f, 0f),
            new Vector2(1.5f, -1.9f),
            new Vector2(-1.5f, -3.8f)
        },
        new Vector2[6] {
            new Vector2(-1.5f, 3.7f),
            new Vector2(1.5f, 3.7f),
            new Vector2(-1.5f, 0f),
            new Vector2(1.5f, 0f),
            new Vector2(-1.5f, -3.7f),
            new Vector2(1.5f, -3.7f)
        },
        new Vector2[7] {
            new Vector2(-1.4f, 4.2f),
            new Vector2(1.4f, 2.8f),
            new Vector2(-1.4f, 1.4f),
            new Vector2(1.4f, 0f),
            new Vector2(-1.4f, -1.4f),
            new Vector2(1.4f, -2.8f),
            new Vector2(-1.4f, -4.2f)
        },
        new Vector2[8] {
            new Vector2(-1.4f, 4.06f),
            new Vector2(1.4f, 4.06f),
            new Vector2(-1.4f, 1.33f),
            new Vector2(1.4f, 1.33f),
            new Vector2(-1.4f, -1.33f),
            new Vector2(1.4f, -1.33f),
            new Vector2(-1.4f, -4f),
            new Vector2(1.4f, -4f)
        }
    };
    private static Vector2[][] cornerOffsets = {
        new Vector2[1] {
            new Vector2(0f, 0f)
        },
        new Vector2[2] {
            new Vector2(-2.1f, 2.1f),
            new Vector2(2.1f, -2.1f)
        },
        new Vector2[3] {
            new Vector2(0f, 2.6f),
            new Vector2(2.25f, -1.3f),
            new Vector2(-2.25f, -1.3f)
        },
        new Vector2[4] {
            new Vector2(-2.5f, 2.5f),
            new Vector2(2.5f, 2.5f),
            new Vector2(-2.5f, -2.5f),
            new Vector2(2.5f, -2.5f)
        },
        new Vector2[5] {
            new Vector2(-3.52f, 1.14f),
            new Vector2(0f, 3.7f),
            new Vector2(3.52f, 1.14f),
            new Vector2(-2.17f, -2.99f),
            new Vector2(2.17f, -2.99f)
        },
        new Vector2[6] {
            new Vector2(-2.5f, 3.5f),
            new Vector2(2.5f, 3.5f),
            new Vector2(-2.5f, 0f),
            new Vector2(2.5f, 0f),
            new Vector2(-2.5f, -3.5f),
            new Vector2(2.5f, -3.5f)
        },
        new Vector2[7] {
            new Vector2(-3.3f, 1.9f),
            new Vector2(0f, 3.8f),
            new Vector2(3.3f, 1.9f),
            new Vector2(0f, 0f),
            new Vector2(-3.3f, -1.9f),
            new Vector2(0f, -3.8f),
            new Vector2(3.3f, -1.9f)
        },
        new Vector2[8] {
            new Vector2(-3.5f, 3.5f),
            new Vector2(0f, 0f),
            new Vector2(3.5f, -3.5f),
            new Vector2(-2.5f, 2.5f),
            new Vector2(2.5f, -2.5f),
            new Vector2(-3.5f, 3.5f),
            new Vector2(0f, 0f),
            new Vector2(3.5f, -3.5f)
        }
    };
    private static Vector2[][] visitingOffsets = {
        new Vector2[1] {
            new Vector2(4.23f, -4.23f)
        },
        new Vector2[2] {
            new Vector2(-1.62f, -4.23f),
            new Vector2(4.23f, 1.62f)
        },
        new Vector2[3] {
            new Vector2(-2f, -4.23f),
            new Vector2(4.23f, -4.23f),
            new Vector2(4.23f, 2f)
        },
        new Vector2[4] {
            new Vector2(-3f, -4.23f),
            new Vector2(1.2f, -4.23f),
            new Vector2(4.23f, -1.2f),
            new Vector2(4.23f, 3f)
        },
        new Vector2[5] {
            new Vector2(-3.5f, -4.23f),
            new Vector2(0.365f, -4.23f),
            new Vector2(4.23f, -4.23f),
            new Vector2(4.23f, -0.365f),
            new Vector2(4.23f, 3.5f)
        },
        new Vector2[6] {
            new Vector2(-3.7f, -4.23f),
            new Vector2(-0.7f, -4.23f),
            new Vector2(2.3f, -4.23f),
            new Vector2(4.23f, -2.3f),
            new Vector2(4.23f, 0.7f),
            new Vector2(4.23f, 3.7f)
        },
        new Vector2[7] {
            new Vector2(-4.23f, -4.23f),
            new Vector2(-1.41f, -4.23f),
            new Vector2(1.41f, -4.23f),
            new Vector2(4.23f, -4.23f),
            new Vector2(4.23f, -1.41f),
            new Vector2(4.23f, 1.41f),
            new Vector2(4.23f, 4.23f)
        },
        new Vector2[8] {
            new Vector2(-4.3f, -4.23f),
            new Vector2(-1.93f, -4.23f),
            new Vector2(0.43f, -4.23f),
            new Vector2(2.8f, -4.23f),
            new Vector2(4.23f, -2.8f),
            new Vector2(4.23f, -0.43f),
            new Vector2(4.23f, 1.93f),
            new Vector2(4.23f, 4.3f)
        }
    };
    #endregion
    private SpaceVisualManager spaceVisualManager;
    private Transform tokenTransform;
    private List<Vector3> attractivePoints;
    private Vector3 velocity;
    private Action updateAction;



    #region static
    public static Vector3 getAttractivePoint(SpaceVisual spaceVisual, int tokensOnSpace, int tokenOrder) {
        //Vector3 majorPoint = spaceVisual.getMajorTargetOffset();
        //SpaceType spaceType = spaceVisual.getSpaceType();
        //Vector2 offset = new Vector2();
        //switch (spaceType) {
        //    case SpaceType.ESTATE: offset = new Vector2(1f, 1.2125f) * otherOffsets[tokensOnSpace - 1][tokenOrder]; break;
        //    case SpaceType.OTHER: offset = otherOffsets[tokensOnSpace - 1][tokenOrder]; break;
        //    case SpaceType.CORNER: offset = cornerOffsets[tokensOnSpace - 1][tokenOrder]; break;
        //    case SpaceType.VISITING: offset = visitingOffsets[tokensOnSpace - 1][tokenOrder]; break;
        //    case SpaceType.JAIL: offset = 0.73f * cornerOffsets[tokensOnSpace - 1][tokenOrder]; break;
        //}
        //Vector3 minorPoint = majorPoint + new Vector3(offset.x, offset.y, 0f);
        return spaceVisual.transformPoint(new Vector3());
    }
    #endregion



    #region public
    public AttractivePointManager(Transform tokenTransform, SpaceVisualManager spaceVisualManager) {
        this.tokenTransform = tokenTransform;
        this.spaceVisualManager = spaceVisualManager;
        attractivePoints = new List<Vector3>();
        updateAction = () => { };
    }
    public void update() {
        updateAction();
    }
    public void updateAttractivePoints(int startIndex, int roll) {
        List<int> spaceIndices = getSpaceIndices(startIndex, roll);
        
    }
    #endregion



    #region private
    private void moveToken() {
        Vector3 direction = attractivePoints[0] - tokenTransform.position;
        Vector3 acceleration = (direction - InterfaceConstants.TOKEN_VELOCITY_CONSTANT * velocity) * InterfaceConstants.TOKEN_ACCELERATION_CONSTANT;
        velocity = velocity + acceleration;
        tokenTransform.position = (tokenTransform.position + velocity * Time.deltaTime);
    }
    private List<int> getSpaceIndices(int currentIndex, int remaining) {
        List<int> indices = new List<int>();
        void updateValues(int added) {
            indices.Add(currentIndex + added);
            currentIndex += added;
            remaining -= added;
        }

        int toCorner = GameConstants.SPACES_ON_EDGE - (currentIndex % GameConstants.SPACES_ON_EDGE);
        if (toCorner <= remaining) updateValues(toCorner);
        while (remaining >= GameConstants.SPACES_ON_EDGE) updateValues(GameConstants.SPACES_ON_EDGE);
        if (remaining > 0) updateValues(remaining);
        return indices;
    }
    #endregion
}
