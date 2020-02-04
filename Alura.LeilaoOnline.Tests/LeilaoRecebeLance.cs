using Alura.LeilaoOnline.Core;
using System.Linq;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoRecebeLance
    {
        [Theory]
        [InlineData(2, new double[] { 800, 900 })]
        [InlineData(4, new double[] { 1000, 1200, 1300, 1400 })]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(int qtdeEsperada, double[] ofertas)
        {
            //Arranje - cenário
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("maria", leilao);
            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];

                if ((i % 2) == 0)
                {
                    leilao.RecebeLance(fulano, valor);
                }
                else
                {
                    leilao.RecebeLance(maria, valor);
                }

            }
            
            leilao.TerminaPregao();

            //Act - método sob teste
            leilao.RecebeLance(fulano, 1000);


            //Assert
            Assert.Equal(qtdeEsperada, leilao.Lances.Count());
        }

        [Fact]
        public void NaoAceitaProximoLanceDadoMesmoClienteRealizouUltimoLance()
        {
            //Arranje - cenário
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            leilao.IniciaPregao();
            leilao.RecebeLance(fulano, 800);

            //Act - método sob teste
            leilao.RecebeLance(fulano, 1000);

            //Assert
            var qtdeEsperada = 1;
            var qtdeObtida = leilao.Lances.Count();
            Assert.Equal(qtdeEsperada,qtdeObtida);
        }
    }
}
