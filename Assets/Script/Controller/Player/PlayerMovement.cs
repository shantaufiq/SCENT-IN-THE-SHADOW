using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace controller.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private float _speed = 5;
        [SerializeField] private float _turnSpeed = 360;
        private Vector3 _input;

        private void Awake()
        {
            Physics.IgnoreLayerCollision(6, 8);
            Physics.IgnoreLayerCollision(0, 8);
            Physics.IgnoreLayerCollision(8, 8);
            Physics.IgnoreLayerCollision(6, 6);
        }
        private void Update()
        {
            Vector2 movement = InputManager.instance.playerInput.Character.Move.ReadValue<Vector2>();
            _input = new Vector3(movement.x, 0, movement.y);
            Look();
        }
        private void FixedUpdate()
        {
            Move();
        }
        private void Look()
        {
            if (_input == Vector3.zero) return;

            var rot = Quaternion.LookRotation(_input.ToIso(), Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnSpeed * Time.deltaTime);
        }

        private void Move()
        {
            _rb.MovePosition(transform.position + transform.forward * _input.normalized.magnitude * _speed * Time.deltaTime);
        }
    }
}

    public static class Helpers
    {
        private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
    }
