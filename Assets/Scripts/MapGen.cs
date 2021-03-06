﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class MapGen : MonoBehaviour
{
	public Text World;
	public Tilemap gridLabirinto;
	public Tilemap gridBackground;
	public List<Tile> tiles = new List<Tile> ();
	public List<Tile> tileBG = new List<Tile> ();
	public GameObject OZbg;
	private const int width = 30;
	private const int height = 25;
	public static bool OZ = false;

	char[,] maze = new char[height, width];
	bool flag;

	//----------------------------------------------------------------
	// Funcao usada para saber se a posicao está dentro do labirinto
	//----------------------------------------------------------------
	bool isDentroLabirinto (int i, int j)
	{
		if ((i <= 0) || (i >= height) || (j <= 0) || (j >= width)) { 
			return false;
		} else {
			return true;
		}
	}

	//--------------------------------------------------------------------
	// Funcao usada para saber se a posicao denota uma borda do labirinto
	//--------------------------------------------------------------------
	bool isBorda (int i, int j)
	{
		if ((i <= 0) || (i >= height) || (j <= 0) || (j >= width)) { 
			return true;
		} else {
			return false;
		}
	}

	//--------------------------------------------------------------------
	// Funcao usada para saber o número de parede ao redor de uma posição
	//--------------------------------------------------------------------
	int numParedes (int i, int j)
	{
		int x;
		{
			if (!isDentroLabirinto (i, j)) {
				return 0;
			} else {
				x = 4; 
				if (!isDentroLabirinto (i + 1, j) || (maze [i + 1, j] != 'P')) {
					x = x - 1;
				}
				if (!isDentroLabirinto (i - 1, j) || (maze [i - 1, j] != 'P')) {
					x = x - 1;
				}
				if (!isDentroLabirinto (i, j - 1) || (maze [i, j - 1] != 'P')) {
					x = x - 1;
				}
				if (!isDentroLabirinto (i, j + 1) || (maze [i, j + 1] != 'P')) {
					x = x - 1;
				}
				return x;
			}     
		}
	}

	//--------------------------------------------------------------------
	// Procedimento que monta o labirinto, a partir de uma heurística.
	//--------------------------------------------------------------------
	void NovoLabirinto ()
	{
		const int BAIXO = 0; 
		const int DIREITA = 1;
		const int CIMA = 2;
		const int ESQUERDA = 3;
		int[] direcoesValidas = new int[4];
		bool podeConstruir; 
		int numPosCandidatas, direcaoEscolhida, x, y;
		Stack<int> pilhaCaminhoSeguido = new Stack<int> ();

		// Define todas as posicoes do labirinto como parede
		for (int i = 0; i < height; i++) {
			for (int j = 0; j < width; j++) {
				{ 
					maze [i, j] = 'P';	     
				}
			}
		}

		// Define a posicao de onde o labirinto irá comecar a ser construido
		x = 3;
		y = 3;
		maze [x, y] = 'B';

		// Variáveis usadas na construcao do labirinto
		podeConstruir = true;

		// Enquanto o labirinto ainda puder ser construido, repete...
		while (podeConstruir) {      
			numPosCandidatas = 0;

			// Heuristica: as posicoes candidatas para próxima posicao
			// na construcao do labirinto possuem uma parede entre a 
			// posicao atual e tem 4 paredes ao seu redor.
			if ((!isBorda (x, y)) && (numParedes (x, y - 2) == 4)) {
				direcoesValidas [numPosCandidatas] = BAIXO;
				numPosCandidatas = numPosCandidatas + 1;
			}   
			if ((!isBorda (x, y)) && (numParedes (x + 2, y) == 4)) {      
				direcoesValidas [numPosCandidatas] = DIREITA;
				numPosCandidatas = numPosCandidatas + 1;
			}            
			if ((!isBorda (x, y)) && (numParedes (x, y + 2) == 4)) {      
				direcoesValidas [numPosCandidatas] = CIMA;
				numPosCandidatas = numPosCandidatas + 1;
			}            
			if ((!isBorda (x, y)) && (numParedes (x - 2, y) == 4)) {      
				direcoesValidas [numPosCandidatas] = ESQUERDA;
				numPosCandidatas = numPosCandidatas + 1;
			}

			// Se foi encontrada pelo menos uma posicao candidata,
			// escolhe aleatoriamente uma dessas posicoes e move para lá.
			if (numPosCandidatas != 0) {
				direcaoEscolhida = direcoesValidas [Random.Range (0, numPosCandidatas)];

				switch (direcaoEscolhida) {
				// knock down walls and make new cell the current cell
				case BAIXO: 
					maze [x, y - 2] = 'B';
					maze [x, y - 1] = 'B';
					y = y - 2;
					break;
				case DIREITA:  
					maze [x + 2, y] = 'B';
					maze [x + 1, y] = 'B';		            
					x = x + 2;
					break;
				case CIMA: 
					maze [x, y + 2] = 'B';
					maze [x, y + 1] = 'B';		           
					y = y + 2;
					break;
				case ESQUERDA:  
					maze [x - 2, y] = 'B';
					maze [x - 1, y] = 'B';		           
					x = x - 2;
					break;
			  
				}
				// Agora, empilha a direcao escolhida
				pilhaCaminhoSeguido.Push (direcaoEscolhida);

			} 

			// Agora, se não foi encontrada nenhuma posicao candidata,
			// Atualiza a posicao atual a partir da direcao armazenada
			// no topo da pilha que contem o caminho seguido
			else {
				//pilhaCaminhoSeguido.Pop();
				// Se a pilha está vazia, a construcao deve parar...
				if (pilhaCaminhoSeguido.Count < 1) {
					podeConstruir = false;
				}
						// Senão, atualiza a posicao atual, voltando...
				else {
					switch (pilhaCaminhoSeguido.Pop ()) {              
					case BAIXO:
						y = y + 2;
						break; 
					case DIREITA:
						x = x - 2;
						break;
					case CIMA:
						y = y - 2;
						break;
					case ESQUERDA:
						x = x + 2;
						break;
					} 
				} 
			}              
		}

		for (int i = 0; i < height; i++) {
			for (int j = width - 1; j > 0; j--) {
				if (maze[i,j] == 'B' && numParedes (i,j) == 3) {
					Place ("Tornado", i, j);
					break;
				}
			}
		}

		for (int i = 0; i < height; i++) {
			for (int j = width - 1; j > 0; j--) {
				if (Random.Range(0,10) == 1 && maze[i,j] == 'B' && numParedes (i,j) == 2 && maze[i+1,j] == 'P' && maze[i-1,j] == 'P') {
					if (OZ? Random.Range(0,3) != 0 : Random.Range (0, 4) == 0) {
						PlaceNew ("TinLionCrow", i, j);
					} else {
						PlaceNew (((i+j)%2 == 0)? "Monster" : "Hunter", i, j);
					}
					break;
				}
			}
		}

		for (int i = 0; i < height; i++) {
			for (int j = width - 1; j > 0; j--) {
				if (maze[i,j] == 'B' && numParedes (i,j) == 2 && maze[i,j+1] == 'P' && maze[i,j-1] == 'P') {
					if (i%2 == 0) {
						PlaceNew ("SpringL", i, j);
					} else {
						PlaceNew ("SpringR", i, j);
					}
				}
			}
		}

		if (Random.Range (0, 7) == 1) {
			for (int i = 0; i < height; i++) {
				for (int j = 0; j < width; j++) {
					if (maze[i,j] == 'B' && numParedes (i,j) == 3) {
						Place ("Sapatos", i, j);
						break;
					}
				}
			}
		}
	}


	//--------------------------------------------------------------------
	// Procedimento que imprime o labirinto.
	//--------------------------------------------------------------------
	void ImprimeLabirinto ()
	{
		if (!OZ) {
			for (int i = 0; i < height; i++) {
				for (int j = 0; j < width; j++) {
					{ 
						gridBackground.SetTile (new Vector3Int (j, -i, 0), tileBG [(Random.value < 0.01) ? Random.Range (17, 19) : Random.Range (0, 17)]);
					}
				}
			}
		}

		for (int i = 0; i < height; i++) {
			for (int j = 0; j < width; j++) {
				{ 
					if (maze [i, j] == 'P') {

						if (i > 0 && maze [i - 1, j] == 'B') {
							if (i < height - 1 && maze [i + 1, j] == 'B') {
								if (j < width && maze [i, j + 1] == 'B') {
									gridLabirinto.SetTile (new Vector3Int (j, -i, 0), tiles [7]);
								} else if (j > 0 && maze [i, j - 1] == 'B') {
									gridLabirinto.SetTile (new Vector3Int (j, -i, 0), tiles [9]);
								} else {
									gridLabirinto.SetTile (new Vector3Int (j, -i, 0), tiles [5]);
								}
							} else {
								if (j < width && maze [i, j + 1] == 'B') {
									if (j > 0 && maze [i, j - 1] == 'B') {
										gridLabirinto.SetTile (new Vector3Int (j, -i, 0), tiles [10]);
									} else {
										gridLabirinto.SetTile (new Vector3Int (j, -i, 0), tiles [11]);
									}
								} else {
									if (j > 0 && maze [i, j - 1] == 'B') {
										gridLabirinto.SetTile (new Vector3Int (j, -i, 0), tiles [14]);
									} else {
										gridLabirinto.SetTile (new Vector3Int (j, -i, 0), tiles [1]);
									}
								}
							}
						} else {
							if (j < width - 1 && maze [i, j + 1] == 'B') {
								if (j > 0 && maze [i, j - 1] == 'B') {
									if (i < height - 1 && maze [i + 1, j] == 'B') {
										gridLabirinto.SetTile (new Vector3Int (j, -i, 0), tiles [8]);
									} else {
										gridLabirinto.SetTile (new Vector3Int (j, -i, 0), tiles [6]);
									}
								} else {
									if (i < height - 1 && maze [i + 1, j] == 'B') {
										gridLabirinto.SetTile (new Vector3Int (j, -i, 0), tiles [12]);
									} else {
										gridLabirinto.SetTile (new Vector3Int (j, -i, 0), tiles [2]);
									}
								}
							} else if (j > 0 && maze [i, j - 1] == 'B') {
								if (i < height - 1 && maze [i + 1, j] == 'B') {
									gridLabirinto.SetTile (new Vector3Int (j, -i, 0), tiles [13]);
								} else {
									gridLabirinto.SetTile (new Vector3Int (j, -i, 0), tiles [4]);
								}
							} else {
								if (i < height - 1 && maze [i + 1, j] == 'B') {
									gridLabirinto.SetTile (new Vector3Int (j, -i, 0), tiles [3]);
								} else {
									gridLabirinto.SetTile (new Vector3Int (j, -i, 0), tiles [0]);
								}
							}
						}
					}	     
				}
			}
		}
	}

	void PintaTiles (Color color)
	{
		gridLabirinto.color = color;
		gridBackground.color = color;
		foreach (var item in tiles) {
			item.colliderType = Tile.ColliderType.Sprite;
		}
		GameObject.Find ("Directional Light").GetComponent<Light> ().color = color;
	}

	void Place(string obj, int i, int j){
		GameObject.Find (obj).transform.SetPositionAndRotation (gridLabirinto.CellToLocal (new Vector3Int (j, -i, 0)) + new Vector3 (0.45f, 0.45f, -1f), Quaternion.identity);
	}

	void PlaceNew(string obj, int i, int j){
		var neo = Instantiate (Resources.Load ("Prefabs/" + obj)) as GameObject;
		neo.transform.SetPositionAndRotation (gridLabirinto.CellToLocal (new Vector3Int (j, -i, 0)) + new Vector3 (0.45f, 0.45f, -1f), Quaternion.identity);
	}

	// Start is called before the first frame update
	void Start ()
	{
		OZ = (Random.value < Mathf.Min(0.03f * PlayerPrefs.GetInt("World",0),0.3f)) ? true : false;
		if (OZ){
			PlayerPrefs.SetInt ("Oz", PlayerPrefs.GetInt ("Oz", 0) + 1);
			OZbg.SetActive (true);
		}
		NovoLabirinto ();
		ImprimeLabirinto ();
		PintaTiles (OZ? Color.white : Color.HSVToRGB (Random.value, 1, 1));
		//GameObject.Find ("Main Camera").transform.SetPositionAndRotation (gridLabirinto.CellToLocal (new Vector3Int (3, -3, 0)) + new Vector3 (0, 0, -10f), Quaternion.identity);
		//GameObject.Find ("Player").transform.SetPositionAndRotation (gridLabirinto.CellToLocal (new Vector3Int (3, -3, 0)), Quaternion.identity);
	}

	// Update is called once per frame
	void Update ()
	{
		World.text = "World: " + PlayerPrefs.GetInt ("World", 0);
	}
}