using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

    void Start()
    {
        var filter = GetComponent<MeshFilter>();
        var mesh = new Mesh();
        filter.mesh = mesh;

        // Vertices
        // locations of vertices
        var verts = new Vector3[8];

        //A
        verts[0] = new Vector3(0, 0, 0);
        verts[1] = new Vector3(1, 0, 0);
        verts[2] = new Vector3(0, 1, 0);
        verts[3] = new Vector3(1, 1, 0);
        verts[4] = new Vector3(0, 0, 1);
        verts[5] = new Vector3(1, 0, 1);
        verts[6] = new Vector3(0, 1, 1);
        verts[7] = new Vector3(1, 1, 1);

        mesh.vertices = verts;

        // Indices
        // determines which vertices make up an individual triangle
        var indices = new int[36];

        //A
        indices[0] = 0;
        indices[1] = 2;
        indices[2] = 1;
        indices[3] = 1;
        indices[4] = 2;
        indices[5] = 3;

        //B
        indices[6] = 0;
        indices[7] = 4;
        indices[8] = 6;
        indices[9] = 6;
        indices[10] = 2;
        indices[11] = 0;

        //C
        indices[12] = 5;
        indices[13] = 7;
        indices[14] = 6;
        indices[15] = 5;
        indices[16] = 6;
        indices[17] = 4;

        //D
        indices[18] = 1;
        indices[19] = 3;
        indices[20] = 5;
        indices[21] = 3;
        indices[22] = 7;
        indices[23] = 5;

        //E
        indices[24] = 6;
        indices[25] = 7;
        indices[26] = 2;
        indices[27] = 2;
        indices[28] = 7;
        indices[29] = 3;
       
        //F
        indices[30] = 0;
        indices[31] = 1;
        indices[32] = 4;
        indices[33] = 4;
        indices[34] = 1;
        indices[35] = 5;


        mesh.triangles = indices;

        // Normals
        // describes how light bounces off the surface (at the vertex level)
        var norms = new Vector3[8];

        norms[0] = -Vector3.forward;
        norms[1] = -Vector3.forward;
        norms[2] = -Vector3.forward;
        norms[3] = -Vector3.forward;
        norms[4] = -Vector3.forward;
        norms[5] = -Vector3.forward;
        norms[6] = -Vector3.forward;
        norms[7] = -Vector3.forward;

        mesh.normals = norms;

        // UVs, STs
        // defines how textures are mapped onto the surface
        var UVs = new Vector2[8];

        UVs[0] = new Vector2(0, 0);
        UVs[1] = new Vector2(1, 0);
        UVs[2] = new Vector2(0, 1);
        UVs[3] = new Vector2(1, 1);

        UVs[4] = new Vector2(0, 0);
        UVs[5] = new Vector2(1, 0);
        UVs[6] = new Vector2(0, 1);
        UVs[7] = new Vector2(1, 1);

        mesh.uv = UVs;
    }

    public bool isEnabled = true;
    public float vertexSize = .1f;
    public List<Color> vertexColors = new List<Color>(new Color[1] { Color.red });
    private void OnDrawGizmos()
    {
        if (isEnabled)
        {
            int i = 0;
            foreach (Vector3 vert in GetComponent<MeshFilter>().mesh.vertices)
            {
                Vector3 vertexPlace = Camera.main.WorldToScreenPoint(transform.TransformPoint(vert));
                Vector2 screenLocation = new Vector2(vertexPlace.x, vertexPlace.y);
                if (i >= vertexColors.Count)
                {
                    Gizmos.DrawSphere(transform.TransformPoint(vert), vertexSize);
                }
                else
                {
                    Gizmos.color = vertexColors[i];
                    drawString("Vertex # " + i, transform.TransformPoint(vert));
                    Gizmos.DrawSphere(transform.TransformPoint(vert), vertexSize);
                    i++;
                }
            }
        }
    }
    static public void drawString(string text, Vector3 worldPos, Color? colour = null)
    {
        UnityEditor.Handles.BeginGUI();

        var restoreColor = GUI.color;

        if (colour.HasValue) GUI.color = colour.Value;
        var view = UnityEditor.SceneView.currentDrawingSceneView;
        if (view != null)
        {
            Vector3 screenPos = view.camera.WorldToScreenPoint(worldPos);

            if (screenPos.y < 0 || screenPos.y > Screen.height || screenPos.x < 0 || screenPos.x > Screen.width || screenPos.z < 0)
            {
                GUI.color = restoreColor;
                UnityEditor.Handles.EndGUI();
                return;
            }

            Vector2 size = GUI.skin.label.CalcSize(new GUIContent(text));
            GUI.Label(new Rect(screenPos.x - (size.x / 2), -screenPos.y + view.position.height + 4, size.x, size.y), text);
            GUI.color = restoreColor;
            UnityEditor.Handles.EndGUI();
        }

    }
}
