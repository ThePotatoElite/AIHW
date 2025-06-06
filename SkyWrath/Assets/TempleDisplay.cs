using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleDisplay : MonoBehaviour
{
    [SerializeField] TempleInfo TempleInfo;
    [SerializeField] int _HP = 0;

    private void Update()
    {
        _HP = GameManager.Instance.fullSaveData.templeInfo.HP;
    }
}
