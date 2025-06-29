using TMPro;
using UnityEngine;

public class PropertyGroupSelect : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private ScriptableObject[] propertyGroups;
    public int PropertyGroupNumber { get; private set; }



    private void Start() {
        PropertyGroupNumber = 1;
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            if (PropertyGroupNumber < 10) {
                PropertyGroupNumber++;
                textMesh.text = PropertyGroupNumber.ToString();
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            if (PropertyGroupNumber > 1) {
                PropertyGroupNumber--;
                textMesh.text = PropertyGroupNumber.ToString();
            }
        }
    }


    public EstateGroupInfo SelectedEstateGroup => (EstateGroupInfo)propertyGroups[PropertyGroupNumber - 1];
    public RailroadInfo getRailroadInfo(int index) {
        RailroadGroupInfo railroadGroupInfo = (RailroadGroupInfo)propertyGroups[8];
        return railroadGroupInfo.getRailroadInfo(index);
    }
    public UtilityInfo getUtilityInfo(int index) {
        UtilityGroupInfo utilityGroupInfo = (UtilityGroupInfo)propertyGroups[9];
        return utilityGroupInfo.getUtilityInfo(index);
    }
}
