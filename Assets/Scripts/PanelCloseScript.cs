using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelCloseScript : MonoBehaviour
{
    public void ClosePanel()
    {
        this.gameObject.SetActive(false);
    }
}
