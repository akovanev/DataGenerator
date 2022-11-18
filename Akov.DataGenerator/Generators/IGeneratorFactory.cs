﻿namespace Akov.DataGenerator.Generators
{
    public interface IGeneratorFactory
    {
        public GeneratorBase Get(string type);
        internal AssignGeneratorBase GetAssign(string id);
    }
}