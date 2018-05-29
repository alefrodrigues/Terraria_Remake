using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proceduralGrid : MonoBehaviour {
	public List <Vector2> uvList = new List <Vector2>();

	public MeshCollider meshCollider;
	public Mesh mesh;

	public Vector3[] vertices;
	public int[] triangles;
	public Vector2[] uvs;


	public Vector3 gridOffSet;
	public int gridSizeX;
	public int gridSizeY;
	public float cellSize = 1;

	public Transform player;

	public int blocoParaDeletar;
	public bool canCreate = true;


	public Camera myCam;
	void Awake(){
		mesh = GetComponent<MeshFilter>().mesh;
	}
	void Start () {

	}
	void Update(){
		if(Input.GetKeyDown(KeyCode.Space) && canCreate){
			makeDiscreteGrid();
			updateMesh();
			canCreate = false;
		}
		if(Input.GetKeyDown(KeyCode.Mouse0)){
			deleteBlock(blocoParaDeletar);
			takeMousePositionOnWorld();
		}

		if(Input.GetKeyDown(KeyCode.Mouse1)){
			createBlock(blocoParaDeletar);
		}
	}
	
	void makeDiscreteGrid () {
		//Set array sizes
		vertices = new Vector3[gridSizeX * gridSizeY * 4];
		uvs = new Vector2[gridSizeX * gridSizeY * 4];
		triangles = new int[gridSizeX * gridSizeY * 6];

		//Set trackers integer 
		int v = 0;
		int t = 0;

		//Set VertexOffSet
		float VertexOffSet = cellSize * 0.5f;

		//Create vertex grid
		for(int x = 0; x < gridSizeX; x++){
			for(int y = 0; y < gridSizeY; y++){
				Vector3 cellOffSet = new Vector3(x * cellSize,y * cellSize, 0);

				vertices[v] = new Vector3( -VertexOffSet, -VertexOffSet, 0 ) + cellOffSet + gridOffSet;
				vertices[v+1] = new Vector3( -VertexOffSet, VertexOffSet, 0 ) + cellOffSet  + gridOffSet;
				vertices[v+2] = new Vector3( VertexOffSet, -VertexOffSet, 0 ) + cellOffSet  + gridOffSet;
				vertices[v+3] = new Vector3( VertexOffSet, VertexOffSet, 0 ) + cellOffSet  + gridOffSet;

				triangles[t] = v;
				triangles[t+1] = triangles[t+4] = v+ 1;
				triangles[t+2] = triangles[t+3] = v+ 2;
				triangles[t+5] = v + 3;

				setUvBlock(4,v);

				v += 4;
				t += 6;


			}
		}
	}

	void updateMesh(){
		mesh.Clear();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.uv = uvs;
		mesh.RecalculateNormals();
		GetComponent<MeshCollider>().sharedMesh = mesh;
	}

	void createBlock(int idBlock){
		int vBlock = idBlock*4;
		int tBlock = idBlock*6;
		//Criando Triangulos
		triangles[tBlock] = vBlock;
		triangles[tBlock+1] = triangles[tBlock+4] = vBlock + 1;
		triangles[tBlock+2] = triangles[tBlock+3] = vBlock + 2;
		triangles[tBlock+5] = vBlock + 3;

		updateMesh();

	}
	void deleteBlock(int idBlock){
		Debug.Log("Bloco deletado: "+ blocoParaDeletar);
		int tBlock = idBlock*6;

		//deletando triangulos
		triangles[tBlock] = tBlock;
		triangles[tBlock+1] =triangles[tBlock+4] = tBlock;
		triangles[tBlock+2] =triangles[tBlock+3] = tBlock;
		triangles[tBlock+5] = tBlock;

		updateMesh();
	}

	void takeMousePositionOnWorld(){
		RaycastHit vHit = new RaycastHit();
        Ray vRay = myCam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(vRay, out vHit, 1000)) 
        {
            print(vHit.point);
        }
	}
	void setUvBlock (int _idTypeBloco,int Vertex) {

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

				uvs[Vertex] = new Vector2(0.25f,0.25f);
				uvs[Vertex+1] = new Vector2(0.25f,0.75f);
				uvs[Vertex+2] = new Vector2(0.75f,0.25f);
				uvs[Vertex+3] = new Vector2(0.75f,0.75f);
		        break;
		    
		}
	}
}
