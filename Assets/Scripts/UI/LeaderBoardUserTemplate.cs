using TMPro;
using UnityEngine;

public class LeaderBoardUserTemplate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _publicName;
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _rank;

    public void Initial(string name, string score, string rank)
    {
        _publicName.text = name;
        _score.text = score;
        _rank.text = rank;
    }
}
