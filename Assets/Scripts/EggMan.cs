using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggMan : Human {
    public System.Action OnJumpTop = delegate { };

    public void OnTop() {
        OnJumpTop.Invoke();
    }
}
