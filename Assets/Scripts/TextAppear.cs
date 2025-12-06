using UnityEngine;
using TMPro;

public class TextAppear: MonoBehaviour
{
    public TMP_Text textMeshPro;

    void Start()
    {
        textMeshPro.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LightText"))
            textMeshPro.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LightText"))
            textMeshPro.enabled = false;
    }
}
