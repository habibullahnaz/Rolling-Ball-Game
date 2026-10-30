using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private bool collected;

    private void OnTriggerEnter(Collider other)
    {
        if (collected || !other.CompareTag("Player"))
            return;

        GameManager manager = GameManager.Instance;
        if (manager == null)
            return;

        collected = true;
        manager.CollectCoin(gameObject);
    }
}
