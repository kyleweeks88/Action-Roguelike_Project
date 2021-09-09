using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody rb;
	public GameObject hitFx_Pf;
	public LayerMask collisionsMask;
	public float projDmg = 10f;
	public float raycastLength = 1f;
    //Cinemachine.CinemachineImpulseSource source;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
		StartCoroutine(DestroyAfterLifetime());
    }

    private void OnTriggerEnter(Collider col)
    {
        ColHit(col);
    }

    void ColHit(Collider col)
    {
        Vector3 hitPoint = col.ClosestPointOnBounds(this.transform.position);

        CharacterStats hitTarget = col.GetComponent<CharacterStats>();
        if (hitTarget != null)
        {
            hitTarget.TakeDamage(this.gameObject, projDmg);
        }
        
        GameObject hitFx = Instantiate(hitFx_Pf, hitPoint, Quaternion.identity);
        
        GameObject.Destroy(this.gameObject);
    }

    public void SetSpeed(float newSpeed, Vector3 dir)
    {
        //source = GetComponent<Cinemachine.CinemachineImpulseSource>();
        //source.GenerateImpulse(Camera.main.transform.forward);

    	StartCoroutine(TranslateProjectile(newSpeed, dir));
    }

    IEnumerator TranslateProjectile(float speed, Vector3 dir)
    {
    	WaitForEndOfFrame wait = new WaitForEndOfFrame();
    	while(this.gameObject != null)
        {
    		this.transform.position += (dir * speed * Time.fixedDeltaTime);
    		yield return wait;
        }
    }

    IEnumerator DestroyAfterLifetime()
	{
		yield return new WaitForSeconds(2f);

        Destroy(gameObject);
	}
}
