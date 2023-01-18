using UnityEngine;
using controller.Player.ItemTargetScanners;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace controller.Player
{
    public class PlayerSkill : MonoBehaviour
    {
        [SerializeField] private float _shootForce = 5;

        public GameObject Bullet, windskill;
        public Transform bulletCheck;

        public ItemTargetScanner playerScanner;

        public GameObject[] Items;

        void Update()
        {
            if (Input.GetKey(KeyCode.G))  // Ubat Input <-------------------------- ( ini buat yang key ditekten )
            {
                Skill1();
            }
            else
            {
                foreach (GameObject b in Items)
                {
                    b.GetComponent<Animator>().Play("idle");
                }
            }
            if (Input.GetKeyDown(KeyCode.H))  // Ubat Input <--------------------------
            {
                Skill2();
            }
            if (Input.GetKeyDown(KeyCode.J))  // Ubat Input <--------------------------
            {
                Skill3();
            }
        }
        private void Skill1()
        {
            foreach(GameObject b in Items)
            {
               playerScanner.DetectPlayer(transform, b);
            }
        }
        private void Skill2()
        {
            Debug.Log("Skill2");
            // nembak 
            GameObject projectile = Instantiate(Bullet, transform.position + transform.forward, transform.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * _shootForce, ForceMode.Impulse);

        }
        private void Skill3()
        {
            Debug.Log("Skill3");
            // niup angin
            Instantiate(windskill, this.transform.position, Quaternion.identity);
        }


        private void OnDrawGizmosSelected()
        {
            playerScanner.EditorGizmo(transform);
        }

    }
}
