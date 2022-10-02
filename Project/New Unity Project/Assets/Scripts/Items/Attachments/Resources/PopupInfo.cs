using UnityEngine;

[CreateAssetMenu(fileName = "PopupInfo", menuName = "Gameplay/Items/New PopupInfo")]
public class PopupInfo : ScriptableObject
{
    [SerializeField]
    private float _traceAccuracy;

    public float traceAccuracy => this._traceAccuracy;
}