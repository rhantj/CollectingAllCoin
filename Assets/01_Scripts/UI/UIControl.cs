using TMPro;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    [Header("Time")]
    [SerializeField] TextMeshProUGUI timeText;

    [Header("EndUI")]
    [SerializeField] GameObject endUI;
    EndPanelControl endPanel;

    [Header("Coin Count")]
    [SerializeField] TextMeshProUGUI coinCountText;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;

    private void Awake()
    {
        endPanel = endUI.GetComponent<EndPanelControl>();
    }

    private void Start()
    {
        GameManager.Instance.OnTimeChanged += UpdateTimeText;
        GameManager.Instance.ToggleEndUI += ToggleEndUI;
        GameManager.Instance.OnCoinDecrease += UpdateCoinText;
        GameManager.Instance.OnScoreChanged += UpdateScoreText;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnTimeChanged -= UpdateTimeText;
        GameManager.Instance.ToggleEndUI -= ToggleEndUI;
        GameManager.Instance.OnCoinDecrease -= UpdateCoinText;
        GameManager.Instance.OnScoreChanged -= UpdateScoreText;
    }

    void UpdateTimeText(int min, int sec)
    {
        timeText.text = string.Format("{0:D2} : {1:D2}", min, sec);
    }

    void ToggleEndUI(bool isEnd)
    {
        if (!isEnd)
        {
            endUI.SetActive(false);
            return;
        }

        int clear = GameManager.Instance.TotalClearTime;
        int best = PlayerPrefs.GetInt("BestTime", clear);

        endPanel.SetText(clear, best);
        endUI.SetActive(true);
    }

    private void UpdateCoinText(int cnt)
    {
        coinCountText.text = cnt.ToString();
    }

    private void UpdateScoreText(int score)
    {
        scoreText.text = $"Score : {score}";
    }

}
