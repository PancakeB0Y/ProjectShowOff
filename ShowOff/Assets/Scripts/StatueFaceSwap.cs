using UnityEngine;

public class StatueFaceSwap : MonoBehaviour
{
    [SerializeField] GameObject[] statueFaces;

    int faceIndex = 0;

    private void Update()
    {
        // enable the current tutorial stage
        for (int i = 0; i < statueFaces.Length; i++)
        {
            if (i == faceIndex && !statueFaces[faceIndex].activeInHierarchy)
            {
                statueFaces[faceIndex].SetActive(true);
            }
            else if (i != faceIndex && statueFaces[i].activeInHierarchy)
            {
                statueFaces[i].SetActive(false);
            }
        }
    }

    public void SetFace(int newFaceIndex)
    {
        if(newFaceIndex < statueFaces.Length)
        {
            faceIndex = newFaceIndex;
        }
    }
}
