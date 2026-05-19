using UnityEngine;
using UnityEngine.UI;

public class StealthUI : MonoBehaviour
{
    public PlayerStealthState playerState;

    public GameObject hiddenIcon;
    public GameObject dangerIcon;

    void Update()
    {
        if (playerState == null) return;

        if (playerState.isHidden)
        {
            hiddenIcon.SetActive(true);
            dangerIcon.SetActive(false);
        }
        else
        {
            hiddenIcon.SetActive(false);
            dangerIcon.SetActive(true);
        }
    }
}