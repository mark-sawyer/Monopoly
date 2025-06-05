using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New LuxuryTaxSpace", menuName = "Space/LuxuryTaxSpace")]
internal class LuxuryTaxSpace : Space, LuxuryTaxSpaceInfo { }

public interface LuxuryTaxSpaceInfo : SpaceInfo { }
