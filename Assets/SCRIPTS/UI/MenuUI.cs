using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public void OpenMenu(GameObject menuToOpen)
    {
        menuToOpen.SetActive(true);
    }

    public void CloseMenu(GameObject menuToClose)
    {
        menuToClose.SetActive(false);
    }


}
