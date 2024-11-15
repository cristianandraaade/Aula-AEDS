using System;
using System.Linq.Expressions;
using System.IO;
using System.Security.Cryptography.X509Certificates;

public class No
{
    public Pokemon atual;
    public No prox;
}
public class Pilha
{
    public No inicio;
    public int tamanho;
    public void Capturar_Pokemon(Pilha q, Fila p, string[] parametros_busca)
    {
        No tmp = p.inicio;
        float[] casos_especiais = new float[2];
        if (float.TryParse(parametros_busca[0], out _) && float.TryParse(parametros_busca[1], out _))
        {
            casos_especiais[0] = float.Parse(parametros_busca[0]);
            casos_especiais[1] = float.Parse(parametros_busca[1]);
        }
        for (; tmp != null; tmp = tmp.prox)
        {

            if (tmp.atual.name == parametros_busca[0])
            {
                Console.WriteLine(tmp.atual.name);
                q.InserirPilha(q, tmp);
                p.RemoveFila(p, tmp);
            }
            else if (tmp.atual.type1 == parametros_busca[0] && tmp.atual.type2 == parametros_busca[1])
            {
                Console.WriteLine(tmp.atual.name);
                q.InserirPilha(q, tmp);
                p.RemoveFila(p, tmp);
            }
            else if (tmp.atual.weight_kg > casos_especiais[0] && tmp.atual.weight_kg <= casos_especiais[1])
            {
                Console.WriteLine(tmp.atual.name);
                q.InserirPilha(q, tmp);
                p.RemoveFila(p, tmp);
            }
        }
    }
    public void InserirPilha(Pilha q, No p)
    {
        if (q.inicio == null)
        {
            q.inicio = new No
            {
                atual = p.atual,
                prox = null,
            };
        }
        else
        {
            No tmp = new No
            {
                atual = p.atual,
                prox = q.inicio,
            };
            q.inicio = tmp;
        }
    }


    public void ImprimirPilha(No inicio)
    {
        No tmp = inicio;
        while (tmp != null)
        {
            Console.WriteLine(tmp.atual.name);
            tmp = tmp.prox;
        }
    }
    public int TamanhoPilha(No inicio)
    {
        No tmp = inicio;
        if (tmp == null)
        {
            return 0;
        }
        else
        {
            return 1 + TamanhoPilha(tmp.prox);
        }
    }
}

public class Fila
{
    public No inicio;
    public int tamanho;

    public void InserirPokemon(Fila f, Pokemon p)
    {
        No novo = new No
        {
            atual = p,
            prox = null,
        };
        if (f.inicio == null)
        {
            f.inicio = novo;
        }
        else
        {
            No tmp = f.inicio;
            while (tmp.prox != null)
            {
                tmp = tmp.prox;
            }
            tmp.prox = novo;
        }
    }
    public void ImprimirLista(No inicio)
    {
        No tmp = inicio;
        if(tmp == null){
            Console.WriteLine();
        }
        while (tmp != null)
        {
            Console.WriteLine(tmp.atual.name);
            tmp = tmp.prox;
        }
    }
    public void RemoveFila(Fila f, No remove)
    {
        if (f.inicio == null || remove == null) return;

        if (f.inicio == remove)
        {
            f.inicio = f.inicio.prox;
            return;
        }

        No tmp = f.inicio;
        while (tmp != null && tmp.prox != remove)
        {
            tmp = tmp.prox;
        }
        if (tmp != null && tmp.prox == remove)
        {
            tmp.prox = remove.prox;
        }
    }
    public int TamanhoFila(No inicial)
    {
        No tmp = inicial;
        if (tmp == null)
        {
            return 0;
        }
        else
        {
            return 1 + TamanhoFila(tmp.prox);
        }
    }
    public void Preparar_Pokemon(Fila f, Pilha p)
    {
        if (f.inicio == null)
        {
            f.inicio = new No
            {
                atual = p.inicio.atual,
                prox = null,
            };
            p.inicio = p.inicio.prox;
        }
        else
        {
            No tmp = f.inicio;
            while (tmp.prox != null)
            {
                tmp = tmp.prox;
            }
            tmp.prox = new No
            {
                atual = p.inicio.atual,
                prox = null,
            };
            p.inicio = p.inicio.prox;
        }
    }
    public Pokemon Remove_Fila(Fila f)
    {
        if (f.inicio == null)
        {
            return null;
        }
        Pokemon tmp = f.inicio.atual;
        f.inicio = f.inicio.prox;
        return tmp;
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
        Fila pokemon_mundo = new Fila();
        Pilha equipe_batalha = new Pilha();
        Fila preparar_pokemon = new Fila();
        for (int i = 0; i < n; i++)
        {
            string[] entrada = Console.ReadLine().Split(";");
            Pokemon p = new Pokemon
            {
                id = int.TryParse(entrada[0], out int id) ? id : 0,
                generation = int.TryParse(entrada[1], out int generation) ? generation : 0,
                name = entrada[2],
                description = entrada[3],
                type1 = validadeType(entrada[4]),
                type2 = validadeType(entrada[5]),
                weight_kg = float.TryParse(entrada[6], out float weight) ? weight : 0.0f,
                height_m = float.TryParse(entrada[7], out float height) ? height : 0.0f,
                capture_rate = int.TryParse(entrada[8], out int captureRate) ? captureRate : 0,
                is_legendary = retornaIs_Legendary(entrada[9]),
                capture_date = entrada[10],
                abilities = validateAbilities(entrada),
            };
            pokemon_mundo.InserirPokemon(pokemon_mundo, p);
        }
        //pokemon_mundo.ImprimirLista(pokemon_mundo.inicio);
        int.TryParse(Console.ReadLine(), out c);
        for (int i = 0; i < c; i++)
        {
            string[] entrada2 = Console.ReadLine().Split(" ");

            if (entrada2[0] == "C")//FUNÇÃO CAPTURAR POKEMON
            {
                string[] parametros_busca = retornaEntrada(entrada2);
                equipe_batalha.Capturar_Pokemon(equipe_batalha, pokemon_mundo, parametros_busca);
            }
            else if (entrada2[0] == "P")//FUNÇÃO PREPARAR POKEMON
            {
                int num_acoes = int.Parse(entrada2[1]);
                for (int j = 1; j <= num_acoes; j++)
                {
                    preparar_pokemon.Preparar_Pokemon(preparar_pokemon, equipe_batalha);
                }
                preparar_pokemon.ImprimirLista(preparar_pokemon.inicio);
            }
            else if (entrada2[0] == "B")// FUNÇÃO BATALHAR POKEMON
            {
                int num_acoes = int.Parse(entrada2[1]);
                for (int j = 1; j <= num_acoes; j++)
                {
                    Pokemon p = preparar_pokemon.Remove_Fila(preparar_pokemon);
                    if (p != null)
                    {
                        Console.WriteLine($"{p.id};{p.generation};{p.name};{p.description};{p.type1};{get_Type(p.type2)};{p.weight_kg};{p.height_m};{p.capture_rate};{get_Is_Legendary(p.is_legendary)};{p.capture_date};{get_Abilities(p.abilities)}");
                    }
                }
            }
            else if (entrada2[0] == "R")//FUNÇÃO RETORNAR O POKEMON
            {
                int num_acoes = int.Parse(entrada2[1]);
                for (int k = 1; k <= num_acoes; k++)
                {
                    Pokemon p = preparar_pokemon.inicio.atual;
                    preparar_pokemon.inicio = preparar_pokemon.inicio.prox;
                    Console.WriteLine(p.name);
                    No novo = new No()
                    {
                        atual = p,
                        prox = equipe_batalha.inicio.prox,
                    };
                    equipe_batalha.inicio = novo;
                }
            }
            else
            {
                int num_acoes = int.Parse(entrada2[1]);
                for (int l = 1; l <= num_acoes; l++)
                {
                    Pokemon p = preparar_pokemon.Remove_Fila(preparar_pokemon);
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
    public static string[] retornaEntrada(string[] entrada)
    {
        string[] retorno = new string[2];
        for (int i = 1; i < entrada.Length; i++)
        {
            if (!string.IsNullOrEmpty(entrada[i]))
            {
                retorno[i - 1] = entrada[i];
            }
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
