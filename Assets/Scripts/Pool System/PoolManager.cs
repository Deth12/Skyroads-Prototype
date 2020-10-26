using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
	public static PoolManager Instance;
	private void Awake()
	{
		if(Instance != null)
			Destroy(Instance);
		else
		{
			Instance = this;
			Initialize();
		}
	}
	
	public PoolConfig PoolConfig;
	
	private Dictionary<string, ObjectsPool> pools;

	public void Initialize() 
    {
	    pools = new Dictionary<string, ObjectsPool>();
	    GameObject r = new GameObject { name = "-Pools" };
	    foreach(var item in PoolConfig.Pools)
		{
			ObjectsPool p = new ObjectsPool();
			GameObject g = GameObject.FindGameObjectWithTag(item.parentTag);
			if (!g)
			{
				g = new GameObject { name = item.parentTag, tag = item.parentTag };
				g.transform.parent = r.transform;
			}
			p.Initialize(item.prefab, item.amount, g.transform);
			pools.Add(item.poolName, p);
		}
	}

	public GameObject GetObject (string name, Vector3 position, Quaternion rotation) 
	{
		GameObject result = null;
		if (pools != null) 
		{
			if (pools.ContainsKey(name))
			{
				result = pools[name].GetObject().gameObject;
				result.transform.position = position;
				result.transform.rotation = rotation;
				result.SetActive (true);
				return result;
			}
		} 
		return result;
	}
}
