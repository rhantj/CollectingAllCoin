using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSFX : MonoBehaviour
{
    [SerializeField] AudioSource collectedSFX;

    private void OnEnable()
    {
        ICollectable.OnCollected += PlaySFX;
    }

    private void OnDisable()
    {
        ICollectable.OnCollected -= PlaySFX;
    }

    private void PlaySFX()
    {
        if (collectedSFX)
        {
            collectedSFX.spatialBlend = 0f;

            collectedSFX.Stop();
            collectedSFX.Play();
        }
    }
}
