using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proceduralGrid : MonoBehaviour {
	List <int> blocosEmVolta = new List <int> ();

	//arrays para montar a MeshGrid
	public List <Vector3> verticesList = new List <Vector3>();
	public List <Vector2> uvList = new List <Vector2>();
	public List <Vector2> colList = new List <Vector2>();
	public List <int> trianglesList = new List <int>();
	public List <int> tipoBloco = new List <int>();
	public List <int> estadoBloco = new List <int>();

	//colisoes da MeshGrid
	public MeshCollider meshCollider;

	//MeshGrid
	public Mesh mesh;

	//tamanho da MeshGrid;
	public int gridSizeX;
	public int gridSizeY;

	//mainCamera
	public Camera myCam;

	//autorizaçao para so chunks começar a se preencher de dados das listas principais
	public static bool podePreencher;
	

	void Awake(){
		mesh = GetComponent<MeshFilter>().mesh;
		meshCollider = GetComponent<MeshCollider> ();
	}
	void Start () {

	}
	void Update(){
		//CRIAR MESHGRID
		if(Input.GetKeyDown(KeyCode.Space)){
			makeDiscreteGrid();
			updateMesh(verticesList.ToArray(),trianglesList.ToArray(),uvList.ToArray());
			//updateCol ();
			//updateRefreshUvGrid ();

			podePreencher = true;
			print(verticesList.Count);
		}
		//DELETAR EM VOLTA
		if(Input.GetKeyDown(KeyCode.C)){
			Vector2 p = myCam.ScreenToWorldPoint (new Vector3(Input.mousePosition.x,Input.mousePosition.y));

			int posX =  Mathf.FloorToInt(Mathf.Round(p.x - 0.5f));
			int posY = Mathf.FloorToInt(Mathf.Round(p.y - 0.5f));

			deletarEmVolta (posX,posY);
			updateMesh(verticesList.ToArray(),trianglesList.ToArray(),uvList.ToArray());
			print("deletou em volta do bloco!");
		}

		//CRIAR EM VOLTA
		if(Input.GetKeyDown(KeyCode.V)){
			Vector2 p = myCam.ScreenToWorldPoint (new Vector3(Input.mousePosition.x,Input.mousePosition.y));

			int posX =  Mathf.FloorToInt(Mathf.Round(p.x - 0.5f));
			int posY = Mathf.FloorToInt(Mathf.Round(p.y - 0.5f));

			criarEmVolta (posX,posY);

			updateMesh(verticesList.ToArray(),trianglesList.ToArray(),uvList.ToArray());
			print("deletou em volta do bloco!");
		}

		//DELETAR BLOCO
		if (Input.GetKey(KeyCode.Mouse0)) {
			Vector2 p = myCam.ScreenToWorldPoint (new Vector3(Input.mousePosition.x,Input.mousePosition.y));

			int posX =  Mathf.FloorToInt(Mathf.Round(p.x - 0.5f));
			int posY = Mathf.FloorToInt(Mathf.Round(p.y - 0.5f));

			int valorTriangulo = (posY + (posX * gridSizeY));

			deletarBloco (valorTriangulo);
		}

		//UPDATE COLISAO
		if(Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) ){
			//updateCol ();
		}

		//CRIAR BLOCO
		if (Input.GetKey(KeyCode.Mouse1)) {
			Vector2 p = myCam.ScreenToWorldPoint (new Vector3(Input.mousePosition.x,Input.mousePosition.y));

			int posX =  Mathf.FloorToInt(Mathf.Round(p.x - 0.5f));
			int posY = Mathf.FloorToInt(Mathf.Round(p.y - 0.5f));

			int valorTriangulo = (posY + (posX * gridSizeY));

			criarBloco (valorTriangulo);
		}
	}

	void updateRefreshUvGrid(){
		GetComponent<refreshUvGrid> ().vertices = verticesList.ToArray ();
		GetComponent<refreshUvGrid> ().triangles = trianglesList.ToArray ();
		GetComponent<refreshUvGrid> ().estadoBloco = estadoBloco.ToArray ();
	}

	void makeDiscreteGrid () {
		//Set trackers integer 
		int v = 0;
		int t = 0;

		//Create vertex grid
		for(int x = 0; x < gridSizeX; x++){
			for(int y = 0; y < gridSizeY; y++){

				//tipo do bloco
				tipoBloco.Add(2);
				tipoBloco.Add(2);
				tipoBloco.Add(2);
				tipoBloco.Add(2);
				tipoBloco.Add(2);
				tipoBloco.Add(2);

				//populando lista de Uvs
				uvList.Add(new Vector2(0.25f,0.666666f));
				uvList.Add(new Vector2(0.25f,0.999999f));
				uvList.Add(new Vector2(0.3125f,0.666666f));
				uvList.Add(new Vector2(0.3125f,0.999999f));


				//estado do bloco
				estadoBloco.Add(1);
				estadoBloco.Add(1);
				estadoBloco.Add(1);
				estadoBloco.Add(1);
				estadoBloco.Add(1);
				estadoBloco.Add(1);
				
				v += 4;
				t += 6;

			}
		}

		//GetComponent<refreshUvGrid> ().uvs = uvList.ToArray ();
		//GetComponent<refreshUvGrid> ().tipoBloco = tipoBloco.ToArray ();

	}

	void deletarBloco(int idBloco){
		idBloco = idBloco * 4;

		estadoBloco[idBloco] = 0;
		estadoBloco[idBloco+1] = 0;
		estadoBloco[idBloco+2] = 0;
		estadoBloco[idBloco+3] = 0;

		updateMesh (verticesList.ToArray(),trianglesList.ToArray(),uvList.ToArray());
		//updateRefreshUvGrid ();
		print("Removeu o bloco: " + idBloco + " estado = "+estadoBloco[idBloco]);
	}

	void criarBloco(int idBloco){
		idBloco = idBloco * 4;
		trianglesList.Add(idBloco);
		trianglesList.Add(idBloco+1);
		trianglesList.Add(idBloco+2);
		trianglesList.Add(idBloco+2);
		trianglesList.Add(idBloco+1);
		trianglesList.Add(idBloco+3);

		estadoBloco [idBloco] = 1;
		estadoBloco[idBloco+1] = 1;
		estadoBloco[idBloco+2] = 1;
		estadoBloco[idBloco+3] = 1;

		updateMesh (verticesList.ToArray(),trianglesList.ToArray(),uvList.ToArray());
		//updateRefreshUvGrid ();
		print("Criou o bloco: " + idBloco + " estado = "+estadoBloco[idBloco]);
	}
	void deletarEmVolta(int posX ,int posY){
		//formulas para identificar todos os blocos em volta do bloco a ser analisado 
		int cimaEsquerda = (posY + (posX * gridSizeY) - (gridSizeY -1));
		int cima = (posY + (posX * gridSizeY) + 1);
		int cimaDireita = (posY + (posX * gridSizeY) + (gridSizeY +1));
		int meioEsquerda = (posY + (posX * gridSizeY) - gridSizeY);
		int meio = (posY + (posX * gridSizeY));
		int meioDireita = (posY + (posX * gridSizeY) + gridSizeY);
		int baixoEsquerda = (posY + (posX * gridSizeY) - (gridSizeY + 10));
		int baixo = (posY + (posX * gridSizeY) - 1);
		int baixoDireita = (posY + (posX * gridSizeY) + (gridSizeY-1));

		deletarBloco (cimaEsquerda);
		deletarBloco (cima);
		deletarBloco (cimaDireita);

		deletarBloco (meioEsquerda);
		deletarBloco (meioDireita);

		deletarBloco (baixoEsquerda);
		deletarBloco (baixo);
		deletarBloco (baixoDireita);

		//updateRefreshUvGrid ();
	}

	void criarEmVolta(int posX ,int posY){
		//formulas para identificar todos os blocos em volta do bloco a ser analisado 
		int cimaEsquerda = (posY + (posX * gridSizeY) - 99);
		int cima = (posY + (posX * gridSizeY) + 1);
		int cimaDireita = (posY + (posX * gridSizeY) + 101);
		int meioEsquerda = (posY + (posX * gridSizeY) - gridSizeY);
		int meio = (posY + (posX * gridSizeY));
		int meioDireita = (posY + (posX * gridSizeY) + gridSizeY);
		int baixoEsquerda = (posY + (posX * gridSizeY) - 101);
		int baixo = (posY + (posX * gridSizeY) - 1);
		int baixoDireita = (posY + (posX * gridSizeY) + 99);

		criarBloco (cimaEsquerda);
		criarBloco (cima);
		criarBloco (cimaDireita);

		criarBloco (meioEsquerda);
		criarBloco (meioDireita);

		criarBloco (baixoEsquerda);
		criarBloco (baixo);
		criarBloco (baixoDireita);
	}
		
	void updateMesh(Vector3[] _vertices,int[] _triangles,Vector2[] _uv){
		/*mesh.Clear();
		mesh.vertices = _vertices;
		mesh.triangles = _triangles;
		mesh.uv = _uv;
		mesh.RecalculateNormals();*/
	}
	void updateCol(){
		GetComponent<MeshCollider> ().sharedMesh = null;
		GetComponent<MeshCollider> ().sharedMesh = mesh;
		//updateRefreshUvGrid ();
	}
}
