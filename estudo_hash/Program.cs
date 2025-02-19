using System;
using System.Net.Security;
class Program
{
    public static void Main()
    {
        Random r = new Random();
        Hash h = new Hash(10);

        for (int i = 1; i <= h.tam; i++)
        {
            h.inserir(r.Next(1, 11));
        }
        h.ImprimirHash(h);
        int n = int.Parse(Console.ReadLine());
        h.remover(n);
        h.ImprimirHash(h);


    }
}
public class No { public int val; public No prox; }
public class Hash
{
    public No[] vetor = null;
    public int tam;

    public Hash(int n)
    {
        vetor = new No[n]; tam = n;
    }
    public int hash(int num)
    {
        return num % tam;
    }
    public bool existe(int num)
    {
        if (vetor == null) { return false; }
        int index = hash(num);
        No atual = vetor[index];
        while ((atual != null) && (atual.val != num))
        {
            atual = atual.prox;
        }
        if (atual == null) { return false; }
        return true;
    }
    public void inserir(int num)
    {
        int index = hash(num);
        No novo = new No
        {
            val = num,
            prox = null,
        };
        No atual = vetor[index];
        if (atual == null) { vetor[index] = novo; }
        else
        {
            while (atual.prox != null)
            {
                atual = atual.prox;
            }
            atual.prox = novo;
        }
    }
    public void ImprimirHash(Hash h)
    {
        for (int i = 0; i < vetor.Length; i++)
        {
            if (vetor[i] != null)
            {
                No atual = vetor[i];
                while (atual != null)
                {
                    Console.Write(atual.val);
                    atual = atual.prox;
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
            }
        }
    }
    public int remover(int num)
    {
        int index = hash(num);
        No atual = vetor[index];
        No tmp = new No();
        if (atual == null) { return -1; }
        else
        {
            while (atual != null)
            {
                if (atual.val == num)
                {
                    int retorno = atual.val;
                    No tmp2 = atual.prox;
                    tmp.prox = tmp2;
                    return retorno;
                }
                tmp = atual;
                atual = atual.prox;
            }
            return -1;
        }
    }
}