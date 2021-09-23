using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class moveDijkstra : MonoBehaviour
{
    Dictionary<int, Vector3> coordSomm = new Dictionary<int, Vector3>();
    void Start()
    {
        coordSomm.Add(0, new Vector3(1, 4, 0));
        coordSomm.Add(1, new Vector3(1, 2, 0));
        coordSomm.Add(2, new Vector3(1, 0, 0));
        coordSomm.Add(3, new Vector3(1, -2, 0));
        coordSomm.Add(4, new Vector3(1, -4, 0));
        coordSomm.Add(5, new Vector3(4, 4, 0));
        coordSomm.Add(6, new Vector3(4, 2, 0));
        coordSomm.Add(7, new Vector3(4, 0, 0));
        coordSomm.Add(8, new Vector3(4, -2, 0));
        coordSomm.Add(9, new Vector3(4, -4, 0));
        coordSomm.Add(10, new Vector3(7, 4, 0));
        coordSomm.Add(11, new Vector3(7, 2, 0));
        coordSomm.Add(12, new Vector3(7, 0, 0));
        coordSomm.Add(13, new Vector3(7, -2, 0));
        coordSomm.Add(14, new Vector3(7, -4, 0));
        coordSomm.Add(15, new Vector3(10, 4, 0));
        coordSomm.Add(16, new Vector3(10, 2, 0));
        coordSomm.Add(17, new Vector3(10, 0, 0));
        coordSomm.Add(18, new Vector3(10, -2, 0));
        coordSomm.Add(19, new Vector3(10, -4, 0));
        coordSomm.Add(20, new Vector3(13, 4, 0));
        coordSomm.Add(21, new Vector3(13, 2, 0));
        coordSomm.Add(22, new Vector3(13, 0, 0));
        coordSomm.Add(23, new Vector3(13, -2, 0));
        coordSomm.Add(24, new Vector3(13, -4, 0));
        coordSomm.Add(25, new Vector3(16, 4, 0));
        coordSomm.Add(26, new Vector3(16, 2, 0));
        coordSomm.Add(27, new Vector3(16, 0, 0));
        coordSomm.Add(28, new Vector3(16, -2, 0));
        coordSomm.Add(29, new Vector3(16, -4, 0));


		const int inf = int.MaxValue; //infini
		const int nbrSommets = 30;

		Graph graph = new Graph(nbrSommets, new int[nbrSommets, nbrSommets]);
		for (int i = 0; i < nbrSommets; i++)
		{
			for (int j = 0; j < nbrSommets; j++)
			{
				graph.ponderation[i, j] = inf;
			}

		}

		string[] readText = File.ReadAllLines("distances.txt");
		//Console.WriteLine(readText.Length);
		//int nbrChemins = readText.Length;
		foreach (string s in readText)
		{
			string[] subs = s.Split(' ');
			graph.ponderation[int.Parse(subs[0]), int.Parse(subs[1])] = int.Parse(subs[2]);
			graph.ponderation[int.Parse(subs[1]), int.Parse(subs[0])] = int.Parse(subs[2]);
		}

		//liste des points du chemin
		List<int> pluscourtchemin = graph.dijkstra(2, 5);
		foreach (var item in pluscourtchemin)
		{
			Debug.Log(item);
		}

	}

    // Update is called once per frame
    void Update()
    {

    }

	class Graph
	{
		const int inf = int.MaxValue;

		public int[,] ponderation;
		//private Dictionary<int, string> nomSommets { get; set; } //unused
		private int nbrSommets;

		public Graph(int nbrSommets, int[,] ponderation)
		{
			this.ponderation = ponderation;
			this.nbrSommets = nbrSommets;
		}

		public List<int> dijkstra(int depart, int arrivee)
		{
			List<int> listSommets = new List<int>();
			int[] distances = new int[nbrSommets]; //distance entre le sommet de départ et un autre sommet i
			int[] predecesseur = new int[nbrSommets]; //predessesseur du sommet i

			for (int i = 0; i < nbrSommets; i++)
			{
				distances[i] = inf;
				listSommets.Add(i);
			}

			distances[depart] = 0;
			int som = depart;

			while (listSommets.Count > 0)
			{
				//Console.WriteLine(som);
				listSommets.Remove(som);

				foreach (var b in listSommets)
				{
					if (ponderation[som, b] == inf) { continue; } //si ce n'est pas un voisin de som, continuer la boucle
																  //Console.WriteLine("le point "+b + " est à une distance de "+depart+" de " + distances[b] + " ou " + (distances[som] + ponderation[som, b]));
					if (distances[b] > (distances[som] + ponderation[som, b]))
					{
						distances[b] = distances[som] + ponderation[som, b];
						predecesseur[b] = som;
					}

				}
				int min = inf;
				int previoussom = som;
				//quel est le sommet avec le plus court chemin du départ :
				//Console.WriteLine("nombre de sommets non testés : "+listSommets.Count);
				foreach (var b in listSommets)
				{
					if (distances[b] < min)
					{
						som = b;
						min = distances[b];
					}
				}
				if (som == previoussom) { break; } //si le nouveau sommet n'a pas changé, c'est que certains points ne sont pas connectés au sommet de départ, l'algo risque alors de boucler à l'infini
												   //Console.WriteLine("-------prochain point-------");
			}

			//list des sommets du chemin le plus court
			List<int> shortestPath = new List<int>();
			int current = arrivee;
			while (current != depart)
			{
				shortestPath.Add(current);
				current = predecesseur[current];
			}

			shortestPath.Reverse();

			return shortestPath;
		}
	}
}
