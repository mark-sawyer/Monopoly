using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EstateGroupDictionary")]
public class EstateGroupDictionary : ScriptableObject {
    [SerializeField] private EstateGroupColours[] estateGroupColours;
    [SerializeField] private ScriptableObject[] estateGroupInfos;
    private Dictionary<EstateColour, EstateGroupColours> colourDictionary;
    private Dictionary<EstateColour, EstateGroupInfo> infoDictionary;
    private static EstateGroupDictionary instance;



    #region public
    public static EstateGroupDictionary Instance {
        get {
            if (instance == null) {
                instance = Resources.Load<EstateGroupDictionary>(
                    "ScriptableObjects/Properties/Property groups/estate_group_dictionary"
                );
            }
            return instance;
        }
    }
    public EstateGroupColours lookupColour(EstateColour estateColour) {
        if (colourDictionary == null) colourDictionary = initialiseColourDict();
        return colourDictionary[estateColour];
    }
    public EstateGroupInfo lookupInfo(EstateColour estateColour) {
        if (infoDictionary == null) infoDictionary = initialiseInfoDict();
        return infoDictionary[estateColour];
    }
    #endregion



    #region initialise
    private Dictionary<EstateColour, EstateGroupColours> initialiseColourDict() {
        Dictionary<EstateColour, EstateGroupColours> newDict = new();
        newDict[EstateColour.BROWN] = estateGroupColours[0];
        newDict[EstateColour.LIGHT_BLUE] = estateGroupColours[1];
        newDict[EstateColour.PINK] = estateGroupColours[2];
        newDict[EstateColour.ORANGE] = estateGroupColours[3];
        newDict[EstateColour.RED] = estateGroupColours[4];
        newDict[EstateColour.YELLOW] = estateGroupColours[5];
        newDict[EstateColour.GREEN] = estateGroupColours[6];
        newDict[EstateColour.DARK_BLUE] = estateGroupColours[7];
        return newDict;
    }
    private Dictionary<EstateColour, EstateGroupInfo> initialiseInfoDict() {
        Dictionary<EstateColour, EstateGroupInfo> newDict = new();
        newDict[EstateColour.BROWN] = (EstateGroupInfo)estateGroupInfos[0];
        newDict[EstateColour.LIGHT_BLUE] = (EstateGroupInfo)estateGroupInfos[1];
        newDict[EstateColour.PINK] = (EstateGroupInfo)estateGroupInfos[2];
        newDict[EstateColour.ORANGE] = (EstateGroupInfo)estateGroupInfos[3];
        newDict[EstateColour.RED] = (EstateGroupInfo)estateGroupInfos[4];
        newDict[EstateColour.YELLOW] = (EstateGroupInfo)estateGroupInfos[5];
        newDict[EstateColour.GREEN] = (EstateGroupInfo)estateGroupInfos[6];
        newDict[EstateColour.DARK_BLUE] = (EstateGroupInfo)estateGroupInfos[7];
        return newDict;
    }
    #endregion
}
