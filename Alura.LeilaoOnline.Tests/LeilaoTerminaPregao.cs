using Alura.LeilaoOnline.Core;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoTerminaPregao
    {
        [Theory]
        [InlineData(1000, new double[] { 800, 900, 1000})]
        [InlineData(1000, new double[] { 800, 900, 1000, 990 })]
        [InlineData(800, new double[] { 800})]
        public void RetornarMaiorValorDadoLeilaoComPeloMenosUmLance(double valorEesperado,  double[] ofertas)
        {
            //Arranje - cenário
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);
            

            foreach (var valor in ofertas)
            {
                leilao.RecebeLance(fulano, valor);
            }
            

            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEesperado, valorObtido);
        }


        [Fact]
        public void RetornaZeroDadoLeilaoSemLances()
        {
            //Arranje - cenário
            var leilao = new Leilao("Van Gogh");

            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert
            var valorEesperado = 0;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEesperado, valorObtido);
        }
    }
}
