using System;
using System.Linq;

class Program
{
    public static void Main(string[] args)
    {
        int numero = -1;
        while (numero != 0) // Repete até que o número seja 0
        {
            int resultado = 0;
            int.TryParse(Console.ReadLine(), out numero);

            if (numero != 0) // Só continua se o número for diferente de 0
            {
                string[] valores = Console.ReadLine().Split(" "); // Lê e divide a linha de entrada em um array de strings
                for (int i = 0; i < numero; i++) // Percorre cada elemento do array
                {
                    if (i != numero - 1) // Verifica se não é o último elemento do array
                    {
                        if (valores[i] == "0") // Se o elemento atual for "0"
                        {
                            if (valores[i + 1] == "0") // E o próximo elemento também for "0"
                            {
                                resultado++; // Incrementa o contador
                                valores[i + 1] = "1"; // Altera o próximo elemento para "1" para evitar contagem dupla
                            }
                        }
                    }
                }
                Resultado(resultado);
            }
        }
    }

    public static void Resultado(int resultado)
    {
        Console.WriteLine(resultado); //resultado 
    }
}