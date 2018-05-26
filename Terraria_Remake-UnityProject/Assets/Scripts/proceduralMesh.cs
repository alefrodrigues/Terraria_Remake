using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class proceduralMesh : MonoBehaviour{

	public Vector3[] vertices;
	public Vector2[] uvs;
	public int[] triangulos;
	public void makeMeshData (Vector3 meshPos,int posTriangles) {
		if(vertices.Length <= 0){
			//Definir posiçao dos vertices
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
			//Definir orden de construcao dos triangulos
			triangulos = new int[]{0,1,2,2,1,3};

			print("fez a mesh");
		}
	}

	//Atualizar a Malha/Mesh;
	public void createMesh(Mesh _mesh){
		if(vertices.Length >= 0){
			_mesh.Clear();
			_mesh.vertices = vertices;
			_mesh.triangles = triangulos;
			_mesh.uv = uvs;
			print("Atualizou a Mesh");
		}
		
	}
	// mover o MAPA UVw de acordo com os vertices , utilizar apenas numeros entre 0 e 1(USAR PLANO CARTESIANO COMO BASE)//
	public void updateUv(Mesh _mesh,float _a,float _b,float _c,float _d,float _e,float _f,float _g,float _h){
		uvs = new Vector2[]{
				new Vector2(_a, _b),
				new Vector2(_c, _d),
				new Vector2(_e, _f),
				new Vector2(_g, _h)
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
