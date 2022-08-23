using tabuleiro;
using Xadrez_Console;
using xadrez;
using tabuleiro.exceptions;

Tabuleiro tab = new Tabuleiro(8, 8);

Posicao pos = new Posicao(7, 0);
Posicao pos2 = new Posicao(0, 0);

tab.ColocarPeca(new Rei(tabuleiro.enums.Cor.Branca, tab), pos);
tab.ColocarPeca(new Rei(tabuleiro.enums.Cor.Preta, tab), pos2);

Tela.ImprimirTabuleiro(tab);