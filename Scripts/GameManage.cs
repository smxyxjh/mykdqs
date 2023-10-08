using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour//Í£Ö¹²Ù×÷·½·¨
{
    // Start is called before the first frame update
    bool enableInput = true;
    public bool IsEnableInput()
    {
        return enableInput;
    }
    public void SetEnableInput(bool enabled)
    {
        enableInput = enabled;
    }

}
