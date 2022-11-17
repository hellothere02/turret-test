using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Turrets
{
    private void FixedUpdate()
    {
        Detect();
        if (isTargetDetevted)
        {
            OnAction?.Invoke();
            if (isAllowToAttack)
            {
                Invoke("Attack", timeToStartAttack);
            }
        }
    }
}
