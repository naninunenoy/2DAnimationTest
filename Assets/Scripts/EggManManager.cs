using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggManManager : MonoBehaviour {
    const float edgeX = 7.0F;
    const float runUnit = 0.1F;
    const float runSpeed = 5.0F;
    const float jumpSpeed = 3.0F;

    enum CharaState { Idle, Jump, Fall, RunRight, RunLeft, Squat }
    CharaState state;

    [SerializeField]
    Transform humanTransform;
    [SerializeField]
    EggMan eggMan;
    [SerializeField]
    SimpleAnimation humanAnimation;

    bool move = false;
    Vector3 moveTo = new Vector3();
    float moveSpeed = 0.0F;

    void Start() {
        state = CharaState.Idle;
        humanAnimation.Play("Default"); // 敢えて書いておく
        eggMan.OnJumpTop += JumpTop;
        eggMan.OnJumpEnd += JumpEnd;
    }

    void OnDestroy() {
        eggMan.OnJumpTop += JumpTop;
        eggMan.OnJumpEnd -= JumpEnd;
    }

    void Update() {
        if (Input.GetKeyDown("up")) {
            Jump();
        } else if (Input.GetKey("right")) {
            RunRight();
        } else if (Input.GetKey("left")) {
            RunLeft();
        } else if (Input.GetKey("down")) {
            Squat();
        } else {
            Idle();
        }
        // 移動
        if (move) {
            var currentPos = humanTransform.position;
            humanTransform.position = Vector3.MoveTowards(currentPos, moveTo, Time.deltaTime * moveSpeed);
        }
    }

    void Idle() {
        if (state == CharaState.Jump || state == CharaState.Fall || state == CharaState.Squat) {
            return;
        }
        state = CharaState.Idle;
        humanAnimation.Play("Default");
        moveSpeed = 0.0F;
        move = false;
    }

    void Jump() {
        if (state == CharaState.Jump || state == CharaState.Fall) {
            return;
        }
        state = CharaState.Jump;
        humanAnimation.Play("Jump");
        // move
        var currentPos = humanTransform.position;
        moveTo = new Vector3(currentPos.x, 1.0F, currentPos.z);
        moveSpeed = jumpSpeed;
        move = true;
    }

    void JumpTop() {
        state = CharaState.Fall;
        // move
        var currentPos = humanTransform.position;
        moveTo = new Vector3(currentPos.x, 0.0F, currentPos.z);
        moveSpeed = jumpSpeed;
        move = true;
    }

    void JumpEnd() {
        state = CharaState.Idle;
        humanAnimation.Play("Default");
        moveSpeed = 0.0F;
        move = false;
    }

    bool filipedLelt = false;
    void FlipHuman() {
        humanTransform.Rotate(Vector3.up, 180.0F);
        filipedLelt = !filipedLelt;
    }

    void RunRight() {
        if (state == CharaState.Jump || state == CharaState.Fall) {
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
        moveTo = new Vector3(
            (currentPos.x + runUnit >= edgeX) ? edgeX : currentPos.x + runUnit, // 端を超えないように
            currentPos.y,
            currentPos.z);
        moveSpeed = runSpeed;
        move = true;
    }

    void RunLeft() {
        if (state == CharaState.Jump || state == CharaState.Fall) {
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
        moveTo = new Vector3(
            (currentPos.x - runUnit <= -edgeX) ? -edgeX : currentPos.x - runUnit, // 端を超えないように
            currentPos.y,
            currentPos.z);
        moveSpeed = runSpeed;
        move = true;
    }

    void Squat() {
        if (state == CharaState.Jump || state == CharaState.Fall) {
            return;
        }
        state = CharaState.Squat;
        humanAnimation.Play("Squat");
        moveSpeed = 0.0F;
        move = false;
    }
}
