using System;
using System.Globalization;
class Program
{
    public static void Main()
    {
        int[] vetor = {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15};
        int index = 0, num, i=1;
        num = int.Parse(Console.ReadLine());
        recursao(vetor, index, num, i);
    }
    public static int recursao(int[] vetor, int index, int num, int i){
        index = (i + vetor.Length)/2;
        if(vetor[index] == num){
            return vetor[index];
        }
        else if(index == 0){
            return -1;
        }
        else{
            recursao(vetor, index, num, i+1);
        }
    }
}



