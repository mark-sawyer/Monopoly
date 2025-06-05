using UnityEngine;

[CreateAssetMenu(fileName = "New IncomeTaxSpace", menuName = "Space/IncomeTaxSpace")]
internal class IncomeTaxSpace : Space, IncomeTaxSpaceInfo { }

public interface IncomeTaxSpaceInfo : SpaceInfo { }
