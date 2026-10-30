using UnityEngine;

public class FallDetector : MonoBehaviour
{
    [SerializeField] private float fallHeight = -5f;

    private bool fallHandled;

    private void Update()
    {
        if (fallHandled || transform.position.y >= fallHeight)
            return;

        GameManager manager = GameManager.Instance;
        if (manager == null)
            return;

        fallHandled = true;
        manager.GameOver();
    }
}
