using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public bool CollisionSystem(Vector3 position,List<Vector3>vertex,Vector3 pos)
	{
        int hitCount = 0;
        List<Vector3> vertexList = new List<Vector3>();

		for (int i = 0; i < vertex.Count; ++i)
		{
			vertexList.Add(new Vector3(pos.x + vertex[i].x, pos.y + vertex[i].y, 0.0f));
		}

		for (int i = 0; i < vertex.Count; ++i)
		{
			Vector3 nextVertexPos = vertexList[0];
			// ステップ１
			if (vertex[i].y == vertex[i + 1].y)
				continue;

			// ステップ２
			if (vertex[i].y < vertex[i + 1].y)
			{
				if (vertexList[i].y < vertex[i].y ||
					vertexList[i].y >= vertex[i + 1].y)
					continue;
			}
			else
			{
				if (vertexList[i].y >= vertex[i].y ||
					vertexList[i].y < vertex[i + 1].y)
					continue;
			}
			// ステップ3
			float t = ((float)(vertex[i + 1].x - vertex[i].x)
				* (vertexList[i].y - vertex[i].y))
				/ (vertex[i + 1].y - vertex[i].y) - (vertexList[i].x - vertex[i].x);

			if (t >= 0.0f)
			{
				++hitCount ;
			}
		}

		return hitCount % 2 != 0;
	}
}
