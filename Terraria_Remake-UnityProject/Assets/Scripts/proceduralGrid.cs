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

	public int idBlocoDeletar;
	public int idBlocoCriar;

	public Camera myCam;

	public GameObject obj1;
	public GameObject obj2;
	RaycastHit hit; 
	void Awake(){
		mesh = GetComponent<MeshFilter>().mesh;

	}
	void Start () {

	}
	void FixedUpdate(){
		if(Input.GetKeyDown(KeyCode.Space)){
			makeDiscreteGrid();
			updateMesh();
		}
		if(Input.GetKeyDown(KeyCode.X)){
			deletarBloco (idBlocoDeletar);
		}

		if(Input.GetKeyDown(KeyCode.C)){
			criarBloco(idBlocoDeletar);
		}

		if (Input.GetKey(KeyCode.Mouse0)) {
			Vector2 p = myCam.ScreenToWorldPoint (new Vector3(Input.mousePosition.x,Input.mousePosition.y));

			Vector2 posMouse = p;
	
			int posX =  Mathf.FloorToInt(Mathf.Round(posMouse.x - 0.5f));
			int posY = Mathf.FloorToInt(Mathf.Round(posMouse.y - 0.5f));
			print (posX+" , "+posY);

			int valorTriangulo = (posY + (posX * 100));

			deletarBloco (valorTriangulo);
		}

		if (Input.GetKey(KeyCode.Mouse1)) {
			Vector2 p = myCam.ScreenToWorldPoint (new Vector3(Input.mousePosition.x,Input.mousePosition.y));

			Vector2 posMouse = p;

			int posX =  Mathf.FloorToInt(Mathf.Round(posMouse.x - 0.5f));
			int posY = Mathf.FloorToInt(Mathf.Round(posMouse.y - 0.5f));
			print (posX+" , "+posY);

			int valorTriangulo = (posY + (posX * 100));

			criarBloco (valorTriangulo);
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
				Vector3 cellOffSet = new Vector3(x * cellSize,y * cellSize, 0);

				vertices [v] = new Vector3 (x,y,0);
				vertices [v+1] = new Vector3 (x,y+1,0);
				vertices [v+2] = new Vector3 (x+1,y,0);
				vertices [v+3] = new Vector3 (x+1,y+1,0);

				/*vertices[v] = new Vector3( -VertexOffSet, -VertexOffSet, 0 ) + cellOffSet + gridOffSet;
				vertices[v+1] = new Vector3( -VertexOffSet, VertexOffSet, 0 ) + cellOffSet  + gridOffSet;
				vertices[v+2] = new Vector3( VertexOffSet, -VertexOffSet, 0 ) + cellOffSet  + gridOffSet;
				vertices[v+3] = new Vector3( VertexOffSet, VertexOffSet, 0 ) + cellOffSet  + gridOffSet;*/

				triangles[t] = v;
				triangles[t+1] = triangles[t+4] = v+ 1;
				triangles[t+2] = triangles[t+3] = v+ 2;
				triangles[t+5] = v + 3;
				setUvBlock(4);

				//print (vertices[v] + " ," +vertices[v+1] + " ," +vertices[v+2] + " ," +vertices[v+3]);

				v += 4;
				t += 6;

			}
		}
	}

	void deletarBloco(int idBloco){
		idBloco = idBloco * 4;
		for(int i=0;i<=triangles.Length;i++){
			if(triangles[i] == idBloco && idBloco <= triangles.Length){
				triangles [i] = idBloco;
				triangles [i + 1] = triangles [i + 4] = idBloco;
				triangles [i + 2] = triangles [i + 3] = idBloco;
				triangles [i + 5] = idBloco;
				break;
			}
		}
		updateMesh ();
	}

	void criarBloco(int idBloco){
		idBloco = idBloco * 4;
		for(int i=0;i<=triangles.Length;i++){
			if(triangles[i] == idBloco && idBloco <= triangles.Length){
				triangles [i] = idBloco;
				triangles [i + 1] = triangles [i + 4] = idBloco+1;
				triangles [i + 2] = triangles [i + 3] = idBloco+2;
				triangles [i + 5] = idBloco+3;
				break;
			}
		}
		updateMesh ();
	}

	void updateMesh(){
		mesh.Clear();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.uv = uvList.ToArray();
		mesh.RecalculateNormals();
		GetComponent<MeshCollider>().sharedMesh = mesh;
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
