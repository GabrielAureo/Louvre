using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandHideMesh : MonoBehaviour
{
    [SerializeField] private Hand hand;

    private SkinnedMeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    private void Update()
    {
        if (hand.ItemInHand && !meshRenderer.enabled)
        {
            meshRenderer.enabled = true;
        }
        else if (!hand.ItemInHand && meshRenderer.enabled)
        {
            meshRenderer.enabled = false;
        }
    }
}
