using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemDescriptionUI : MonoBehaviour, IObserver
{
    [SerializeField] private ItemInteraction itemInteractionScript;

    private TextMeshProUGUI textMeshPro;
    private bool isActive = false;
    private bool hasBeenNotified = false;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        textMeshPro.enabled = isActive;
    }

    private void Start()
    {
        itemInteractionScript.AddObserver(this);
    }

    private void Update()
    {
        if (!hasBeenNotified && isActive)
        {
            isActive = false;
            textMeshPro.enabled = isActive;
        }

        hasBeenNotified = false;
    }

    public void SetText(string text)
    {
        textMeshPro.text = text;
    }

    public void OnNotify(NotifyArg arg)
    {
        hasBeenNotified = true;
        isActive = true;
        textMeshPro.enabled = isActive;
        SetText(arg.stringArg);
    }
}
