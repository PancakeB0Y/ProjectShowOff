using Adobe.Substance.Connector;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    [SerializeField] GameObject[] popUps;
    [SerializeField] DoorController mainDoor;
    public int tutorialIndex = 0;

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
            if (i == tutorialIndex && popUps[i] != null) {
                popUps[tutorialIndex].SetActive(true);
            }
            else
            {
                if (popUps[i] != null)
                {
                    popUps[i].SetActive(false);
                }
            }
        }

        // all tutorial checks
        switch (tutorialIndex)
        {
            case 1:
                if (Input.GetKeyDown(KeyCode.I))
                {
                    tutorialIndex++;
                }
                break;
            case 3:
                if (mainDoor != null && mainDoor.isDoorOpen)
                {
                    mainDoor.CloseDoor(0.7f);
                }

                if(PlayerInputs.instance != null)
                {
                    PlayerInputs.instance.CanLightLantern = true;
                }

                if (Input.GetKeyDown(KeyCode.F))
                {
                    tutorialIndex++;
                }
                break;
            case 5:
                if (mainDoor != null && !mainDoor.isDoorOpen)
                {
                    mainDoor.OpenDoor();
                    if(UIManager.Instance != null)
                    {
                        UIManager.Instance.OpenWinMenu();
                    }
                }
                break;
            default:
                break;
        }
    }

    public void SetTutorialIndex(int index)
    {
        if (index > tutorialIndex)
        {
            tutorialIndex = index;
        }
    }
}
