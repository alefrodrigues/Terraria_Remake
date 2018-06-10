using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chunkControl : MonoBehaviour {

	//procedural grid lugar aonde fica todas as informaçoes do mundo criado
	proceduralGrid pGrid;

	//propriedades da MeshGrid do Chunk
	public Mesh mesh;

	public List<Vector2> chunkUVGrid = new List<Vector2>();
	public List<Vector3> chunkVerticesGrid = new List<Vector3>();
	public List<int> chunkTrianglesGrid = new List<int>();
	public List<int> chunkEstadoBloco = new List<int>();
	public List<int> chunkTipoBloco = new List<int>();

	public List<int> blocosEmVolta = new List<int>();

	public int chunkX;
	public int chunkY;

	public bool chunkAtualizado = false;
	void Start(){

		//criarVerticesChunk();

	}
	void Awake(){
		mesh = GetComponent<MeshFilter>().mesh;
		pGrid = (proceduralGrid)FindObjectOfType(typeof(proceduralGrid));
	}

	void Update() {
		if(proceduralGrid.podePreencher && chunkAtualizado == false){

			preencherChunk();

			updateMesh();

			chunkAtualizado = true;
		}

		if(Vector2.Distance(new Vector2(chunkX,chunkY),sceneManager.playerPosition) <= 200.0f){
			mostrarMesh();
		}else if(Vector2.Distance(new Vector2(chunkX,chunkY),sceneManager.playerPosition) > 200.0f){
			esconderMesh();
		}

		if(Input.GetKey(KeyCode.Mouse0)){
			Vector2 p = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x,Input.mousePosition.y));
			
			int posX =  Mathf.FloorToInt(p.x);
			int posY = Mathf.FloorToInt(p.y);

			if(p.y > 99){
				posX =  Mathf.FloorToInt(p.x - 1f);
			}
			if((posX >= chunkX && posX <= chunkX + 99) && (posY >= chunkY && posY <= chunkY + 99)){
				deletarBloco(posX,posY);
				definirTexturaBloco(p.x,p.y);
			}
		}

		if(Input.GetKey(KeyCode.Mouse1)){
			Vector2 p = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x,Input.mousePosition.y));
			
			int posX =  Mathf.FloorToInt(p.x);
			int posY = Mathf.FloorToInt(p.y);

			if(p.y > 99){
				posX =  Mathf.FloorToInt(p.x - 1f);
			}
			if((posX >= chunkX && posX <= chunkX + 99) && (posY >= chunkY && posY <= chunkY + 99)){
				criarBloco(posX,posY);
				definirTexturaBloco(p.x,p.y);
			}
		}

	}

	void preencherChunk(){
		//resetando valores das listas do chunk
		chunkVerticesGrid.Clear();
		chunkTrianglesGrid.Clear();
		chunkUVGrid.Clear();
		chunkEstadoBloco.Clear();
		chunkTipoBloco.Clear();
		//tracker value
		int blocoAtual = 0;

		int t = 0;

		for( int x = chunkX; x < chunkX + 100; x++){
			for( int y = chunkY; y < chunkY+100; y++){
				blocoAtual = (y + (x * pGrid.gridSizeY))*4;

				//preenchendo vertices do chunk
					chunkVerticesGrid.Add(new Vector3 (x,y,0));
					chunkVerticesGrid.Add(new Vector3 (x,y+1,0));
					chunkVerticesGrid.Add(new Vector3 (x+1,y,0));
					chunkVerticesGrid.Add(new Vector3 (x+1,y+1,0)); 

				//preenchendo estado dos blocos do chunk
					chunkEstadoBloco.Add(pGrid.estadoBloco[blocoAtual]);
					chunkEstadoBloco.Add(pGrid.estadoBloco[blocoAtual+1]);
					chunkEstadoBloco.Add(pGrid.estadoBloco[blocoAtual+2]);
					chunkEstadoBloco.Add(pGrid.estadoBloco[blocoAtual+3]);
					chunkEstadoBloco.Add(pGrid.estadoBloco[blocoAtual+4]);
					chunkEstadoBloco.Add(pGrid.estadoBloco[blocoAtual+5]);

				//preenchendo tipo dos blocos do chunk
					chunkTipoBloco.Add(pGrid.tipoBloco[blocoAtual]);
					chunkTipoBloco.Add(pGrid.tipoBloco[blocoAtual+1]);
					chunkTipoBloco.Add(pGrid.tipoBloco[blocoAtual+2]);
					chunkTipoBloco.Add(pGrid.tipoBloco[blocoAtual+3]);
					chunkTipoBloco.Add(pGrid.tipoBloco[blocoAtual+4]);
					chunkTipoBloco.Add(pGrid.tipoBloco[blocoAtual+5]);

				if(pGrid.estadoBloco[blocoAtual] == 1){

					//preenchendo triangulos do chunk
						chunkTrianglesGrid.Add(t);
						chunkTrianglesGrid.Add(t+1);
						chunkTrianglesGrid.Add(t+2);
						chunkTrianglesGrid.Add(t+2);
						chunkTrianglesGrid.Add(t+1);
						chunkTrianglesGrid.Add(t+3);

				}else if(pGrid.estadoBloco[blocoAtual] == 0){

					//preenchendo triangulos do chunk
						chunkTrianglesGrid.Add(t);
						chunkTrianglesGrid.Add(t);
						chunkTrianglesGrid.Add(t);
						chunkTrianglesGrid.Add(t);
						chunkTrianglesGrid.Add(t);
						chunkTrianglesGrid.Add(t);
				}

				//populando Uvs do chunk
					chunkUVGrid.Add(new Vector2(0.25f,0.666666f));
					chunkUVGrid.Add(new Vector2(0.25f,0.999999f));
					chunkUVGrid.Add(new Vector2(0.3125f,0.666666f));
					chunkUVGrid.Add(new Vector2(0.3125f,0.999999f));
				t += 4;
			}
		}
	}

	void deletarBloco(int posX, int posY){
		int blocoAtual = (posY + (posX * pGrid.gridSizeY))*4;

		if(posX > 99){
			posX -= chunkX;
		}

		int chunkBlocoAtual = (posY + (posX * 100))*4;

		pGrid.estadoBloco[blocoAtual] = 0;
		pGrid.estadoBloco[blocoAtual+1] = 0;
		pGrid.estadoBloco[blocoAtual+2] = 0;
		pGrid.estadoBloco[blocoAtual+3] = 0;

		chunkEstadoBloco[chunkBlocoAtual] = 0;
		chunkEstadoBloco[chunkBlocoAtual+1] = 0;
		chunkEstadoBloco[chunkBlocoAtual+2] = 0;
		chunkEstadoBloco[chunkBlocoAtual+3] = 0;

		chunkTrianglesGrid.Remove(chunkBlocoAtual);
		chunkTrianglesGrid.Remove(chunkBlocoAtual+1);
		chunkTrianglesGrid.Remove(chunkBlocoAtual+2);
		chunkTrianglesGrid.Remove(chunkBlocoAtual+2);
		chunkTrianglesGrid.Remove(chunkBlocoAtual+1);
		chunkTrianglesGrid.Remove(chunkBlocoAtual+3);

		print(chunkEstadoBloco[chunkBlocoAtual]);

		updateMesh();

	}

	void criarBloco(int posX, int posY){

		int blocoAtual = (posY + (posX * pGrid.gridSizeY))*4;

		if(posX > 99){
			posX -= chunkX;
		}

		int chunkBlocoAtual = (posY + (posX * 100))*4;

		pGrid.estadoBloco[blocoAtual] = 1;
		pGrid.estadoBloco[blocoAtual+1] = 1;
		pGrid.estadoBloco[blocoAtual+2] = 1;
		pGrid.estadoBloco[blocoAtual+3] = 1;

		chunkEstadoBloco[chunkBlocoAtual] = 1;
		chunkEstadoBloco[chunkBlocoAtual+1] = 1;
		chunkEstadoBloco[chunkBlocoAtual+2] = 1;
		chunkEstadoBloco[chunkBlocoAtual+3] = 1;

		chunkTrianglesGrid.Add(chunkBlocoAtual);
		chunkTrianglesGrid.Add(chunkBlocoAtual+1);
		chunkTrianglesGrid.Add(chunkBlocoAtual+2);
		chunkTrianglesGrid.Add(chunkBlocoAtual+2);
		chunkTrianglesGrid.Add(chunkBlocoAtual+1);
		chunkTrianglesGrid.Add(chunkBlocoAtual+3);

		print(chunkEstadoBloco[chunkBlocoAtual]);

		updateMesh();
	}

	public void updateMesh(){
		mesh.Clear();
		mesh.vertices = chunkVerticesGrid.ToArray();
		mesh.triangles = chunkTrianglesGrid.ToArray();
		mesh.uv = chunkUVGrid.ToArray();
		mesh.RecalculateNormals();
	}

	void mostrarMesh(){
		mesh.Clear();
		mesh.vertices = chunkVerticesGrid.ToArray();
		mesh.triangles = chunkTrianglesGrid.ToArray();
		mesh.uv = chunkUVGrid.ToArray();
		mesh.RecalculateNormals();
	}

	void esconderMesh(){
		mesh.Clear();
	}

	void setUvBlock (int idDirecaoBloco, int indexBloco) {



		//total de blocos na HORIZONTAL
		float totalBlocosHorizontal = 16;

		//total de blocos na VERTICAL
		float totalBlocosVertical = 3;

		//posiçoes da UV;
		float posX = (1 / totalBlocosHorizontal);
		float posY = (1 / totalBlocosVertical);

		if(chunkTipoBloco[indexBloco] == 0){
			posY = 0;
		}
		else if(chunkTipoBloco[indexBloco] == 1){
			posY = (1 / totalBlocosVertical);
		}
		else if(chunkTipoBloco[indexBloco] == 2){
			posY = (1 / totalBlocosVertical)*2;
		}

		switch (idDirecaoBloco)
		{


			case 1:
				//cima esquerda
			chunkUVGrid [indexBloco] = new Vector2(posX-posX,posY);
			chunkUVGrid [indexBloco+1] = new Vector2(posX-posX,posY+(1 / totalBlocosVertical));
			chunkUVGrid [indexBloco+2] = new Vector2(posX,posY);
			chunkUVGrid [indexBloco+3] = new Vector2(posX,posY+(1 / totalBlocosVertical));
				break;
			case 2:
				//cima
			chunkUVGrid [indexBloco] = new Vector2(posX,posY);
			chunkUVGrid [indexBloco+1] = new Vector2(posX,posY+(1 / totalBlocosVertical));
			chunkUVGrid [indexBloco+2] = new Vector2(posX*2,posY);
			chunkUVGrid [indexBloco+3] = new Vector2(posX*2,posY+(1 / totalBlocosVertical));
				break;
			case 3:
				//cima direita
			chunkUVGrid [indexBloco] = new Vector2(posX*2,posY);
			chunkUVGrid [indexBloco+1] = new Vector2(posX*2,posY+(1 / totalBlocosVertical));
			chunkUVGrid [indexBloco+2] = new Vector2(posX*3,posY);
			chunkUVGrid [indexBloco+3] = new Vector2(posX*3,posY+(1 / totalBlocosVertical));
				break;

			case 4:
				//meio esquerda
			chunkUVGrid [indexBloco] = new Vector2(posX*3,posY);
			chunkUVGrid [indexBloco+1] = new Vector2(posX*3,posY+(1 / totalBlocosVertical));
			chunkUVGrid [indexBloco+2] = new Vector2(posX*4,posY);
			chunkUVGrid [indexBloco+3] = new Vector2(posX*4,posY+(1 / totalBlocosVertical));
				break;
			case 5:
				//meio
			chunkUVGrid [indexBloco] = new Vector2(posX+(posX*3),posY);
			chunkUVGrid [indexBloco+1] = new Vector2(posX+(posX*3),posY+(1 / totalBlocosVertical));
			chunkUVGrid [indexBloco+2] = new Vector2(posX+(posX*4),posY);
			chunkUVGrid [indexBloco+3] = new Vector2(posX+(posX*4),posY+(1 / totalBlocosVertical));
				break;
			case 6:
				//meio direita
			chunkUVGrid [indexBloco] = new Vector2(posX*5,posY);
			chunkUVGrid [indexBloco+1] = new Vector2(posX*5,posY+(1 / totalBlocosVertical));
			chunkUVGrid [indexBloco+2] = new Vector2(posX*6,posY);
			chunkUVGrid [indexBloco+3] = new Vector2(posX*6,posY+(1 / totalBlocosVertical));
				break;
			case 7:
				//baixo esquerda
			chunkUVGrid [indexBloco] = new Vector2(posX*6,posY);
			chunkUVGrid [indexBloco+1] = new Vector2(posX*6,posY+(1 / totalBlocosVertical));
			chunkUVGrid [indexBloco+2] = new Vector2(posX*7,posY);
			chunkUVGrid [indexBloco+3] = new Vector2(posX*7,posY+(1 / totalBlocosVertical));
				break;
			case 8:
				//baixo
			chunkUVGrid [indexBloco] = new Vector2(posX*7,posY);
			chunkUVGrid [indexBloco+1] = new Vector2(posX*7,posY+(1 / totalBlocosVertical));
			chunkUVGrid [indexBloco+2] = new Vector2(posX*8,posY);
			chunkUVGrid [indexBloco+3] = new Vector2(posX*8,posY+(1 / totalBlocosVertical));
				break;
			case 9:
				//baixo direita
			chunkUVGrid [indexBloco] = new Vector2(posX*8,posY);
			chunkUVGrid [indexBloco+1] = new Vector2(posX*8,posY+(1 / totalBlocosVertical));
			chunkUVGrid [indexBloco+2] = new Vector2(posX*9,posY);
			chunkUVGrid [indexBloco+3] = new Vector2(posX*9,posY+(1 / totalBlocosVertical));
				break;
			case 10:
				//bloco inteiro
			chunkUVGrid [indexBloco] = new Vector2(posX*9,posY);
			chunkUVGrid [indexBloco+1] = new Vector2(posX*9,posY+(1 / totalBlocosVertical));
			chunkUVGrid [indexBloco+2] = new Vector2(posX*10,posY);
			chunkUVGrid [indexBloco+3] = new Vector2(posX*10,posY+(1 / totalBlocosVertical));
				break;
			case 11:
			//bloco ponta cima
			chunkUVGrid [indexBloco] = new Vector2(posX*10,posY);
			chunkUVGrid [indexBloco+1] = new Vector2(posX*10,posY+(1 / totalBlocosVertical));
			chunkUVGrid [indexBloco+2] = new Vector2(posX*11,posY);
			chunkUVGrid [indexBloco+3] = new Vector2(posX*11,posY+(1 / totalBlocosVertical));
				break;
			case 12:
			//bloco ponta baixo
			chunkUVGrid [indexBloco] = new Vector2(posX*11,posY);
			chunkUVGrid [indexBloco+1] = new Vector2(posX*11,posY+(1 / totalBlocosVertical));
			chunkUVGrid [indexBloco+2] = new Vector2(posX*12,posY);
			chunkUVGrid [indexBloco+3] = new Vector2(posX*12,posY+(1 / totalBlocosVertical));
			break;
			case 13:
			//bloco ponta esquerda
			chunkUVGrid [indexBloco] = new Vector2(posX*12,posY);
			chunkUVGrid [indexBloco+1] = new Vector2(posX*12,posY+(1 / totalBlocosVertical));
			chunkUVGrid [indexBloco+2] = new Vector2(posX*13,posY);
			chunkUVGrid [indexBloco+3] = new Vector2(posX*13,posY+(1 / totalBlocosVertical));
			break;
			case 14:
			//bloco ponta direita
			chunkUVGrid [indexBloco] = new Vector2(posX*13,posY);
			chunkUVGrid [indexBloco+1] = new Vector2(posX*13,posY+(1 / totalBlocosVertical));
			chunkUVGrid [indexBloco+2] = new Vector2(posX*14,posY);
			chunkUVGrid [indexBloco+3] = new Vector2(posX*14,posY+(1 / totalBlocosVertical));
			break;
			case 15:
			//bloco meio vertical
			chunkUVGrid [indexBloco] = new Vector2(posX*14,posY);
			chunkUVGrid [indexBloco+1] = new Vector2(posX*14,posY+(1 / totalBlocosVertical));
			chunkUVGrid [indexBloco+2] = new Vector2(posX*15,posY);
			chunkUVGrid [indexBloco+3] = new Vector2(posX*15,posY+(1 / totalBlocosVertical));
				break;
			case 16:
			//bloco meio horizontal
			chunkUVGrid [indexBloco] = new Vector2(posX*15,posY);
			chunkUVGrid [indexBloco+1] = new Vector2(posX*15,posY+(1 / totalBlocosVertical));
			chunkUVGrid [indexBloco+2] = new Vector2(posX*16,posY);
			chunkUVGrid [indexBloco+3] = new Vector2(posX*16,posY+(1 / totalBlocosVertical));
				break;

		}
	}

	public void definirTexturaBloco(float posX,float posY){

		if(posX > 99f){
			posX -= chunkX;
		}
		if(posX < 0f){
			posX = 0f;
		}
		if(posY > 99f){
			posY -= chunkY;
		}
		if(posY < 0f){
			posY = 0f;
		}

		Vector2 cimaE = new Vector2 (posX-1,posY+1);
		Vector2 cima = new Vector2 (posX,posY+1);
		Vector2 cimaD = new Vector2 (posX+1,posY+1);
		Vector2 mE = new Vector2 (posX-1,posY);
		Vector2 mD = new Vector2 (posX+1,posY);
		Vector2 baixoE = new Vector2 (posX-1,posY-1);
		Vector2 baixo = new Vector2 (posX,posY-1);
		Vector2 baixoD = new Vector2 (posX+1,posY-1);

		definirTexturaMeshGrid (Mathf.FloorToInt(cimaE.x),Mathf.FloorToInt(cimaE.y));
		definirTexturaMeshGrid (Mathf.FloorToInt(cima.x),Mathf.FloorToInt(cima.y));
		definirTexturaMeshGrid (Mathf.FloorToInt(cimaD.x),Mathf.FloorToInt(cimaD.y));
		definirTexturaMeshGrid (Mathf.FloorToInt(mE.x),Mathf.FloorToInt(mE.y));
		definirTexturaMeshGrid (Mathf.FloorToInt(mD.x),Mathf.FloorToInt(mD.y));
		definirTexturaMeshGrid (Mathf.FloorToInt(baixoE.x),Mathf.FloorToInt(baixoE.y));
		definirTexturaMeshGrid (Mathf.FloorToInt(baixo.x),Mathf.FloorToInt(baixo.y));
		definirTexturaMeshGrid (Mathf.FloorToInt(baixoD.x),Mathf.FloorToInt(baixoD.y));

		updateMesh ();
	}

	public void definirTexturaMeshGrid(int posX, int posY){

		int[] sequencia;
		//formulas para identificar todos os blocos em volta do bloco a ser analisado
		if(posX > 99){
			posX -= chunkX;
		}
		if(posX < 0){
			posX = 0;
		}
		int cimaEsquerda = (posY + (posX * 100) - 99)*4;
		int cima = (posY + (posX * 100) + 1) * 4;
		int cimaDireita = (posY + (posX * 100) + 101) * 4;
		int meioEsquerda = (posY + (posX * 100) - 100) * 4;
		int meio = (posY + (posX * 100)) * 4;
		int meioDireita = (posY + (posX * 100) + 100) * 4;
		int baixoEsquerda = (posY + (posX * 100) - 101) * 4;
		int baixo = (posY + (posX * 100) - 1) * 4;
		int baixoDireita = (posY + (posX * 100) + 99) * 4;

		/*print(  cimaEsquerda+","+
				cima+","+
				meioEsquerda+","+
				meio+","+
				meioDireita+","+
				baixoEsquerda+","+
				baixo+","+
				baixoDireita);*/

		//resetando os bloco que estiverem em posiçoes negativas e/ou sua posiçoes forem maiores menores que o vetor
		if(cimaEsquerda < 0){
			cimaEsquerda = 0;
			chunkEstadoBloco[cimaEsquerda] = 0;
			pGrid.estadoBloco[cimaEsquerda] = 0;
		}
		if(cima < 0){
			cima = 0;
			chunkEstadoBloco[cima] = 0;
			pGrid.estadoBloco[cima] = 0;
		}
		if(cimaDireita < 0){
			cimaDireita = 0;
			chunkEstadoBloco[cimaDireita] = 0;
			pGrid.estadoBloco[cimaDireita] = 0;
		}
		if(meioEsquerda < 0){
			meioEsquerda = 0;
			chunkEstadoBloco[meioEsquerda] = 0;
			pGrid.estadoBloco[meioEsquerda] = 0;
		}
		if(meioDireita < 0){
			meioDireita = 0;
			chunkEstadoBloco[meioDireita] = 0;
			pGrid.estadoBloco[meioDireita] = 0;
		}
		if(baixoEsquerda < 0){
			baixoEsquerda = 0;
			chunkEstadoBloco[baixoEsquerda] = 0;
			pGrid.estadoBloco[baixoEsquerda] = 0;
		}
		if(baixo < 0){
			baixo = 0;
			chunkEstadoBloco[baixo] = 0;
			pGrid.estadoBloco[baixo] = 0;
		}
		if(baixoDireita < 0){
			baixoDireita = 0;
			chunkEstadoBloco[baixoDireita] = 0;
			pGrid.estadoBloco[baixoDireita] = 0;
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

		blocosEmVolta.Add(chunkEstadoBloco[cimaEsquerda]);
		blocosEmVolta.Add(chunkEstadoBloco[cima]);
		blocosEmVolta.Add(chunkEstadoBloco[cimaDireita]);
		blocosEmVolta.Add(chunkEstadoBloco[meioEsquerda]);
		blocosEmVolta.Add(chunkEstadoBloco[meio]);
		blocosEmVolta.Add(chunkEstadoBloco[meioDireita]);
		blocosEmVolta.Add(chunkEstadoBloco[baixoEsquerda]);
		blocosEmVolta.Add(chunkEstadoBloco[baixo]);
		blocosEmVolta.Add(chunkEstadoBloco[baixoDireita]);

		print(blocosEmVolta[0]+","+
			blocosEmVolta[1]+","+
			blocosEmVolta[2]+","+
			blocosEmVolta[3]+","+
			blocosEmVolta[4]+","+
			blocosEmVolta[5]+","+
			blocosEmVolta[6]+","+
			blocosEmVolta[7]+","+
			blocosEmVolta[8]);


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

		sequencia = new int[9]{	1,1,0,
								0,1,0,
								0,0,0	};

		compararListas(blocosEmVolta.ToArray(),sequencia,12,meio);

		sequencia = new int[9]{	0,1,1,
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

		updateMesh ();

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
