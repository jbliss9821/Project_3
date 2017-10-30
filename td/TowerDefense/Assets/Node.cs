using System.Collections;
using UnityEngine;

public class Node : MonoBehaviour {

    public Color hover_color;
    private Color start_color;
    private Renderer rend;

    public Vector3 position_offset;

    private GameObject turret; //current turret on node

    private void Start()
    {
        rend = GetComponent<Renderer>();
        start_color = rend.material.color;
    }

    void OnMouseDown()
    {
        if (turret != null)
        {
            Debug.Log("Turret already in this location");  //TODO display this on screen
            return;
        }

        //build a turret
        GameObject turret_to_build = Build_manager.instance.Get_turret_to_build();
        turret = (GameObject)Instantiate(turret_to_build, transform.position + position_offset, transform.rotation);
    }

    void OnMouseEnter()
    {
        rend.material.color = hover_color;
    }

    void OnMouseExit()
    {
        rend.material.color = start_color;
    }
}
