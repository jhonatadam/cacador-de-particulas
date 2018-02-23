using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformsGraph {

	//Grafo das plataformas
	public static ArrayList adjacentList;

	static void start () {
		adjacentList = new ArrayList ();
		adjacentList.Add (new ArrayList ());
		adjacentList.Add (new ArrayList ());
		adjacentList.Add (new ArrayList ());
		adjacentList.Add (new ArrayList ());
		adjacentList.Add (new ArrayList ());
		adjacentList.Add (new ArrayList ());
		adjacentList.Add (new ArrayList ());
		adjacentList.Add (new ArrayList ());
		adjacentList.Add (new ArrayList ());

		//chao
		((ArrayList)adjacentList [0]).Add (1);
		((ArrayList)adjacentList [0]).Add (2);
		((ArrayList)adjacentList [0]).Add (3);

		((ArrayList)adjacentList [1]).Add (0);
		((ArrayList)adjacentList [1]).Add (2);
		((ArrayList)adjacentList [1]).Add (4);

		((ArrayList)adjacentList [2]).Add (0);
		((ArrayList)adjacentList [2]).Add (1);
		((ArrayList)adjacentList [2]).Add (4);
		((ArrayList)adjacentList [2]).Add (3);
		((ArrayList)adjacentList [2]).Add (5);

		((ArrayList)adjacentList [3]).Add (0);
		((ArrayList)adjacentList [3]).Add (2);
		((ArrayList)adjacentList [3]).Add (5);

		((ArrayList)adjacentList [4]).Add (1);
		((ArrayList)adjacentList [4]).Add (2);
		((ArrayList)adjacentList [4]).Add (4);
		((ArrayList)adjacentList [4]).Add (6);
		((ArrayList)adjacentList [4]).Add (7);

		((ArrayList)adjacentList [5]).Add (2);
		((ArrayList)adjacentList [5]).Add (3);
		((ArrayList)adjacentList [5]).Add (4);
		((ArrayList)adjacentList [5]).Add (7);
		((ArrayList)adjacentList [5]).Add (8);

		((ArrayList)adjacentList [6]).Add (4);
		((ArrayList)adjacentList [6]).Add (7);

		((ArrayList)adjacentList [7]).Add (6);
		((ArrayList)adjacentList [7]).Add (4);
		((ArrayList)adjacentList [7]).Add (5);
		((ArrayList)adjacentList [7]).Add (8);

		((ArrayList)adjacentList [8]).Add (7);
		((ArrayList)adjacentList [8]).Add (5);

	}

	//algoritmo dijkstra
	public static int nextPlatform(int source, int dest) {

		start ();

		//conjunto dos vértices não explorados
		ArrayList Q = new ArrayList();

		//vetor de distancias ate source
		int[] dist = new int[adjacentList.Count];
		int[] prev = new int[adjacentList.Count];

		for (int v = 0; v < adjacentList.Count; v++) {
			dist [v] = int.MaxValue;
			prev [v] = -1;
			Q.Add (v);


		}

		dist [source] = 0;

		while (Q.Count > 0) {
			//selecionando o vertice u com menor dist[u]
			int u = (int)Q [0];
			foreach (int q in Q) {
				if (dist [q] < dist [u]) {
					u = q;
				}
			}

			Q.Remove (u);

			//para cada vizinho v de u
			foreach (int v in (ArrayList)adjacentList[u]) {
				if (!Q.Contains (v))
					continue;

				int alt = dist [u] + 1;
				if (alt < dist [v]) {
					dist [v] = alt;
					prev [v] = u;
				}
			}
		}

		int k = prev[dest];
		int temp = dest;
		while (k != source) {
			temp = k;
			k = prev [k];
		}

		return temp;

	}
}
