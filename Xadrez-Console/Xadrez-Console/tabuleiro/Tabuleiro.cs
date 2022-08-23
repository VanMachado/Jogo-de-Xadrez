using tabuleiro.exceptions;

namespace tabuleiro
{
    internal class Tabuleiro
    {
        public int Linhas { get; private set; }
        public int Colunas { get; private set; }
        private Peca[,] Pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            Pecas = new Peca[linhas, colunas];
        }

        public Peca PosicaoPeca(int linha, int coluna)
        {
            return Pecas[linha, coluna];
        }

        public Peca PosicaoPeca(Posicao pos)
        {
            return Pecas[pos.Linha, pos.Coluna];
        }

        public bool ExistePeca(Posicao pos)
        {
            ValidarPosicao(pos);
            return PosicaoPeca(pos) != null;
        }

        public void ColocarPeca(Peca peca, Posicao pos)
        {
            if(ExistePeca(pos))
            {
                throw new TabuleiroExceptions("Já existe uma peça nessa posição!");
            }
            Pecas[pos.Linha, pos.Coluna] = peca;
            peca.Posicao = pos;
        }

        public bool PosicaoValida(Posicao pos)
        {
            if(pos.Linha < 0 || pos.Linha >= Linhas || pos.Coluna < 0 || pos.Coluna >= Colunas)
            {
                return false;
            }

            return true;
        }

        public void ValidarPosicao(Posicao pos)
        {
            if(!PosicaoValida(pos))
            {
                throw new TabuleiroExceptions("Posição invalida!");
            }
        }
    }
}
