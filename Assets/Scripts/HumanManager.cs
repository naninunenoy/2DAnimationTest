using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanManager : MonoBehaviour {
    const float edgeX = 7.0F;
    const float runUnit = 0.1F;
    const float runSpeed = 5.0F;

    enum CharaState { Idle, Jump, RunRight, RunLeft, Spinig }
    CharaState state;

    [SerializeField]
    Transform humanTransform;
    [SerializeField]
    SimpleAnimation humanAnimation;

	void Start () {
        state = CharaState.Idle;
        humanAnimation.Play("Default"); // 敢えて書いておく
    }
    
    void Update () {
        if (Input.GetKey("right")) {
            RunRight();
        } else if (Input.GetKey("left")) {
            RunLeft();
        } else {
            Idle();
        }
    }

    void Idle() {
        if (state == CharaState.Jump || state == CharaState.Spinig) {
            return;
        }
        state = CharaState.Idle;
        humanAnimation.Play("Default");
    }

    bool filipedLelt = false;
    void FlipHuman() {
        humanTransform.Rotate(Vector3.up, 180.0F);
        filipedLelt = !filipedLelt;
    }

    void RunRight() {
        if (state == CharaState.Jump || state == CharaState.Spinig) {
            return;
        }
        // 左を向いているので右を向く
        if (filipedLelt) {
            FlipHuman();
        }
        state = CharaState.RunRight;
        humanAnimation.Play("Run");
        // move
        var currentPos = humanTransform.position;
        var moveTo = new Vector3(
            (currentPos.x + runUnit >= edgeX) ? edgeX : currentPos.x  + runUnit, // 端を超えないように
            currentPos.y,
            currentPos.z);
        humanTransform.position = Vector3.MoveTowards(currentPos, moveTo, Time.deltaTime * runSpeed);
    }

    void RunLeft() {
        if (state == CharaState.Jump || state == CharaState.Spinig) {
            return;
        }
        // 右を向いているので左を向く
        if (!filipedLelt) {
            FlipHuman();
        }
        state = CharaState.RunLeft;
        humanAnimation.Play("Run");
        // move
        var currentPos = humanTransform.position;
        var moveTo = new Vector3(
            (currentPos.x - runUnit <= -edgeX) ? -edgeX : currentPos.x - runUnit, // 端を超えないように
            currentPos.y,
            currentPos.z);
        humanTransform.position = Vector3.MoveTowards(currentPos, moveTo, Time.deltaTime * runSpeed);
    }
}
