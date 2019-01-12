using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class Weapon : MonoBehaviour
{   
    [System.Serializable]
    public class WeaponInput
    {
        public bool shoot;
        public bool reload;
    }
    [SerializeField]
    public WeaponInput input;

    [System.Serializable]
    public class WeaponSettings
    {
        public Vector3 Target;
        public int maxAmmo = 20;
        public int carryingAmmo = 0;
        public int currentAmmo = 3;
        public int ammoNeeded;

        public bool AutoReload;
        public float reloadTime = 1.5f;
    }
    [SerializeField]
    public WeaponSettings settings;

    [System.Serializable]
    public class BulletSettings
    {
        public GameObject spawnPos;
        public GameObject shellEjectPos;


        public float bulletThrust = 2f;
    }
    [SerializeField]
    public BulletSettings bulletSettings;

    UserInput userinput;
    BulletPool pool;

    Camera maincam;

    float x, y;

    public Rigidbody player;

    public TMP_Text bullet;

    AudioSource s;

    #region SingleTon
    public static Weapon Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        userinput = UserInput.Instance;
        maincam = Camera.main;

        pool = BulletPool.Instance;

        x = Screen.width / 2f;
        y = Screen.height / 2f;

        bullet.text = settings.currentAmmo + "/" + settings.carryingAmmo;

        if(!player)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        }

        s = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Shoot();
        Reload();

        bullet.text = settings.currentAmmo + "/" + settings.carryingAmmo;
    }

    void GetInput()
    {
        input.shoot = userinput._inputData.shoot;
        input.reload = Input.GetKeyDown(KeyCode.R);
    }

    void Shoot()
    {
        x = Screen.width / 2f;
        y = Screen.height / 2f;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        //Ray ray = maincam.ScreenPointToRay(new Vector3(x, y, 0f));
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.yellow);
        if (Physics.Raycast(ray, out hit, 1000f))
        {
            Debug.Log("Hit");
            /*settings.Target = new Vector3(hit.point.x - bulletSettings.spawnPos.transform.position.x, 
                                            hit.point.y - bulletSettings.spawnPos.transform.position.y,
                                                    hit.point.z - bulletSettings.spawnPos.transform.position.z).normalized;*/
            settings.Target = (hit.point - bulletSettings.spawnPos.transform.position).normalized;
        }
        else
        {
            Debug.Log("Not Hit");
            settings.Target = ray.direction;
        }

        settings.Target = ray.direction;

        if(input.shoot && settings.currentAmmo > 0)
        {
            settings.currentAmmo--;
            settings.ammoNeeded = settings.maxAmmo - settings.currentAmmo;

           // bullet.text = settings.currentAmmo + "/" + settings.carryingAmmo;

            GameObject o = pool.SpawmFromPool("GrowBullet", bulletSettings.spawnPos.transform.position, Quaternion.FromToRotation(Vector3.up, settings.Target));
            o.GetComponent<BulletMovement>().direction = settings.Target;

            player.AddForce(bulletSettings.bulletThrust *  (player.transform.forward) * -1f, ForceMode.Impulse);
            s.Play();
        }
    }

    void Reload()
    {
        if( (settings.currentAmmo == 0 && settings.AutoReload) || input.reload)
        {
            settings.ammoNeeded = settings.maxAmmo - settings.currentAmmo;
            if(settings.ammoNeeded == 0)
            {
                return;
            }

            StartCoroutine(DealyAndReload(settings.ammoNeeded));
        }
    }

    IEnumerator DealyAndReload(int needAmmo)
    {
        yield return new WaitForSeconds(settings.reloadTime);
        if (needAmmo >= settings.carryingAmmo)
        {
            settings.currentAmmo = settings.carryingAmmo;
            settings.carryingAmmo = 0;
            settings.ammoNeeded = 0;
        }
        else
        {
            settings.currentAmmo = needAmmo;
            settings.carryingAmmo -= settings.ammoNeeded;
            settings.ammoNeeded = 0;
        }

        bullet.text = settings.currentAmmo + "/" + settings.carryingAmmo;
    }
}
