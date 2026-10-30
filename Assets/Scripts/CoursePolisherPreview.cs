using UnityEngine;

/// <summary>
/// Shows the clean CoursePolisher pass while editing the scene, not only in Play Mode.
/// </summary>
[ExecuteAlways]
[DisallowMultipleComponent]
public sealed class CoursePolisherPreview : MonoBehaviour
{
    private void OnEnable()
    {
        if (!Application.isPlaying)
            CoursePolisher.Apply();
    }

    private void OnValidate()
    {
        if (!Application.isPlaying)
            CoursePolisher.Apply();
    }
}
