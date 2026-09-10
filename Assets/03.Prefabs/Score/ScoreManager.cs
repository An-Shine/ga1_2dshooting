using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // 관리 : 특정 데이터에 대한 무결성과 생성, 일기, 수정, 삭제 등 관련된 게임로직 
    private int _bestScore;
    private int _currentScore;

    // UI 책임 추가 (텍스트메시프로참조)
    [SerializeField] private TextMeshProUGUI _bestScoreText;
    [SerializeField] private TextMeshProUGUI _currentScoreText;

    private void Update()
    {
        _bestScoreText.text = $"BestScore :  {_bestScore}";
        _currentScoreText.text = $"CurrentScore :  {_currentScore}";
    }
}