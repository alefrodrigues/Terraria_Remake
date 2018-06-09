using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class refreshUvGrid : MonoBehaviour {

	//procedural grid lugar aonde fica todas as informaçoes do mundo criado
	proceduralGrid pGrid;

	//chunk que ira ser atualizado
	chunkControl cControl;

	//arrays para montar a MeshGrid

	public Vector3[] vertices;
	public Vector2[] uvs;
	public int[] triangles;
	public int[] estadoBloco;
	public int[] tipoBloco;
	public int[] materialBloco;

	List <int> blocosEmVolta = new List <int> ();

	//MeshGrid
	public Mesh mesh;


	public int chunkX;
	public int chunkY;

	void Awake(){
		
		pGrid = (proceduralGrid)FindObjectOfType(typeof(proceduralGrid));
		cControl = (chunkControl)FindObjectOfType(typeof(chunkControl));
		
	}

	void Start () {

	}

	void Update () {

		if(cControl.chunkAtualizado == true){
			mesh = GetComponent<MeshFilter> ().mesh;

			vertices = cControl.chunkVerticesGrid.ToArray();
			triangles = cControl.chunkTrianglesGrid.ToArray();
		}


		if(Input.GetKeyDown(KeyCode.Mouse0)){
			Vector2 p = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x,Input.mousePosition.y));
			
			int posX =  Mathf.FloorToInt(p.x);
			int posY = Mathf.FloorToInt(p.y);

			if((posX >= chunkX && posX <= chunkX + 100) && (posY >= chunkY && posY <= chunkY + 100)){
				definirTexturaBloco(p.x,p.y);
			}
		}

		if(Input.GetKeyDown(KeyCode.Mouse1)){
			Vector2 p = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x,Input.mousePosition.y));
			
			int posX =  Mathf.FloorToInt(p.x);
			int posY = Mathf.FloorToInt(p.y);

			int indexBloco = (posY+(posX*100))*4;

			setUvBlock(8,indexBloco);
			updateUV();
		}


	}
		
	void updateUV(){
		//print(vertices.Length);
		cControl.mesh.Clear ();
		cControl.mesh.vertices = vertices;
		cControl.mesh.triangles = triangles;
		cControl.mesh.uv = uvs;
		cControl.mesh.RecalculateNormals ();
	}

	void trocarTexutaBloco(int idBloco,int indexBloco){
		setUvBlock(idBloco,indexBloco);
		updateUV();
	}

	void setUvBlock (int idDirecaoBloco, int indexBloco) {
		//total de blocos na HORIZONTAL
		float totalBlocosHorizontal = 16;

		//total de blocos na VERTICAL
		float totalBlocosVertical = 3;

		//posiçoes da UV;
		float posX = (1 / totalBlocosHorizontal);
		float posY = (1 / totalBlocosVertical);

		if(tipoBloco[indexBloco] == 0){
			posY = 0;
		}
		else if(tipoBloco[indexBloco] == 1){
			posY = (1 / totalBlocosVertical);
		}
		else if(tipoBloco[indexBloco] == 2){
			posY = (1 / totalBlocosVertical)*2;
		}

		print(idDirecaoBloco);
		switch (idDirecaoBloco)
		{


			case 1:
				//cima esquerda
			cControl.chunkUVGrid [indexBloco] = new Vector2(posX-posX,posY);
			cControl.chunkUVGrid [indexBloco+1] = new Vector2(posX-posX,posY+(1 / totalBlocosVertical));
			cControl.chunkUVGrid [indexBloco+2] = new Vector2(posX,posY);
			cControl.chunkUVGrid [indexBloco+3] = new Vector2(posX,posY+(1 / totalBlocosVertical));
				break;
			case 2:
				//cima
			cControl.chunkUVGrid [indexBloco] = new Vector2(posX,posY);
			cControl.chunkUVGrid [indexBloco+1] = new Vector2(posX,posY+(1 / totalBlocosVertical));
			cControl.chunkUVGrid [indexBloco+2] = new Vector2(posX*2,posY);
			cControl.chunkUVGrid [indexBloco+3] = new Vector2(posX*2,posY+(1 / totalBlocosVertical));
				break;
			case 3:
				//cima direita
			cControl.chunkUVGrid [indexBloco] = new Vector2(posX*2,posY);
			cControl.chunkUVGrid [indexBloco+1] = new Vector2(posX*2,posY+(1 / totalBlocosVertical));
			cControl.chunkUVGrid [indexBloco+2] = new Vector2(posX*3,posY);
			cControl.chunkUVGrid [indexBloco+3] = new Vector2(posX*3,posY+(1 / totalBlocosVertical));
				break;

			case 4:
				//meio esquerda
			cControl.chunkUVGrid [indexBloco] = new Vector2(posX*3,posY);
			cControl.chunkUVGrid [indexBloco+1] = new Vector2(posX*3,posY+(1 / totalBlocosVertical));
			cControl.chunkUVGrid [indexBloco+2] = new Vector2(posX*4,posY);
			cControl.chunkUVGrid [indexBloco+3] = new Vector2(posX*4,posY+(1 / totalBlocosVertical));
				break;
			case 5:
				//meio
			cControl.chunkUVGrid [indexBloco] = new Vector2(posX+(posX*3),posY);
			cControl.chunkUVGrid [indexBloco+1] = new Vector2(posX+(posX*3),posY+(1 / totalBlocosVertical));
			cControl.chunkUVGrid [indexBloco+2] = new Vector2(posX+(posX*4),posY);
			cControl.chunkUVGrid [indexBloco+3] = new Vector2(posX+(posX*4),posY+(1 / totalBlocosVertical));
				break;
			case 6:
				//meio direita
			cControl.chunkUVGrid [indexBloco] = new Vector2(posX*5,posY);
			cControl.chunkUVGrid [indexBloco+1] = new Vector2(posX*5,posY+(1 / totalBlocosVertical));
			cControl.chunkUVGrid [indexBloco+2] = new Vector2(posX*6,posY);
			cControl.chunkUVGrid [indexBloco+3] = new Vector2(posX*6,posY+(1 / totalBlocosVertical));
				break;
			case 7:
				//baixo esquerda
			cControl.chunkUVGrid [indexBloco] = new Vector2(posX*6,posY);
			cControl.chunkUVGrid [indexBloco+1] = new Vector2(posX*6,posY+(1 / totalBlocosVertical));
			cControl.chunkUVGrid [indexBloco+2] = new Vector2(posX*7,posY);
			cControl.chunkUVGrid [indexBloco+3] = new Vector2(posX*7,posY+(1 / totalBlocosVertical));
				break;
			case 8:
				//baixo
			cControl.chunkUVGrid [indexBloco] = new Vector2(posX*7,posY);
			cControl.chunkUVGrid [indexBloco+1] = new Vector2(posX*7,posY+(1 / totalBlocosVertical));
			cControl.chunkUVGrid [indexBloco+2] = new Vector2(posX*8,posY);
			cControl.chunkUVGrid [indexBloco+3] = new Vector2(posX*8,posY+(1 / totalBlocosVertical));
				break;
			case 9:
				//baixo direita
			cControl.chunkUVGrid [indexBloco] = new Vector2(posX*8,posY);
			cControl.chunkUVGrid [indexBloco+1] = new Vector2(posX*8,posY+(1 / totalBlocosVertical));
			cControl.chunkUVGrid [indexBloco+2] = new Vector2(posX*9,posY);
			cControl.chunkUVGrid [indexBloco+3] = new Vector2(posX*9,posY+(1 / totalBlocosVertical));
				break;
			case 10:
				//bloco inteiro
			cControl.chunkUVGrid [indexBloco] = new Vector2(posX*9,posY);
			cControl.chunkUVGrid [indexBloco+1] = new Vector2(posX*9,posY+(1 / totalBlocosVertical));
			cControl.chunkUVGrid [indexBloco+2] = new Vector2(posX*10,posY);
			cControl.chunkUVGrid [indexBloco+3] = new Vector2(posX*10,posY+(1 / totalBlocosVertical));
				break;
			case 11:
			//bloco ponta cima
			cControl.chunkUVGrid [indexBloco] = new Vector2(posX*10,posY);
			cControl.chunkUVGrid [indexBloco+1] = new Vector2(posX*10,posY+(1 / totalBlocosVertical));
			cControl.chunkUVGrid [indexBloco+2] = new Vector2(posX*11,posY);
			cControl.chunkUVGrid [indexBloco+3] = new Vector2(posX*11,posY+(1 / totalBlocosVertical));
				break;
			case 12:
			//bloco ponta baixo
			cControl.chunkUVGrid [indexBloco] = new Vector2(posX*11,posY);
			cControl.chunkUVGrid [indexBloco+1] = new Vector2(posX*11,posY+(1 / totalBlocosVertical));
			cControl.chunkUVGrid [indexBloco+2] = new Vector2(posX*12,posY);
			cControl.chunkUVGrid [indexBloco+3] = new Vector2(posX*12,posY+(1 / totalBlocosVertical));
			break;
			case 13:
			//bloco ponta esquerda
			cControl.chunkUVGrid [indexBloco] = new Vector2(posX*12,posY);
			cControl.chunkUVGrid [indexBloco+1] = new Vector2(posX*12,posY+(1 / totalBlocosVertical));
			cControl.chunkUVGrid [indexBloco+2] = new Vector2(posX*13,posY);
			cControl.chunkUVGrid [indexBloco+3] = new Vector2(posX*13,posY+(1 / totalBlocosVertical));
			break;
			case 14:
			//bloco ponta direita
			cControl.chunkUVGrid [indexBloco] = new Vector2(posX*13,posY);
			cControl.chunkUVGrid [indexBloco+1] = new Vector2(posX*13,posY+(1 / totalBlocosVertical));
			cControl.chunkUVGrid [indexBloco+2] = new Vector2(posX*14,posY);
			cControl.chunkUVGrid [indexBloco+3] = new Vector2(posX*14,posY+(1 / totalBlocosVertical));
			break;
			case 15:
			//bloco meio vertical
			cControl.chunkUVGrid [indexBloco] = new Vector2(posX*14,posY);
			cControl.chunkUVGrid [indexBloco+1] = new Vector2(posX*14,posY+(1 / totalBlocosVertical));
			cControl.chunkUVGrid [indexBloco+2] = new Vector2(posX*15,posY);
			cControl.chunkUVGrid [indexBloco+3] = new Vector2(posX*15,posY+(1 / totalBlocosVertical));
				break;
			case 16:
			//bloco meio horizontal
			cControl.chunkUVGrid [indexBloco] = new Vector2(posX*15,posY);
			cControl.chunkUVGrid [indexBloco+1] = new Vector2(posX*15,posY+(1 / totalBlocosVertical));
			cControl.chunkUVGrid [indexBloco+2] = new Vector2(posX*16,posY);
			cControl.chunkUVGrid [indexBloco+3] = new Vector2(posX*16,posY+(1 / totalBlocosVertical));
				break;

			cControl.updateMesh();

		}
	}

	public void definirTexturaBloco(float posX,float posY){

		Vector2 cimaE = new Vector2 (posX-1,posY+1);
		Vector2 cima = new Vector2 (posX,posY+1);
		Vector2 cimaD = new Vector2 (posX+1,posY+1);
		Vector2 mE = new Vector2 (posX-1,posY);
		Vector2 mD = new Vector2 (posX+1,posY);
		Vector2 baixoE = new Vector2 (posX-1,posY-1);
		Vector2 baixo = new Vector2 (posX,posY-1);
		Vector2 baixoD = new Vector2 (posX+1,posY-1);

		if(cimaE.x < 0){
			cimaE.x = 0;
		}
		if(cimaE.y < 0){
			cimaE.y = 0;
		}
		if(cimaE.x > 100){
			cimaE.x = 99;
		}
		if(cimaE.x >= 100){
			cimaE.y = 99;
		}

		if(cima.x < 0){
			cima.x = 0;
		}
		if(cima.y < 0){
			cima.y = 0;
		}
		if(cima.x > 100){
			cima.x = 99;
		}
		if(cima.x >= 100){
			cima.y = 99;
		}

		if(cimaD.x < 0){
			cimaD.x = 0;
		}
		if(cimaD.y < 0){
			cimaD.y = 0;
		}
		if(cimaD.x > 100){
			cimaD.x = 99;
		}
		if(cimaD.x >= 100){
			cimaD.y = 99;
		}

		if(mE.x < 0){
			mE.x = 0;
		}
		if(mE.y < 0){
			mE.y = 0;
		}
		if(mE.x > 100){
			mE.x = 99;
		}
		if(mE.x >= 100){
			mE.y = 99;
		}

		if(mD.x < 0){
			mD.x = 0;
		}
		if(mD.y < 0){
			mD.y = 0;
		}
		if(mD.x > 100){
			mD.x = 99;
		}
		if(mD.x >= 100){
			mD.y = 99;
		}

		if(baixoE.x < 0){
			baixoE.x = 0;
		}
		if(baixoE.y < 0){
			baixoE.y = 0;
		}
		if(baixoE.x > 100){
			baixoE.x = 99;
		}
		if(baixoE.x >= 100){
			baixoE.y = 99;
		}

		if(baixo.x < 0){
			baixo.x = 0;
		}
		if(baixo.y < 0){
			baixo.y = 0;
		}
		if(baixo.x > 100){
			baixo.x = 99;
		}
		if(baixo.x >= 100){
			baixo.y = 99;
		}

		if(baixoD.x < 0){
			baixoD.x = 0;
		}
		if(baixoD.y < 0){
			baixoD.y = 0;
		}
		if(baixoD.x > 100){
			baixoD.x = 99;
		}
		if(baixoD.x >= 100){
			baixoD.y = 99;
		}

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

		estadoBloco = cControl.chunkEstadoBloco.ToArray();
		tipoBloco = cControl.chunkTipoBloco.ToArray();

		int[] sequencia;
		//formulas para identificar todos os blocos em volta do bloco a ser analisado
		if(posX > 99){
			posX -= chunkX;
		}
		if(posY > 99){
			posY -= chunkY;
		}
		if(posX < 0){
			posX = 0;
		}
		if(posY < 0){
			posY = 0;
		}
		int cimaEsquerda = (posY + (posX * 100) - 99);
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
