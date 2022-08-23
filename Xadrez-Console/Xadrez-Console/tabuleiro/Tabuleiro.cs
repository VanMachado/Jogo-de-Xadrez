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

        public void ColocarPeca(Peca peca, Posicao pos)
        {
            Pecas[pos.Linha, pos.Coluna] = peca;
            peca.Posicao = pos;
        }
    }
}
