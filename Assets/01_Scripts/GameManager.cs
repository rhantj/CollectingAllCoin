using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    WaitForSeconds wait = new WaitForSeconds(1f);

    [Header("Coin Spawn")]
    [SerializeField] Transform ground;
    [SerializeField] LayerMask groundLayer;
    public List<GameObject> spawnedCoin = new();
    private const string coinName = "Coin";
    MinimapControl miniMap;

    float gX, gY;
    int maxCoinCnt = 20;
    int totaleCoinCnt = 0;
    public int TotaleCoinCnt
    {
        get { return totaleCoinCnt; }
        set 
        { 
            totaleCoinCnt = value;
            OnCoinDecrease?.Invoke(totaleCoinCnt);
        }
    }
    public event Action<int> OnCoinDecrease;

    [Header("Timer")]
    public int sec;
    int min;
    public event Action<int, int> OnTimeChanged;
    private static string bestTimeText = "BestTime";


    [Header("Game End")]
    private bool isEnd;
    private bool IsEnd
    {
        get { return isEnd; }
        set { 
            isEnd = value; 
            ToggleEndUI?.Invoke(isEnd);
        }
    }
    public event Action<bool> ToggleEndUI;

    [Header("Scores")]
    private int score;
    public int Score
    {
        get { return score; }
        set 
        { 
            score = value;
            OnScoreChanged?.Invoke(score);
        }
    }
    public event Action<int> OnScoreChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        gX = 10f * ground.localScale.x;
        gY = 10f * ground.localScale.z;

        PlayerPrefs.DeleteKey(bestTimeText);
        miniMap = GetComponent<MinimapControl>();

        StartCoroutine(Co_GameFlow());
    }

    private void Start()
    {
        RestartEvent.OnRestart += RestartSetting;
    }

    private void OnDestroy()
    {
        RestartEvent.OnRestart -= RestartSetting;
    }

    void RestartSetting()
    {
        IsEnd = false;
        StartCoroutine(Co_GameFlow());
    }

    IEnumerator Co_GameFlow()
    {
        yield return StartCoroutine(Co_SpawnCoins());

        yield return StartCoroutine(Co_CalculateTime());
    }

    IEnumerator Co_SpawnCoins()
    {
        yield return wait;

        for (int i = 0; i < maxCoinCnt; ++i)
        {
            var pos = CoinSpawnPosition();

            if (Physics.Raycast(pos + Vector3.up * 2f, Vector3.down, 5f, groundLayer))
            {
                pos += Vector3.up;
            }

            TotaleCoinCnt++;
            ObjectPoolManager.Instance.SpawnFromPool(coinName, pos, out var coin);
            spawnedCoin.Add(coin);
        }

        miniMap.SetCoinIcon(spawnedCoin);
    }

    IEnumerator Co_CalculateTime()
    {
        sec = 0; min = 0;

        while (min < 1)
        {
            if (TotaleCoinCnt <= 0)
            {
                AtGameEnd();
                break;
            }

            yield return wait;

            sec++;

            if (sec >= 60)
            {
                sec = 0;
                min++;
            }

            OnTimeChanged?.Invoke(min, sec);
        }

        AtGameEnd();
    }

    void AtGameEnd()
    {
        foreach (var obj in spawnedCoin)
        {
            ObjectPoolManager.Instance.ReturnToPool(obj.name, obj);
        }
        spawnedCoin.Clear();

        Score = 0;
        TotaleCoinCnt = 0;

        JudgeBestTime();
        IsEnd = true;
    }

    void JudgeBestTime()
    {
        int clear = TotalClearTime;
        int best = PlayerPrefs.GetInt(bestTimeText, -1);

        if (clear < best || best == -1) PlayerPrefs.SetInt(bestTimeText, clear);
    }

    public int TotalClearTime => min * 60 + sec;

    Vector3 CoinSpawnPosition()
    {
        float x = Random.Range(-gX / 2, gX / 2);
        float y = Random.Range(-gY / 2, gY / 2);

        var local = new Vector3(x, 1.5f, y);

        return local;
    }
}