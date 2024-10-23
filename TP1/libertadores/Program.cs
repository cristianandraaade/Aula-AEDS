using System;
using System.Linq;

class Program
{
    public static void Main(string[] args)
    {
        int numeroCasos = 0;
        int.TryParse(Console.ReadLine(), out numeroCasos);
        string separador = " x "; // String usada como separador no Split
        for (int i = 0; i < numeroCasos; i++) // Percorre todos os casos
        {
            // Primeiro time
            string[] entradaTime1 = Console.ReadLine().Split(separador);
            int[] valoresTime1 = entradaTime1.Select(int.Parse).ToArray();
            int golsTime1 = valoresTime1[0];
            int golsSofridosTime1 = valoresTime1[1];

            // Segundo time
            string[] entradaTime2 = Console.ReadLine().Split(separador);
            int[] valoresTime2 = entradaTime2.Select(int.Parse).ToArray();
            int golsTime2 = valoresTime2[0];
            int golsSofridosTime2 = valoresTime2[1];

            // Calcula os pontos de cada time
            int pontosTime1 = CalcularPontos(golsTime1, golsSofridosTime1);
            int pontosTime2 = CalcularPontos(golsTime2, golsSofridosTime2);

            // Verifica o time vencedor ou se haverá penalidade
            string resultado = VerificarResultado(pontosTime1, golsTime1, golsSofridosTime1, pontosTime2, golsTime2, golsSofridosTime2);

            Console.WriteLine(resultado); // Exibe o resultado
        }
    }

    static int CalcularPontos(int golsMarcados, int golsSofridos) // Calcula os pontos que o time ganhou
    {
        if (golsMarcados > golsSofridos)
            return 3;
        else if (golsMarcados == golsSofridos)
            return 1;
        else
            return 0;
    }

    static string VerificarResultado(int pontosTime1, int golsTime1, int golsSofridosTime1, int pontosTime2, int golsTime2, int golsSofridosTime2) // Verifica o vencedor ou se haverá penalidade
    {
        if (pontosTime1 > pontosTime2)
        {
            return "Time 1";
        }
        else if (pontosTime1 < pontosTime2)
        {
            return "Time 2";
        }
        else
        {
            if (golsTime1 - golsSofridosTime1 > golsTime2 - golsSofridosTime2)
                return "Time 1";
            else if (golsTime1 - golsSofridosTime1 < golsTime2 - golsSofridosTime2)
                return "Time 2";
            else if (golsSofridosTime1 < golsSofridosTime2)
                return "Time 1";
            else if (golsSofridosTime1 > golsSofridosTime2)
                return "Time 2";
            else
                return "Penaltis";
        }
    }
}