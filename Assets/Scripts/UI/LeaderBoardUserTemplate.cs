using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardUserTemplate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _publicName;
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _rank;
    [SerializeField] private Image _rankImage;
    [SerializeField] private Image _avatarImage;
    [SerializeField] private Sprite[] _avatarsSprites;
    [SerializeField] private Sprite[] _rankSprites;
    [SerializeField] private Image _borderImage;
    [SerializeField] private Image _backGroundImage;
    [SerializeField] private Color _ownBorderColor;
    [SerializeField] private Color _ownBackgroundColor;
    [SerializeField] private Color _ownTextColor;

    public void Initial(string name, string score, int rank, Sprite avatar, bool ownPlayer)
    {
        _publicName.text = name;
        _score.text = score;

        if (ownPlayer)
        {
            _backGroundImage.color = _ownBackgroundColor;
            _score.color = _ownTextColor;
            _publicName.color = _ownTextColor;
            _borderImage.color = _ownBorderColor;
        }


        SetRank(rank, ownPlayer);
        SetAvatar(avatar);
    }

    private void SetRank(int rank, bool ownPlayer)
    {
        if (rank < 4)
        {
            _rankImage.sprite = _rankSprites[rank - 1];
            _rankImage.gameObject.SetActive(true);
        }
        else
        {
            if (ownPlayer)
                _rank.color = _ownTextColor;
            
            _rank.text = rank.ToString();
            _rank.gameObject.SetActive(true);
        }
    }

    private void SetAvatar(Sprite avatar)
    {
        if (avatar == null)
        {
            int value = Random.Range(0, _avatarsSprites.Length);
            _avatarImage.sprite = _avatarsSprites[value];
        }
        else
        {
            _avatarImage.sprite = avatar;
        }
    }
}