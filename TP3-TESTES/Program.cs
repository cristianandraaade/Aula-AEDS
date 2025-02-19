using System;
using System.Collections;
class CCelula
{
    public Pokemon atual;
    public CCelula prox;
    public CCelula()
    {
        atual = null;
        prox = null;
    }
    public CCelula(Pokemon ValorItem)
    {
        atual = ValorItem;
        prox = null;
    }
    public CCelula(Pokemon ValorItem, CCelula ProxCelula)
    {
        atual = ValorItem;
        prox = ProxCelula;
    }
}
class CFila
{
    public CCelula inicio;
    public CCelula fim;
    private int Qtde = 0;

    public void Imprimir(CFila f)
    {
        for (CCelula aux = f.inicio; aux != null; aux = aux.prox)
            Console.WriteLine(aux.atual.name);
    }
    public void Enfileira(CFila f, Pokemon ValorItem)
    {
        if (f.inicio == null)
        {
            f.inicio = f.fim = new CCelula(ValorItem);
        }
        else
        {
            f.fim.prox = new CCelula(ValorItem);
            f.fim = f.fim.prox;
        }
        f.Qtde++;
    }
    public Pokemon Desenfileira(CFila f)
    {
        Pokemon Item = null;
        if (f.inicio != f.fim)
        {
            Item = f.inicio.atual;
            f.inicio = f.inicio.prox;
            f.Qtde--;
        }
        else if (f.inicio == f.fim)
        {
            if (f.inicio == null)
            {
                return null;
            }
            else
            {
                Item = f.inicio.atual;
                f.inicio = f.fim = null;
            }
        }
        return Item;
    }
    public Pokemon Desenfileira_busca(CFila f, Pokemon p)
    {
        Pokemon Item = null;
        if (f.inicio.atual == p)
        {
            Item = f.inicio.atual;
            f.inicio = f.inicio.prox;
            if (f.inicio == null) { f.fim = null; }
            f.Qtde--;
            return Item;
        }
        CCelula tmp = f.inicio;
        while (tmp.prox != null)
        {
            if (tmp.prox.atual == p)
            {
                Item = tmp.prox.atual;
                tmp.prox = tmp.prox.prox;
                if (tmp.prox == null) { fim = tmp; }
                f.Qtde--;
            }
            tmp = tmp.prox;
        }
        return Item;
    }
    public int Quantidade(CFila f)
    {
        return f.Qtde;
    }
    public void CapturarPokemon(CFila f, CPilha q, string[] parametros_busca)
    {
        float[] casos_especiais = new float[2];
        if (float.TryParse(parametros_busca[0], out _) && float.TryParse(parametros_busca[1], out _))
        {
            casos_especiais[0] = float.Parse(parametros_busca[0]);
            casos_especiais[1] = float.Parse(parametros_busca[1]);
        }
        for (CCelula aux = f.inicio; aux != null; aux = aux.prox)
        {
            if (aux.atual.name == parametros_busca[0])
            {
                Pokemon p = Desenfileira_busca(f, aux.atual);
                Console.WriteLine(p.name);
                q.Empilha(q, p);
            }
            else if (aux.atual.type1 == parametros_busca[0] && aux.atual.type2 == parametros_busca[1])
            {
                Pokemon p = Desenfileira_busca(f, aux.atual);
                Console.WriteLine(p.name);
                q.Empilha(q, p);
            }
            else if (aux.atual.weight_kg > casos_especiais[0] && aux.atual.weight_kg <= casos_especiais[1])
            {
                Pokemon p = Desenfileira_busca(f, aux.atual);
                Console.WriteLine(p.name);
                q.Empilha(q, p);
            }
        }
    }
}
class CPilha
{
    public CCelula topo = null;
    public int Qtde = 0;
    public void Empilha(CPilha p, Pokemon ValorItem)
    {
        p.topo = new CCelula(ValorItem, p.topo);
        p.Qtde++;
    }
    public Pokemon Desempilha(CPilha p)
    {
        Pokemon Item = null;
        if (p.topo != null)
        {
            Item = p.topo.atual;
            p.topo = p.topo.prox;
            p.Qtde--;
        }
        return Item;
    }
    public void Imprimir(CPilha p)
    {
        for (CCelula aux = p.topo; aux != null; aux = aux.prox)
            Console.WriteLine(aux.atual.name);
    }

}
public class Pokemon
{
    public int id;
    public int generation;
    public string name;
    public string description;
    public string type1;
    public string type2;
    public float weight_kg;
    public float height_m;
    public int capture_rate;
    public bool is_legendary;
    public string capture_date;
    public string[] abilities;
}
public class Program
{
    static void Main(string[] args)
    {
        int n = 0, c = 0;
        int.TryParse(Console.ReadLine(), out n);
        CFila pokemon_mundo = new CFila();
        CPilha equipe_batalha = new CPilha();
        CFila preparar_pokemon = new CFila();
        for (int i = 0; i < n; i++)
        {
            string[] entrada = Console.ReadLine().Split(";");
            Pokemon p = new Pokemon
            {
                id = int.Parse(entrada[0]), 
                generation = int.Parse(entrada[1]),
                name = entrada[2],
                description = entrada[3],
                type1 = validadeType(entrada[4]),
                type2 = validadeType(entrada[5]),
                weight_kg = float.TryParse(entrada[6], out float weight) ? weight : 0.0f,
                height_m = float.TryParse(entrada[7], out float height) ? height : 0.0f,
                capture_rate = int.Parse(entrada[8]), 
                is_legendary = retornaIs_Legendary(entrada[9]),
                capture_date = entrada[10],
                abilities = validateAbilities(entrada),
            };
            if(p.name != "")
            pokemon_mundo.Enfileira(pokemon_mundo, p);
        }
        //pokemon_mundo.Imprimir(pokemon_mundo);
        int.TryParse(Console.ReadLine(), out c);
        for (int i = 0; i < c; i++)
        {
            string[] entrada2 = Console.ReadLine().Split(" ");

            if (entrada2[0] == "C")//FUNÇÃO CAPTURAR POKEMON
            {
                string[] parametros_busca = retornaEntrada(entrada2);
                pokemon_mundo.CapturarPokemon(pokemon_mundo, equipe_batalha, parametros_busca);
            }
            else if (entrada2[0] == "P")//FUNÇÃO PREPARAR POKEMON
            {
                int num_acoes = int.Parse(entrada2[1]);
                for (int j = 1; j <= num_acoes; j++)
                {
                    Pokemon p = equipe_batalha.Desempilha(equipe_batalha);
                    if (p != null)
                    {
                        Console.WriteLine(p.name);
                        preparar_pokemon.Enfileira(preparar_pokemon, p);
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                }

            }
            else if (entrada2[0] == "B")// FUNÇÃO BATALHAR POKEMON
            {
                int num_acoes = int.Parse(entrada2[1]);
                for (int j = 1; j <= num_acoes; j++)
                {
                    Pokemon p = preparar_pokemon.Desenfileira(preparar_pokemon);
                    if (p != null)
                    {
                        Console.WriteLine($"{p.id};{p.generation};{p.name};{p.description};{p.type1};{get_Type(p.type2)};{p.weight_kg};{p.height_m};{p.capture_rate};{get_Is_Legendary(p.is_legendary)};{p.capture_date};{get_Abilities(p.abilities)}");
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                }
            }
            else if (entrada2[0] == "R")//FUNÇÃO RETORNAR O POKEMON
            {
                int num_acoes = int.Parse(entrada2[1]);
                for (int k = 1; k <= num_acoes; k++)
                {
                    Pokemon p = preparar_pokemon.Desenfileira(preparar_pokemon);
                    Console.WriteLine(p.name);
                    equipe_batalha.Empilha(equipe_batalha, p);
                }
            }
            else if(entrada2[0] == "L")
            {
                int num_acoes = int.Parse(entrada2[1]);
                for (int l = 1; l <= num_acoes; l++)
                {
                    Pokemon p = preparar_pokemon.Desenfileira(preparar_pokemon);
                    if (p != null)
                    {
                        Console.WriteLine($"{p.id};{p.generation};{p.name};{p.description};{p.type1};{get_Type(p.type2)};{p.weight_kg};{p.height_m};{p.capture_rate};{get_Is_Legendary(p.is_legendary)};{p.capture_date};{get_Abilities(p.abilities)}");
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                }
            }
        }
    }


    //FUNÇOES DE TRATAMENTO DE DADOS
    public static string[] retornaEntrada(string[] entrada)
    {
        string[] retorno = new string[2];
        for (int i = 1; i < entrada.Length && i - 1 < retorno.Length; i++)
        {
            retorno[i - 1] = entrada[i];
        }
        return retorno;
    }
    public static string[] validateAbilities(string[] entrada)
    {
        {
            string[] retorno = new string[4];
            int habilidades = entrada.Length - 11;
            for (int i = 0; i < habilidades && i < 4; i++)
            {
                retorno[i] = entrada[11 + i];
            }

            return retorno;
        }
    }
    public static bool retornaIs_Legendary(string is_legendary)
    {
        if (is_legendary == "1")
        {
            return true;
        }
        return false;
    }
    public static string validadeType(string type)
    {
        if (!string.IsNullOrEmpty(type))
        {
            return type;
        }

        return "Unknown";
    }
    public static string get_Type(string type)
    {
        if (type == "Unknown")
        {
            return "";
        }
        else
        {
            return type;
        }
    }
    public static int get_Is_Legendary(bool n)
    {
        if (n == true)
        {
            return 1;
        }
        return 0;
    }
    public static string get_Abilities(string[] abilities)
    {
        string concat = null;
        for (int i = 0; i < abilities.Length; i++)
        {
            if (abilities[i] != null)
            {
                if (abilities[i + 1] != null)
                {
                    concat += abilities[i] + ";";
                }
                else
                {
                    concat += abilities[i];
                }
            }
        }
        return concat;
    }
}
