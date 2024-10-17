using System;
using System.Linq;

class Program
{
    public static void Main(string[] args)
    {
        int n = 0;
        int.TryParse(Console.ReadLine(), out n);
        string[] paises = new string[n];
        int[] ouro = new int[n];
        int[] prata = new int[n];
        int[] bronze = new int[n];

        for (int i = 0; i < n; i++)
        {

            string[] entrada = Console.ReadLine().Split(" ");
            paises[i] = entrada[0];
            ouro[i] = int.Parse(entrada[1]);
            prata[i] = int.Parse(entrada[2]);
            bronze[i] = int.Parse(entrada[3]);
        }
        ordenaVetor(ouro, prata, bronze, paises, 0, 0 + 1);

        for (int i = 0; i < n; i++)
        {

            Console.WriteLine($"{paises[i]} {ouro[i]} {prata[i]} {bronze[i]}");
        }
    }
    public static void ordenaVetor(int[] vGold, int[] vSilver, int[] vBronze, string[] paises, int i, int j)
    {
        int n = paises.Length;
        if (i < n)
        {
            if (j < n)
            {
                if (vGold[i] < vGold[j] || (vGold[i] == vGold[j] && vSilver[i] < vSilver[j]) || (vGold[i] == vGold[j] && vSilver[i] == vSilver[j] && vBronze[i] < vBronze[j]))
                {
                    string tmpPaises = paises[j];
                    int tmpBronze = vBronze[j];
                    int tmpPrata = vSilver[j];
                    int tmpGold = vGold[j];

                    //ordena os ouros
                    vGold[j] = vGold[i];
                    vGold[i] = tmpGold;

                    //faz o vetor das pratas acompanharem a mudança
                    vSilver[j] = vSilver[i];
                    vSilver[i] = tmpPrata;

                    //faz o vetor dos paises acompanharem a mudança
                    vBronze[j] = vBronze[i];
                    vBronze[i] = tmpBronze;

                    //faz o vetor dos paises acompanharem a mudança
                    paises[j] = paises[i];
                    paises[i] = tmpPaises;

                    ordenaVetor(vGold, vSilver, vBronze, paises, i, j + 1);
                }
                else
                {
                    ordenaVetor(vGold, vSilver, vBronze, paises, i, j + 1);
                }
            }
            else
            {
                ordenaVetor(vGold, vSilver, vBronze, paises, i + 1, i + 2);
            }
        }
    }
}
