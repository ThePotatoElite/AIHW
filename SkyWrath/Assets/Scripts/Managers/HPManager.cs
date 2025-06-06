using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    public Slider hp;

    private void Update()
    {
        hp.value = GameManager.Instance.fullSaveData.templeInfo.HP;
    }
}
