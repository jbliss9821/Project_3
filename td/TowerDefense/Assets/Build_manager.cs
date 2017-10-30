using UnityEngine;

public class Build_manager : MonoBehaviour {

    public static Build_manager instance;  //a way to make sure there is only one build manager within the scene

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one build manager in scene");
            return;
        }
        instance = this;  //each time start the game, creates one build manager that is held within this instance variable that can be accessed anywhere
    }

    public GameObject standard_turret_prefab;

    void Start()
    {
        turret_to_build = standard_turret_prefab;
    }

    private GameObject turret_to_build;

    public GameObject Get_turret_to_build()
    {
        return turret_to_build;
    }
}
