using Adobe.Substance.Connector;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    [SerializeField] GameObject[] popUps;
    [SerializeField] DoorController mainDoor;
    int tutorialIndex = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Update()
    {
        // enable the current tutorial stage
        for (int i = 0; i < popUps.Length; i++) {
            if (i == tutorialIndex) {
                popUps[tutorialIndex].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }

        // all tutorial checks
        switch (tutorialIndex)
        {
            case 1:
                if (mainDoor != null && mainDoor.isDoorOpen)
                {
                    mainDoor.CloseDoor(0.7f);
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    tutorialIndex++;
                }
                break;
            case 2:
                
                break;
            case 3:
                if (mainDoor != null && !mainDoor.isDoorOpen)
                {
                    mainDoor.OpenDoor();
                }
                break;
            default:
                break;
        }
    }

    public void SetTutorialIndex(int index)
    {
        if (index > tutorialIndex) {
            tutorialIndex = index;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            
        }
    } 
}
