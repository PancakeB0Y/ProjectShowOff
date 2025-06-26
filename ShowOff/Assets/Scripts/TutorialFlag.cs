using UnityEngine;

public class TutorialFlag : MonoBehaviour
{
    [SerializeField] int nextTutorialIndex;

    private void OnTriggerEnter(Collider other)
    {
        if(TutorialManager.Instance == null)
        {
            return;
        }

        if (other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            TutorialManager.Instance.SetTutorialIndex(nextTutorialIndex);
        }
    }
}
