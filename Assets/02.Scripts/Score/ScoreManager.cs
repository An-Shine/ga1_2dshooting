using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // 싱글톤 패턴
    // 1. 전역적으로 접근 가능하다
    // 2. 인스턴스(생성된 객체)가 하나임을 보장한다
    public static ScoreManager _instance;
    public static ScoreManager Instance => _instance;

    // 관리 : 특정 데이터에 대한 무결성과 생성, 일기, 수정, 삭제 등 관련된 게임로직 
    private int _bestScore;
    private int _currentScore = 0;
    private const string SaveKey = "BestScore";

    // UI 책임 추가 (텍스트메시프로참조)
    [SerializeField] private TextMeshProUGUI _bestScoreText;
    [SerializeField] private TextMeshProUGUI _currentScoreText;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    private void Start()
    {
        // 입력 : Input
        // 저장, 불러오기 : PlayerPrefs
        if (PlayerPrefs.HasKey(SaveKey))
        {
            _bestScore = PlayerPrefs.GetInt(SaveKey);
        }

        Refresh();
    }

    public void AddScore(int score)
    {
        if (score <= 0) return;

        _currentScore += score;
        if (_currentScore > _bestScore)
        {
            _bestScore = _currentScore;

            // 저장 : Set 시리즈를 이용해서 int/float/string 저장 가능
            // 내 컴퓨터 어딘가에 저장이된다
            PlayerPrefs.SetInt(SaveKey, _bestScore);
            PlayerPrefs.Save();
        }

        Refresh();
    }

    private void Refresh()
    {
        _bestScoreText.text = $"BestScore :  {_bestScore}";
        _currentScoreText.text = $"Score :  {_currentScore}";
    }
}