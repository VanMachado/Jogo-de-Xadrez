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
        private HashSet<Peca> Peca;
        private HashSet<Peca> Capturada;
        public bool Xeque { get; private set; }
        public Peca vulneravelEnPassant { get; private set; }

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Brancas;
            Terminada = false;
            Xeque = false;
            vulneravelEnPassant = null;
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

            // #Jogada especial "Roque pequeno"
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tab.RetirarPeca(origemT);
                T.IncrementaQtdMov();
                Tab.ColocarPeca(T, destinoT);
            }
            // #Jogada especial "Roque grande"
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tab.RetirarPeca(origemT);
                T.IncrementaQtdMov();
                Tab.ColocarPeca(T, destinoT);
            }

            // #Jogada especial "En Passant"
            if (p is Peao)
            {
                //A funçao do null aqui eh identificar que houve a movimentação do peao
                //porem nao houve a captura, ja q o En Passant eh uma excecao a movimentacao
                //padrao do peao
                if (origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if (p.Cor == Cor.Brancas)
                    {
                        posP = new Posicao(destino.Linha + 1, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.Linha - 1, destino.Coluna);
                    }
                    pecaCapturada = Tab.RetirarPeca(posP);
                    Capturada.Add(pecaCapturada);
                }
            }

            return pecaCapturada;
        }

        public void DesfazOMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tab.RetirarPeca(destino);
            p.DecrementaQtdMov();

            if (pecaCapturada != null)
            {
                Tab.ColocarPeca(pecaCapturada, destino);
                Capturada.Remove(pecaCapturada);
            }

            Tab.ColocarPeca(p, origem);

            // #Jogada especial "Roque grande"
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tab.RetirarPeca(destinoT);
                T.DecrementaQtdMov();
                Tab.ColocarPeca(T, origemT);
            }

            // #Jogada especial "Roque grande"
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tab.RetirarPeca(destinoT);
                T.DecrementaQtdMov();
                Tab.ColocarPeca(T, origemT);
            }

            // #Jogada especial "En Passant"
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Peca peao = Tab.RetirarPeca(destino);
                    Posicao posP;
                    if (p.Cor == Cor.Brancas)
                    {
                        posP = new Posicao(3, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, destino.Coluna);
                    }
                    Tab.ColocarPeca(peao, posP);
                }
            }
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);

            if (EstaEmXeque(JogadorAtual))
            {
                DesfazOMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroExceptions("Você não pode se por em xeque!");
            }

            Peca p = Tab.PosicaoPeca(destino);

            // #Joga especial "Promocao"
            if (p is Peao)
            {
                if (p.Cor == Cor.Brancas && destino.Linha == 0 || 
                    p.Cor == Cor.Pretas && destino.Linha == 7)
                {
                    p.Tabuleiro.RetirarPeca(destino);
                    Peca.Remove(p);
                    Peca dama = new Dama(Tab, p.Cor);
                    Peca.Add(dama);
                }
            }


            if (EstaEmXeque(Adversaria(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }
            if (XequeMate(Adversaria(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                MudaJogador();
            }

            // #Jogada especial "En Pasant"            
            if (p is Peao && destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2)
            {
                vulneravelEnPassant = p;
            }
            else
            {
                vulneravelEnPassant = null;
            }
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

        private Cor Adversaria(Cor cor)
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
            foreach (Peca peca in PecasEmJogo(cor))
            {
                if (peca is Rei)
                {
                    return peca;
                }
            }

            return null;
        }

        public bool EstaEmXeque(Cor cor)
        {
            Peca R = Rei(cor);

            if (R == null)
            {
                throw new TabuleiroExceptions("Cadê o baia... digo, o Rei?");
            }

            foreach (Peca peca in PecasEmJogo(Adversaria(cor)))
            {
                bool[,] mat = peca.MovimentosPosiveis();
                if (mat[R.Posicao.Linha, R.Posicao.Coluna])
                {
                    return true;
                }
            }

            return false;
        }

        public bool XequeMate(Cor cor)
        {
            if (!EstaEmXeque(cor))
            {
                return false;
            }

            foreach (Peca peca in PecasEmJogo(cor))
            {
                bool[,] mat = peca.MovimentosPosiveis();
                for (int i = 0; i < Tab.Linhas; i++)
                {
                    for (int j = 0; j < Tab.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = peca.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutaMovimento(peca.Posicao, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazOMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
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
            if (!Tab.PosicaoPeca(origem).MovimentoPosivel(destino))
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
            ColocarNovaPeca('a', 1, new Torre(Tab, Cor.Brancas));
            ColocarNovaPeca('b', 1, new Cavalo(Tab, Cor.Brancas));
            ColocarNovaPeca('c', 1, new Bispo(Tab, Cor.Brancas));
            ColocarNovaPeca('d', 1, new Dama(Tab, Cor.Brancas));
            ColocarNovaPeca('e', 1, new Rei(Tab, Cor.Brancas, this));
            ColocarNovaPeca('f', 1, new Bispo(Tab, Cor.Brancas));
            ColocarNovaPeca('g', 1, new Cavalo(Tab, Cor.Brancas));
            ColocarNovaPeca('h', 1, new Torre(Tab, Cor.Brancas));
            ColocarNovaPeca('a', 2, new Peao(Tab, Cor.Brancas, this));
            ColocarNovaPeca('b', 2, new Peao(Tab, Cor.Brancas, this));
            ColocarNovaPeca('c', 2, new Peao(Tab, Cor.Brancas, this));
            ColocarNovaPeca('d', 2, new Peao(Tab, Cor.Brancas, this));
            ColocarNovaPeca('e', 2, new Peao(Tab, Cor.Brancas, this));
            ColocarNovaPeca('f', 2, new Peao(Tab, Cor.Brancas, this));
            ColocarNovaPeca('g', 2, new Peao(Tab, Cor.Brancas, this));
            ColocarNovaPeca('h', 2, new Peao(Tab, Cor.Brancas, this));

            ColocarNovaPeca('a', 8, new Torre(Tab, Cor.Pretas));
            ColocarNovaPeca('b', 8, new Cavalo(Tab, Cor.Pretas));
            ColocarNovaPeca('c', 8, new Bispo(Tab, Cor.Pretas));
            ColocarNovaPeca('d', 8, new Dama(Tab, Cor.Pretas));
            ColocarNovaPeca('e', 8, new Rei(Tab, Cor.Pretas, this));
            ColocarNovaPeca('f', 8, new Bispo(Tab, Cor.Pretas));
            ColocarNovaPeca('g', 8, new Cavalo(Tab, Cor.Pretas));
            ColocarNovaPeca('h', 8, new Torre(Tab, Cor.Pretas));
            ColocarNovaPeca('a', 7, new Peao(Tab, Cor.Pretas, this));
            ColocarNovaPeca('b', 7, new Peao(Tab, Cor.Pretas, this));
            ColocarNovaPeca('c', 7, new Peao(Tab, Cor.Pretas, this));
            ColocarNovaPeca('d', 7, new Peao(Tab, Cor.Pretas, this));
            ColocarNovaPeca('e', 7, new Peao(Tab, Cor.Pretas, this));
            ColocarNovaPeca('f', 7, new Peao(Tab, Cor.Pretas, this));
            ColocarNovaPeca('g', 7, new Peao(Tab, Cor.Pretas, this));
            ColocarNovaPeca('h', 7, new Peao(Tab, Cor.Pretas, this));
        }
    }
}
