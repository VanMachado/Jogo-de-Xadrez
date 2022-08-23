using tabuleiro;
using tabuleiro.enums;

namespace xadrez
{
    internal class Peao : Peca
    {
        public Peao(Cor cor, Tabuleiro tabuleiro) : base(cor, tabuleiro)
        {
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
