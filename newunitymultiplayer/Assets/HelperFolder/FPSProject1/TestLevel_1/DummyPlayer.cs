using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB.GameElements
{
    public class DummyPlayer : GameElement
    {
        [SerializeField] Vector3 _targetPos = Vector3.zero;
        [SerializeField] Vector3 _targetRot = Vector3.zero;
        [SerializeField] Animator animator;

        public override void OnUpdate()
        {
            animator.SetFloat("Speed",_targetPos.magnitude);
            this.transform.position = Vector3.Lerp(this.transform.position, _targetPos, 0.2f);
            this.transform.GetChild(0).rotation = Quaternion.Euler(Vector3.Lerp(this.transform.GetChild(0).rotation.eulerAngles, _targetRot, 0.2f));
        }


        public override void SetTargetPosition(Vector3 position)
        {
            _targetPos = position;
        }
        public override void SetTargetRotation(Vector3 rotation)
        {
            _targetRot = rotation;
        }

    }
}