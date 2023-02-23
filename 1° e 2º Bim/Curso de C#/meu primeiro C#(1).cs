using System;

class RaizesQuadradas
{
    static void main()
    {
        int numero;
        numero = 10;
        while (numero <= 100)
        {
            // Calcula o valor da raiz quadrada e
            // envia-la para o ecrã
            double raiz = Math.Sqrt(numero);
            Console.WriterLine("A raiz de {0} é {1}", numero, raiz);

            // Passa para o próximo número
            numero = numero + 10;
        }
    }
}