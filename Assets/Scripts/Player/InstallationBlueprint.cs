using UnityEngine;

public class InstallationBlueprint : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        SetVisibility(false);
    }

    public void SetVisibility(bool isVisible)
    {
        meshRenderer.enabled = isVisible;
    }

    public void SetPositionAndRotation(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }
}
