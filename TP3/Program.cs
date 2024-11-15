using System;
using System.Linq.Expressions;
using System.IO;
using System.Security.Cryptography.X509Certificates;

public class Pilha
{
    public Pokemon atual;
    public Pilha prox;

    public Pilha Capturar_Pokemon(Pilha q, Fila p, string[] parametros_busca)
    {
        float[] casos_especiais = new float[2];
        if (float.TryParse(parametros_busca[0], out _) && float.TryParse(parametros_busca[1], out _))
        {
            casos_especiais[0] = float.Parse(parametros_busca[0]);
            casos_especiais[1] = float.Parse(parametros_busca[1]);
        }
        for (Fila tmp = p; tmp != null; tmp = tmp.prox)
        {

            if (tmp.atual.name == parametros_busca[0])
            {
                Console.WriteLine(tmp.atual.name);
                q = Inserir_Pilha(q, tmp.atual);
            }
            else if (tmp.atual.type1 == parametros_busca[0] && tmp.atual.type2 == parametros_busca[1])
            {
                Console.WriteLine(tmp.atual.name);
                q = Inserir_Pilha(q, tmp.atual);
            }
            else if (tmp.atual.weight_kg > casos_especiais[0] && tmp.atual.weight_kg <= casos_especiais[1])
            {
                Console.WriteLine(tmp.atual.name);
                q = Inserir_Pilha(q, tmp.atual);
            }
        }
        return q;
    }
    public Pilha Inserir_Pilha(Pilha q, Pokemon p)
    {
        if (q == null)
        {
            q.atual = p; q.prox = null;
            return q;
        }
        else
        {
            Pilha tmp = new Pilha(); tmp.atual = p;
            tmp.prox = q;
            q = tmp;
            return q;
        }
    }
}
public class Fila
{
    public Pokemon atual;
    public Fila prox;
    public void InserirPokemon(Fila f, Pokemon p)
    {

        if (f.atual == null)
        {
            f.atual = p;
            f.prox = null;
        }
        else if (f.prox != null)
        {
            InserirPokemon(f.prox, p);
        }
        else
        {
            f.prox = new Fila();
            f.prox.atual = p;
            f.prox.prox = null;
        }
    }
    public void ImprimiLista(Fila f)
    {
        if (f == null)
        {
            Console.WriteLine("A fila está vazia.");
            return;
        }

        for (Fila tmp = f; tmp != null; tmp = tmp.prox)
        {
            if (tmp.atual != null)
            {
                Console.WriteLine(tmp.atual.name);
            }
            else
            {
                Console.WriteLine("Pokémon nulo encontrado.");
            }
        }
    }
    public int TamanhoFila(Fila f)
    {
        if (f.prox == null) { return 0; }
        else { return 1 + TamanhoFila(f.prox); }
    }
    public Fila Preparar_Pokemon(Fila q, Pilha p, int num_acoes)
    {
        // VERIFICAÇÃO SE A FILA DE ENTRADA ESTA VAZIA
        if (q == null)
        {
            q = new Fila();
        }

        for (int i = 0; i < num_acoes; i++)
        {
            if (p.atual != null)
            {

                Fila novoNo = new Fila();
                novoNo.atual = p.atual; // ATRIBUI O POKEMON DA PILHA DENTRO DA FILA CRIADA
                if (q.prox == null)
                {
                    //ADICIONA O POKEMON DENTRO DA LISTA DE ENTRADA (NO CASO DA FILA ESTAR VAZIA)
                    q.prox = novoNo;
                    //Console.WriteLine(q.prox.atual.name);
                }
                else
                {
                    //ADICIONA O POKEMON DENTRO DA LISTA DE ENTRADA (AQUI ELE PERCORRE A LISTA ATE O ELEMENTO FINAL)
                    Fila temp = q;
                    while (temp.prox != null)
                    {
                        temp = temp.prox;
                    }
                    temp.prox = novoNo;
                    //Console.WriteLine(temp.prox.atual.name);
                }
            }
            p = p.prox;
        }
        return q;
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
        int.TryParse(Console.ReadLine(), out c);
        for (int i = 0; i < c; i++)
        {
            string[] entrada2 = Console.ReadLine().Split(" ");

            if (entrada2[0] == "C")//FUNÇÃO CAPTURAR POKEMON
            {
                string[] parametros_busca = retornaEntrada(entrada2);
                equipe_batalha = equipe_batalha.Capturar_Pokemon(equipe_batalha, pokemon_mundo, parametros_busca);
            }

            else if (entrada2[0] == "P")//FUNÇÃO PREPARAR POKEMON
            {
                int num_acoes = int.Parse(entrada2[1]);
                preparar_pokemon = preparar_pokemon.Preparar_Pokemon(preparar_pokemon, equipe_batalha, num_acoes);
                for (Fila tmp = preparar_pokemon.prox; tmp != null; tmp = tmp.prox)
                {
                    Console.WriteLine(tmp.atual.name);
                }
            }

            else if (entrada2[0] == "B")// FUNÇÃO BATALHAR POKEMON
            {
                int num_acoes = int.Parse(entrada2[1]);
                for (int j = 0; j <= num_acoes; j++)
                {
                    Pokemon p = preparar_pokemon.atual;
                    if (p != null)
                    {
                        Console.WriteLine($"{p.id};{p.generation};{p.name};{p.description};{p.type1};{get_Type(p.type2)};{p.weight_kg};{p.height_m};{p.capture_rate};{get_Is_Legendary(p.is_legendary)};{p.capture_date};{get_Abilities(p.abilities)}");
                    }
                    preparar_pokemon = preparar_pokemon.prox;
                }
            }

            else if (entrada2[0] == "R")//FUNÇÃO RETORNAR O POKEMON
            {
                int num_acoes = int.Parse(entrada2[1]);
                for (int k = 1; k <= num_acoes; k++)
                {
                    Pokemon p = preparar_pokemon.atual;
                    preparar_pokemon = preparar_pokemon.prox;
                    Console.WriteLine(p.name);
                    Pilha novoNo = new Pilha();
                    novoNo.atual = p;
                    novoNo.prox = equipe_batalha;
                    equipe_batalha = novoNo;
                }
            }
            else
            {
                int num_acoes = int.Parse(entrada2[1]);
                for (int l = 1; l <= num_acoes; l++)
                {
                    Pokemon p = preparar_pokemon.atual;
                    Fila tmp = preparar_pokemon.prox;
                    if (preparar_pokemon.prox != null && preparar_pokemon.atual != null)
                    {
                        Console.WriteLine($"{p.id};{p.generation};{p.name};{p.description};{p.type1};{get_Type(p.type2)};{p.weight_kg};{p.height_m};{p.capture_rate};{get_Is_Legendary(p.is_legendary)};{p.capture_date};{get_Abilities(p.abilities)}");
                    }
                    if (tmp != null)
                    {
                        preparar_pokemon = tmp;
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




