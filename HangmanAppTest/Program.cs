using System;
using System.Collections.Generic;
using System.Threading;

namespace HangmanAppTest
{
	internal class Program
	{
		private static void printHangman(int wrong)
		{
			if (wrong == 0)
			{
				Console.WriteLine("\n+---+");
				Console.WriteLine("    |");
				Console.WriteLine("    |");
				Console.WriteLine("    |");
				Console.WriteLine("   ===");
			}
			else if (wrong == 1)
			{
				Console.WriteLine("\n+---+");
				Console.WriteLine("O   |");
				Console.WriteLine("    |");
				Console.WriteLine("    |");
				Console.WriteLine("   ===");
			}
			else if (wrong == 2)
			{
				Console.WriteLine("\n+---+");
				Console.WriteLine("O   |");
				Console.WriteLine("|   |");
				Console.WriteLine("    |");
				Console.WriteLine("   ===");
			}
			else if (wrong == 3)
			{
				Console.WriteLine("\n+---+");
				Console.WriteLine(" O  |");
				Console.WriteLine("/|  |");
				Console.WriteLine("    |");
				Console.WriteLine("   ===");
			}
			else if (wrong == 4)
			{
				Console.WriteLine("\n+---+");
				Console.WriteLine(" O  |");
				Console.WriteLine("/|\\ |");
				Console.WriteLine("    |");
				Console.WriteLine("   ===");
			}
			else if (wrong == 5)
			{
				Console.WriteLine("\n+---+");
				Console.WriteLine(" O  |");
				Console.WriteLine("/|\\ |");
				Console.WriteLine("/   |");
				Console.WriteLine("   ===");
			}
			else if (wrong == 6)
			{
				Console.WriteLine("\n+---+");
				Console.WriteLine(" O   |");
				Console.WriteLine("/|\\  |");
				Console.WriteLine("/ \\  |");
				Console.WriteLine("    ===");
			}
		}

		

		private static int printWord(List<char> guessedLetters, string randomWord)
		{
			int counter = 0;
			int rightLetters = 0;
			Console.Write("\r\n");
			foreach (char c in randomWord)
			{
				if (guessedLetters.Contains(c))
				{
					Console.Write(c + " ");
					rightLetters += 1;
				}
				else
				{
					Console.Write("  ");
				}
				counter += 1;
			}

			return rightLetters;
		}

		private static void printLines(string randomWord)
		{
			Console.Write("\r");
			foreach (char c in randomWord)
			{
				Console.OutputEncoding = System.Text.Encoding.Unicode;
				Console.Write("\u0305 ");
			}
		}

		static bool PlayGame(int maxErrors, ref int score)

		{
			
			
			Random random = new Random();
			List<string> wordDictionary = new List<string> { "nutrologo", "apicultor", "agronomo", "auditor", "bartender", "cerimonialista","desembargador", "turismologo" };
			int index = random.Next(wordDictionary.Count);
			string randomWord = wordDictionary[index];
			Console.WriteLine("\nTema: Profissões ");
			Console.WriteLine("\nAperte enter para começar o jogo: ");
			Console.ReadLine();

			List<char> correctLetters = new List<char>(randomWord.Distinct()); // Letras corretas na palavra
			List<char> currentLettersGuessed= new List<char>(); // Letras adivinhadas

			foreach (char x in randomWord)
			{
				Console.Write("_ ");
			}

			int lengthOfWordToGuess = randomWord.Length;
			int amountOfTimesWrong = 0;
			int currentLettersRight = 0;

			while (amountOfTimesWrong != 6 && currentLettersRight != lengthOfWordToGuess)
			{
				
				
				
				
				Console.WriteLine("\nLetras adivinhadas corretas: " + string.Join(" ", currentLettersGuessed.Intersect(correctLetters)));
				Console.WriteLine("Letras adivinhadas erradas: " + string.Join(" ", currentLettersGuessed.Except(correctLetters)));
				Console.WriteLine("Número de erros: " + amountOfTimesWrong);
				Console.WriteLine("Tamanho da palavra: " + lengthOfWordToGuess);

				// Pausa antes de pedir ao jogador que adivinhe uma letra
				Thread.Sleep(1000);

				// Prompt user for input
				Console.Write("\nAdivinhe uma letra: ");

				char letterGuessed;
				while (true)
				{
					try
					{
						letterGuessed = char.ToLower(Console.ReadLine()[0]);

						if (!char.IsLower(letterGuessed) || !char.IsLetter(letterGuessed))
						{
							throw new ArgumentException("Por favor, insira uma letra minúscula válida.");
						}

						break; // Se a entrada for válida, saia do loop
					}
					catch (Exception e)
					{
						Console.WriteLine(e.Message);
					}
				}
				
				if (currentLettersGuessed.Contains(letterGuessed))
				{
					Console.Write("\r\n Você já adivinhou essa letra! Tente novamente");
				}

				else
				{
					bool right = false;
					for (int i = 0; i < randomWord.Length; i++)
					{
						if (letterGuessed == randomWord[i])
						{
							right = true;
							currentLettersGuessed.Add(letterGuessed);
						}
					}

					// User is right
					if (right)
					{
						printHangman(amountOfTimesWrong);
						// Print word
						currentLettersRight = printWord(currentLettersGuessed, randomWord);
						Console.Write("\r\n");
						printLines(randomWord);
					}
					// User was wrong
					else
					{
						amountOfTimesWrong += 1;
						currentLettersGuessed.Add(letterGuessed);
						// Update the drawing
						printHangman(amountOfTimesWrong);
						// Print word
						currentLettersRight = printWord(currentLettersGuessed, randomWord);
						Console.Write("\r\n");
						printLines(randomWord);
					}
					if (currentLettersRight == lengthOfWordToGuess)
					{
						// O jogador venceu, retorne true
						return true;
					}
				}
			}

			Console.WriteLine("\r\n O jogo acabou! Obrigado por jogar :)");
			Console.WriteLine(" A palavra era: " + randomWord);
			Console.WriteLine(" Letras adivinhadas corretas: " + string.Join(" ", currentLettersGuessed.Intersect(correctLetters)));
			Console.WriteLine(" Letras adivinhadas erradas: " + string.Join(" ", currentLettersGuessed.Except(correctLetters)));
			Console.WriteLine(" Número de erros: " + amountOfTimesWrong);
			return false;
		}

		static void Main(string[] args)
		{
			int score = 0;
			int maxErrors = 6;

			Console.WriteLine("Bem vindo ao jogo da Forca!");
			Console.WriteLine("------------------------------|+---+");
			Console.WriteLine("| ### ####  ####  ####   #    ||   |");
			Console.WriteLine("| #   #  #  #  #  #     # #   ||   O");
			Console.WriteLine("| ### #  #  ####  #    #####  ||  /|\\");
			Console.WriteLine("| #   #  #  #  #  #    #   #  ||  / \\");
			Console.WriteLine("| #   ####  #   # #### #   #  |===");
			Console.WriteLine("--------------------------------------------\n");
			Thread.Sleep(5000); // Aguardar por 5 segundos

			Console.WriteLine("\n\n\n\t\t Instruções");
			Console.WriteLine("\t\t ===========");
			Console.WriteLine("   1: Forca é o jogo onde você tem que adivinhar a palavra");
			Console.WriteLine("   2: Tente adivinhar o máximo de letras");
			Console.WriteLine("   3: Você tem 6 chances para encontrar a palavra correta \n\n");
			Thread.Sleep(6000); // Aguardar por 6 segundos
			Console.Clear();
			Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\t\t\t\t CARREGANDO ");
			Thread.Sleep(2000);
			Console.Clear();

			while (true)
			{
				bool victory = PlayGame(maxErrors, ref score);

				if (victory)
				{
					Console.WriteLine("Você venceu! Parabéns!");
				}
				else
				{

					Console.WriteLine("\nVocê perdeu! Mais sorte na próxima vez!");
				}

				Console.WriteLine("\nDeseja jogar novamente? (S para Sim, qualquer outra tecla para sair)\n");

				char playAgain = Console.ReadKey().KeyChar;

				if (char.ToUpper(playAgain) != 'S')
				{
					
					break;
				}
				
			}
		}
	}
}


