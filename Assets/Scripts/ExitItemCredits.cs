using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitItemCredits : MenuItemBasic
{
    public override void OnClick()
    {
        Application.Quit();
    }
}