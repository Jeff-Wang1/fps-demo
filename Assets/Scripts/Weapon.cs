using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("子弹设置")]
    public int ammoAmount;                      //子弹总数
    public int ammoLoadAmount;                  //每次重新装载子弹的数量
    public float ammoInterval;                  //每两发子弹的时间间隔
    private float ammoDeltatime;                //对子弹间隔时间计时的工具变量

    private PlayerControl playerControl;

    private Animator weaponAnimator;

    [Header ("音频设置")]
    public AudioClip fireAudio;
    public AudioClip reloadAudio;

    [Header("子弹轨迹设置")]
    public LineRenderer ammoRayTrailPrefab;     //子弹轨迹的prefab
    public Transform ammoShotTransform;         //子弹发射的初始位置，用模型的枪梢的位置标记

    [Header("子弹粒子特效")]
    public ParticleSystem ammoShotParticlePrefab;

    class ActiveTrail                           //存取每发子弹轨迹的各个特征变量
    {
        public LineRenderer renderer;
        public Vector3 direction;
        public float remainingTime;
    }
    List<ActiveTrail> m_ActiveTrails = new List<ActiveTrail>();

    private AudioSource audioSource;

    void Start()
    {
        weaponAnimator = GetComponent<Animator>();
        playerControl = PlayerControl._instance;
        audioSource = GetComponent<AudioSource>();

        ammoDeltatime = ammoInterval;
    }
    
    void Update()
    {
        TestWeaponState();

        Vector3[] trailPos = new Vector3[2];
        for(int i = 0; i < m_ActiveTrails.Count; ++i)
        {
            var curTrail = m_ActiveTrails[i];

            curTrail.renderer.GetPositions(trailPos);
            curTrail.remainingTime -= Time.deltaTime;

            trailPos[0] += curTrail.direction * Time.deltaTime * 50f;
            trailPos[1] += curTrail.direction * Time.deltaTime * 50f;

            curTrail.renderer.SetPositions(trailPos);

            if (curTrail.remainingTime < 0)
            {
                m_ActiveTrails.RemoveAt(i);
                Destroy(curTrail.renderer.gameObject);
                i--;
            }
        }
    }

    private void TestWeaponState()
    {
        ammoDeltatime -= Time.deltaTime;

        weaponAnimator.SetBool("isRunning", playerControl.isRunning);
        if (Input.GetMouseButtonDown(0) && ammoDeltatime < 0)
        {
            weaponAnimator.SetTrigger("fire");
            audioSource.PlayOneShot(fireAudio);
            ammoDeltatime = ammoInterval;

            AmmoShotRayCast();
        }
    }

    private void AmmoShotRayCast()
    {
        RaycastHit raycastHit;
        Ray r = playerControl.playerCamera.ViewportPointToRay(Vector3.one * 0.5f);
        Vector3 hitPosition = r.origin + r.direction * 100.0f;

        if(Physics.Raycast(r,out raycastHit, 1000.0f, ~(1 << 10), QueryTriggerInteraction.Ignore))
        {
            // 子弹射击点太近，子弹路径则不会显示
            if (raycastHit.distance > 5.0f)
            {
                hitPosition = raycastHit.point;
            }

            // 子弹击中的粒子特效
            if(raycastHit.distance < 200f)
            {
                Instantiate(ammoShotParticlePrefab, raycastHit.point, Quaternion.identity);
                //TODO: 需要用 objectpool 存取子弹轨迹和击中的粒子系统
                // ！！！
            }

            //TODO：子弹打到怪物
            if (raycastHit.collider.gameObject.layer == 11)
            {

            }
        }

        // 通过LineRenderer 显示子弹轨迹
        Vector3[] trailPos = new Vector3[] { ammoShotTransform.position,
            ammoShotTransform.position + (hitPosition - ammoShotTransform.position).normalized * 10f };
        LineRenderer trail = Instantiate(ammoRayTrailPrefab);
        trail.SetPositions(trailPos);
        m_ActiveTrails.Add(new ActiveTrail()
        {
            renderer = trail,
            direction = (hitPosition - ammoShotTransform.position).normalized,
            remainingTime = 0.5f
        });
    }
    
}
