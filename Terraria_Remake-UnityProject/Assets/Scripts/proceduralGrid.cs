using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proceduralGrid : MonoBehaviour {

	//arrays para montar a MeshGrid
	public List <int> tipoBloco = new List <int>();
	public List <int> materialBloco = new List <int>();
	public List <int> estadoBloco = new List <int>();

	//tamanho da MeshGrid;
	public int gridSizeX;
	public int gridSizeY;

	//mainCamera
	public Camera myCam;

	//autorizaçao para so chunks começar a se preencher de dados das listas principais
	public static bool podePreencher;
	

	void Awake(){
	}
	void Start () {

	}
	void Update(){
		//CRIAR MESHGRID
		if(Input.GetKeyDown(KeyCode.Space)){
			makeDiscreteGrid();

			podePreencher = true;
		}

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

				//material do bloco
				materialBloco.Add(1);
				materialBloco.Add(1);
				materialBloco.Add(1);
				materialBloco.Add(1);
				materialBloco.Add(1);
				materialBloco.Add(1);

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
}
