﻿namespace tabuleiro
{
    internal class Peca
    {
        public Posicao Posicao { get; private set; }
        public Cor Cor { get; protected set; }
        public int QtdMovimentos { get; protected set; }
        public Tabuleiro Tabuleiro { get; set; }

        public Peca(Posicao posicao, Cor cor, Tabuleiro tabuleiro)
        {
            Posicao = posicao;
            Cor = cor;
            Tabuleiro = tabuleiro;
            QtdMovimentos = 0;
        }
    }
}
