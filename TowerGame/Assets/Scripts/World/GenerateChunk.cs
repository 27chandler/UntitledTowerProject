using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[ExecuteInEditMode]
public class GenerateChunk : MonoBehaviour
{
    private Mesh mesh;
    [SerializeField] private MeshFilter filter;
    [SerializeField] private MeshCollider collider;
    [SerializeField] private bool activateDelete = false;
    [SerializeField] private bool isDeleteDone = false;
    [SerializeField] private bool doRegenTerrain = false;
    [SerializeField] private Transform sphere;
    [SerializeField] private Vector3 deletePosition;
    [SerializeField] private Vector3Int size = new Vector3Int(8, 8, 8);
    
    private Vector3 pointPos;
    private Vector3 gridPos;
    private Vector3 gridOffset = new Vector3(-1.0f, 0.0f, -1.0f);

    private List<Voxel> Voxels = new List<Voxel>();

    public class Voxel
    {
        public int id;
        public Quad[] faces;
        public Vector3 position;
        public Vector3Int gridPosition;
        public List<Vector3> verts = new List<Vector3>();
        public List<int> ebos = new List<int>();
        public Quad.DIRECTION directionFlags =
            Quad.DIRECTION.UP | 
            Quad.DIRECTION.DOWN | 
            Quad.DIRECTION.LEFT | 
            Quad.DIRECTION.RIGHT | 
            Quad.DIRECTION.FORWARD | 
            Quad.DIRECTION.BACKWARD;

        public Voxel(Vector3 position, int id)
        {
            this.position = position;
            this.id = id;

            RegenFaces();
        }

        public void RegenFaces()
        {
            verts.Clear();
            ebos.Clear();

            gridPosition = new Vector3Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y), Mathf.RoundToInt(position.z));
            faces = new Quad[]
            {
                new Quad(position, Quad.DIRECTION.UP),
                new Quad(position + new Vector3(1.0f,-1.0f,0.0f), Quad.DIRECTION.DOWN),
                new Quad(position + new Vector3(0.0f,-1.0f,0.0f), Quad.DIRECTION.RIGHT),
                new Quad(position + new Vector3(1.0f, 0.0f,0.0f), Quad.DIRECTION.LEFT),
                new Quad(position + new Vector3(0.0f, 0.0f,1.0f), Quad.DIRECTION.FORWARD),
                new Quad(position + new Vector3(0.0f,-1.0f,0.0f), Quad.DIRECTION.BACKWARD),
            };

            foreach (var face in faces)
            {
                if ((face.dir & directionFlags) == face.dir)
                {
                    foreach (var vert in face.verts)
                    {
                        verts.Add(vert);
                    }
                }
                else
                {
                    Debug.Log("Face is blocked");
                }
            }

            int face_id = 0;
            foreach (var face in faces)
            {
                if ((face.dir & directionFlags) == face.dir)
                {
                    foreach (var ebo in face.ebo)
                    {
                        ebos.Add((face_id * 4) + ebo);
                    }
                    face_id++;
                }
            }
        }

        public void DisableFace(Quad.DIRECTION face)
        {
            directionFlags = directionFlags & ~face;

            RegenFaces();
        }
    }
    public class Quad
    {
        [Flags]
        public enum DIRECTION
        {
            NONE = 0,
            UP = 1,
            DOWN = 2,
            LEFT = 4,
            RIGHT = 8,
            FORWARD = 16,
            BACKWARD = 32
        }

        private Vector3 topRightPostion = new Vector3(0.0f,0.0f,0.0f);
        private Vector3 normal = new Vector3(0.0f, 1.0f, 0.0f);
        private Vector3 right;
        public DIRECTION dir;
        public Vector3[] verts;
        public int[] ebo = new int[]
        {
            0,1,2,1,3,2
        };

        public Vector2 FindRotation(DIRECTION direction)
        {
            Vector2 return_rotation = new Vector2();

            switch (direction)
            {
                case DIRECTION.UP:
                    {
                        return_rotation = new Vector2(0.0f, 0.0f);
                        break;
                    }
                case DIRECTION.DOWN:
                    {
                        return_rotation = new Vector2(180.0f, 0.0f);
                        break;
                    }
                case DIRECTION.LEFT:
                    {
                        return_rotation = new Vector2(270.0f, 0.0f);
                        break;
                    }
                case DIRECTION.RIGHT:
                    {
                        return_rotation = new Vector2(90.0f, 0.0f);
                        break;
                    }
                case DIRECTION.FORWARD:
                    {
                        return_rotation = new Vector2(0.0f, 90.0f);
                        break;
                    }
                case DIRECTION.BACKWARD:
                    {
                        return_rotation = new Vector2(0.0f, 270.0f);
                        break;
                    }
            }
            return return_rotation;
        }

        public Quad(Vector3 position, DIRECTION direction)
        {
            topRightPostion = position;
            Vector2 rotation = FindRotation(direction);
            dir = direction;

            verts = new Vector3[]
            {
                new Vector3(0.0f,0.0f,0.0f) + position, // Bottom Left
                (Quaternion.AngleAxis(rotation.y, Vector3.right) * Quaternion.AngleAxis(rotation.x, Vector3.forward) * new Vector3(0.0f,0.0f,1.0f)) + position, // Top Left
                (Quaternion.AngleAxis(rotation.y, Vector3.right) * Quaternion.AngleAxis(rotation.x, Vector3.forward) * new Vector3(1.0f,0.0f,0.0f)) + position, // Bottom Right
                (Quaternion.AngleAxis(rotation.y, Vector3.right) * Quaternion.AngleAxis(rotation.x, Vector3.forward) * new Vector3(1.0f,0.0f,1.0f)) + position // Top Right
            };
        }
        

    }
    // Start is called before the first frame update
    void Start()
    {
        GenerateMesh();
        UpdateMesh();
    }

    private void Update()
    {
        if (doRegenTerrain) {
            doRegenTerrain = false;
            GenerateMesh();
            UpdateMesh();
        }

        deletePosition = sphere.position;

        if (activateDelete && !isDeleteDone)
        {
            DeleteVoxel(FindGridPosition(deletePosition));
            isDeleteDone = true;
        }
    }

    public Vector3Int FindGridPosition(Vector3 position)
    {
        pointPos = position;


        Vector3 local_position = transform.InverseTransformPoint(position) + gridOffset;
        Vector3Int grid_position = new Vector3Int(Mathf.CeilToInt(local_position.x), Mathf.CeilToInt(local_position.y), Mathf.CeilToInt(local_position.z));
        Debug.Log(grid_position);

        gridPos = grid_position;

        return grid_position;
    }

    public bool HasVoxel(Vector3Int position)
    {
        Voxel found_voxel = Voxels.Find(x => x.gridPosition == position);

        if (found_voxel != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DeleteVoxel(Vector3Int position)
    {
        Voxel found_voxel = Voxels.Find(x => x.gridPosition == position);

        if (found_voxel != null)
        {
            Voxels.Remove(found_voxel);
            UpdateMesh();
        }
        else
        {
            Debug.LogError("ERROR, VOXEL NOT FOUND " + gameObject.name + position);
        }
    }

    private void GenerateMesh()
    {
        mesh = new Mesh();
        mesh.Clear();
        Voxels.Clear();

        float separation = 1.0f;

        //List<Vector3> verts = new List<Vector3>();
        //List<int> ebos = new List<int>();

        int id_counter = 0;
        for (int y = 0; y < size.y; y++)
        {
            for (int z = 0; z < size.z; z++)
            {
                for (int x = 0; x < size.x; x++)
                {
                    //int random = Random.Range(0, 2);

                    //if (random == 0)
                    //{
                    //    continue;
                    //}

                    int new_id = id_counter;
                    Voxel cube = new Voxel(new Vector3((float)x * separation, (float)-y * separation, (float)z * separation), new_id);
                    Voxels.Add(cube);
                    //verts.AddRange(cube.verts);

                    //foreach (var ebo in cube.ebos)
                    //{
                    //    ebos.Add((cube.id * 24) + ebo);
                    //}

                    id_counter++;
                }
            }
        }

        //mesh.vertices = verts.ToArray();
        //mesh.triangles = ebos.ToArray();

        //filter.mesh = mesh;
        //collider.sharedMesh = mesh;
        //mesh.RecalculateNormals();
    }

    private void UpdateMesh()
    {
        List<Vector3> verts = new List<Vector3>();
        List<int> ebos = new List<int>();

        // Hide unsee-able faces (When another voxel is adjacent to it)
        foreach (var voxel in Voxels)
        {
            if (HasVoxel(voxel.gridPosition + new Vector3Int(0, 1, 0)))
            {
                voxel.DisableFace(Quad.DIRECTION.UP);
            }

            if (HasVoxel(voxel.gridPosition + new Vector3Int(0, -1, 0)))
            {
                voxel.DisableFace(Quad.DIRECTION.DOWN);
            }
        }

        int id_counter = 0;

        foreach (var voxel in Voxels)
        {
            verts.AddRange(voxel.verts);

            foreach (var ebo in voxel.ebos)
            {
                ebos.Add((id_counter * voxel.verts.Count) + ebo);
            }


            id_counter++;
        }

        mesh.triangles = null;
        mesh.vertices = null;

        mesh.vertices = verts.ToArray();
        mesh.triangles = ebos.ToArray();

        Debug.Log("Generated Mesh: Verts: " + verts.Count + " ebos: " + ebos[ebos.Count - 2]);

        filter.mesh = mesh;
        collider.sharedMesh = mesh;
        mesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(pointPos, 0.1f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + gridPos, 0.1f);
    }
}
