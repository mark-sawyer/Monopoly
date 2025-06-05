using UnityEngine;

[CreateAssetMenu(fileName = "New GoToJailSpace", menuName = "Space/GoToJailSpace")]
internal class GoToJailSpace : Space, GoToJailSpaceInfo { }

public interface GoToJailSpaceInfo : SpaceInfo { }
