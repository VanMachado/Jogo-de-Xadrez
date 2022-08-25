using tabuleiro;
using tabuleiro.enums;

namespace xadrez
{
    internal class Rainha //: Peca
    {
        public Rainha(Cor cor, Tabuleiro tabuleiro) //: base(cor, tabuleiro)
        {
        }

        /*private bool PodeMover(Posicao pos)
        {
            Peca p = Tabuleiro.PosicaoPeca(pos);
            return p == null || p.Cor != Cor;
        }*/

       /* public override bool[,] MovimentosPosiveis()
        {
            

            return mat;
        }*/

        public override string ToString()
        {
            return "Q";
        }
    }
}
