using System;
using GeneticSolver.RequiredInterfaces;

namespace GeneticSolver.GenomeProperty
{
    public class BooleanGenomeProperty<T> : IGenomeProperty<T>
    {
        private readonly System.Random _random;

        private readonly Func<T, bool> _getterFunc;
        private readonly Action<T, bool> _setterAction;

        public BooleanGenomeProperty(Func<T, bool> getterFunc, Action<T, bool> setterAction, System.Random random)
        {
            _getterFunc = getterFunc;
            _setterAction = setterAction;
            _random = random;
        }

        public void Mutate(T genome)
        {
            _setterAction(genome, !_getterFunc(genome));
        }

        public void SetRandom(T genome)
        {
            _setterAction(genome, _random.Next(0, 1) == 1);
        }

        public void Merge(T parent1, T parent2, T child)
        {
            bool parent1Value = _getterFunc(parent1);
            bool parent2Value = _getterFunc(parent2);

            if (parent1Value != parent2Value)
            {
                SetRandom(child);
            }
            else
            {
                _setterAction(child, parent1Value);
            }
        }
    }
}