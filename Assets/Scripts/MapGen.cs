using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGen : MonoBehaviour
{
	public Tilemap grid;
	public Tile tile;

	char[,] maze = new char[15, 20];
	bool flag;

	//----------------------------------------------------------------
	// Funcao usada para saber se a posicao está dentro do labirinto
	//----------------------------------------------------------------
	bool isDentroLabirinto (int i, int j)
	{
		if ((i <= 0) || (i >= 15) || (j <= 0) || (j >= 20)) { 
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
		if ((i <= 0) || (i >= 15) || (j <= 0) || (j >= 20)) { 
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
		for (int i = 0; i < 15; i++) {
			for (int j = 0; j < 20; j++) {
				{ 
					maze [i, j] = 'P';	     
				}
			}
		}

		// Define a posicao de onde o labirinto irá comecar a ser construido
		x = 3;
		y = 3;

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
	}

	// Start is called before the first frame update
	void Start ()
	{
		NovoLabirinto ();
		for (int i = 0; i < 15; i++) {
			for (int j = 0; j < 20; j++) {
				{ 
					if (maze [i, j] == 'P') {
						grid.SetTile (new Vector3Int (j, -i, 0), tile);
					}	     
				}
			}
		}
		GameObject.Find ("Main Camera").transform.SetPositionAndRotation (grid.CellToLocal (new Vector3Int (3, -3, 0)) + new Vector3 (0, 0, -10f), Quaternion.identity);
	}

	// Update is called once per frame
	void Update ()
	{
        
	}
}