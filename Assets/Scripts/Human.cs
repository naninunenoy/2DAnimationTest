using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour {
    public System.Action OnJumpEnd = delegate { };

	public void OnGround() {
        OnJumpEnd.Invoke();
    }
}
