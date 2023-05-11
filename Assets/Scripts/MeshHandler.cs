using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshHandler : MonoBehaviour
{
    public int divisions = 3; // ���������� ���������� ��������� �� ���������

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        DrawLines();
    }

    private void Update()
    {
    }

    void DrawLines()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            Debug.LogError("MeshFilter component not found on the plane object!");
            return;
        }

        Mesh mesh = meshFilter.sharedMesh;
        if (mesh == null)
        {
            Debug.LogError("Mesh not found!");
            return;
        }

        Vector3[] vertices = mesh.vertices;
        Bounds bounds = mesh.bounds;
        float height = bounds.size.z;

        // ������ ��������� ����� �������
        float interval = height / (divisions + 1);

        // �������� ������� ������
        Vector3[] positions = new Vector3[divisions * 2];
        int index = 0;

        // ���������� ������ ��� �����
        for (int i = 1; i <= divisions; i++)
        {
            float y = bounds.min.z + i * interval;
            Vector3 start = new Vector3(bounds.min.x, .1f, y);
            Vector3 end = new Vector3(bounds.max.x, .1f, y);
            positions[index] = start;
            positions[index + 1] = end;
            index += 2;
        }

        // ��������� ���������� LineRenderer
        lineRenderer.positionCount = divisions;
        lineRenderer.SetPositions(positions);
        lineRenderer.startWidth = .5f;
        lineRenderer.endWidth = .5f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.material.color = Color.red;
    }

}
