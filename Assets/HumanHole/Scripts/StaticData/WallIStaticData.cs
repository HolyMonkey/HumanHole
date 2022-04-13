using UnityEngine;

[CreateAssetMenu(fileName = "WalIStaticData", menuName = "ScriptableObjects/WallStaticData", order = 1)]
public class WallIStaticData : ScriptableObject
{
    public int Index;
    public Wall Wall;
    public Sprite HoleCounterSprite;
}