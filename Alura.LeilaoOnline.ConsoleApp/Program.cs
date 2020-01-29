using Alura.LeilaoOnline.Core;
using System;

namespace Alura.LeilaoOnline.ConsoleApp
{
    class Program
    {
        private static void LeilaoComVariosLances()
        {
            //Arranje - cenário
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.RecebeLance(fulano, 800);
            leilao.RecebeLance(maria, 900);
            leilao.RecebeLance(fulano, 1000);
            leilao.RecebeLance(maria, 990);

            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert
            var valorEesperado = 1000;
            var valorObtido = leilao.Ganhador.Valor;

            Verifica(valorEesperado, valorObtido);
        }

        private static void LeilaoComApenasUmLance()
        {
            //Arranje - cenário
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);

            leilao.RecebeLance(fulano, 800);

            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert
            var valorEesperado = 800;
            var valorObtido = leilao.Ganhador.Valor;

            Verifica(valorEesperado, valorObtido);
        }

        private static void Verifica(double esperado, double obtido)
        {
            var cor = Console.ForegroundColor;
            if (esperado == obtido)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("TESTE OK");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"TESTE FALHOU! esperado: {esperado}, obtido: {obtido}");
            }

            Console.ForegroundColor = cor;
                
        }


        static void Main()
        {
            LeilaoComVariosLances();
            LeilaoComApenasUmLance();
        }
    }
}
