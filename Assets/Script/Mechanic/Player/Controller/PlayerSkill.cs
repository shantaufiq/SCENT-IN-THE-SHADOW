using UnityEngine; 

namespace ScentInTheShadow.Mechanic.Player.Controller
{
    public class PlayerSkill : MonoBehaviour
    {
        [Header("Skill Prefab")]
        public GameObject Bullet, windskill;
        public Transform bulletCheck;

        [Header("Scanning Skill")]
        public ItemTargetScanner playerScanner;

        [Header("Skill1 Tweak / Scanning")]
        [SerializeField] private float SK1cooldown;
        [SerializeField] private float SK1ActivatedTime;
        bool OnSK1;
        float SK1Timer;
        float SK1Timer2;

        [Header("Skill2 Tweak / Shoot projectile")]
        [SerializeField] private float _Skill2ShootForce = 5;
        [SerializeField] private float SK2cooldown;
        float SK2Timer;

        [Header("Skill3 Tweak / Wind pusher")]
        [SerializeField] private float _Skill3ShootForce = 5;
        [SerializeField] private float SK3cooldown;
        float SK3Timer;

        [Header("GameObject Interactable Items")]
        public GameObject[] Items;

        void Update()
        {
            // Input Key 
            if (InputManager.instance.playerInput.Character.Skill1.IsPressed() && SK1Timer <= 0 && SK1Timer2 <= 0)
            {
                SK1Timer = SK1ActivatedTime;
                OnSK1 = true;
            }
            if (InputManager.instance.playerInput.Character.Skill2.IsPressed() && SK2Timer <= 0)
            {
                Skill2();
                SK2Timer = SK2cooldown;
            }
            if (InputManager.instance.playerInput.Character.Skill3.IsPressed() && SK3Timer <= 0)
            {
                Skill3();
                SK3Timer = SK3cooldown;
            }

            // Timer Skill 1 Activated & cooldown
            if (SK1Timer <= 0)
            {
                if (OnSK1)
            {
                    SK1Timer2 = SK1cooldown;
                foreach (GameObject b in Items)
                {
                    b.GetComponent<Animator>().Play("idle");
                }
                    OnSK1 = false;
                }
                if (SK1Timer2 >= 0)
                {
                    SK1Timer2 -= Time.deltaTime;
                }
            }
            else
            {
                Skill1();
                SK1Timer -= Time.deltaTime;
            }

            // Timer Skill 2 Cooldown 
            if (SK2Timer >= 0)
            {
                SK2Timer -= Time.deltaTime;
            }

            // Timer Skill 3 Cooldown 
            if (SK3Timer >= 0)
            {
                SK3Timer -= Time.deltaTime;
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
            rb.AddForce(transform.forward * _Skill2ShootForce, ForceMode.Impulse);
        }
        private void Skill3()
        {
            Debug.Log("Skill3");
            // niup angin
            GameObject projectile = Instantiate(windskill, transform.position + transform.forward, transform.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * _Skill3ShootForce, ForceMode.Impulse);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            playerScanner.EditorGizmo(transform);
        }
#endif

    }
}
