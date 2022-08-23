using tabuleiro;
using tabuleiro.enums;

namespace xadrez
{
    internal class Rainha : Peca
    {
        public Rainha(Cor cor, Tabuleiro tabuleiro) : base(cor, tabuleiro)
        {
        }

        public override string ToString()
        {
            return "Q";
        }
    }
}
