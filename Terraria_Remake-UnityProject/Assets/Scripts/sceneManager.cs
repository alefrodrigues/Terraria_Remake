using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneManager : MonoBehaviour {
	public float moveSpeed = 10f;

	Mesh mesh;
	public int worldSizeX;
	public int worldSizeY;
	public GameObject block;
	public bool startCreate;

	public List <Vector3> listaVertices = new List <Vector3>();
	public List <Vector2> uvList = new List <Vector2>();
	public List <int> listaTriangulos = new List <int>();
	public List <int> idBlocos = new List <int>();
	public List <float> idTypeBlocos = new List <float>();
	void setUvBlock (int _idTypeBloco) {

		switch (_idTypeBloco)
		{
		    case 0:
		    	//bloco1
		        uvList.Add(new Vector2(0f,0f));
				uvList.Add(new Vector2(0f,0.5f));
				uvList.Add(new Vector2(0.5f,0f));
				uvList.Add(new Vector2(0.5f,0.5f));
		        break;
		    case 1:
		        //bloco2
				uvList.Add(new Vector2(0f,0.5f));
				uvList.Add(new Vector2(0f,1f));
				uvList.Add(new Vector2(0.5f,0.5f));
				uvList.Add(new Vector2(0.5f,1f));
		        break;
		    case 2:
		        //bloco3
				uvList.Add(new Vector2(0.5f,0f));
				uvList.Add(new Vector2(0.5f,0.5f));
				uvList.Add(new Vector2(1f,0f));
				uvList.Add(new Vector2(1f,0.5f));
		        break;
		    case 3:
		        //bloco4
				uvList.Add(new Vector2(0.5f,0.5f));
				uvList.Add(new Vector2(0.5f,1f));
				uvList.Add(new Vector2(1f,0.5f));
				uvList.Add(new Vector2(1f,1f));
		        break;
		    
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate(Input.GetAxis("Horizontal")* moveSpeed*Time.fixedDeltaTime,
							0,
							Input.GetAxis("Vertical")* moveSpeed*Time.fixedDeltaTime);
		/*if(startCreate){
			criarCenario();
		}*/
	}
	void criarCenario(){
		int id = 0;
		GameObject newBlock = Instantiate(block,new Vector3(0,0,0),Quaternion.identity);
		mesh = newBlock.GetComponent<MeshFilter>().mesh;

		for(int x = 0;x<= worldSizeX;x++){
			for(int y = 0;y<= worldSizeY;y++){

				idBlocos.Add(id);
				listaVertices.Add(new Vector3(0+x,0+y,0));
				listaVertices.Add(new Vector3(0+x,1+y,0));
				listaVertices.Add(new Vector3(1+x,0+y,0));
				listaVertices.Add(new Vector3(1+x,1+y,0));

				listaTriangulos.Add(0+id);
				listaTriangulos.Add(1+id);
				listaTriangulos.Add(2+id);
				listaTriangulos.Add(2+id);
				listaTriangulos.Add(1+id);
				listaTriangulos.Add(3+id);
				id += 1;
				
				setUvBlock (Random.Range(0,5));
				newBlock.GetComponent<proceduralMesh>().createMesh(mesh,listaVertices.ToArray(),listaTriangulos.ToArray(),uvList.ToArray());
			}
		}
		startCreate = false;
	}
}
