using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandle : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            GameManager.Instance.OnHeldMouse();
        }

        for (int i = 0; i < 3; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                GameManager.Instance.OnSwitchBullet(i);
                break;
            }
        }
    }
}
