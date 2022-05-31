using Core;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsPanel : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText = null;

    [SerializeField]
    private Text _fundsText = null;

    [SerializeField]
    private Text _coreHealthText = null;

    [SerializeField]
    private GameManager _gameManager = null;

    [SerializeField]
    private PlayerCore _playerCore = null;

    private void Update()
    {
        /*if (!_gameManager.IsInGame)
        {
            return;
        }
        _scoreText.text = _gameManager.PlayerState.Score.ToString();
        _fundsText.text = _gameManager.PlayerState.Funds.ToString();
        _coreHealthText.text = _playerCore.Hp.ToString();*/
    }
}
