using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSkin : MonoBehaviour
{
    public Data data;
    public void ChangeSkin(int index)
    {
        data.skinIndex = index;
    }
}
