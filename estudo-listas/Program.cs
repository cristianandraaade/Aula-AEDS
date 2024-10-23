using System;

class Program
{
    static void Main(string[] args)
    {
        int num_elemenos;
        int.TryParse(Console.ReadLine(), out num_elemenos);
        ListaSimples l = new ListaSimples();
        {
            l.InserirInicio(num_elemenos);

            l.ImprimiLista();
        }
    }
}
