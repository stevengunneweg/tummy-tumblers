using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshCollider))]
public class Mountain : MonoBehaviour {
    
    private Mesh originalMountainMesh;
    public AnimationCurve diffFromOriginalCurve;
    public float diffMaximum = 2f;

    #region Generator
    [System.Serializable]
    public class Generator {

        [Header("Vertices Amount")]
        public int width;
        public int length;

        public int NumberOfVertices {
            get {
                return width * length;
            }
        }

        public bool IsValidXY(int x, int y) {
            return x >= 0 && x < width && y >= 0 && y < length;
        }

        public int XYToIndex(int x, int y) {
            return y * width + x;
        }

        public int IndexToX(int index) {
            return index % width;
        }

        public int IndexToY(int index) {
            return (index - (index % width)) / width;
        }

        public int LocalPointToIndex(Vector3 localPoint) {
            float xT = localPoint.x / widthScale + 0.5f;
            float yT = localPoint.z / lengthScale;

            int x = Mathf.RoundToInt(xT * width);
            int y = Mathf.RoundToInt(yT * length);

            // Clamp to the mesh
            x = Math.Max(0, Mathf.Min(width - 1, x));
            y = Math.Max(0, Mathf.Min(length - 1, y));

            return XYToIndex(x, y);
        }

        public int[] GetIndicesAround(int index, int radius) {
            List<int> indices = new List<int>();
            int xCenter = IndexToX(index);
            int yCenter = IndexToY(index);
            for (int x = xCenter - radius; x <= xCenter; x++) {
                for (int y = yCenter - radius; y <= yCenter; y++) {
                    if ((x - xCenter) * (x - xCenter) + (y - yCenter) * (y - yCenter) <= radius * radius) {
                        int xSym = xCenter - (x - xCenter);
                        int ySym = yCenter - (y - yCenter);

                        if (IsValidXY(x, y))        indices.Add(XYToIndex(x, y));
                        if (IsValidXY(xSym, y))     indices.Add(XYToIndex(xSym, y));
                        if (IsValidXY(x, ySym))     indices.Add(XYToIndex(x, ySym));
                        if (IsValidXY(xSym, ySym))  indices.Add(XYToIndex(xSym, ySym));
                    }
                }
            }
            return indices.ToArray();
        }

        [Header("Vertices Scale")]
        public float widthScale;
        public float lengthScale;
        public float heightScale;

        [Header("UV Scale")]
        public float uvScaleWidth;
        public float uvScaleLength;

        public Generator() {
            width = 10;
            length = 100;

            uvScaleWidth = 1f;
            uvScaleLength = 1f;

            heightScale = 1f;
            widthScale = 1f;
            lengthScale = 10f;
        }

        public Mesh GenerateMesh() {
            Mesh mesh = new Mesh();

            // Calculate normal
            Vector3 a = new Vector3(0, -heightScale, lengthScale);
            Vector3 b = new Vector3(widthScale, -heightScale, 0);
            Vector3 defaultNormal = Vector3.Cross(a, b);
            if (defaultNormal.y < 0) {
                defaultNormal = defaultNormal * -1;
            }

            // Create vertices and UVs
            Vector3[] vertices = new Vector3[NumberOfVertices];
            Vector2[] uv = new Vector2[NumberOfVertices];
            Vector3[] normals = new Vector3[NumberOfVertices];
            for (int i = 0; i < NumberOfVertices; i++) {
                float xT = IndexToX(i) / (float)width;
                float yT = IndexToY(i) / (float)length;
                float hT = 1 - yT;
                vertices[i] = new Vector3((xT - 0.5f) * widthScale, hT * heightScale, yT * lengthScale);
                uv[i] = new Vector2(xT * uvScaleWidth, yT * uvScaleLength);
                normals[i] = defaultNormal;
            }

            // Create Triangles
            List<int> triangles = new List<int>();
            for (int i = width; i < NumberOfVertices - 1; i += 1) {
                int x = IndexToX(i);
                if (x == width - 1)
                    continue;

                // Triangle A
                triangles.Add(i);
                triangles.Add(i + 1);
                triangles.Add(i - width);

                // Triangle B
                triangles.Add(i + 1);
                triangles.Add(i - width + 1);
                triangles.Add(i - width);
            }

            // Link to mesh
            mesh.vertices = vertices;
            mesh.triangles = triangles.ToArray();
            mesh.uv = uv;
            mesh.normals = normals;
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            return mesh;
        }
    }

    [SerializeField, Header("Generator")]
    private Generator generator;

    private MeshFilter meshFilter {
        get {
            return GetComponent<MeshFilter>();
        }
    }
    private MeshCollider meshCollider {
        get {
            return GetComponent<MeshCollider>();
        }
    }
    
    [ContextMenu("Generate and Apply new Mesh")]
    public void GenerateAndApplyNewMesh() {
        Mesh mesh = generator.GenerateMesh();
        meshFilter.sharedMesh = mesh;
        meshCollider.sharedMesh = mesh;
    }
    #endregion

    protected void Start() {
        originalMountainMesh = meshFilter.sharedMesh;
        Mesh mesh = MeshUtils.CloneMesh(meshFilter.sharedMesh);
        meshFilter.sharedMesh = mesh;
        meshCollider.sharedMesh = mesh;
    }

    public void Increase(Vector3 worldPoint, float globalRadius, AnimationCurve falloffCurve, float amount) {
        Vector3 localPoint = transform.InverseTransformPoint(worldPoint);
        int localRadius = Mathf.CeilToInt(transform.InverseTransformVector(Vector3.up * globalRadius).magnitude);
        
        int centerVertexIndex = generator.LocalPointToIndex(localPoint);
        int[] indices = generator.GetIndicesAround(centerVertexIndex, localRadius);

        Mesh mesh = meshFilter.sharedMesh;
        Vector3[] vertices = mesh.vertices;
        SortedList<int, bool> usedIndices = new SortedList<int, bool>();
        foreach (int index in indices) {
            if (usedIndices.ContainsKey(index))
                continue;

            // Calculate new height
            float sqrDistanceToCenter = (new Vector2(vertices[index].x, vertices[index].z) - new Vector2(vertices[centerVertexIndex].x, vertices[centerVertexIndex].z)).sqrMagnitude;
            float distanceT = sqrDistanceToCenter / (localRadius * localRadius);
            vertices[index].y += amount * falloffCurve.Evaluate(1 - distanceT);

            // Limit max height differene
            if (originalMountainMesh != null) {
                float originalHeight = originalMountainMesh.vertices[index].y;
                float deltaLength = generator.IndexToY(index) / (float)generator.length;
                float diffAtThisDelta = diffMaximum * diffFromOriginalCurve.Evaluate(deltaLength);
                vertices[index].y = Mathf.Max(originalHeight - diffAtThisDelta, Mathf.Min(originalHeight + diffAtThisDelta, vertices[index].y));
            }

            // Cache that this index is now already changed
            usedIndices.Add(index, true);
        }

        mesh.vertices = vertices;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        meshCollider.sharedMesh = mesh;
    }

    [ContextMenu("Smooth (might take some time!)")]
    public void Smooth() {
        // Get the sourceMesh from the originalSkinnedMesh
        Mesh sourceMesh = meshFilter.sharedMesh;
        // Clone the sourceMesh 
        Mesh workingMesh = MeshUtils.CloneMesh(sourceMesh);
        // Reference workingMesh to see deformations
        meshFilter.sharedMesh = workingMesh;
        
        // Apply Laplacian Smoothing Filter to Mesh
        int iterations = 1;
        for (int i = 0; i < iterations; i++)
            //workingMesh.vertices = SmoothFilter.laplacianFilter(workingMesh.vertices, workingMesh.triangles);
            sourceMesh.vertices = SmoothFilter.hcFilter(workingMesh.vertices, sourceMesh.vertices, workingMesh.triangles, 0.0f, 0.5f);
    }

}
