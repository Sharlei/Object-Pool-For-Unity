// https://frarees.github.io/default-gist-license

using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public class MinMaxSliderAttribute : PropertyAttribute
{
    public readonly int min;
    public readonly int max;

    public MinMaxSliderAttribute() : this(0, 1) {}

    public MinMaxSliderAttribute(int min, int max)
    {
        this.min = min;
        this.max = max;
    }
}