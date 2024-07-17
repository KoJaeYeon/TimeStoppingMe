using UnityEngine;
using System.Collections;

public class PlaceableItem : Item
{
    public float installationTime = 3f;
    public float durationTime = 10f;
    public MeshRenderer MeshRenderer;
    public Material newMaterial; // 새로운 마테리얼을 설정할 변수

    public Material[] MeshRenderrrMt;
    public override void Use(Player player)
    {
        //player.StartPlacingItem(this);
        Startblebuild();
    }
    
    private void Startblebuild()
    {
        // MeshRenderer의 Materials 배열을 받아옵니다.
        MeshRenderer = transform.GetComponent<MeshRenderer>();
        Material[] materials = MeshRenderer.materials;

        // 기존 Materials 배열에 새로운 마테리얼을 추가한 배열을 생성합니다.
        Material[] newMaterials = new Material[materials.Length + 1];
        for (int i = 0; i < materials.Length; i++)
        {
            newMaterials[i] = materials[i];
        }
        Material instantiatedNewMaterial = new Material(newMaterial);
        newMaterials[materials.Length] = instantiatedNewMaterial;

        MeshRenderer.materials = newMaterials;

        StartCoroutine(Placeablebuild(instantiatedNewMaterial, "_Float", 0.5f, -0.3f, installationTime));
    }
    private IEnumerator Placeablebuild(Material material, string propertyName, float startValue, float endValue, float duration)
    {
        float elapsedTime = 0f;
        if (!material.HasProperty(propertyName))
        {
            Debug.LogError($"{propertyName} 라는 마테리얼 없음");
            yield break;
        }
        else
        {
            Debug.Log("있음");
        }

        material.SetFloat(propertyName, startValue);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newValue = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            material.SetFloat(propertyName, newValue);
            yield return null;
        }

        material.SetFloat(propertyName, endValue);
        StartCoroutine(Distroy());
    }
    IEnumerator Distroy()
    {
        Debug.Log("파괴시작");
        yield return new WaitForSecondsRealtime(durationTime);

        Destroy(gameObject);
    }
}
