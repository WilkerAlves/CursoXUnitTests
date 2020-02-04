using System.Collections.Generic;
using System.Linq;

namespace Alura.LeilaoOnline.Core
{
    public enum EstadoLeilao
    {
        LeilaoAntesDoPregao,
        LeilaoEmAndamento,
        LeilaoFinalizado
    }


    public class Leilao
    {
        private IList<Lance> _lances;
        private Interessada _ultimoCliente;
        private IModalidadeAvaliacao _avaliador;
        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }
        public Lance Ganhador { get; private set; }
        public EstadoLeilao Estado { get; set; }
        
        

        private bool NovoLanceEhAceito(Interessada cliente)
        {
            return (Estado == EstadoLeilao.LeilaoEmAndamento)
                && (cliente != _ultimoCliente);
        }

        public Leilao(string peca, IModalidadeAvaliacao avaliador)
        {
            Peca = peca;
            _lances = new List<Lance>();
            Estado = EstadoLeilao.LeilaoAntesDoPregao;
            _avaliador = avaliador;
        }

        public void RecebeLance(Interessada cliente, double valor)
        {
            if (NovoLanceEhAceito(cliente))
            {
                _lances.Add(new Lance(cliente, valor));
                _ultimoCliente = cliente;
            }
        }

        public void IniciaPregao()
        {
            Estado = EstadoLeilao.LeilaoEmAndamento;
        }

        public void TerminaPregao()
        {
            if (Estado != EstadoLeilao.LeilaoEmAndamento)
                throw new System.InvalidOperationException("Não é possível terminar o pregão sem que ele tenha começado. Utilize o método inicia pregão");
            Ganhador = _avaliador.Avalia(this);
            Estado = EstadoLeilao.LeilaoFinalizado;
        }
    }
}