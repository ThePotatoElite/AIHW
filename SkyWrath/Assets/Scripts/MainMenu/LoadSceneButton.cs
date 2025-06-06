using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LoadSceneButton : MonoBehaviour
{
    public void OnButtonClick()
    {
        gameObject.SetActive(false);
    }
}
