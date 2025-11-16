using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapControl : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Camera minimapCam;
    [SerializeField] RectTransform miniMap;
    [SerializeField] Image icon;

    List<GameObject> spawnedCoins = new();
    private const string coinIconText = "CoinIcon";
    float mapX;
    float mapY;

    private void Awake()
    {
        mapX = miniMap.rect.width;
        mapY = miniMap.rect.height;
    }

    private void LateUpdate()
    {
        CalculatePosition();
    }

    void CalculatePosition()
    {
        var newPos = TransformPoint(player.transform.position);
        icon.transform.localPosition = newPos;
    }

    public void SetCoinIcon()
    {
        spawnedCoins.Clear();
        spawnedCoins = GameManager.Instance.SpawndCoinList;

        foreach (var coin in spawnedCoins)
        {
            var newPos = TransformPoint(coin.transform.position);
            ObjectPoolManager.Instance.SpawnFromPool(coinIconText, Vector3.zero, out var obj);

            obj.transform.localPosition = newPos;
        }
    }

    Vector2 TransformPoint(Vector3 position)
    {
        Vector2 pos = minimapCam.WorldToScreenPoint(position);
        var newPos = new Vector2(pos.x - mapX, pos.y - mapY);

        return newPos;
    }
}