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
			updateMesh(verticesList.ToArray(),trianglesList.ToArray(),uvList.ToArray());
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
		estadoBloco[idBloco+1] = 0;
		estadoBloco[idBloco+2] = 0;
		estadoBloco[idBloco+3] = 0;
		estadoBloco[idBloco+4] = 0;
		estadoBloco[idBloco+5] = 0;

		updateMesh (verticesList.ToArray(),trianglesList.ToArray(),uvList.ToArray());
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

		estadoBloco[idBloco] = 1;

		updateMesh (verticesList.ToArray(),trianglesList.ToArray(),uvList.ToArray());
		print("Criou o bloco: " + idBloco + " estado = "+estadoBloco[idBloco]);
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
	void setUvBlock (int _idTypeBloco, int _idUv) {
		// 0 = baixoEsquerda, 1 = cimaEsquerda, 2 = baixoDireita, 3 = cimaDireita, 4 = meio, 5 = cima, 6 = baixo
		// 7 = meioEsquerda, 8 = meioDireita
		switch (_idTypeBloco)
		{
		    
		    	
		    case 1:
		        //cima esquerda
				uvList [_idUv] = new Vector2(0f,0.5f);
				uvList [_idUv+1] = new Vector2(0f,1f);
				uvList [_idUv+2] = new Vector2(0.5f,0.5f);
				uvList [_idUv+3] = new Vector2(0.5f,1f);
		        break;
		    case 2:
		        //cima
				uvList [_idUv] = new Vector2(0.25f,0.5f);
				uvList [_idUv+1] = new Vector2(0.25f,1f);
				uvList [_idUv+2] = new Vector2(0.75f,0.5f);
				uvList [_idUv+3] = new Vector2(0.75f,1f);
	        	break;
		    case 3:
		        //cima direita
				uvList [_idUv] = new Vector2(0.5f,0.5f);
				uvList [_idUv+1] = new Vector2(0.5f,1f);
				uvList [_idUv+2] = new Vector2(1f,0.5f);
				uvList [_idUv+3] = new Vector2(1f,1f);
		        break;
	        
	        case 4:
		        //meio esquerda
				uvList [_idUv] = new Vector2(0f,0.25f);
				uvList [_idUv+1] = new Vector2(0f,0.75f);
				uvList [_idUv+2] = new Vector2(0.5f,0.25f);
				uvList [_idUv+3] = new Vector2(0.5f,0.75f);
	        	break;
	        case 5:
		        //meio
				uvList [_idUv] = new Vector2(0.25f,0.25f);
				uvList [_idUv+1] = new Vector2(0.25f,0.75f);
				uvList [_idUv+2] = new Vector2(0.75f,0.25f);
				uvList [_idUv+3] = new Vector2(0.75f,0.75f);
		        break;
	        case 6:
		        //meio direita
				uvList [_idUv] = new Vector2(0.5f,0.25f);
				uvList [_idUv+1] = new Vector2(0.5f,0.75f);
				uvList [_idUv+2] = new Vector2(1f,0.25f);
				uvList [_idUv+3] = new Vector2(1f,0.75f);
	        	break;
			case 7:
	       		//baixo esquerda
		        uvList[_idUv] = new Vector2(0f,0f);
				uvList[_idUv+1] = new Vector2(0f,0.5f);
				uvList[_idUv+2] = new Vector2(0.5f,0f);
				uvList[_idUv+3] = new Vector2(0.5f,0.5f);
		        break;
		    case 8:
		        //baixo
				uvList [_idUv] = new Vector2(0.25f,0f);
				uvList [_idUv+1] = new Vector2(0.25f,.5f);
				uvList [_idUv+2] = new Vector2(0.75f,0f);
				uvList [_idUv+3] = new Vector2(0.75f,0.75f);
	        	break;
	        case 9:
		        //baixo direita
				uvList [_idUv] = new Vector2(0.5f,0f);
				uvList [_idUv+1] = new Vector2(0.5f,0.5f);
				uvList [_idUv+2] = new Vector2(1f,0f);
				uvList [_idUv+3] = new Vector2(1f,0.5f);
		        break;
		    case 10:
		        //bloco inteiro
				uvList [_idUv] = new Vector2(0f,0f);
				uvList [_idUv+1] = new Vector2(0f,1f);
				uvList [_idUv+2] = new Vector2(1f,0f);
				uvList [_idUv+3] = new Vector2(1f,1f);
	        	break;
		    
		}
	}

	//definir qual uv o bloco tera de acordo com a sua coordenada (x,y) , seus vertices e seus triangulos
	void definirTextureBloco(int posX, int posY){

		int idTypeBloco = 0;

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

		if(cimaEsquerda < 0){
			cimaEsquerda = 0;
		}
		if(cima < 0){
			cima = 0;
		}
		if(cimaDireita < 0){
			cimaDireita = 0;
		}
		if(meioEsquerda < 0){
			meioEsquerda = 0;
		}
		if(meioDireita < 0){
			meioDireita = 0;
		}
		if(baixoEsquerda < 0){
			baixoEsquerda = 0;
		}
		if(baixo < 0){
			baixo = 0;
		}
		if(baixoDireita < 0){
			baixoDireita = 0;
		}
		//procurando e analisando os blocos em volta do bloco anasalisado
			if(estadoBloco[cimaEsquerda] == 0 )
			{
				idTypeBloco += 1;
			}

			if(estadoBloco[cima] == 0)
			{
				idTypeBloco += 2;
			}

			if(estadoBloco[cimaDireita] == 0)
			{
				idTypeBloco += 3;
			}

			if(estadoBloco[meioEsquerda] == 0)
			{
				idTypeBloco += 4;
			}

			if(estadoBloco[meioDireita] == 0)
			{
				idTypeBloco += 5;
			}

			if(estadoBloco[baixoEsquerda] == 0)
			{
				idTypeBloco += 6;
			}

			if(estadoBloco[baixo] == 0)
			{
				idTypeBloco += 7;
			}

			if(estadoBloco[baixoDireita] == 0)
			{
				idTypeBloco += 8;
			}
			if(posX < 1){
				_cimaDireita = false;
				_meioEsquerda = false;
				_baixoEsquerda = false;
			}
			if(posY < 1){
				_baixoEsquerda = false;
				_baixo = false;
				_baixoDireita = false;
			}
			if(posY >=99){
				_cimaEsquerda = false;
				_cima = false;
				_cimaDireita = false;
			}

			//defindindo posiçao textura do bloco
			/*
			valores por direcao
			1 = cimaEsquerda
			2 = cima
			3 = cimaDireita
			4 = meioEsquerda
			5 =	meio
			6 = meioDireita
			7 = baixoEsquerda
			8 = baixo
			9 = baixoDireita
			10 = blocoInteiro
			*/

			//se todas as direçoes forem falso
			//nada
			if(idTypeBloco == 0){
				setUvBlock(5,meio);
			}
			//tudo
			else if(idTypeBloco == 36){
				setUvBlock(10,meio);
			}

			idTypeBloco = 0;
		
	}
}
