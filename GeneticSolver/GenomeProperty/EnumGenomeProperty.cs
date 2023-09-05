using System;
using System.Collections.Generic;
using System.Linq;
using GeneticSolver.RequiredInterfaces;

namespace GeneticSolver.GenomeProperty
{
    public class EnumGenomeProperty<T, TEnum> : IGenomeProperty<T> where TEnum : struct, Enum
    {
        private readonly System.Random _random;

        private readonly Func<T, TEnum> _getterFunc;
        private readonly Action<T, TEnum> _setterAction;
        private readonly TEnum[] _enumValues;

        public EnumGenomeProperty(Func<T, TEnum> getterFunc, Action<T, TEnum> setterAction, System.Random random)
        {
            _getterFunc = getterFunc;
            _setterAction = setterAction;
            _enumValues = Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToArray();
            _random = random;
        }

        public void Mutate(T genome)
        {
            SetRandom(genome);
        }

        public void SetRandom(T genome)
        {
            _setterAction(genome, _enumValues[_random.Next(0, _enumValues.Length)]);
        }

        public void Merge(T parent1, T parent2, T child)
        {
            TEnum parent1Value = _getterFunc(parent1);
            TEnum parent2Value = _getterFunc(parent2);

            if (!EqualityComparer<TEnum>.Default.Equals(parent1Value, parent2Value))
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