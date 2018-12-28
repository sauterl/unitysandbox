using UnityEngine;

namespace DefaultNamespace
{
    public static class ModelFactory
    {

        /// <summary>
        /// Creates a wall between two positions
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="height"></param>
        /// <param name="materialName"></param>
        /// <returns></returns>
        public static GameObject CreatePositionedWall(Vector3 start, Vector3 end, float height, string materialName = null)
        {
            float width = Vector3.Distance(start, end);
            float a = Vector3.Angle(end-start, Vector3.right);
            GameObject go = new GameObject("PositionedWall");
            GameObject wall = CreateWall(width, height, materialName);
            
            wall.transform.parent = go.transform;
            wall.transform.position = Vector3.zero;
            wall.transform.RotateAround(Vector3.zero, Vector3.up,-a);
            go.transform.position = start;
            return go;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position">center of room</param>
        /// <param name="size"></param>
        /// <param name="height"></param>
        /// <param name="materialNames">0 floor, 1 ceiling, 2 north (pos z), 3 east (pos x), 4 south (neg z), 5 west (neg x)</param>
        /// <returns></returns>
        public static GameObject CreateSquareRoom(Vector3 position, float size, float height, string[] materialNames)
        {
            GameObject root = new GameObject("SquareRoom");
            
            
            return null;
        }

        
        

        /// <summary>
        /// Creates a wall game object to position later.
        /// The wall is flat and always "upright", e.g. the normal of the mesh is negative z.
        /// Use the resulting gameobject to rotate and re-position the wall.
        /// </summary>
        /// <param name="width">The width of the wall in Unity units</param>
        /// <param name="height">The height of the wall in Unity units</param>
        /// <param name="materialName">The wall's material name. Expects the material file to be at Resources/Materials/materalName. If no present, the word Material will be suffixed.</param>
        /// <returns></returns>
        public static GameObject CreateWall(float width, float height, string materialName = null)
        {
            GameObject go = new GameObject("Wall");
            MeshFilter meshFilter = go.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = go.AddComponent<MeshRenderer>();
            Mesh mesh = meshFilter.mesh;
            Vector3[] vertices = new Vector3[4];
            vertices[0] = new Vector3(0, 0, 0);
            vertices[1] = new Vector3(width, 0, 0);
            vertices[2] = new Vector3(0, height, 0);
            vertices[3] = new Vector3(width, height, 0);

            mesh.vertices = vertices;

            int[] tri= new int[6];

            tri[0] = 0;
            tri[1] = 2;
            tri[2] = 1;
    
            tri[3] = 2;
            tri[4] = 3;
            tri[5] = 1;
    
            mesh.triangles = tri;
    
            Vector3[] normals  = new Vector3[4];
    
            normals[0] = -Vector3.forward;
            normals[1] = -Vector3.forward;
            normals[2] = -Vector3.forward;
            normals[3] = -Vector3.forward;
    
            mesh.normals = normals;
    
            Vector2[] uv = new Vector2[4];

            float xUnit = 1;
            float yUnit = 1;
            
            if (width > height)
            {
                yUnit = height / width;
            }
            else
            {
                xUnit = width / height;
            }
            
            uv[0] = new Vector2(0, 0);
            uv[1] = new Vector2(xUnit, 0);
            uv[2] = new Vector2(0, yUnit);
            uv[3] = new Vector2(xUnit, yUnit);
    
            mesh.uv = uv;
            
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            mesh.RecalculateTangents();

            if (!string.IsNullOrEmpty(materialName))
            {
                if (!materialName.EndsWith("Material"))
                {
                    materialName = materialName + "Material";
                }
                Material mat =  Resources.Load<Material>("Materials/" + materialName);
                meshRenderer.material.CopyPropertiesFromMaterial(mat);
                //meshRenderer.material.SetTextureScale("_MainTex", new Vector2(1,1));
                meshRenderer.material.name = mat.name;



            }
            
            
            return go;
        }
    }
}