using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class refreshUvGrid : MonoBehaviour {

	//arrays para montar a MeshGrid

	public Vector3[] vertices;
	public Vector2[] uvs;
	public int[] triangles;
	public int[] estadoBloco;

	List <int> blocosEmVolta = new List <int> ();

	//MeshGrid
	public Mesh mesh;

	//tamanho da MeshGrid;
	public int gridSizeX = 100;
	public int gridSizeY = 100;

	void Awake(){
		mesh = GetComponent<MeshFilter> ().mesh;
		vertices = mesh.vertices;
		triangles = mesh.triangles;
		uvs = mesh.uv;
	}
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Z)){
			for(int x = 0;x < gridSizeX;x++){
				for(int y = 0;y < gridSizeY;y++){
					definirTexturaMeshGrid (x,y);
				}
			}
			updateUV ();
		}

		if(Input.GetKey(KeyCode.Mouse0)){
			definirTexturaBloco ();

		}

		if(Input.GetKeyDown(KeyCode.Alpha1)){

			Vector2 p = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x,Input.mousePosition.y,0));

			int posX =  Mathf.FloorToInt(Mathf.Round(p.x - 0.5f));
			int posY = Mathf.FloorToInt(Mathf.Round(p.y - 0.5f));

			int indexBloco = (posY + (posX * 100)) * 4;

			trocarTexutaBloco (14, indexBloco);
		}

	}
		
	void updateUV(){
		mesh.Clear ();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.uv = uvs;
		mesh.RecalculateNormals ();
	}



	void trocarTexutaBloco(int idBloco,int indexBloco){
		setUvBlock(idBloco,indexBloco);
		updateUV();
	}

	void setUvBlock (int _idTypeBloco, int _idUv) {
		// 0 = baixoEsquerda, 1 = cimaEsquerda, 2 = baixoDireita, 3 = cimaDireita, 4 = meio, 5 = cima, 6 = baixo
		// 7 = meioEsquerda, 8 = meioDireita
		switch (_idTypeBloco)
		{


			case 1:
				//cima esquerda
			uvs [_idUv] = new Vector2(0f,0.5f);
			uvs [_idUv+1] = new Vector2(0f,1f);
			uvs [_idUv+2] = new Vector2(0.5f,0.5f);
			uvs [_idUv+3] = new Vector2(0.5f,1f);
				break;
			case 2:
				//cima
			uvs [_idUv] = new Vector2(0.25f,0.5f);
			uvs [_idUv+1] = new Vector2(0.25f,1f);
			uvs [_idUv+2] = new Vector2(0.75f,0.5f);
			uvs [_idUv+3] = new Vector2(0.75f,1f);
				break;
			case 3:
				//cima direita
			uvs [_idUv] = new Vector2(0.5f,0.5f);
			uvs [_idUv+1] = new Vector2(0.5f,1f);
			uvs [_idUv+2] = new Vector2(1f,0.5f);
			uvs [_idUv+3] = new Vector2(1f,1f);
				break;

			case 4:
				//meio esquerda
			uvs [_idUv] = new Vector2(0f,0.25f);
			uvs [_idUv+1] = new Vector2(0f,0.75f);
			uvs [_idUv+2] = new Vector2(0.5f,0.25f);
			uvs [_idUv+3] = new Vector2(0.5f,0.75f);
				break;
			case 5:
				//meio
			uvs [_idUv] = new Vector2(0.25f,0.25f);
			uvs [_idUv+1] = new Vector2(0.25f,0.75f);
			uvs [_idUv+2] = new Vector2(0.75f,0.25f);
			uvs [_idUv+3] = new Vector2(0.75f,0.75f);
				break;
			case 6:
				//meio direita
			uvs [_idUv] = new Vector2(0.5f,0.25f);
			uvs [_idUv+1] = new Vector2(0.5f,0.75f);
			uvs [_idUv+2] = new Vector2(1f,0.25f);
			uvs [_idUv+3] = new Vector2(1f,0.75f);
				break;
			case 7:
				//baixo esquerda
			uvs[_idUv] = new Vector2(0f,0f);
			uvs[_idUv+1] = new Vector2(0f,0.5f);
			uvs[_idUv+2] = new Vector2(0.5f,0f);
			uvs[_idUv+3] = new Vector2(0.5f,0.5f);
				break;
			case 8:
				//baixo
			uvs [_idUv] = new Vector2(0.25f,0f);
			uvs [_idUv+1] = new Vector2(0.25f,0.75f);
			uvs [_idUv+2] = new Vector2(0.75f,0f);
			uvs [_idUv+3] = new Vector2(0.75f,0.75f);
				break;
			case 9:
				//baixo direita
			uvs [_idUv] = new Vector2(0.5f,0f);
			uvs [_idUv+1] = new Vector2(0.5f,0.5f);
			uvs [_idUv+2] = new Vector2(1f,0f);
			uvs [_idUv+3] = new Vector2(1f,0.5f);
				break;
			case 10:
				//bloco inteiro
			uvs [_idUv] = new Vector2(0f,0f);
			uvs [_idUv+1] = new Vector2(0f,1f);
			uvs [_idUv+2] = new Vector2(1f,0f);
			uvs [_idUv+3] = new Vector2(1f,1f);
				break;
			case 11:
			//bloco ponta cima
			uvs [_idUv] = new Vector2(0f,0.25f);
			uvs [_idUv+1] = new Vector2(0f,1f);
			uvs [_idUv+2] = new Vector2(1f,0.25f);
			uvs [_idUv+3] = new Vector2(1f,1f);
				break;
			case 12:
			//bloco ponta baixo
			uvs [_idUv] = new Vector2(0f,0f);
			uvs [_idUv+1] = new Vector2(0f,0.75f);
			uvs [_idUv+2] = new Vector2(1f,0f);
			uvs [_idUv+3] = new Vector2(1f,0.75f);
			break;
			case 13:
			//bloco ponta esquerda
			uvs [_idUv] = new Vector2(0f,0f);
			uvs [_idUv+1] = new Vector2(0f,1f);
			uvs [_idUv+2] = new Vector2(0.75f,0f);
			uvs [_idUv+3] = new Vector2(0.75f,1f);
			break;
			case 14:
			//bloco ponta direita
			uvs [_idUv] = new Vector2(0.25f,0f);
			uvs [_idUv+1] = new Vector2(0.25f,1f);
			uvs [_idUv+2] = new Vector2(1f,0f);
			uvs [_idUv+3] = new Vector2(1f,1f);
			break;
			case 15:
			//bloco meio vertical
			uvs [_idUv] = new Vector2(0f,0.25f);
			uvs [_idUv+1] = new Vector2(0f,0.75f);
			uvs [_idUv+2] = new Vector2(1f,0.25f);
			uvs [_idUv+3] = new Vector2(1f,0.75f);
				break;
			case 16:
			//bloco meio horizontal
			uvs [_idUv] = new Vector2(0.25f,0f);
			uvs [_idUv+1] = new Vector2(0.25f,1f);
			uvs [_idUv+2] = new Vector2(0.75f,0f);
			uvs [_idUv+3] = new Vector2(0.75f,1f);
				break;

		}
	}
	public void definirTexturaBloco(){
		Vector2 p = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x,Input.mousePosition.y,0));

		Vector2 cimaE = new Vector2 (p.x-1,p.y+1);
		Vector2 cima = new Vector2 (p.x,p.y+1);
		Vector2 cimaD = new Vector2 (p.x+1,p.y+1);
		Vector2 mE = new Vector2 (p.x-1,p.y);
		Vector2 mD = new Vector2 (p.x+1,p.y);
		Vector2 baixoE = new Vector2 (p.x-1,p.y-1);
		Vector2 baixo = new Vector2 (p.x,p.y-1);
		Vector2 baixoD = new Vector2 (p.x+1,p.y-1);

		definirTexturaMeshGrid (Mathf.FloorToInt(cimaE.x),Mathf.FloorToInt(cimaE.y));
		definirTexturaMeshGrid (Mathf.FloorToInt(cima.x),Mathf.FloorToInt(cima.y));
		definirTexturaMeshGrid (Mathf.FloorToInt(cimaD.x),Mathf.FloorToInt(cimaD.y));
		definirTexturaMeshGrid (Mathf.FloorToInt(mE.x),Mathf.FloorToInt(mE.y));
		definirTexturaMeshGrid (Mathf.FloorToInt(mD.x),Mathf.FloorToInt(mD.y));
		definirTexturaMeshGrid (Mathf.FloorToInt(baixoE.x),Mathf.FloorToInt(baixoE.y));
		definirTexturaMeshGrid (Mathf.FloorToInt(baixo.x),Mathf.FloorToInt(baixo.y));
		definirTexturaMeshGrid (Mathf.FloorToInt(baixoD.x),Mathf.FloorToInt(baixoD.y));

		updateUV ();
	}
	public void definirTexturaMeshGrid(int posX, int posY){
		int[] sequencia;
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

		//resetando os bloco que estiverem em posiçoes negativas e/ou sua posiçoes forem maiores menores que o vetor
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
		11 = pontaCima
		12 = pontaBaixo
		13 = pontaEsquerda
		14 = pontaDireita
		15 = meioVertical
		16 = meioHorizontal
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
		sequencia = new int[9]{	1,1,1,
								1,1,1,
								1,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,5,meio);

		sequencia = new int[9]{	1,0,0,
								0,1,0,
								0,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,10,meio);

		sequencia = new int[9]{	0,0,1,
								0,1,0,
								1,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,10,meio);

		sequencia = new int[9]{	0,0,0,
								0,1,0,
								0,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,10,meio);


		sequencia = new int[9]{	0,0,1,
								0,1,0,
								0,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,10,meio);

		sequencia = new int[9]{	1,0,0,
								0,1,0,
								0,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,10,meio);

		sequencia = new int[9]{	0,0,0,
								0,1,0,
								1,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,10,meio);

		sequencia = new int[9]{	0,0,0,
								0,1,0,
								0,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,10,meio);

		sequencia = new int[9]{	1,0,1,
								0,1,0,
								0,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,10,meio);

		sequencia = new int[9]{	0,0,0,
								0,1,0,
								1,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,10,meio);

		sequencia = new int[9]{	1,0,0,
								0,1,0,
								1,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,10,meio);

		sequencia = new int[9]{	0,0,1,
								0,1,0,
								0,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,10,meio);

		sequencia = new int[9]{	1,0,1,
								0,1,0,
								1,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,10,meio);

		sequencia = new int[9]{	1,0,1,
								0,1,0,
								0,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,10,meio);

		sequencia = new int[9]{	1,0,0,
								0,1,0,
								1,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,10,meio);

		sequencia = new int[9]{	0,0,1,
								0,1,0,
								1,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,10,meio);

		sequencia = new int[9]{	1,0,1,
								0,1,0,
								1,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,10,meio);

		//------------------CIMA ESQUERDA-----------------
		sequencia = new int[9]{	0,0,1,
								0,1,1,
								1,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,1,meio);

		sequencia = new int[9]{	1,0,1,
								0,1,1,
								1,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,1,meio);

		sequencia = new int[9]{	1,0,0,
								0,1,1,
								1,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,1,meio);

		sequencia = new int[9]{	1,0,0,
								0,1,1,
								0,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,1,meio);

		sequencia = new int[9]{	0,0,0,
								0,1,1,
								1,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,1,meio);

		sequencia = new int[9]{	0,0,0,
								0,1,1,
								0,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,1,meio);

		sequencia = new int[9]{	0,0,1,
								0,1,1,
								0,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,1,meio);

		sequencia = new int[9]{	1,0,1,
								0,1,1,
								0,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,1,meio);

		sequencia = new int[9]{	1,0,1,
								0,1,1,
								1,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,1,meio);

		sequencia = new int[9]{	1,0,1,
								0,1,1,
								0,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,1,meio);

		sequencia = new int[9]{	1,0,0,
								0,1,1,
								1,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,1,meio);

		sequencia = new int[9]{	1,0,0,
								0,1,1,
								0,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,1,meio);

		sequencia = new int[9]{	0,0,1,
								0,1,1,
								1,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,1,meio);

		sequencia = new int[9]{	0,0,1,
								0,1,1,
								0,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,1,meio);

		sequencia = new int[9]{	0,0,0,
								0,1,1,
								1,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,1,meio);

		sequencia = new int[9]{	0,0,0,
								0,1,1,
								0,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,1,meio);

		//------------FIM-------------


		//-----------------CIMA DIREITA ----------
		sequencia = new int[9]{	1,0,0,
								1,1,0,
								1,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,3,meio);

		sequencia = new int[9]{	1,0,1,
								1,1,0,
								1,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,3,meio);

		sequencia = new int[9]{	0,0,1,
								1,1,0,
								1,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,3,meio);

		sequencia = new int[9]{	0,0,1,
								1,1,0,
								1,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,3,meio);

		sequencia = new int[9]{	0,0,0,
								1,1,0,
								1,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,3,meio);

		sequencia = new int[9]{	0,0,0,
								1,1,0,
								1,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,3,meio);

		sequencia = new int[9]{	1,0,0,
								1,1,0,
								1,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,3,meio);

		sequencia = new int[9]{	1,0,1,
								1,1,0,
								1,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,3,meio);

		sequencia = new int[9]{	1,0,1,
								1,1,0,
								0,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,3,meio);

		sequencia = new int[9]{	0,0,1,
								1,1,0,
								0,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,3,meio);

		sequencia = new int[9]{	1,0,1,
								1,1,0,
								0,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,3,meio);

		sequencia = new int[9]{	0,0,1,
								1,1,0,
								0,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,3,meio);

		sequencia = new int[9]{	1,0,0,
								1,1,0,
								0,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,3,meio);

		sequencia = new int[9]{	1,0,0,
								1,1,0,
								0,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,3,meio);

		sequencia = new int[9]{	0,0,0,
								1,1,0,
								0,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,3,meio);

		sequencia = new int[9]{	0,0,0,
								1,1,0,
								0,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,3,meio);

		//------------FIM-------------


		//---------------BAIXO DIREITA
		sequencia = new int[9]{	1,1,1,
								1,1,0,
								1,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,9,meio);

		sequencia = new int[9]{	1,1,0,
								1,1,0,
								0,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,9,meio);

		sequencia = new int[9]{	1,1,1,
								1,1,0,
								0,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,9,meio);

		sequencia = new int[9]{	1,1,0,
								1,1,0,
								1,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,9,meio);

		sequencia = new int[9]{	1,1,1,
								1,1,0,
								1,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,9,meio);

		sequencia = new int[9]{	1,1,0,
								1,1,0,
								0,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,9,meio);

		sequencia = new int[9]{	1,1,1,
								1,1,0,
								0,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,9,meio);

		sequencia = new int[9]{	1,1,0,
								1,1,0,
								1,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,9,meio);

		sequencia = new int[9]{	0,1,1,
								1,1,0,
								1,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,9,meio);

		sequencia = new int[9]{	0,1,1,
								1,1,0,
								1,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,9,meio);

		sequencia = new int[9]{	0,1,1,
								1,1,0,
								0,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,9,meio);

		sequencia = new int[9]{	0,1,0,
								1,1,0,
								1,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,9,meio);

		sequencia = new int[9]{	0,1,0,
								1,1,0,
								0,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,9,meio);

		sequencia = new int[9]{	0,1,1,
								1,1,0,
								1,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,9,meio);

		sequencia = new int[9]{	0,1,0,
								1,1,0,
								1,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,9,meio);

		sequencia = new int[9]{	0,1,1,
								1,1,0,
								0,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,9,meio);

		sequencia = new int[9]{	0,1,0,
								1,1,0,
								0,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,9,meio);

		//------------FIM-------------


		//--------------BAIXO ESQUERDA
		sequencia = new int[9]{	1,1,1,
								0,1,1,
								0,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,7,meio);

		sequencia = new int[9]{	0,1,1,
								0,1,1,
								0,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,7,meio);

		sequencia = new int[9]{	1,1,1,
								0,1,1,
								0,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,7,meio);

		sequencia = new int[9]{	0,1,1,
								0,1,1,
								0,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,7,meio);

		sequencia = new int[9]{	1,1,1,
								0,1,1,
								1,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,7,meio);

		sequencia = new int[9]{	0,1,1,
								0,1,1,
								1,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,7,meio);

		sequencia = new int[9]{	1,1,1,
								0,1,1,
								1,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,7,meio);

		sequencia = new int[9]{	0,1,1,
								0,1,1,
								1,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,7,meio);

		sequencia = new int[9]{	0,1,0,
								0,1,1,
								1,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,7,meio);

		sequencia = new int[9]{	1,1,0,
								0,1,1,
								1,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,7,meio);

		sequencia = new int[9]{	0,1,0,
								0,1,1,
								1,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,7,meio);

		sequencia = new int[9]{	1,1,0,
								0,1,1,
								1,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,7,meio);

		sequencia = new int[9]{	1,1,0,
								0,1,1,
								0,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,7,meio);

		sequencia = new int[9]{	1,1,0,
								0,1,1,
								0,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,7,meio);

		sequencia = new int[9]{	0,1,0,
								0,1,1,
								0,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,7,meio);

		sequencia = new int[9]{	0,1,0,
								0,1,1,
								0,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,7,meio);

		//------------FIM-------------


		//-----------------BAIXO-----------

		sequencia = new int[9]{	1,0,1,
								1,1,1,
								1,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,2,meio);

		sequencia = new int[9]{	1,0,1,
								1,1,1,
								1,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,2,meio);

		sequencia = new int[9]{	1,0,1,
								1,1,1,
								0,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,2,meio);

		sequencia = new int[9]{	1,0,1,
								1,1,1,
								0,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,2,meio);

		sequencia = new int[9]{	0,0,1,
								1,1,1,
								1,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,2,meio);

		sequencia = new int[9]{	0,0,1,
								1,1,1,
								1,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,2,meio);

		sequencia = new int[9]{	0,0,1,
								1,1,1,
								0,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,2,meio);

		sequencia = new int[9]{	0,0,1,
								1,1,1,
								0,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,2,meio);

		sequencia = new int[9]{	1,0,0,
								1,1,1,
								1,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,2,meio);

		sequencia = new int[9]{	1,0,0,
								1,1,1,
								1,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,2,meio);

		sequencia = new int[9]{	1,0,0,
								1,1,1,
								0,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,2,meio);

		sequencia = new int[9]{	1,0,0,
								1,1,1,
								0,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,2,meio);

		sequencia = new int[9]{	0,0,0,
								1,1,1,
								1,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,2,meio);

		sequencia = new int[9]{	0,0,0,
								1,1,1,
								1,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,2,meio);

		sequencia = new int[9]{	0,0,0,
								1,1,1,
								0,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,2,meio);

		//------------FIM-------------


		//--------------CIMA----------


		sequencia = new int[9]{	1,1,1,
								1,1,1,
								1,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,8,meio);

		sequencia = new int[9]{	0,1,1,
								1,1,1,
								1,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,8,meio);

		sequencia = new int[9]{	1,1,0,
								1,1,1,
								1,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,8,meio);

		sequencia = new int[9]{	0,1,0,
								1,1,1,
								1,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,8,meio);

		sequencia = new int[9]{	1,1,1,
								1,1,1,
								0,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,8,meio);

		sequencia = new int[9]{	0,1,1,
								1,1,1,
								0,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,8,meio);

		sequencia = new int[9]{	1,1,0,
								1,1,1,
								0,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,8,meio);

		sequencia = new int[9]{	0,1,0,
								1,1,1,
								0,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,8,meio);

		sequencia = new int[9]{	1,1,1,
								1,1,1,
								1,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,8,meio);

		sequencia = new int[9]{	0,1,1,
								1,1,1,
								1,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,8,meio);

		sequencia = new int[9]{	1,1,0,
								1,1,1,
								1,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,8,meio);

		sequencia = new int[9]{	0,1,0,
								1,1,1,
								1,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,8,meio);

		sequencia = new int[9]{	1,1,1,
								1,1,1,
								0,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,8,meio);



		//------------FIM-------------




		//-------------MEIO ESQUERDA--------
		sequencia = new int[9]{	0,1,1,
								0,1,1,
								0,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,4,meio);

		sequencia = new int[9]{	0,1,0,
								0,1,1,
								0,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,4,meio);

		sequencia = new int[9]{	0,1,1,
								0,1,1,
								0,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,4,meio);

		sequencia = new int[9]{	1,1,1,
								0,1,1,
								1,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,4,meio);

		sequencia = new int[9]{	1,1,0,
								0,1,1,
								1,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,4,meio);

		sequencia = new int[9]{	1,1,1,
								0,1,1,
								1,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,4,meio);

		sequencia = new int[9]{	1,1,0,
								0,1,1,
								1,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,4,meio);

		sequencia = new int[9]{	0,1,1,
								0,1,1,
								1,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,4,meio);

		sequencia = new int[9]{	0,1,1,
								0,1,1,
								1,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,4,meio);

		sequencia = new int[9]{	0,1,0,
								0,1,1,
								1,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,4,meio);

		sequencia = new int[9]{	1,1,1,
								0,1,1,
								0,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,4,meio);

		sequencia = new int[9]{	1,1,0,
								0,1,1,
								0,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,4,meio);

		sequencia = new int[9]{	1,1,1,
								0,1,1,
								0,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,4,meio);

		//------------FIM-------------


		//------------MEIO DIREITA-------

		sequencia = new int[9]{	1,1,0,
								1,1,0,
								1,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,6,meio);

		sequencia = new int[9]{	1,1,0,
								1,1,0,
								0,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,6,meio);

		sequencia = new int[9]{	0,1,0,
								1,1,0,
								1,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,6,meio);

		sequencia = new int[9]{	0,1,0,
								1,1,0,
								0,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,6,meio);

		sequencia = new int[9]{	1,1,1,
								1,1,0,
								1,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,6,meio);

		sequencia = new int[9]{	0,1,1,
								1,1,0,
								1,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,6,meio);

		sequencia = new int[9]{	1,1,1,
								1,1,0,
								0,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,6,meio);

		sequencia = new int[9]{	0,1,1,
								1,1,0,
								0,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,6,meio);

		sequencia = new int[9]{	1,1,0,
								1,1,0,
								1,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,6,meio);

		sequencia = new int[9]{	0,1,0,
								1,1,0,
								1,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,6,meio);

		sequencia = new int[9]{	1,1,0,
								1,1,0,
								0,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,6,meio);

		sequencia = new int[9]{	0,1,0,
								1,1,0,
								0,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,6,meio);

		sequencia = new int[9]{	1,1,1,
								1,1,0,
								1,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,6,meio);

		sequencia = new int[9]{	0,1,1,
								1,1,0,
								1,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,6,meio);

		sequencia = new int[9]{	1,1,1,
								1,1,0,
								0,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,6,meio);

		sequencia = new int[9]{	0,1,1,
								1,1,0,
								0,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,6,meio);


		//------------FIM-------------



		//------------PONTA CIMA----------

		sequencia = new int[9]{	0,0,0,
								0,1,0,
								1,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,11,meio);

		sequencia = new int[9]{	0,0,0,
								0,1,0,
								0,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,11,meio);

		sequencia = new int[9]{	0,0,0,
								0,1,0,
								1,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,11,meio);

		sequencia = new int[9]{	1,0,0,
								0,1,0,
								1,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,11,meio);

		sequencia = new int[9]{	1,0,0,
								0,1,0,
								0,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,11,meio);

		sequencia = new int[9]{	1,0,0,
								0,1,0,
								1,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,11,meio);

		sequencia = new int[9]{	1,0,0,
								0,1,0,
								0,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,11,meio);

		sequencia = new int[9]{	0,0,1,
								0,1,0,
								1,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,11,meio);

		sequencia = new int[9]{	0,0,1,
								0,1,0,
								0,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,11,meio);

		sequencia = new int[9]{	0,0,1,
								0,1,0,
								1,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,11,meio);

		sequencia = new int[9]{	0,0,1,
								0,1,0,
								0,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,11,meio);

		sequencia = new int[9]{	1,0,1,
								0,1,0,
								1,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,11,meio);

		sequencia = new int[9]{	1,0,1,
								0,1,0,
								0,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,11,meio);

		sequencia = new int[9]{	1,0,1,
								0,1,0,
								1,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,11,meio);

		sequencia = new int[9]{	1,0,1,
								0,1,0,
								0,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,11,meio);

		sequencia = new int[9]{	0,0,0,
								0,1,0,
								0,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,11,meio);


		//------------FIM-------------



		//--------------PONTA BAIXO------

		sequencia = new int[9]{	1,1,1,
								0,1,0,
								1,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,12,meio);

		sequencia = new int[9]{	1,1,1,
								0,1,0,
								0,0,1	};
		
		compararListas(blocosEmVolta.ToArray(),sequencia,12,meio);

		sequencia = new int[9]{	1,1,1,
								0,1,0,
								1,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,12,meio);

		sequencia = new int[9]{	1,1,1,
								0,1,0,
								0,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,12,meio);

		sequencia = new int[9]{	0,1,1,
								0,1,0,
								1,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,12,meio);

		sequencia = new int[9]{	0,1,1,
								0,1,0,
								0,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,12,meio);

		sequencia = new int[9]{	1,1,1,
								0,1,0,
								0,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,12,meio);

		sequencia = new int[9]{	1,1,0,
								0,1,0,
								0,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,12,meio);

		sequencia = new int[9]{	1,1,1,
								0,1,0,
								1,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,12,meio);

		sequencia = new int[9]{	0,1,1,
								0,1,0,
								1,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,12,meio);

		sequencia = new int[9]{	1,1,0,
								0,1,0,
								1,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,12,meio);

		sequencia = new int[9]{	1,1,0,
								0,1,0,
								1,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,12,meio);

		sequencia = new int[9]{	0,1,0,
								0,1,0,
								1,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,12,meio);

		sequencia = new int[9]{	0,1,0,
								0,1,0,
								0,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,12,meio);

		sequencia = new int[9]{	0,1,0,
								0,1,0,
								1,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,12,meio);

		sequencia = new int[9]{	0,1,0,
								0,1,0,
								0,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,12,meio);

		//------------FIM-------------


		//-------------BLOCO PONTA ESQUERDA-------------


		sequencia = new int[9]{	0,0,1,
								0,1,1,
								0,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,13,meio);

		sequencia = new int[9]{	0,0,0,
								0,1,1,
								0,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,13,meio);

		sequencia = new int[9]{	0,0,1,
								0,1,1,
								0,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,13,meio);

		sequencia = new int[9]{	0,0,0,
								0,1,1,
								0,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,13,meio);

		sequencia = new int[9]{	1,0,1,
								0,1,1,
								0,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,13,meio);

		sequencia = new int[9]{	1,0,0,
								0,1,1,
								0,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,13,meio);

		sequencia = new int[9]{	1,0,1,
								0,1,1,
								0,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,13,meio);

		sequencia = new int[9]{	1,0,0,
								0,1,1,
								0,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,13,meio);

		sequencia = new int[9]{	0,0,1,
								0,1,1,
								1,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,13,meio);

		sequencia = new int[9]{	0,0,1,
								0,1,1,
								1,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,13,meio);

		sequencia = new int[9]{	0,0,0,
								0,1,1,
								1,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,13,meio);

		sequencia = new int[9]{	0,0,0,
								0,1,1,
								1,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,13,meio);

		sequencia = new int[9]{	1,0,1,
								0,1,1,
								1,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,13,meio);

		sequencia = new int[9]{	1,0,1,
								0,1,1,
								1,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,13,meio);

		sequencia = new int[9]{	1,0,0,
								0,1,1,
								1,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,13,meio);

		sequencia = new int[9]{	1,0,0,
								0,1,1,
								1,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,13,meio);

		//-----------------FIM-------------



		//---------------PONTA DIREITA----------

		sequencia = new int[9]{	1,0,0,
								1,1,0,
								1,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,14,meio);

		sequencia = new int[9]{	1,0,0,
								1,1,0,
								1,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,14,meio);

		sequencia = new int[9]{	1,0,1,
								1,1,0,
								1,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,14,meio);

		sequencia = new int[9]{	1,0,1,
								1,1,0,
								1,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,14,meio);

		sequencia = new int[9]{	0,0,0,
								1,1,0,
								1,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,14,meio);

		sequencia = new int[9]{	0,0,1,
								1,1,0,
								1,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,14,meio);

		sequencia = new int[9]{	0,0,0,
								1,1,0,
								1,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,14,meio);

		sequencia = new int[9]{	0,0,1,
								1,1,0,
								1,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,14,meio);

		sequencia = new int[9]{	1,0,0,
								1,1,0,
								0,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,14,meio);

		sequencia = new int[9]{	1,0,1,
								1,1,0,
								0,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,14,meio);

		sequencia = new int[9]{	1,0,0,
								1,1,0,
								0,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,14,meio);

		sequencia = new int[9]{	1,0,1,
								1,1,0,
								0,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,14,meio);


		sequencia = new int[9]{	0,0,0,
								1,1,0,
								0,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,14,meio);


		sequencia = new int[9]{	0,0,1,
								1,1,0,
								0,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,14,meio);


		sequencia = new int[9]{	0,0,0,
								1,1,0,
								0,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,14,meio);


		sequencia = new int[9]{	0,0,1,
								1,1,0,
								0,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,14,meio);

		//--------------FiM-----------


		//---------------MEIO VERTICAL----------

		sequencia = new int[9]{	0,1,0,
								0,1,0,
								0,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,15,meio);

		sequencia = new int[9]{	1,1,0,
								0,1,0,
								0,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,15,meio);

		sequencia = new int[9]{	1,1,0,
								0,1,0,
								1,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,15,meio);

		sequencia = new int[9]{	1,1,0,
								0,1,0,
								0,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,15,meio);

		sequencia = new int[9]{	1,1,1,
								0,1,0,
								0,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,15,meio);

		sequencia = new int[9]{	1,1,1,
								0,1,0,
								1,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,15,meio);

		sequencia = new int[9]{	1,1,1,
								0,1,0,
								0,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,15,meio);

		sequencia = new int[9]{	1,1,1,
								0,1,0,
								1,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,15,meio);

		sequencia = new int[9]{	0,1,0,
								0,1,0,
								1,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,15,meio);

		sequencia = new int[9]{	0,1,0,
								0,1,0,
								0,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,15,meio);

		sequencia = new int[9]{	0,1,1,
								0,1,0,
								0,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,15,meio);

		sequencia = new int[9]{	0,1,0,
								0,1,0,
								1,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,15,meio);

		sequencia = new int[9]{	0,1,1,
								0,1,0,
								1,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,15,meio);

		sequencia = new int[9]{	1,1,0,
								0,1,0,
								1,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,15,meio);

		sequencia = new int[9]{	0,1,1,
								0,1,0,
								1,1,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,15,meio);

		sequencia = new int[9]{	0,1,1,
								0,1,0,
								0,1,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,15,meio);

		//--------------FIM-----------


		//--------------MEIO HORIZONTAL----------

		sequencia = new int[9]{	0,0,0,
								1,1,1,
								0,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,16,meio);

		sequencia = new int[9]{	1,0,0,
								1,1,1,
								1,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,16,meio);

		sequencia = new int[9]{	1,0,0,
								1,1,1,
								0,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,16,meio);

		sequencia = new int[9]{	1,0,0,
								1,1,1,
								0,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,16,meio);

		sequencia = new int[9]{	1,0,1,
								1,1,1,
								0,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,16,meio);

		sequencia = new int[9]{	1,0,0,
								1,1,1,
								1,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,16,meio);

		sequencia = new int[9]{	1,0,1,
								1,1,1,
								0,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,16,meio);

		sequencia = new int[9]{	0,0,1,
								1,1,1,
								0,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,16,meio);

		sequencia = new int[9]{	0,0,0,
								1,1,1,
								0,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,16,meio);

		sequencia = new int[9]{	0,0,0,
								1,1,1,
								1,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,16,meio);

		sequencia = new int[9]{	0,0,1,
								1,1,1,
								1,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,16,meio);

		sequencia = new int[9]{	1,0,1,
								1,1,1,
								1,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,16,meio);

		sequencia = new int[9]{	0,0,1,
								1,1,1,
								0,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,16,meio);

		sequencia = new int[9]{	1,0,1,
								1,1,1,
								1,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,16,meio);

		sequencia = new int[9]{	0,0,1,
								1,1,1,
								1,0,1	};

		compararListas(blocosEmVolta.ToArray(),sequencia,16,meio);

		//--------------FIM-----------



		blocosEmVolta.Clear();

		updateUV ();

	}

	void compararListas(int[] blocosEmVolta,int[] sequencia,int idTypeBloco,int indexBloco){
		if( blocosEmVolta[0] == sequencia[0] &&
			blocosEmVolta[1] == sequencia[1] &&
			blocosEmVolta[2] == sequencia[2] &&
			blocosEmVolta[3] == sequencia[3] &&
			blocosEmVolta[4] == sequencia[4] &&
			blocosEmVolta[5] == sequencia[5] &&
			blocosEmVolta[6] == sequencia[6] &&
			blocosEmVolta[7] == sequencia[7] &&
			blocosEmVolta[8] == sequencia[8])
		{
			setUvBlock(idTypeBloco, indexBloco);
		}
	}
}
