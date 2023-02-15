using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Spawner : MonoBehaviour
{
    public GameObject Soldier;
    public GameObject Gunner;
    public GameObject Sniper;

    public static Spawner Instance;

    public void Awake()
    {
        Instance = this;
    }

    public object Spawn(GameObject visual, Transform transform)
    {
        return Instantiate(visual, transform.position, transform.rotation);
    }

    public object Spawn(Object visual, Vector3 position)
    {
        return Instantiate(visual, position, new Quaternion());
    }

    public object Spawn(Object visual, Transform transform)
    {
        return Instantiate(visual, transform.position, transform.rotation);
    }
}
