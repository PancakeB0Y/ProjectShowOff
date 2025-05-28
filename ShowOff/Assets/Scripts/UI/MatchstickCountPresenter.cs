using System.Collections;
using TMPro;
using UnityEngine;

public class MatchstickCountPresenter : MonoBehaviour
{
    TextMeshProUGUI textMesh;
    UnityEngine.UI.Image matchstickImage;

    private void OnEnable()
    {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        matchstickImage = GetComponentInChildren<UnityEngine.UI.Image>();

        if(matchstickImage != null)
        {
            matchstickImage.gameObject.SetActive(false);
        }

        if (InventoryManager.instance != null)
        {
            InventoryManager.instance.OnPickupItem += AddMatch;
            InventoryManager.instance.OnRemoveItem += RemoveMatch;
        }
    }

    private void OnDisable()
    {
        if (InventoryManager.instance != null)
        {
            InventoryManager.instance.OnPickupItem -= AddMatch;
            InventoryManager.instance.OnRemoveItem -= RemoveMatch;
        }
    }


    void UpdateMatchCount()
    {
        if (textMesh == null) {
            textMesh = GetComponentInChildren<TextMeshProUGUI>();
        }

        if (textMesh != null && InventoryManager.instance != null)
        {
            textMesh.text = InventoryManager.instance.GetMatchstickCount().ToString();
        }
    }

    void AddMatch(ItemController newItem)
    {
        if (IsItemControllerMatchstick(newItem))
        {
            UpdateMatchCount();
        }
    }

    void RemoveMatch(ItemController newItem)
    {
        if (IsItemControllerMatchstick(newItem)) {
            StartCoroutine(LoseMatchCoroutine());
        }
    }

    IEnumerator LoseMatchCoroutine()
    {
        if (textMesh == null)
        {
            textMesh = GetComponentInChildren<TextMeshProUGUI>();
        }

        if (textMesh != null)
        {
            textMesh.color = Color.red;
        }

        if (matchstickImage != null) {
            matchstickImage.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(1f);

        if (textMesh != null)
        {
            UpdateMatchCount();
            textMesh.color = Color.white;

            yield return new WaitForSeconds(0.6f);

            //Hide image
            if (matchstickImage != null)
            {
                matchstickImage.gameObject.SetActive(false);
            }
        }
    }

    bool IsItemControllerMatchstick(ItemController itemController)
    {
        return itemController.item.itemType == itemType.Matchstick;
    }
}
