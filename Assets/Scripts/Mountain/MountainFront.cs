using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshCollider))]
public class MountainFront : MonoBehaviour {

    public Mountain mountain {
        get {
            return GetComponentInParent<Mountain>();
        }
    }

    public MountainSide mountainSide {
        get {
            return mountain.GetComponentInChildren<MountainSide>();
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

    public int length = 5;
    public float lengthScale = 10;
    public AnimationCurve lengthCurve;

    public class IndexedVertex {

        public Mesh mesh;
        public int index = 0;
        public Vector3 vertex;

        public IndexedVertex(Mesh mesh, int index, Vector3 vertex) {
            this.mesh = mesh;
            this.index = index;
            this.vertex = vertex;
        }
    }

    public void Regenerate() {
        Mesh mountainMesh = mountain.meshFilter.sharedMesh;
        Mesh mountainSideMesh = mountainSide.meshFilter.sharedMesh;

        // Find side vertices of mountain and sides (in correct order)
        List<IndexedVertex> side = new List<IndexedVertex>();
        // 1) Add mountainSide A frontSide
        int mountainSideA_Y = mountain.generator.length - 1;
        for (int mountainSideA_X = 0; mountainSideA_X < mountainSide.sideWidth; mountainSideA_X++) {
            int mountainSideA_Index = mountainSideA_Y * mountainSide.sideWidth + mountainSideA_X;
            side.Add(new IndexedVertex(mountainSideMesh, mountainSideA_Index, mountainSideMesh.vertices[mountainSideA_Index]));
        }

        // 2) Add mountain frontSide
        int mountainY = mountain.generator.length - 1;
        for (int mountainX = 0; mountainX < mountain.generator.width; mountainX++) {
            int mountainIndex = mountain.generator.XYToIndex(mountainX, mountainY);
            side.Add(new IndexedVertex(mountainMesh, mountainIndex, mountainMesh.vertices[mountainIndex]));
        }
        // 3) Add mountainSide B frontSide
        int mountainSideB_Y = mountain.generator.length - 1;
        for (int mountainSideB_X = mountainSide.sideWidth - 1; mountainSideB_X >= 0; mountainSideB_X--) {
            int half = mountainSide.sideWidth * mountain.generator.length;
            int mountainSideB_Index = half + mountainSideB_Y * mountainSide.sideWidth + mountainSideB_X;
            side.Add(new IndexedVertex(mountainSideMesh, mountainSideB_Index, mountainSideMesh.vertices[mountainSideB_Index]));
        }

        // Create Vertices (with uv) of this mesh
        int numberOfVertices = side.Count * length;
        Vector3[] vertices = new Vector3[numberOfVertices];
        Vector2[] uv = new Vector2[numberOfVertices];
        for (int x = 0; x < length; x++) {
            for (int y = 0; y < side.Count; y++) {
                IndexedVertex sideVertex = side[y];

                int index = y * length + x;
                float lengthT = 1 - (x / (float)(length - 1));
                float evalT = lengthCurve.Evaluate(lengthT);
                float sideHeight = sideVertex.vertex.y;

                float height = mountainSide.baseY + (sideHeight - mountainSide.baseY) * evalT;
                float vertexZ = lengthT * lengthScale;
                vertices[index] = new Vector3(sideVertex.vertex.x, height, vertexZ + mountain.generator.length - 1);
                uv[index] = sideVertex.mesh.uv[sideVertex.index];
                uv[index].y = evalT;
            }
        }

        // Create Triangles
        List<int> triangles = new List<int>();
        for (int i = length; i < numberOfVertices - 1; i += 1) {
            int x = i % length;
            if (x == length - 1)
                continue;

            // Triangle A
            triangles.Add(i);
            triangles.Add(i + 1);
            triangles.Add(i - length);

            // Triangle B
            triangles.Add(i + 1);
            triangles.Add(i - length + 1);
            triangles.Add(i - length);
        }

        // Create the Mesh
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
