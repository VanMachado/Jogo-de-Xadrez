using tabuleiro.enums;

namespace tabuleiro
{
    internal abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QtdMovimentos { get; protected set; }
        public Tabuleiro Tabuleiro { get; set; }

        public Peca(Cor cor, Tabuleiro tabuleiro)
        {
            Posicao = null;
            Cor = cor;
            Tabuleiro = tabuleiro;
            QtdMovimentos = 0;
        }

        public bool ExisteMovimentosPossiveis()
        {
            bool[,] mat = MovimentosPosiveis();
            for(int i = 0; i < Tabuleiro.Linhas; i++)
            {
                for(int j = 0; j < Tabuleiro.Colunas; j++)
                {
                    if (mat[i,j])
                    {
                        return true; 
                    }
                }
            }
            return false;
        }

        public bool MovimentoPosivel(Posicao pos)
        {                 
            return MovimentosPosiveis()[pos.Linha, pos.Coluna];
        }

        public abstract bool[,] MovimentosPosiveis();

        public void IncrementaQtdMov()
        {
            QtdMovimentos++;
        }

        public void DecrementaQtdMov()
        {
            QtdMovimentos--;
        }

    }
}
