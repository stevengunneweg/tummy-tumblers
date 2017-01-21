using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(MeshFilter))]
public class MountainSide : MonoBehaviour {

	public Mountain mountain {
        get {
            return GetComponentInParent<Mountain>();
        }
    }

    public MeshFilter meshFilter {
        get {
            return GetComponent<MeshFilter>();
        }
    }

    public MeshCollider meshCollider {
        get {
            return GetComponent<MeshCollider>();
        }
    }

    public int sideWidth = 12;
    public AnimationCurve sideCurve;
    public float baseY = -5f;
    public float sideWidthScale = 1f;

    public class IndexedVertex {

        public int index = 0;
        public Vector3 vertex;

        public IndexedVertex(int index, Vector3 vertex) {
            this.index = index;
            this.vertex = vertex;
        }
    }

    public void Regenerate() {
        Mesh mountainMesh = mountain.meshFilter.sharedMesh;

        // Find side vertices
        IndexedVertex[] sideA = mountainMesh.vertices
            .Select((vertex, index) => { return new IndexedVertex(index, vertex); })
            .Where(iv => mountain.generator.IndexToX(iv.index) == 0)
            .ToArray();
        IndexedVertex[] sideB = mountainMesh.vertices
            .Select((vertex, index) => { return new IndexedVertex(index, vertex); })
            .Where(iv => mountain.generator.IndexToX(iv.index) == mountain.generator.width - 1)
            .ToArray();

        // Create Vertices
        int numberOfVertices = sideA.Length * sideWidth * 2;
        Vector3[] vertices = new Vector3[numberOfVertices];
        Vector2[] uv = new Vector2[numberOfVertices];
        for (int sideI = 0; sideI < 2; sideI++) {
            float sideDirection = sideI == 1 ? -1 : 1;
            IndexedVertex[] side = sideI == 0 ? sideA : sideB;
            for (int x = 0; x < sideWidth; x++) {
                for (int y = 0; y < side.Length; y++) {
                    IndexedVertex mountainSideVertex = side[y];
                    
                    int index = ((numberOfVertices / 2) * sideI) + y * sideWidth + x;
                    float widthT = x / (float)(sideWidth - 1);
                    float mountainSideHeight = mountainSideVertex.vertex.y;
                    float height = baseY + (mountainSideHeight - baseY) * sideCurve.Evaluate(widthT);
                    float halfMountainWidth = sideWidthScale + mountain.generator.widthScale / 2;
                    float vertexX = (sideDirection * widthT * sideWidthScale) - (halfMountainWidth * sideDirection) + -sideI;
                    vertices[index] = new Vector3(vertexX, height, mountainSideVertex.vertex.z);
                    uv[index] = mountainMesh.uv[mountainSideVertex.index];
                    uv[index].x = sideCurve.Evaluate(widthT);
                }
            }
        }

        // Create Triangles
        List<int> triangles = new List<int>();
        for (int i = sideWidth; i < numberOfVertices - 1; i += 1) {
            int x = i % sideWidth;
            if (x == sideWidth - 1)
                continue;

            // Skip the center line
            int y = (i - x) / sideWidth;
            if (y == sideA.Length)
                continue;

            bool overHalfway = y > sideA.Length;

            // Triangle A
            triangles.Add(i);
            if (!overHalfway)
                triangles.Add(i + 1);
            triangles.Add(i - sideWidth);
            if (overHalfway)
                triangles.Add(i + 1);

            // Triangle B
            triangles.Add(i + 1);
            if (!overHalfway)
                triangles.Add(i - sideWidth + 1);
            triangles.Add(i - sideWidth);
            if (overHalfway)
                triangles.Add(i - sideWidth + 1);
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles.ToArray();
        mesh.uv = uv;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        meshFilter.sharedMesh = mesh;
        meshCollider.sharedMesh = mesh;
    }
}
