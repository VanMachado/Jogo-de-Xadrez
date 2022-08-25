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
        public HashSet<Peca> Peca { get; private set; }
        public HashSet<Peca> Capturada { get; private set; }

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Brancas;
            Terminada = false;
            Peca = new HashSet<Peca>();
            Capturada = new HashSet<Peca>();
            ColocarPeca();
        }

        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementaQtdMov();
            Peca pecaCapturada = Tab.RetirarPeca(destino);            
            Tab.ColocarPeca(p, destino);
            if(pecaCapturada != null)
            {
                Capturada.Add(pecaCapturada);
            }
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            ExecutaMovimento(origem, destino);
            Turno++;
            MudaJogador();
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca peca in Capturada)
            {
                if (peca.Cor == cor)
                {
                    aux.Add(peca);
                }
            }

            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca peca in Peca)
            {
                if (peca.Cor == cor)
                {
                    aux.Add(peca);
                }
            }
            aux.ExceptWith(PecasCapturadas(cor));

            return aux;
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
            if (!Tab.PosicaoPeca(origem).PodeMoverPara(destino))
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

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            Peca.Add(peca);
        }

        public void ColocarPeca()
        {
            ColocarNovaPeca('c', 1, new Torre(Cor.Brancas, Tab));
            ColocarNovaPeca('d', 1, new Rei(Cor.Brancas, Tab));
            ColocarNovaPeca('e', 1, new Torre(Cor.Brancas, Tab));
            ColocarNovaPeca('c', 2, new Torre(Cor.Brancas, Tab));
            ColocarNovaPeca('d', 2, new Torre(Cor.Brancas, Tab));
            ColocarNovaPeca('e', 2, new Torre(Cor.Brancas, Tab));

            ColocarNovaPeca('c', 8, new Torre(Cor.Pretas, Tab));
            ColocarNovaPeca('d', 8, new Rei(Cor.Pretas, Tab));
            ColocarNovaPeca('e', 8, new Torre(Cor.Pretas, Tab));
            ColocarNovaPeca('c', 7, new Torre(Cor.Pretas, Tab));
            ColocarNovaPeca('d', 7, new Torre(Cor.Pretas, Tab));
            ColocarNovaPeca('e', 7, new Torre(Cor.Pretas, Tab));
        }
    }
}
