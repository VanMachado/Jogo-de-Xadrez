using tabuleiro;
using tabuleiro.enums;

namespace xadrez
{
    internal class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        private int Turno { get; set; }
        private Cor JogadorAtual { get; set; }
        public bool Terminada { get; private set; }

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
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

        public void ColocarPeca()
        {
            Tab.ColocarPeca(new Torre(Cor.Branca, Tab), new PosicaoXadrez('a', 1).ToPosicao());
            //Tab.ColocarPeca(new Cavalo(Cor.Branca, Tab), new PosicaoXadrez('b', 1).ToPosicao());
            //Tab.ColocarPeca(new Bispo(Cor.Branca, Tab), new PosicaoXadrez('c', 1).ToPosicao());
           // Tab.ColocarPeca(new Rainha(Cor.Branca, Tab), new PosicaoXadrez('d', 1).ToPosicao());
            Tab.ColocarPeca(new Rei(Cor.Branca, Tab), new PosicaoXadrez('e', 1).ToPosicao());
           // Tab.ColocarPeca(new Bispo(Cor.Branca, Tab), new PosicaoXadrez('f', 1).ToPosicao());
           // Tab.ColocarPeca(new Cavalo(Cor.Branca, Tab), new PosicaoXadrez('g', 1).ToPosicao());
            Tab.ColocarPeca(new Torre(Cor.Branca, Tab), new PosicaoXadrez('h', 1).ToPosicao());            

            /*Tab.ColocarPeca(new Peao(Cor.Branca, Tab), new PosicaoXadrez('a', 2).ToPosicao());
            Tab.ColocarPeca(new Peao(Cor.Branca, Tab), new PosicaoXadrez('b', 2).ToPosicao());
            Tab.ColocarPeca(new Peao(Cor.Branca, Tab), new PosicaoXadrez('c', 2).ToPosicao());
            Tab.ColocarPeca(new Peao(Cor.Branca, Tab), new PosicaoXadrez('d', 2).ToPosicao());
            Tab.ColocarPeca(new Peao(Cor.Branca, Tab), new PosicaoXadrez('e', 2).ToPosicao());
            Tab.ColocarPeca(new Peao(Cor.Branca, Tab), new PosicaoXadrez('f', 2).ToPosicao());
            Tab.ColocarPeca(new Peao(Cor.Branca, Tab), new PosicaoXadrez('g', 2).ToPosicao());
            Tab.ColocarPeca(new Peao(Cor.Branca, Tab), new PosicaoXadrez('h', 2).ToPosicao());

            Tab.ColocarPeca(new Torre(Cor.Preta, Tab), new PosicaoXadrez('a', 8).ToPosicao());
            Tab.ColocarPeca(new Cavalo(Cor.Preta, Tab), new PosicaoXadrez('b', 8).ToPosicao());
            Tab.ColocarPeca(new Bispo(Cor.Preta, Tab), new PosicaoXadrez('c', 8).ToPosicao());
            Tab.ColocarPeca(new Rainha(Cor.Preta, Tab), new PosicaoXadrez('d', 8).ToPosicao());
            Tab.ColocarPeca(new Rei(Cor.Preta, Tab), new PosicaoXadrez('e', 8).ToPosicao());
            Tab.ColocarPeca(new Bispo(Cor.Preta, Tab), new PosicaoXadrez('f', 8).ToPosicao());
            Tab.ColocarPeca(new Cavalo(Cor.Preta, Tab), new PosicaoXadrez('g', 8).ToPosicao());
            Tab.ColocarPeca(new Torre(Cor.Preta, Tab), new PosicaoXadrez('h', 8).ToPosicao());

            Tab.ColocarPeca(new Peao(Cor.Preta, Tab), new PosicaoXadrez('a', 7).ToPosicao());
            Tab.ColocarPeca(new Peao(Cor.Preta, Tab), new PosicaoXadrez('b', 7).ToPosicao());
            Tab.ColocarPeca(new Peao(Cor.Preta, Tab), new PosicaoXadrez('c', 7).ToPosicao());
            Tab.ColocarPeca(new Peao(Cor.Preta, Tab), new PosicaoXadrez('d', 7).ToPosicao());
            Tab.ColocarPeca(new Peao(Cor.Preta, Tab), new PosicaoXadrez('e', 7).ToPosicao());
            Tab.ColocarPeca(new Peao(Cor.Preta, Tab), new PosicaoXadrez('f', 7).ToPosicao());
            Tab.ColocarPeca(new Peao(Cor.Preta, Tab), new PosicaoXadrez('g', 7).ToPosicao());
            Tab.ColocarPeca(new Peao(Cor.Preta, Tab), new PosicaoXadrez('h', 7).ToPosicao());*/
        }
    }
}
