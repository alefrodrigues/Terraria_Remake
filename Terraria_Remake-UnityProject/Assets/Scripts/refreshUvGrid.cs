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
		if(Input.GetKeyDown(KeyCode.KeypadEnter)){
			for(int x = 0;x < gridSizeX;x++){
				for(int y = 0;y < gridSizeY;y++){
					definirTextureBloco (x,y);
				}
			}
			updateUV ();
			print ("script RefreshUV atualizou a UVLIST!");
		}
	}
		
	void updateUV(){
		mesh.Clear ();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.uv = uvs;
		mesh.RecalculateNormals ();
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
			uvs [_idUv+1] = new Vector2(0.25f,.5f);
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

		}
	}

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
