using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    public ItemData coinData;
    private string m_name;
    private string m_description;
    private int m_score;

    [Header("Text")]
    [SerializeField] GameObject descriptionPanel;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI descriptionText;


    void OnEnable()
    {
        CoinInit(coinData);
    }

    void OnDisable()
    {
        OnPointerExit();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (Vector3.Distance(transform.position, other.transform.position) < 0.3f)
        {
            Collected();
        }

        transform.position = 
            Vector3.MoveTowards(transform.position, other.transform.position, .1f);
    }

    public void CoinInit(ItemData data)
    {
        m_name = data.name;
        m_description = data.Description;
        m_score = data.Score;

        nameText.text = m_name;
        descriptionText.text = m_description;
    }


    public void Collected()
    {
        GameManager.Instance.Score += m_score;
        GameManager.Instance.TotaleCoinCnt--;
        ObjectPoolManager.Instance.ReturnToPool("Coin", gameObject);
        ICollectable.CollectedInvoke();
    }

    public void OnPointerEnter()
    {
        descriptionPanel.SetActive(true);
    }

    public void OnPointerExit()
    {
        descriptionPanel.SetActive(false);
    }
}
