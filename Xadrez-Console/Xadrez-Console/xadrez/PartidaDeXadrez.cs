using tabuleiro;
using tabuleiro.enums;
using tabuleiro.exceptions;

namespace xadrez
{
    internal class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Brancas;
            Terminada = false;
            ColocarPeca();
        }

        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementaQtdMov();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            ExecutaMovimento(origem, destino);
            Turno++;
            MudaJogador();
        }

        public void ValidarPosicaoDeOrigem(Posicao pos)
        {
            if (Tab.PosicaoPeca(pos) == null)
            {
                throw new TabuleiroExceptions("Não existe peça na posição de origem!");
            }
            if (JogadorAtual != Tab.PosicaoPeca(pos).Cor)
            {
                throw new TabuleiroExceptions("A peça de origem não é sua!");
            }
            if (!Tab.PosicaoPeca(pos).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroExceptions("Não existe movimentos disponíveis!");
            }
        }

        public void ValidarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.PosicaoPeca(origem ).PodeMoverPara(destino))
            {
                throw new TabuleiroExceptions("Posição de destino invalida!");
            }            
        }

        private void MudaJogador()
        {
            if (JogadorAtual == Cor.Brancas)
            {
                JogadorAtual = Cor.Pretas;
            }
            else
            {
                JogadorAtual = Cor.Brancas;
            }
        }

        public void ColocarPeca()
        {
            Tab.ColocarPeca(new Torre(Cor.Brancas, Tab), new PosicaoXadrez('c', 1).ToPosicao());
            Tab.ColocarPeca(new Rei(Cor.Brancas, Tab), new PosicaoXadrez('d', 1).ToPosicao());
            Tab.ColocarPeca(new Torre(Cor.Brancas, Tab), new PosicaoXadrez('e', 1).ToPosicao());
            Tab.ColocarPeca(new Torre(Cor.Brancas, Tab), new PosicaoXadrez('c', 2).ToPosicao());
            Tab.ColocarPeca(new Torre(Cor.Brancas, Tab), new PosicaoXadrez('d', 2).ToPosicao());
            Tab.ColocarPeca(new Torre(Cor.Brancas, Tab), new PosicaoXadrez('e', 2).ToPosicao());

            Tab.ColocarPeca(new Torre(Cor.Pretas, Tab), new PosicaoXadrez('b', 7).ToPosicao());
            Tab.ColocarPeca(new Rei(Cor.Pretas, Tab), new PosicaoXadrez('c', 6).ToPosicao());
            Tab.ColocarPeca(new Torre(Cor.Pretas, Tab), new PosicaoXadrez('f', 5).ToPosicao());
            Tab.ColocarPeca(new Torre(Cor.Pretas, Tab), new PosicaoXadrez('d', 7).ToPosicao());
            Tab.ColocarPeca(new Torre(Cor.Pretas, Tab), new PosicaoXadrez('c', 8).ToPosicao());
        }
    }
}
