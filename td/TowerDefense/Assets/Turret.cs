using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour {

    private Transform target;
    public float range = 15f;
    public float fire_rate = 1f; //1 bullet per second
    private float fire_countdown = 0f; //fire immediately then set to 1/fire_rate

    public string enemy_tag = "enemy";

    public Transform part_to_rotate;
    public float turn_speed = 10f;

    public GameObject bullet_prefab;
    public Transform fire_point;

	// Use this for initialization
	void Start ()
    {
        InvokeRepeating("Update_target", 0f, 0.5f);  //0f = start right away, 0.5f = do every 0.5 seconds
	}

    void Update_target()  //updates target but not every frame so as to not to make too computation intensive
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemy_tag);

        float shortest_distance = Mathf.Infinity;//stores shortest distance to enemy
        GameObject nearest_enemy = null; //stores closest enemy for targeting

        foreach (GameObject enemy in enemies)
        {
            float distance_to_enemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance_to_enemy < shortest_distance)  //finds closest enemy and sets that to the current enemy within the array of enemies
            {
                shortest_distance = distance_to_enemy;
                nearest_enemy = enemy;
            }
        }

        if (nearest_enemy != null && shortest_distance <= range)  //if nearesest enemy is available and the distance is within the range, set the target to the nearest enemy
        {
            target = nearest_enemy.transform;
        }
        else
        {
            target = null;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        fire_countdown -= Time.deltaTime;

        if (target == null)
        {
            return;
        }

        Vector3 dir = target.position - transform.position; //direction in which to point for turret
        Quaternion look_rotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(part_to_rotate.rotation, look_rotation, Time.deltaTime*turn_speed).eulerAngles; //xyz angles, Lerp to smooth changing enemy target
        part_to_rotate.rotation = Quaternion.Euler(0f, rotation.y, 0f); //rotate around y-axis

        if (fire_countdown <= 0f)
        {
            Shoot();
            fire_countdown = 1f / fire_rate;
        }

	}

    void Shoot()
    {
        GameObject bullet_go = Instantiate(bullet_prefab, fire_point.position, fire_point.rotation);
        Bullet bullet = bullet_go.GetComponent<Bullet>();  //grabs bullet component of bullet object

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
