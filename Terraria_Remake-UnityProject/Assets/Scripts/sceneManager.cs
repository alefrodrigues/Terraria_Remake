using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneManager : MonoBehaviour {

	public int worldSizeX;
	public int worldSizeY;
	public GameObject block;
	public bool startCreate;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(startCreate){
			criarCenario();
		}
	}
	void criarCenario(){
		for(int x = 0;x<= worldSizeX;x++){
			for(int y = 0;y<= worldSizeY;y++){
				GameObject newBlock = Instantiate(block,new Vector3(x,y,0),Quaternion.identity);
				newBlock.GetComponent<blockControl>().positionMeshData = new Vector3(x,y,0);
				newBlock.GetComponent<blockControl>().posTriangles = 0;
			}
		}
		startCreate = false;
	}
}
