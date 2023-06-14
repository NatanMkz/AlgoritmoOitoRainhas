using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmoOitoRainhas.src
{
    public class GeneticAlgorithm
    {
        public int[,] populacao;
        public int TamTabuleiro { get; set; }
        public int TamPopulacao { get; set; } = 100;
        public int MaxFitness { get; set; } = 0;
        public int Geracao { get; set; } = 1;

        public GeneticAlgorithm(int tamTabuleiro, int tamPopulacao)
        {
            TamTabuleiro = tamTabuleiro;
            TamPopulacao = tamPopulacao;
            populacao = new int[tamPopulacao, tamTabuleiro];

            int count = tamTabuleiro - 1;

            while (count > 0)
            {
                MaxFitness += count;
                count--;
            }
        }

        public int ResolveRainhas()
        {
            int index = -1;
            bool found;

            this.CriaPopulacao();

            do
            {
                List<int> fitnessList = new List<int>();

                for (int i = 0; i < TamPopulacao; i++)
                {
                    var fitness = this.CalcFitness(i);
                    fitnessList.Add(fitness);
                }

                found = fitnessList.Contains(MaxFitness);

                if (found)
                {
                    index = fitnessList.IndexOf(MaxFitness);
                    break;
                }

                this.GerarNovaPopulacao(fitnessList);
                Geracao++;
            } while (!found);
            //Usar esse retorno
            return index;
        }

        public void GerarNovaPopulacao(List<int> fitnessList)
        {
            int[,] novaPopulacao = new int[TamPopulacao, TamTabuleiro];
            List<List<int>> fragas = new List<List<int>>();

            var rand = new System.Random();
            bool populacaoCheia = false;
            int somaFitness = fitnessList.Sum();
            int swap;
            int with;
            int temp;

            while (!populacaoCheia)
            {
                int linhaCorte = rand.Next(1, TamTabuleiro - 1);
                int pai1 = this.SelecionaPais(fitnessList, somaFitness);
                int pai2;

                do
                {
                    pai2 = this.SelecionaPais(fitnessList, somaFitness);
                } while (pai1 == pai2);

                List<int> filho1 = new List<int>();
                List<int> filho2 = new List<int>();

                for (int j = 0; j < linhaCorte; j++)
                {
                    filho1.Add(populacao[pai1, j]);
                    filho2.Add(populacao[pai2, j]);
                }

                for (int j = linhaCorte; j < TamTabuleiro; j++)
                {
                    if (!filho1.Contains(populacao[pai2, j]))
                    {
                        filho1.Add(populacao[pai2, j]);
                    }
                    else
                    {
                        for (int k = 0; k < TamTabuleiro; k++)
                        {
                            if (!filho1.Contains(populacao[pai2, k]))
                            {
                                filho1.Add(populacao[pai2, k]);
                                break;
                            }
                        }
                    }

                    if (!filho2.Contains(populacao[pai1, j]))
                    {
                        filho2.Add(populacao[pai1, j]);
                    }
                    else
                    {
                        for (int k = 0; k < TamTabuleiro; k++)
                        {
                            if (!filho2.Contains(populacao[pai1, k]))
                            {
                                filho2.Add(populacao[pai1, k]);
                                break;
                            }
                        }
                    }
                }

                swap = rand.Next(0, TamTabuleiro);

                do
                {
                    with = rand.Next(0, TamTabuleiro);
                } while (swap == with);

                temp = filho1.ElementAt(swap);
                filho1[swap] = filho1.ElementAt(with);
                filho1[with] = temp;

                fragas.Add(filho1);

                if (fragas.Count() < TamPopulacao)
                {
                    swap = rand.Next(0, TamTabuleiro);

                    do
                    {
                        with = rand.Next(0, TamTabuleiro);
                    } while (swap == with);

                    temp = filho2.ElementAt(swap);
                    filho2[swap] = filho2.ElementAt(with);
                    filho2[with] = temp;

                    fragas.Add(filho2);
                }

                populacaoCheia = fragas.Count() >= TamPopulacao;
            }



            for (int a = 0; a < TamPopulacao; a++)
            {
                for (int b = 0; b < TamTabuleiro; b++)
                {
                    populacao[a, b] = fragas.ElementAt(a).ElementAt(b);
                }
            }
        }


        public int SelecionaPais(List<int> fitnessList, int somaFitness)
        {
            var rand = new System.Random();
            int valor = rand.Next(0, somaFitness);
            int temp = 0;
            int index = 0;

            foreach (int fit in fitnessList)
            {
                temp += fit;

                if (temp > valor)
                {
                    break;
                }

                index++;
            }

            return index;
        }

        public void CriaPopulacao()
        {
            var rand = new System.Random();

            for (int i = 0; i < TamPopulacao; i++)
            {
                var incluido = new HashSet<int>();

                for (int j = 0; j < TamTabuleiro; j++)
                {
                    var range = Enumerable.Range(1, TamTabuleiro).Where(i => !incluido.Contains(i));
                    int index = rand.Next(0, TamTabuleiro - incluido.Count());
                    populacao[i, j] = range.ElementAt(index);
                    incluido.Add(range.ElementAt(index));
                }
            }
        }

        public int CalcFitness(int index)
        {
            int batidas = 0;

            for (int i = 0; i < TamTabuleiro; i++)
            {
                for (int j = i; j < TamTabuleiro; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
;
                    int dx = Math.Abs(i - j);
                    int dy = Math.Abs(populacao[index, i] - populacao[index, j]);

                    if (dx == dy)
                    {
                        batidas++;
                    }
                }
            }

            return MaxFitness - batidas;
        }

    }
}
