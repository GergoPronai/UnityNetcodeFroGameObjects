using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB.GameElements
{
    public class GameElement : MonoBehaviour
    {
        protected IGameInitializer _initializer = null;
        protected IListener _listener = null;

        public virtual void InitGameElement(IGameInitializer initializer)
        {
            _initializer = initializer;
        }

        public virtual void OnFixedUpdate()
        {

        }

        public virtual void OnUpdate()
        {

        }

        public virtual void OnLateUpdate()
        {

        }

        public virtual void SetTargetPosition(Vector3 position)
        {

        }
        public virtual void SetTargetRotation(Vector3 rotation)
        {

        }

        public virtual Vector3 GetTargetPosition()
        {
            return Vector3.zero;
        }
        public virtual Vector3 GetTargetRotation()
        {
            return Vector3.zero;
        }
    }
}