using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proceduralGrid : MonoBehaviour {

	public List <Vector2> uvList = new List <Vector2>();

	public Mesh mesh;
	public Vector3[] vertices;
	public int[] triangles;
	public Vector2[] uvs;


	public Vector3 gridOffSet;
	public int gridSize;
	public int gridSizeX;
	public int gridSizeY;
	public float cellSize = 1;

	void Awake(){
		mesh = GetComponent<MeshFilter>().mesh;
	}
	void Start () {
		
		
	}
	void Update(){
		if(Input.GetKeyDown(KeyCode.Space)){
			makeDiscreteGrid();
			updateMesh();
		}
	}
	
	void makeDiscreteGrid () {
		//Set array sizes
		vertices = new Vector3[gridSizeX * gridSizeY * 4];
		triangles = new int[gridSizeX * gridSizeY * 6];

		//Set trackers integer 
		int v = 0;
		int t = 0;

		//Set VertexOffSet
		float VertexOffSet = cellSize * 0.5f;

		//Create vertex grid
		for(int x = 0; x < gridSizeX; x++){
			for(int y = 0; y < gridSizeY; y++){
				Vector3 cellOffSet = new Vector3(x * cellSize,0,y * cellSize);

				vertices[v] = new Vector3( -VertexOffSet, 0, -VertexOffSet ) + cellOffSet + gridOffSet;
				vertices[v+1] = new Vector3( -VertexOffSet, 0, VertexOffSet ) + cellOffSet  + gridOffSet;
				vertices[v+2] = new Vector3( VertexOffSet, 0, -VertexOffSet ) + cellOffSet  + gridOffSet;
				vertices[v+3] = new Vector3( VertexOffSet, 0, VertexOffSet ) + cellOffSet  + gridOffSet;

				triangles[t] = v;
				triangles[t+1] = triangles[t+4] = v+ 1;
				triangles[t+2] = triangles[t+3] = v+ 2;
				triangles[t+5] = v + 3;

				if(y < gridSize -1){
					setUvBlock(4);
				}else {
					setUvBlock(3);
				}
				

				v += 4;
				t += 6;

			}
		}
	}

	void updateMesh(){
		mesh.Clear();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.uv = uvList.ToArray();
		mesh.RecalculateNormals();

	}

	void setUvBlock (int _idTypeBloco) {

		switch (_idTypeBloco)
		{
		    case 0:
		    	//bloco1
		        uvList.Add(new Vector2(0f,0f));
				uvList.Add(new Vector2(0f,0.5f));
				uvList.Add(new Vector2(0.5f,0f));
				uvList.Add(new Vector2(0.5f,0.5f));
		        break;
		    case 1:
		        //bloco2
				uvList.Add(new Vector2(0f,0.5f));
				uvList.Add(new Vector2(0f,1f));
				uvList.Add(new Vector2(0.5f,0.5f));
				uvList.Add(new Vector2(0.5f,1f));
		        break;
		    case 2:
		        //bloco3
				uvList.Add(new Vector2(0.5f,0f));
				uvList.Add(new Vector2(0.5f,0.5f));
				uvList.Add(new Vector2(1f,0f));
				uvList.Add(new Vector2(1f,0.5f));
		        break;
		    case 3:
		        //bloco4
				uvList.Add(new Vector2(0.5f,0.5f));
				uvList.Add(new Vector2(0.5f,1f));
				uvList.Add(new Vector2(1f,0.5f));
				uvList.Add(new Vector2(1f,1f));
		        break;
		     case 4:
		        //bloco4
				uvList.Add(new Vector2(0.25f,0.25f));
				uvList.Add(new Vector2(0.25f,0.75f));
				uvList.Add(new Vector2(0.75f,0.25f));
				uvList.Add(new Vector2(0.75f,0.75f));
		        break;
		    
		}
	}
}
