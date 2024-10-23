using System;

public class ListaSimples
{
    public class No
    {
        public int elemento;
        public No proximo;
    }
    public No inicio = null;

    public int Tamanho() { return Tamanho(inicio); }
    public int Tamanho(No no)
    {
        Console.Write("[");
        if (inicio == null)
        {
            return 0;
        }
        else
        {
            Console.Write(no.elemento + " ");
            return 1 + Tamanho(no.proximo);
        }

    }
    public void InserirInicio(int termos)
    {
        Random r = new Random();
        for (int i = 1; i <= termos; i++)
        {
            if (inicio == null)
            {
                inicio = new No { proximo = null, elemento = r.Next(1, 11) };
            }
            else
            {
                No no = new No { proximo = inicio, elemento = r.Next(1, 11) };
                inicio = no;
            }
        }
    }
    public void concat(ListaSimples l)
    {
        No no = inicio;
        while (no.proximo != null) { no = no.proximo; }
        for (No tmp = l.inicio; tmp != null; tmp = tmp.proximo)
        {
            no.proximo = new No();
            no.proximo.elemento = tmp.elemento;
            no = no.proximo;
        }
        no.proximo = null;
    }
    public int ImprimiLista() { return ImprimiLista(inicio); }
    public int ImprimiLista(No no)
    {
        Console.Write("[");
        for (No tmp = no; tmp != null; tmp = tmp.proximo)
        {
            Console.Write(tmp.elemento + " ");
        }
        Console.Write("]");
        return 0;
    }
    public float mediaLista(){
        int soma = 0; 
        for(No tmp = inicio; tmp != null; tmp = tmp.proximo){
            soma+=tmp.elemento;
        }
    }
}