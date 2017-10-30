using UnityEngine;

public class Bullet : MonoBehaviour {

    private Transform target;

    public float speed = 70f;
    public GameObject impact_effect;

    public void Seek(Transform _target)
    {
        target = _target;
    }
	
	// Update is called once per frame
	void Update () {
		
        if(target == null)
        {
            Destroy(gameObject);  //if target disappears, then destroy the bullet, change to a range dependent bullet
            return;
        }

        //direction of bullet pointing at target

        Vector3 dir = target.position - transform.position;

        float distance_per_frame = speed * Time.deltaTime;

        if (dir.magnitude <= distance_per_frame)  //if mag is less than distance per frame, then hit enemy
        {
            //want object to hit before target moves
            Hit_target();
            return;
        }

        transform.Translate(dir.normalized * distance_per_frame, Space.World);

	}

    void Hit_target()
    {
        GameObject effect_instance = Instantiate(impact_effect, transform.position, transform.rotation);
        Destroy(effect_instance, 2f);
        Destroy(target.gameObject);
        Destroy(gameObject);  //destory bullet if hit enemy
    }
}
