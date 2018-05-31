using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proceduralGrid : MonoBehaviour {

	//arrays para montar a MeshGrid
	public List <Vector3> verticesList = new List <Vector3>();
	public List <Vector2> uvList = new List <Vector2>();
	public List <Vector2> colList = new List <Vector2>();
	public List <int> trianglesList = new List <int>();
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

	//propriedades para analisar a textura de cada bloco
	public bool _cimaEsquerda = true;
	public bool _cima = true;
	public bool _cimaDireita = true;
	public bool _meioEsquerda = true;
	public bool _meio = true;
	public bool _meioDireita = true;
	public bool _baixoEsquerda = true;
	public bool _baixo = true;
	public bool _baixoDireita = true;

	void Awake(){
		mesh = GetComponent<MeshFilter>().mesh;
		meshCollider = GetComponent<MeshCollider> ();
	}
	void Start () {

	}
	void Update(){
		if(Input.GetKeyDown(KeyCode.Space)){
			makeDiscreteGrid();
			updateMesh(verticesList.ToArray(),trianglesList.ToArray(),uvList.ToArray());
			updateCol ();
		}

		if(Input.GetKeyDown(KeyCode.X)){
			
			for(int x = 0; x < gridSizeX; x++){
				for(int y = 0; y < gridSizeY; y++){
					definirTextureBloco(x,y);
				}
			}
			print("Atualizou blocos!");
		}
		if (Input.GetKey(KeyCode.Mouse0)) {
			Vector2 p = myCam.ScreenToWorldPoint (new Vector3(Input.mousePosition.x,Input.mousePosition.y));

			int posX =  Mathf.FloorToInt(Mathf.Round(p.x - 0.5f));
			int posY = Mathf.FloorToInt(Mathf.Round(p.y - 0.5f));

			int valorTriangulo = (posY + (posX * gridSizeY));

			deletarBloco (valorTriangulo);
		}
		if(Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) ){
			updateCol ();
		}

		if (Input.GetKey(KeyCode.Mouse1)) {
			Vector2 p = myCam.ScreenToWorldPoint (new Vector3(Input.mousePosition.x,Input.mousePosition.y));

			int posX =  Mathf.FloorToInt(Mathf.Round(p.x - 0.5f));
			int posY = Mathf.FloorToInt(Mathf.Round(p.y - 0.5f));

			int valorTriangulo = (posY + (posX * gridSizeY));

			criarBloco (valorTriangulo);
		}
	}
	
	void makeDiscreteGrid () {
		//Set trackers integer 
		int v = 0;
		int t = 0;

		//Create vertex grid
		for(int x = 0; x < gridSizeX; x++){
			for(int y = 0; y < gridSizeY; y++){

				//populando lista de Vertices
				verticesList.Add(new Vector3 (x,y,0));
				verticesList.Add(new Vector3 (x,y+1,0));
				verticesList.Add(new Vector3 (x+1,y,0));
				verticesList.Add(new Vector3 (x+1,y+1,0));

				//populando lista de Triangulos
				trianglesList.Add(v);
				trianglesList.Add(v+1);
				trianglesList.Add(v+2);
				trianglesList.Add(v+2);
				trianglesList.Add(v+1);
				trianglesList.Add(v+3);

				//populando lista de colisao
				colList.Add(new Vector3 (x,y,0));
				colList.Add(new Vector3 (x,y+1,0));
				colList.Add(new Vector3 (x+1,y,0));
				colList.Add(new Vector3 (x+1,y+1,0));

				//populando lista de Uvs
				uvList.Add(new Vector2(0.25f,0.25f));
				uvList.Add(new Vector2(0.25f,0.75f));
				uvList.Add(new Vector2(0.75f,0.25f));
				uvList.Add(new Vector2(0.75f,0.75f));

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
	}

	void deletarBloco(int idBloco){
		idBloco = idBloco * 4;
		trianglesList.Remove(idBloco);
		trianglesList.Remove(idBloco+1);
		trianglesList.Remove(idBloco+2);
		trianglesList.Remove(idBloco+2);
		trianglesList.Remove(idBloco+1);
		trianglesList.Remove(idBloco+3);

		estadoBloco[idBloco] = 0;

		updateMesh (verticesList.ToArray(),trianglesList.ToArray(),uvList.ToArray());
		print("Removeu o bloco: " + idBloco);
	}

	void criarBloco(int idBloco){
		idBloco = idBloco * 4;
		trianglesList.Add(idBloco);
		trianglesList.Add(idBloco+1);
		trianglesList.Add(idBloco+2);
		trianglesList.Add(idBloco+2);
		trianglesList.Add(idBloco+1);
		trianglesList.Add(idBloco+3);

		estadoBloco[idBloco] = 1;

		updateMesh (verticesList.ToArray(),trianglesList.ToArray(),uvList.ToArray());
	}

	void updateMesh(Vector3[] _vertices,int[] _triangles,Vector2[] _uv){
		mesh.Clear();
		mesh.vertices = _vertices;
		mesh.triangles = _triangles;
		mesh.uv = _uv;
		mesh.RecalculateNormals();


	}
	void updateCol(){
		GetComponent<MeshCollider> ().sharedMesh = null;
		GetComponent<MeshCollider> ().sharedMesh = mesh;
	}
	void updateUV(int idUV,Vector2 newUvPos0,Vector2 newUvPos1,Vector2 newUvPos2,Vector2 newUvPos3){
		uvList[idUV] = newUvPos0;
		uvList[idUV+1] = newUvPos1;
		uvList[idUV+2] = newUvPos2;
		uvList[idUV+3] = newUvPos3;
		updateMesh (verticesList.ToArray(),trianglesList.ToArray(),uvList.ToArray());
	}
	void setUvBlock (int _idTypeBloco, int _idUv) {
		// 0 = baixoEsquerda, 1 = cimaEsquerda, 2 = baixoDireita, 3 = cimaDireita, 4 = meio, 5 = cima, 6 = baixo
		// 7 = meioEsquerda, 8 = meioDireita
		switch (_idTypeBloco)
		{
		    case 0:
		    	//baixo esquerda
		        uvList[_idUv] = new Vector2(0f,0f);
				uvList[_idUv] = new Vector2(0f,0.5f);
				uvList[_idUv] = new Vector2(0.5f,0f);
				uvList[_idUv] = new Vector2(0.5f,0.5f);
		        break;
		    case 1:
		        //cima esquerda
				uvList.Add(new Vector2(0f,0.5f));
				uvList.Add(new Vector2(0f,1f));
				uvList.Add(new Vector2(0.5f,0.5f));
				uvList.Add(new Vector2(0.5f,1f));
		        break;
		    case 2:
		        //baixo direita
				uvList [_idUv] = new Vector2(0.5f,0f);
				uvList [_idUv] = new Vector2(0.5f,0.5f);
				uvList [_idUv] = new Vector2(1f,0f);
				uvList [_idUv] = new Vector2(1f,0.5f);
		        break;
		    case 3:
		        //cima direita
				uvList [_idUv] = new Vector2(0.5f,0.5f);
				uvList [_idUv] = new Vector2(0.5f,1f);
				uvList [_idUv] = new Vector2(1f,0.5f);
				uvList [_idUv] = new Vector2(1f,1f);
		        break;
		    case 4:
		        //meio
				uvList [_idUv] = new Vector2(0.25f,0.25f);
				uvList [_idUv] = new Vector2(0.25f,0.75f);
				uvList [_idUv] = new Vector2(0.75f,0.25f);
				uvList [_idUv] = new Vector2(0.75f,0.75f);
		        break;
	        case 5:
		        //cima
				uvList [_idUv] = new Vector2(0.25f,0.5f);
				uvList [_idUv] = new Vector2(0.25f,1f);
				uvList [_idUv] = new Vector2(0.75f,0.5f);
				uvList [_idUv] = new Vector2(0.75f,1f);
	        	break;
	        case 6:
		        //baixo
				uvList [_idUv] = new Vector2(0.25f,0f);
				uvList [_idUv] = new Vector2(0.25f,.5f);
				uvList [_idUv] = new Vector2(0.75f,0f);
				uvList [_idUv] = new Vector2(0.75f,0.75f);
	        	break;
	        case 7:
		        //meio esquerda
				uvList [_idUv] = new Vector2(0f,0.25f);
				uvList [_idUv] = new Vector2(0f,0.75f);
				uvList [_idUv] = new Vector2(0.5f,0.25f);
				uvList [_idUv] = new Vector2(0.75f,0.75f);
	        	break;
	        case 8:
		        //meio direita
				uvList [_idUv] = new Vector2(0.5f,0.25f);
				uvList [_idUv] = new Vector2(0.5f,0.75f);
				uvList [_idUv] = new Vector2(1f,0.25f);
				uvList [_idUv] = new Vector2(1f,0.75f);
	        	break;
		    
		}
	}

	//definir qual uv o bloco tera de acordo com a sua coordenada (x,y) , seus vertices e seus triangulos
	void definirTextureBloco(int posX, int posY){
		//formulas para identificar todos os blocos em volta do bloco a ser analisado 
		int cimaEsquerda = (posY + (posX * gridSizeY) - 99) * 4;
		int cima = (posY + (posX * gridSizeY) + 1) * 4;
		int cimaDireita = (posY + (posX * gridSizeY) + 101) * 4;
		int meioEsquerda = (posY + (posX * gridSizeY) - gridSizeY) * 4;
		int meio = (posY + (posX * gridSizeY)) * 4;
		int meioDireita = (posY + (posX * gridSizeY) + gridSizeY) * 4;
		int baixoEsquerda = (posY + (posX * gridSizeY) - 101) * 4;
		int baixo = (posY + (posX * gridSizeY) - 1) * 4;
		int baixoDireita = (posY + (posX * gridSizeY) + 99) * 4;

		//procurando e analisando os blocos em volta do bloco anasalisado
		for(int i = 0 ;i < trianglesList.Count;i++){
			if(estadoBloco[cimaEsquerda] == 0)
			{
				_cimaEsquerda = false;
				print("meio");
			}

			else if(estadoBloco[cima] == 0)
			{
				_cima = false;
				print("meio");
			}

			else if(trianglesList[i] == cimaDireita && trianglesList[i + 1] == cimaDireita && trianglesList[i + 2] == cimaDireita
				&& trianglesList [i + 3] == cimaDireita && trianglesList [i + 4] == cimaDireita && trianglesList[i + 5] == cimaDireita)
			{
				_cimaDireita = false;
				print("meio");
			}

			else if(trianglesList[i] == meioEsquerda && trianglesList[i + 1] == meioEsquerda && trianglesList[i + 2] == meioEsquerda
				&& trianglesList [i + 3] == meioEsquerda && trianglesList [i + 4] == meioEsquerda && trianglesList[i + 5] == meioEsquerda)
			{
				_meioEsquerda = false;
				print("meio");
			}

			else if(trianglesList[i] == meioDireita && trianglesList[i + 1] == meioDireita && trianglesList[i + 2] == meioDireita
				&& trianglesList [i + 3] == meioDireita && trianglesList [i + 4] == meioDireita && trianglesList[i + 5] == meioDireita)
			{
				_meioDireita = false;
				print("meio");
			}

			else if(trianglesList[i] == baixoEsquerda && trianglesList[i + 1] == baixoEsquerda && trianglesList[i + 2] == baixoEsquerda
				&& trianglesList [i + 3] == baixoEsquerda && trianglesList [i + 4] == baixoEsquerda && trianglesList[i + 5] == baixoEsquerda)
			{
				_baixoEsquerda = false;
				print("meio");
			}

			else if(trianglesList[i] == baixo && trianglesList[i + 1] == baixo && trianglesList[i + 2] == baixo
				&& trianglesList [i + 3] == baixo && trianglesList [i + 4] == baixo && trianglesList[i + 5] == baixo)
			{
				_baixo = false;
				print("meio");
			}

			else if(trianglesList[i] == baixoDireita && trianglesList[i + 1] == baixoDireita && trianglesList[i + 2] == baixoDireita
				&& trianglesList [i + 3] == baixoDireita && trianglesList [i + 4] == baixoDireita && trianglesList[i + 5] == baixoDireita)
			{
				_baixoDireita = false;
				print("meio");
			}else{
				_cimaEsquerda = true;
				_cima = true;
				_cimaDireita = true;
				_meioEsquerda = true;
				_meio = true;
				_meioDireita = true;
				_baixoEsquerda = true;
				_baixo = true;
				_baixoDireita = true;
				break;
			}

			//defindindo posiçao textura do bloco
			if( _cimaEsquerda == true &&
				_cima == true &&
				_cimaDireita == true &&
				_meioEsquerda == true &&
				_meio == true &&
				_meioDireita == true &&
				_baixoEsquerda == true &&
				_baixo == true &&
				_baixoDireita == true)
			{
				setUvBlock(1,meio);
				print("meio");
			}
			else if( _cimaEsquerda == true ||
				_cima == true ||
				_cimaDireita == true ||
				_meioEsquerda == true ||
				_meio == true ||
				_meioDireita == true ||
				_baixoEsquerda == true ||
				_baixo == true ||
				_baixoDireita == true)
			{
				setUvBlock (7,meio);
				print("cimaEsquerda");
			}
		}
	}
}
