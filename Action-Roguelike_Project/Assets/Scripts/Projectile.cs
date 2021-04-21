using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public GameObject hitFx_Pf;
	public LayerMask collisionsMask;
	public float projDmg = 10f;
    public float projSpeed = 10f;
	public float raycastLength = 1f;

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
    	projSpeed = newSpeed;
    	StartCoroutine(TranslateProjectile(newSpeed, dir));
    }

    IEnumerator TranslateProjectile(float speed, Vector3 dir)
    {
    	WaitForEndOfFrame wait = new WaitForEndOfFrame();
    	while(this.gameObject != null)
        {
    		this.transform.position += (dir * speed * Time.deltaTime);
    		yield return wait;
        }
    }

    IEnumerator DestroyAfterLifetime()
	{
		yield return new WaitForSeconds(2f);

        Destroy(gameObject);
	}
}
