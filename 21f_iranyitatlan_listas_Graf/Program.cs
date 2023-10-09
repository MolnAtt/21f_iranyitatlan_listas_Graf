using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _21f_iranyitatlan_listas_Graf
{
	internal class Program
	{
		class Graf
		{
			List<List<int>> szomszedai;
			public int N; // csúcsok száma
			int M; // élek száma
			public Graf()
			{ 
				szomszedai = new List<List<int>>();
				string sor = Console.ReadLine();
				string[] sortomb = sor.Split(' ');
				N = int.Parse(sortomb[0]);
				M = int.Parse(sortomb[1]);

				for (int i = 0; i < N; i++)
				{
					szomszedai.Add(new List<int>());
				}

				for (int i = 0; i < M; i++)
				{
					sor = Console.ReadLine();
					sortomb = sor.Split(' ');
					int bal = int.Parse(sortomb[0]);
					int jobb = int.Parse(sortomb[1]);
					szomszedai[bal].Add(jobb);
					if (bal!=jobb)
					{
						szomszedai[jobb].Add(bal);
					}
				}
			}

			public void Diagnosztika()
			{
				for (int i = 0; i < N; i++)
				{
                    Console.WriteLine($"{i}: [{ String.Join(";", szomszedai[i]) }]");
                }

                Console.WriteLine(GraphViz());
			}

			public string GraphViz()
			{
				string s = "graph{\n";

				for (int i = 0; i < szomszedai.Count; i++)
				{
					if (szomszedai[i].Count == 0)
						s += i + ";\n";
					foreach (int szomszed in szomszedai[i])
						if (i <= szomszed)
							s += $"{i} -- {szomszed};\n";
				}

				return s + "\n}";
			}

			public bool Van_el(int a, int b)
			{
				return szomszedai[a].Contains(b);
			}

			public bool Izolalt(int a)
			{
				return szomszedai[a].Count == 0;
			}

			public bool Van_hurok(int a) => Van_el(a, a);

			public int Fokszam(int a) // feltettük itt most, hogy nincsenek többszörös élek.
			{
				return Van_hurok(a) ? szomszedai[a].Count + 1 : szomszedai[a].Count;
			}


			/// <summary>
			/// Véges sok lépésen belül elérhető-e. Szélességi bejárás (sor adatszerkezettel)
			/// </summary>
			/// <param name="honnan"></param>
			/// <param name="b"></param>
			/// <returns></returns>
			public bool Elerheto(int honnan, int hova)
			{
				int fehér = 0;	// abszolút érintetlen
				int kék = 1;	// már találkoztam vele csak még nem dolgoztam fel (tennivalók közt ott van)
				int piros = 2;  // feldolgoztam, nincs már vele több munka (tennivalókból kikerült)

				int[] szin = new int[N]; // ennek most mindegyik értéke nulla, azaz FEHÉR.

				Queue<int> tennivalok = new Queue<int>();

				tennivalok.Enqueue(honnan);
				szin[honnan] = kék;

				while (tennivalok.Count != 0)
				{
					int tennivalo = tennivalok.Dequeue();

					// FELDOLGOZÁS

					if (tennivalo == hova)
						return true;
					szin[tennivalo] = piros;


					// SZOMSZÉDAIVAL FOGLALKOZUNK/TÖLTJÜK A TENNIVALÓKAT

					foreach (int szomszed in szomszedai[tennivalo])
					{
						if (szin[szomszed]==fehér)
						{
							tennivalok.Enqueue(szomszed);
							szin[szomszed] = kék;
						}
					}

				}

				return false;
			}

			/// <summary>
			/// A csúcsból véges sok lépésben elérhető csúcsok száma
			/// </summary>
			/// <param name="a"></param>
			/// <returns></returns>
			public int Komponens_szamossaga(int honnan)
			{
				int fehér = 0;  // abszolút érintetlen
				int kék = 1;    // már találkoztam vele csak még nem dolgoztam fel (tennivalók közt ott van)
				int piros = 2;  // feldolgoztam, nincs már vele több munka (tennivalókból kikerült)

				int[] szin = new int[N]; // ennek most mindegyik értéke nulla, azaz FEHÉR.

				Queue<int> tennivalok = new Queue<int>();

				tennivalok.Enqueue(honnan);
				szin[honnan] = kék;

				int db = 1;

				while (tennivalok.Count != 0)
				{
					int tennivalo = tennivalok.Dequeue();

					// FELDOLGOZÁS

					db++;

					szin[tennivalo] = piros;

					// SZOMSZÉDAIVAL FOGLALKOZUNK/TÖLTJÜK A TENNIVALÓKAT

					foreach (int szomszed in szomszedai[tennivalo])
					{
						if (szin[szomszed] == fehér)
						{
							tennivalok.Enqueue(szomszed);
							szin[szomszed] = kék;
						}
					}

				}

				return db;
			}

			/// <summary>
			/// csúcsból véges sok lépésben elérhető csúcsok listája (magát is beleértve)
			/// </summary>
			/// <param name="a"></param>
			/// <returns></returns>
			public List<int> Komponens(int a)
			{
				return new List<int>();
			}

			/// <summary>
			/// Hány komponens van az adott gráfban összesen?
			/// </summary>
			/// <returns></returns>
			public int Komponensek_szama()
			{
				return -1;
			}


			public int Legrovidebb_ut_hossza(int honnan, int hova)
			{
				return -1;
			}

			private List<int> Honnan_vektor_felgongyolitese(int[] honnan, int end)
			{ 
				throw new NotImplementedException();
			}



			public List<int> Legrovidebb_ut(int start, int end)
			{

				int fehér = 0;  // abszolút érintetlen
				int kék = 1;    // már találkoztam vele csak még nem dolgoztam fel (tennivalók közt ott van)
				int piros = 2;  // feldolgoztam, nincs már vele több munka (tennivalókból kikerült)

				int[] szin = new int[N]; // ennek most mindegyik értéke nulla, azaz FEHÉR.

				Queue<int> tennivalok = new Queue<int>();

				tennivalok.Enqueue(start);
				szin[start] = kék;

				int[] honnan = new int[N];
				for (int i = 0; i < honnan.Length; i++)
				{
					honnan[i] = -2;
				}
				honnan[start] = -1;


				while (tennivalok.Count != 0)
				{
					int tennivalo = tennivalok.Dequeue();

					// FELDOLGOZÁS

					if (tennivalo == end)
					{
						return Honnan_vektor_felgongyolitese(honnan, end);
					}
					szin[tennivalo] = piros;


					// SZOMSZÉDAIVAL FOGLALKOZUNK/TÖLTJÜK A TENNIVALÓKAT

					foreach (int szomszed in szomszedai[tennivalo])
					{
						if (szin[szomszed] == fehér)
						{
							tennivalok.Enqueue(szomszed);
							szin[szomszed] = kék;
							honnan[szomszed] = tennivalo; // kulcsfontosságú elem
						}
					}

				}

				return null;

			}




		}
		static void Main(string[] args)
		{
			Graf graf = new Graf();
			graf.Diagnosztika();

			for (int i = 0; i < graf.N; i++)
			{
				for (int j = i; j < graf.N; j++)
				{
                    Console.WriteLine($"A {i} csúcsból elérhető a {j} csúcs: {graf.Elerheto(i,j)}");
                }
			}
		}
	}
}

/*
6 5
0 2
1 4
1 5
4 5
5 5

*/
