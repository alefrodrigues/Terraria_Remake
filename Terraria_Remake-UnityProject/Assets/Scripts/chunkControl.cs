using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chunkControl : MonoBehaviour {
	proceduralGrid pGrid;
	//propriedades da MeshGrid do Chunk
	public Mesh mesh;

	public List<Vector2> chunkUVGrid = new List<Vector2>();
	public List<Vector3> chunkVerticesGrid = new List<Vector3>();
	public List<int> chunkTrianglesGrid = new List<int>();
	public List<int> chunkEstadoBloco = new List<int>();

	public int chunkX;
	public int chunkY;

	bool chunkAtualizado = false;
	void Start(){

		//criarVerticesChunk();

	}
	void Awake(){
		mesh = GetComponent<MeshFilter>().mesh;
		pGrid = (proceduralGrid)FindObjectOfType(typeof(proceduralGrid));
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Alpha2) && proceduralGrid.podePreencher && chunkAtualizado == false){

			preencherChunk();

			updateMesh();
			print("Preencheu");

			chunkAtualizado = true;
		}

		if(Input.GetKey(KeyCode.Mouse0)){
			Vector2 p = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x,Input.mousePosition.y));
			
			int posX =  Mathf.FloorToInt(Mathf.Round(p.x - 0.5f));
			int posY = Mathf.FloorToInt(Mathf.Round(p.y - 0.5f));

			if(p.x >= chunkX && p.x <= chunkX + 100 && p.y >= chunkY && p.y <= chunkY + 100 ){
				updateChunk(posX,posY);
			}
		}
	}

	void preencherChunk(){
		//resetando valores das listas do chunk
		chunkVerticesGrid.Clear();
		chunkTrianglesGrid.Clear();
		chunkUVGrid.Clear();
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
	void updateChunk(int posX, int posY){
		chunkEstadoBloco.Clear();
		int blocoAtual = 0;
		blocoAtual = (posY + (posX * pGrid.gridSizeY))*4;
		int chunkBlocoAtual = (posY + (posX * 100))*4;

		pGrid.estadoBloco[blocoAtual] = 0;
		pGrid.estadoBloco[blocoAtual+1] = 0;
		pGrid.estadoBloco[blocoAtual+2] = 0;
		pGrid.estadoBloco[blocoAtual+3] = 0;

		chunkTrianglesGrid.Remove(chunkBlocoAtual);
		chunkTrianglesGrid.Remove(chunkBlocoAtual+1);
		chunkTrianglesGrid.Remove(chunkBlocoAtual+2);
		chunkTrianglesGrid.Remove(chunkBlocoAtual+2);
		chunkTrianglesGrid.Remove(chunkBlocoAtual+1);
		chunkTrianglesGrid.Remove(chunkBlocoAtual+3);

		updateMesh();

	}
	void updateMesh(){
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
	
}
