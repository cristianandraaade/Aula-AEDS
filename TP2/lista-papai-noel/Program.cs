using System;

class Program
{
    public static void Main(string[] args)
    {
        int n;
        int.TryParse(Console.ReadLine(), out n);
        char[] comportamento = new char[n];
        string[] nomes = new string[n];
        int bom = 0, mal = 0;
        for (int i = 0; i < n; i++)
        {
            string[] entrada = Console.ReadLine().Split(" ");
            comportamento[i] = char.Parse(entrada[0]);
            nomes[i] = entrada[1];
        }


        for (int i = 0; i < n; i++)
        {
            if (comportamento[i] == '+')
            {
                bom++;
            }
            else
            {
                mal++;
            }
        }


        ordenaPapaiNoel(comportamento, nomes, 0, 1);


        for (int i = 0; i < n; i++)
        {
            Console.WriteLine(nomes[i]);
        }
        Console.WriteLine("Se comportaram: " + bom + " | Nao se comportaram: " + mal);
    }

    public static void ordenaPapaiNoel(char[] comportamento, string[] nomes, int i, int j)
    {
        int n = nomes.Length;

        if (i < n)
        {
            if (j < n)
            {
                if (nomes[i].CompareTo(nomes[j]) > 0)
                {
                    string tempNome = nomes[j];
                    nomes[j] = nomes[i];
                    nomes[i] = tempNome;

                    ordenaPapaiNoel(comportamento, nomes, i, j + 1);
                }
                else
                {
                    ordenaPapaiNoel(comportamento, nomes, i, j + 1);
                }
            }
            else
            {
                ordenaPapaiNoel(comportamento, nomes, i+1, i + 2);
            }
        }

    }
}
