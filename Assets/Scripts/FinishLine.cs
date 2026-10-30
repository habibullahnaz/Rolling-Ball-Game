using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        GameManager manager = GameManager.Instance;
        if (manager != null)
        {
            manager.ReachFinish();
        }
    }
}
