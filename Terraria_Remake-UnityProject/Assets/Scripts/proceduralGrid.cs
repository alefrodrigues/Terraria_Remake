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
			updateCol ();
		}

		//DEFINIR TEXTURA DO BLOCO
		if(Input.GetKeyDown(KeyCode.X)){
			
			for(int x = 0; x < gridSizeX; x++){
				for(int y = 0; y < gridSizeY; y++){
					definirTextureBloco(x,y);
				}
			}
			updateMesh(verticesList.ToArray(),trianglesList.ToArray(),uvList.ToArray());
			print("Atualizou blocos!");
		}

		//DELETAR EM VOLTA
		if(Input.GetKeyDown(KeyCode.C)){
			Vector2 p = myCam.ScreenToWorldPoint (new Vector3(Input.mousePosition.x,Input.mousePosition.y));

			int posX =  Mathf.FloorToInt(Mathf.Round(p.x - 0.5f));
			int posY = Mathf.FloorToInt(Mathf.Round(p.y - 0.5f));

			deletarEmVolta (posX,posY);
			definirTextureBloco(posX,posY);
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
			definirTextureBloco(posX,posY);
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
			updateCol ();
			/*for(int x = 0;x<= gridSizeX;x++){
				for(int y = 0;y<= gridSizeX;y++){
					definirTextureBloco (x,y);
				}
			}*/
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
		estadoBloco [idBloco] = 1;
		estadoBloco[idBloco+1] = 1;
		estadoBloco[idBloco+2] = 1;
		estadoBloco[idBloco+3] = 1;
		estadoBloco[idBloco+4] = 1;
		estadoBloco[idBloco+5] = 1;

		updateMesh (verticesList.ToArray(),trianglesList.ToArray(),uvList.ToArray());
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
	
		//formulas para identificar todos os blocos em volta do bloco a ser analisado 
		int cimaEsquerda = (posY + (posX * gridSizeY) - 99) * 4;
		int cima = (posY + (posX * gridSizeY) + 1) * 4;
		int cimaDireita = (posY + (posX * gridSizeY) + 101) * 4;
		int meioEsquerda = (posY + (posX * gridSizeY) - gridSizeY) * 4;
		int meio = (posY + (posX * 100)) * 4;
		int meioDireita = (posY + (posX * gridSizeY) + gridSizeY) * 4;
		int baixoEsquerda = (posY + (posX * gridSizeY) - 101) * 4;
		int baixo = (posY + (posX * gridSizeY) - 1) * 4;
		int baixoDireita = (posY + (posX * gridSizeY) + 99) * 4;

		
		if(cimaEsquerda < 0){
			cimaEsquerda = 0;
			estadoBloco[cimaEsquerda] = 0;
		}
		if(cima < 0){
			cima = 0;
			estadoBloco[cima] = 0;
		}
		if(cimaDireita < 0){
			cimaDireita = 0;
			estadoBloco[cimaDireita] = 0;
		}
		if(meioEsquerda < 0){
			meioEsquerda = 0;
			estadoBloco[meioEsquerda] = 0;
		}
		if(meioDireita < 0){
			meioDireita = 0;
			estadoBloco[meioDireita] = 0;
		}
		if(baixoEsquerda < 0){
			baixoEsquerda = 0;
			estadoBloco[baixoEsquerda] = 0;
		}
		if(baixo < 0){
			baixo = 0;
			estadoBloco[baixo] = 0;
		}
		if(baixoDireita < 0){
			baixoDireita = 0;
			estadoBloco[baixoDireita] = 0;
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
		blocosEmVolta.Add(estadoBloco[cimaEsquerda]);
		blocosEmVolta.Add(estadoBloco[cima]);
		blocosEmVolta.Add(estadoBloco[cimaDireita]);
		blocosEmVolta.Add(estadoBloco[meioEsquerda]);
		blocosEmVolta.Add(estadoBloco[meio]);
		blocosEmVolta.Add(estadoBloco[meioDireita]);
		blocosEmVolta.Add(estadoBloco[baixoEsquerda]);
		blocosEmVolta.Add(estadoBloco[baixo]);
		blocosEmVolta.Add(estadoBloco[baixoDireita]);
		/*print(  blocosEmVolta[0] +  ","+blocosEmVolta[1] +  ","+blocosEmVolta[2] +  ","+
				blocosEmVolta[3] +  ","+blocosEmVolta[4] +  ","+blocosEmVolta[5] +  ","+
				blocosEmVolta[6] +  ","+blocosEmVolta[7] +  ","+blocosEmVolta[8]);*/

		//nada
		int[] todos = new int[9]{	1,1,1,
									1,1,1,
									1,1,1	};

		int[] nenhum = new int[9]{	0,0,0,
									0,0,0,
									0,0,0	};

		//cima esquerda
		int[] ce = new int[9]{	0,0,1,
								0,1,1,
								1,1,1	};

		int[] ce1 = new int[9]{	1,0,1,
								0,1,1,
								1,1,1	};

		int[] ce2 = new int[9]{	1,0,0,
								0,1,1,
								1,1,1	};

		int[] ce3 = new int[9]{	1,0,0,
								0,1,1,
								0,1,1	};

		int[] ce4 = new int[9]{	0,0,0,
								0,1,1,
								1,1,1	};

		int[] ce5 = new int[9]{	0,0,0,
								0,1,1,
								0,1,1	};

		int[] ce6 = new int[9]{	0,0,1,
								0,1,1,
								0,1,1	};

		int[] ce7 = new int[9]{	1,0,1,
								0,1,1,
								0,1,1	};

		//cima esquerda
		int[] cd = new int[9]{	1,0,0,
								1,1,0,
								1,1,1	};

		int[] cd1 = new int[9]{	1,0,1,
								1,1,0,
								1,1,1	};

		int[] cd2 = new int[9]{	0,0,1,
								1,1,0,
								1,1,1	};

		int[] cd3 = new int[9]{	0,0,1,
								1,1,0,
								1,1,0	};

		int[] cd4 = new int[9]{	0,0,0,
								1,1,0,
								1,1,1	};

		int[] cd5 = new int[9]{	0,0,0,
								1,1,0,
								1,1,0	};

		int[] cd6 = new int[9]{	1,0,0,
								1,1,0,
								1,1,0	};

		int[] cd7 = new int[9]{	1,0,1,
								1,1,0,
								1,1,0	};

		//baixo direita
		int[] bd = new int[9]{	1,1,1,
								1,1,0,
								1,0,0	};

		int[] bd1 = new int[9]{	1,1,0,
								1,1,0,
								0,0,0	};
								
		int[] bd2 = new int[9]{	1,1,1,
								1,1,0,
								0,0,0	};
								
		int[] bd3 = new int[9]{	1,1,0,
								1,1,0,
								1,0,0	};

		int[] bd4 = new int[9]{	1,1,1,
								1,1,0,
								1,0,1	};

		int[] bd5 = new int[9]{	1,1,0,
								1,1,0,
								0,0,1	};
								
		int[] bd6 = new int[9]{	1,1,1,
								1,1,0,
								0,0,1	};
								
		int[] bd7 = new int[9]{	1,1,0,
								1,1,0,
								1,0,1	};

		//baixo esquerda
		int[] be = new int[9]{	1,1,1,
								0,1,1,
								0,0,1	};

		int[] be1 = new int[9]{	0,1,1,
								0,1,1,
								0,0,0	};
								
		int[] be2 = new int[9]{	1,1,1,
								0,1,1,
								0,0,0	};
								
		int[] be3 = new int[9]{	0,1,1,
								0,1,1,
								0,0,1	};

		int[] be4 = new int[9]{	1,1,1,
								0,1,1,
								1,0,1	};

		int[] be5 = new int[9]{	0,1,1,
								0,1,1,
								1,0,0	};
								
		int[] be6 = new int[9]{	1,1,1,
								0,1,1,
								1,0,0	};
								
		int[] be7 = new int[9]{	0,1,1,
								0,1,1,
								1,0,1	};

		//baixo
		int[] b = new int[9]{	1,0,1,
								1,1,1,
								1,1,1	};

		int[] b1 = new int[9]{	0,0,1,
								1,1,1,
								1,1,1	};

		int[] b2 = new int[9]{	1,0,0,
								1,1,1,
								1,1,1	};

		int[] b3 = new int[9]{	0,0,0,
								1,1,1,
								1,1,1	};

		//cima
		int[] c = new int[9]{	1,1,1,
								1,1,1,
								1,0,1	};

		int[] c1 = new int[9]{	1,1,1,
								1,1,1,
								0,0,1	};

		int[] c2 = new int[9]{	1,1,1,
								1,1,1,
								1,0,0	};

		int[] c3 = new int[9]{	1,1,1,
								1,1,1,
								0,0,0	};

		//meio esquerda
		int[] me = new int[9]{	0,1,1,
								0,1,1,
								0,1,1	};

		int[] me1 = new int[9]{	1,1,1,
								0,1,1,
								1,1,1	};

		int[] me2 = new int[9]{	0,1,1,
								0,1,1,
								1,1,1	};

		//meio direita
		int[] md = new int[9]{	1,1,0,
								1,1,0,
								1,1,0	};

		int[] md1 = new int[9]{	1,1,1,
								1,1,0,
								1,1,1	};

		int[] md2 = new int[9]{	1,1,0,
								1,1,0,
								1,1,1	};

		if(compararListas(blocosEmVolta.ToArray(),todos)){
			setUvBlock(5,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),nenhum)){
			setUvBlock(10,meio);
		}

		//cima esquerda
		if(compararListas(blocosEmVolta.ToArray(),ce)){
			setUvBlock(1,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),ce1)){
			setUvBlock(1,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),ce2)){
			setUvBlock(1,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),ce3)){
			setUvBlock(1,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),ce4)){
			setUvBlock(1,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),ce5)){
			setUvBlock(1,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),ce6)){
			setUvBlock(1,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),ce7)){
			setUvBlock(1,meio);
		}

		//cima direita
		if(compararListas(blocosEmVolta.ToArray(),cd)){
			setUvBlock(3,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),cd1)){
			setUvBlock(3,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),cd2)){
			setUvBlock(3,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),cd3)){
			setUvBlock(3,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),cd4)){
			setUvBlock(3,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),cd5)){
			setUvBlock(3,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),cd6)){
			setUvBlock(3,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),cd7)){
			setUvBlock(3,meio);
		}

		//baixo direita
		if(compararListas(blocosEmVolta.ToArray(),bd)){
			setUvBlock(9,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),bd1)){
			setUvBlock(9,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),bd2)){
			setUvBlock(9,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),bd3)){
			setUvBlock(9,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),bd4)){
			setUvBlock(9,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),bd5)){
			setUvBlock(9,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),bd6)){
			setUvBlock(9,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),bd7)){
			setUvBlock(9,meio);
		}

		//baixo esquerda
		if(compararListas(blocosEmVolta.ToArray(),be)){
			setUvBlock(7,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),be1)){
			setUvBlock(7,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),be2)){
			setUvBlock(7,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),be3)){
			setUvBlock(7,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),be4)){
			setUvBlock(7,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),be5)){
			setUvBlock(7,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),be6)){
			setUvBlock(7,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),be7)){
			setUvBlock(7,meio);
		}

		//baixo
		if(compararListas(blocosEmVolta.ToArray(),b)){
			setUvBlock(2,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),b1)){
			setUvBlock(2,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),b2)){
			setUvBlock(2,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),b3)){
			setUvBlock(2,meio);
		}

		//cima
		if(compararListas(blocosEmVolta.ToArray(),c)){
			setUvBlock(8,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),c1)){
			setUvBlock(8,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),c2)){
			setUvBlock(8,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),c3)){
			setUvBlock(8,meio);
		}

		//meio esquerda
		if(compararListas(blocosEmVolta.ToArray(),me)){
			setUvBlock(4,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),me1)){
			setUvBlock(4,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),me2)){
			setUvBlock(4,meio);
		}

		//meio direita
		if(compararListas(blocosEmVolta.ToArray(),md)){
			setUvBlock(6,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),md1)){
			setUvBlock(6,meio);
		}
		if(compararListas(blocosEmVolta.ToArray(),md2)){
			setUvBlock(6,meio);
		}

		blocosEmVolta.Clear();
	}

	bool compararListas(int[] lista1,int[] lista2){
		bool resultado = false;
		if( lista1[0] == lista2[0] &&
			lista1[1] == lista2[1] &&
			lista1[2] == lista2[2] &&
			lista1[3] == lista2[3] &&
			lista1[4] == lista2[4] &&
			lista1[5] == lista2[5] &&
			lista1[6] == lista2[6] &&
			lista1[7] == lista2[7] &&
			lista1[8] == lista2[8])
		{
			return resultado = true;
		}else{
			return resultado = false;
		}
	}
}
