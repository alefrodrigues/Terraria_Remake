using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meshGrid : MonoBehaviour {
	//propriedades da MeshGrid
	public Mesh mesh;
	public Vector2[] uvGrid;
	public Vector3[] verticesGrid;
	public int[] trianglesGrid;
	public int cellSize;
	public int gridSizeX;
	public int gridSizeY;

	//propriedades da funcao updateUV
	public float resolucao;
	public float cutSize;
	//valores a serem mudados na UVGRID
	public Vector2 uv1;
	public Vector2 uv2;
	public Vector2 uv3;
	public Vector2 uv4;

	void Awake(){
		mesh = GetComponent<MeshFilter>().mesh;
	}

	void Start () {
		makeMeshGrid();

		updateMesh();


	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.U)){
			updateUV(resolucao,cutSize);
			updateMesh();
		}
	}

	void makeMeshGrid(){
		//Especificando o tamanho dos Arrays
		verticesGrid = new Vector3[gridSizeX * gridSizeY * 4];
		trianglesGrid = new int[gridSizeX * gridSizeY * 6];
		uvGrid = new Vector2[gridSizeX * gridSizeY * 4];

		//trackers values
		int v = 0;
		int t = 0;

		for(int x = 0; x < gridSizeX; x++){
			for(int y = 0; y < gridSizeY; y++){
				//criando vertices
				verticesGrid [v] = new Vector3 (x,y,0);
				verticesGrid [v+1] = new Vector3 (x,y+1,0);
				verticesGrid [v+2] = new Vector3 (x+1,y,0);
				verticesGrid [v+3] = new Vector3 (x+1,y+1,0);

				//criando triangulos
				trianglesGrid[t] = v;
				trianglesGrid[t+1] = trianglesGrid[t+4] = v+ 1;
				trianglesGrid[t+2] = trianglesGrid[t+3] = v+ 2;
				trianglesGrid[t+5] = v + 3;

				//criando uvs
				uvGrid[v] = new Vector2(0.25f,0.25f);
				uvGrid[v+1] = new Vector2(0.25f,0.75f);
				uvGrid[v+2] = new Vector2(0.75f,0.25f);
				uvGrid[v+3] = new Vector2(0.75f,0.75f);

				v += 4;
				t += 6;
			}
		}
	}

	void updateMesh(){
		mesh.Clear();
		mesh.vertices = verticesGrid;
		mesh.triangles = trianglesGrid;
		mesh.uv = uvGrid;
		mesh.RecalculateNormals();
	}

	void updateUV(float _resolucao, float _cutSize){
		int uvTracker  = 0;
		uv1 = new Vector2(uv1.x*cutSize/_resolucao,uv1.y*cutSize/_resolucao);
		uv2 = new Vector2(uv2.x*cutSize/_resolucao,uv2.y*cutSize/_resolucao);
		uv3 = new Vector2(uv3.x*cutSize/_resolucao,uv3.y*cutSize/_resolucao);
		uv4 = new Vector2(uv4.x*cutSize/_resolucao,uv4.y*cutSize/_resolucao);

		for(int x = 0; x < gridSizeX; x++){
			for(int y = 0; y < gridSizeY; y++){
				uvGrid[uvTracker] = uv1;
				uvGrid[uvTracker+1] = uv2;
				uvGrid[uvTracker+2] = uv3;
				uvGrid[uvTracker+3] = uv4;

				uvTracker += 4;
			}
		}
	}
}
