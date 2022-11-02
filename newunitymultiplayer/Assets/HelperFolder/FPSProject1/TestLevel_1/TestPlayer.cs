using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB.GameElements
{
    public class TestPlayer : GameElement
    {
        public Vector3 moveDirection;
        private float moveSpeed = 5f;
        public float animSpeed = 0f;
        public Transform PlayerVisuals;
        public Animator animator;

        public override void InitGameElement(IGameInitializer initializer)
        {
            _initializer = initializer;
        }

        public override void OnUpdate()
        {
            DetectedInputDevice input = _initializer.INPUT_DEVICES.GetInputDevice(0);
            GatherInput();
            Look();
            Move();
        }

        private void GatherInput()
        {
            moveDirection = new Vector3(-Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal"));
        }

        private void Look()
        {
            if (moveDirection == Vector3.zero) return;

            var relative = (transform.position + moveDirection) - transform.position;
            var rot = Quaternion.LookRotation(relative.ToIso(), Vector3.up);
            PlayerVisuals.rotation = rot;
        }

        private void Move()
        {
            var relative = (transform.position + moveDirection) - transform.position;
            relative.Normalize();
            transform.localPosition += relative.ToIso() * moveSpeed * Time.deltaTime;
            animSpeed = moveDirection.magnitude;
            animator.SetFloat("Speed", animSpeed);
            _initializer.STEAM_CONTROL.Send_Player_Position(this.transform.position);
        }
    }
}
public static class Helpers
{
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}