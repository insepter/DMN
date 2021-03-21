namespace Common.Number
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public static class ExGetNumbers
    {
        public static IEnumerable<int> GetNumbers(int lenght) => Enumerable.Range(0, lenght);

        public static void GetNumbersDiv(this IEnumerable<int> victim, int lenght, int denominator, int Integer) => victim = Enumerable.Range(0, lenght).Where(item => item / denominator == Integer);
        public static IEnumerable<int> GetNumbersDiv(int lenght, int denominator, int Integer) => Enumerable.Range(0, lenght).Where(item => item / denominator == Integer);

        public static void GetNumbersMod(this IEnumerable<int> victim, int lenght, int denominator, int Integer) => victim = Enumerable.Range(0, lenght).Where(item => item % denominator == Integer);
        public static IEnumerable<int> GetNumbersMod(int lenght, int denominator, int Integer) => Enumerable.Range(0, lenght).Where(item => item % denominator == Integer);
    }
}
