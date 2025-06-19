using Adobe.Substance.Connector;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    [SerializeField] GameObject[] popUps;
    [SerializeField] DoorController mainDoor;
    [SerializeField] Collider popUp1Collider;
    int popUpIndex = -1;

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
        for (int i = 0; i < popUps.Length; i++) {
            if (i == popUpIndex) {
                popUps[popUpIndex].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }


        // all tutorial
        if(popUpIndex == 0)
        {
            if (Input.GetKeyDown(KeyCode.F)) {
                popUpIndex = -1;
            }
        }
    }

    public void StartTutorial()
    {
        popUpIndex = 0;

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerController>(out PlayerController player)){
            CloseDoor();

            GetComponent<Collider>().enabled = false;

            StartTutorial(); 
        }
    }

    void CloseDoor()
    {
        if (mainDoor != null)
        {
            mainDoor.CloseDoor(0.7f);
        }

        
    }

    
}
