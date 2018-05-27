using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class proceduralMesh : MonoBehaviour{


	public Vector3[] vertices;
	public Vector2[] uvs;
	public int[] triangulos;



	public void makeMeshData (Vector3 meshPos,int posTriangles) {
		if(vertices.Length <= 0){
			//Definir posiçao dos vertices do bloco
			vertices = new Vector3[]{
				new Vector3(0,0,meshPos.z),
				new Vector3(0,1,meshPos.z),
				new Vector3(1,0,meshPos.z),
				new Vector3(1,1,meshPos.z)
			};

			uvs = new Vector2[]{
				new Vector2(0f, 0f),
				new Vector2(0f, 0.20f),
				new Vector2(0.20f, 0f),
				new Vector2(0.20f, 0.20f)
			};
			//Definir orden de construcao dos triangulos do bloco
			triangulos = new int[]{0,1,2,2,1,3};
			print("fez a mesh");
		}
	}

	//Atualizar a Malha/Mesh;
	public void createMesh(Mesh _mesh,Vector3[] _vertices,int[] _triangulos,Vector2[] _uvs){
		if(vertices.Length >= 0){
			_mesh.Clear();
			_mesh.vertices = _vertices;
			_mesh.triangles = _triangulos;
			_mesh.uv = _uvs;
			print("Atualizou a Mesh");
		}
		
	}
	// mover o MAPA UVw de acordo com os vertices , utilizar apenas numeros entre 0 e 1(USAR PLANO CARTESIANO COMO BASE)//
	public void updateUv(Mesh _mesh,Vector2 v1,Vector2 v2,Vector2 v3,Vector2 v4){
		uvs = new Vector2[]{
				new Vector2(v1.x, v1.y),
				new Vector2(v2.x, v2.y),
				new Vector2(v3.x, v3.y),
				new Vector2(v4.x, v4.y)
			};

		_mesh.uv = uvs;
	}
	public void clearMesh(Mesh _mesh){
		if(vertices.Length > 0){
			_mesh.Clear();
			print("Limpou a mesh");
		}
		
	}
}
