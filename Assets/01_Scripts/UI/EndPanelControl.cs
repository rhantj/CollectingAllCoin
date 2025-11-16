using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndPanelControl : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] TextMeshProUGUI bestTimeText;
    [SerializeField] TextMeshProUGUI clearTimeText;
    [SerializeField] Button restartBtn;
    [SerializeField] Button exitBtn;

    private void Awake()
    {
        restartBtn.onClick.AddListener(OnRestartButtonClicked);
        exitBtn.onClick.AddListener(OnExitButtonClicked);
    }

    public void SetText(int clear, int best)
    {
        int bestMin = best / 60;
        int bestSec = best % 60;

        int clearMin = clear / 60;
        int clearSec = clear % 60;

        bestTimeText.text = $"Best Time : {bestMin:D2} : {bestSec:D2}";
        clearTimeText.text = $"Clear Time : {clearMin:D2} : {clearSec:D2}";
    }

    void OnRestartButtonClicked()
    {
        RestartEvent.RestartInvoke();
    }

    void OnExitButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
