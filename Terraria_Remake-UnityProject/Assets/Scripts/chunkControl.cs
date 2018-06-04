using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chunkControl : MonoBehaviour {
	//propriedades da MeshGrid do Chunk
	public Mesh mesh;

	public Vector2[] mainUVGrid;
	public Vector3[] mainVerticesGrid;
	public int[] mainTrianglesGrid;
	public int[] mainEstadoBloco;
	public int[] mainTipoBloco;

	public Vector2[] chunkUVGrid;
	public Vector3[] chunkVerticesGrid;
	public int[] chunkTrianglesGrid;
	public int[] chunkEstadoBloco;
	public int[] chunkTipoBloco;

	public int chunkMinSizeX;
	public int chunkMaxSizeX;
	public int chunkMinSizeY;
	public int chunkMaxSizeY;

	void Awake(){
		mesh = GetComponent<MeshFilter>().mesh;
	}

	void Start () {

		preencherChunk();
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	void preencherChunk(){
		//Especificando o tamanho dos Arrays
		chunkVerticesGrid = new Vector3[chunkMaxSizeX * chunkMaxSizeY * 4];
		chunkTrianglesGrid = new int[chunkMaxSizeX * chunkMaxSizeY * 6];
		chunkUVGrid = new Vector2[chunkMaxSizeX * chunkMaxSizeY * 4];

		//trackers values
		int v = chunkMinSizeX;
		int t = chunkMinSizeY;

		for( int x = chunkMinSizeX; chunkMinSizeX < chunkMaxSizeX; x++){
			for( int y = chunkMinSizeY; chunkMinSizeY < chunkMaxSizeY; y++){
				//preenchendo vertices do chunk
				chunkVerticesGrid [v] = mainVerticesGrid [v];
				chunkVerticesGrid [v+1] = mainVerticesGrid [v+1];
				chunkVerticesGrid [v+2] = mainVerticesGrid [v+2];
				chunkVerticesGrid [v+3] = mainVerticesGrid [v+3];

				//preenchendo triangulos do chunk
				chunkTrianglesGrid[t] = chunkTrianglesGrid[v];
				chunkTrianglesGrid[t+1] = chunkTrianglesGrid[t+4] = chunkTrianglesGrid[v+1];
				chunkTrianglesGrid[t+2] = chunkTrianglesGrid[t+3] = chunkTrianglesGrid[v+2];
				chunkTrianglesGrid[t+5] = chunkTrianglesGrid[v+3];

				//preenchendo uvs do chunk
				chunkUVGrid[v] = mainUVGrid[v];
				chunkUVGrid[v+1] = mainUVGrid[v+1];
				chunkUVGrid[v+2] = mainUVGrid[v+2];
				chunkUVGrid[v+3] = mainUVGrid[v+3];

				v += 4;
				t += 6;
			}
		}
	}

	void updateMesh(){
		mesh.Clear();
		mesh.vertices = chunkVerticesGrid;
		mesh.triangles = chunkTrianglesGrid;
		mesh.uv = chunkUVGrid;
		mesh.RecalculateNormals();
	}
}
