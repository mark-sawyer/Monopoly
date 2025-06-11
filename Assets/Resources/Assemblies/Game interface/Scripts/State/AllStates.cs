using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "AllStates")]
public class AllStates : ScriptableObject {
    [SerializeField] private State[] states;
    public State getState<T>() {
        return states.First(x => x is T);
    }
}
