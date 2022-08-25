using tabuleiro;
using Xadrez_Console;
using xadrez;
using tabuleiro.exceptions;

PartidaDeXadrez partida = new PartidaDeXadrez();

while (!partida.Terminada)
{
    try
    {
        Console.Clear();
        Tela.ImprimirPartida(partida);    

        Console.WriteLine();
        Console.Write("Origem: ");
        Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
        partida.ValidarPosicaoDeOrigem(origem);


        bool[,] posicoesPossiveis = partida.Tab.PosicaoPeca(origem).MovimentosPosiveis();

        Console.Clear();
        Tela.ImprimirTabuleiro(partida.Tab, posicoesPossiveis);

        Console.WriteLine();
        Console.Write("Destino: ");
        Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();
        partida.ValidarPosicaoDestino(origem, destino);

        partida.RealizaJogada(origem, destino);        
    }
    catch (TabuleiroExceptions e)
    {
        Console.WriteLine(e.Message);
        Console.ReadLine();
    }
}