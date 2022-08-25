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
        public bool Xeque { get; private set; }

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Brancas;
            Terminada = false;
            Xeque = false;
            Peca = new HashSet<Peca>();
            Capturada = new HashSet<Peca>();
            ColocarPeca();
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementaQtdMov();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                Capturada.Add(pecaCapturada);
            }

            return pecaCapturada;
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);
            p.DecrementaQtdMov();

            if (pecaCapturada != null)
            {
                Tab.ColocarPeca(pecaCapturada, destino);
                Capturada.Remove(pecaCapturada);
            }

            Tab.ColocarPeca(p, origem);
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);

            if (EstaEmCheque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroExceptions("Você não pode se colocar em cheque!");
            }

            if (EstaEmCheque(Adversario(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }

            Turno++;
            MudaJogador();
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca peca in Capturada)
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

        private Cor Adversario(Cor cor)
        {
            if (cor == Cor.Brancas)
            {
                return Cor.Pretas;
            }
            else
            {
                return Cor.Brancas;
            }
        }

        private Peca Rei(Cor cor)
        {
            foreach (Peca p in PecasEmJogo(cor))
            {
                if (p is Rei)
                {
                    return p;
                }
            }

            return null;
        }

        public bool EstaEmCheque(Cor cor)
        {
            Peca R = Rei(cor);
            if (R == null)
            {
                throw new TabuleiroExceptions($"Não em rei da cor {cor} no tabuleiro!");
            }

            foreach (Peca peca in PecasEmJogo(Adversario(cor)))
            {
                bool[,] mat = peca.MovimentosPosiveis();
                if (mat[R.Posicao.Linha, R.Posicao.Coluna])
                {
                    return true;
                }
            }

            return false;
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
            //ColocarNovaPeca('d', 3, new Rainha(Cor.Brancas, Tab));

            ColocarNovaPeca('c', 8, new Torre(Cor.Pretas, Tab));
            ColocarNovaPeca('d', 8, new Rei(Cor.Pretas, Tab));
            ColocarNovaPeca('e', 8, new Torre(Cor.Pretas, Tab));
            ColocarNovaPeca('c', 7, new Torre(Cor.Pretas, Tab));
            ColocarNovaPeca('d', 7, new Torre(Cor.Pretas, Tab));
            ColocarNovaPeca('e', 7, new Torre(Cor.Pretas, Tab));
            //ColocarNovaPeca('d', 6, new Rainha(Cor.Pretas, Tab));
        }
    }
}
