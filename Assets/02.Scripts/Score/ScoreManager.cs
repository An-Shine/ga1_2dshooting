using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // 싱글톤 패턴
    // 1. 전역적으로 접근 가능하다
    // 2. 인스턴스(생성된 객체)가 하나임을 보장한다
    public static ScoreManager Instance;

    // 관리 : 특정 데이터에 대한 무결성과 생성, 일기, 수정, 삭제 등 관련된 게임로직 
    private int _bestScore;
    private int _currentScore;

    // UI 책임 추가 (텍스트메시프로참조)
    [SerializeField] private TextMeshProUGUI _bestScoreText;
    [SerializeField] private TextMeshProUGUI _currentScoreText;

    private void Awake()
    {
        Instance = this;
    }

    public void AddScore(int score)
    {
        _currentScore += score;
        if (_currentScore > _bestScore)
        {
            _bestScore = _currentScore;
        }
    }

    private void Update()
    {
        Refresh();
    }

    private void Refresh()
    {
        _bestScoreText.text = $"BestScore :  {_bestScore}";
        _currentScoreText.text = $"CurrentScore :  {_currentScore}";
    }
}