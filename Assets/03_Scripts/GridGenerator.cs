using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class GridGenerator : MonoBehaviour
{
    public IEnumerator GenerateGrid(GameObject cubePrefab, int gridSize = 10, float spacing = 0.1f, float waitTime = 0.5f)
    {
        if (cubePrefab == null)
        {
            Debug.LogError("Cube Prefab is not assigned.");
            yield break;
        }

        GameObject container = new GameObject("----GRIDCONTAINER----");
        container.transform.position = Vector3.zero;

        Vector3 prefabSize = cubePrefab.GetComponent<BaseCube>().cubeRenderer.bounds.size;
        float step = prefabSize.x + spacing;

        int totalCubes = gridSize * gridSize;
        int cubesCreated = 0;

        HashSet<Vector3> positions = new HashSet<Vector3>();
        Vector3 center = new Vector3(0, 0, 0);
        positions.Add(center);

        Queue<Vector3> frontier = new Queue<Vector3>();
        frontier.Enqueue(center);

        while (cubesCreated < totalCubes && frontier.Count > 0)
        {
            Vector3 current = frontier.Dequeue();
            Instantiate(cubePrefab, current, Quaternion.identity, transform);
            cubesCreated++;

            Vector3[] directions = new Vector3[]
            {
                new Vector3( step, 0, 0),
                new Vector3( step, 0,  step),
                new Vector3(0, 0,  step),
                new Vector3(-step, 0,  step),
                new Vector3(-step, 0, 0),
                new Vector3(-step, 0, -step),
                new Vector3(0, 0, -step),
                new Vector3( step, 0, -step)
            };

            foreach (var dir in directions)
            {
                Vector3 newPos = current + dir;

                if (!positions.Contains(newPos) && cubesCreated + frontier.Count < totalCubes)
                {
                    positions.Add(newPos);
                    frontier.Enqueue(newPos);
                }
            }

            yield return new WaitForSeconds(waitTime);
        }

    }
}

