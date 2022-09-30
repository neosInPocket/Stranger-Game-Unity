using UnityEngine;

[CreateAssetMenu(fileName = "LaserSightInfo", menuName = "Gameplay/Items/New LaserSightInfo")]
public class LaserSightInfo : ScriptableObject
{
    [SerializeField]
    private Color _laserColor;

    public Color laserColor => this._laserColor;
}