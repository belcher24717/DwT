using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShellScript : MonoBehaviour
{
    public int Damage;
    public Transform Target;
    public float Radius;
    public ParticleSystem Explosion;

    // Use this for initialization
    void Start()
    {
        Vector3 launch = TrajectoryFactory.HitTargetAtTime(transform.position, Target.position, Physics.gravity, 0.2f);
        GetComponent<Rigidbody>().AddForce(launch, ForceMode.VelocityChange);
    }

    void OnCollisionEnter(Collision collision)
    {
        ParticleSystem explosionRef = Instantiate(Explosion, transform.position, Quaternion.identity);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, Radius);
        foreach(Collider collider in hitColliders)
        {
            Enemy e = collider.GetComponent<Enemy>();
            e?.TakeDamage(Damage, AttackType.Explosive);
        }

        Destroy(this.gameObject);
    }
    
    void Update()
    {

    }
}
