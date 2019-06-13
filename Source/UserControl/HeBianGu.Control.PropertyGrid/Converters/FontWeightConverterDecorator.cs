﻿using System.ComponentModel;
using System.Windows;

namespace HeBianGu.Control.PropertyGrid
{
  /// <summary>
  /// Extended <see cref="FontWeightConverter"/> that provides standard values collection.
  /// </summary>
  public class FontWeightConverterDecorator : FontConverterDecorator
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="FontWeightConverterDecorator"/> class.
    /// </summary>
    public FontWeightConverterDecorator() : base(new FontWeightConverter()) { }

    /// <summary>
    /// Returns a collection of standard values for the data type this type converter is designed for when provided with a format context.
    /// </summary>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> that provides a format context that can be used to extract additional information about the environment from which this converter is invoked. This parameter or properties of this parameter can be null.</param>
    /// <returns>
    /// A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection"/> that holds a standard set of valid values, or null if the data type does not support a standard set of values.
    /// </returns>
    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
    {
      return new StandardValuesCollection(
        new[] 
        { 
          FontWeights.Thin, 
          FontWeights.ExtraLight, 
          FontWeights.Light, 
          FontWeights.Normal, 
          FontWeights.Medium, 
          FontWeights.SemiBold, 
          FontWeights.Bold, 
          FontWeights.ExtraBold, 
          FontWeights.Black, 
          FontWeights.ExtraBlack });
    }
  }
}
