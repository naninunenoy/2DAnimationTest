using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanManager : MonoBehaviour {
    [SerializeField]
    SimpleAnimation humanAnimation;


	void Start () {
        humanAnimation.Play("Run");
    }
    
    void Update () {
		
	}
}
