﻿using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace HeBianGu.Control.PropertyGrid
{  
  /// <summary>
  /// Default enum converter.
  /// </summary>
  // See more details here http://www.codeproject.com/KB/WPF/FriendlyEnums.aspx
  [ValueConversion(typeof(object), typeof(string))]
  public class EnumValueConverter : IValueConverter
  {
    /// <summary>
    /// Default string presenting a null value.
    /// </summary>
    public const string NullValueString = "(null)";

    #region IValueConverter Members

    /// <summary>
    /// Converts a value.
    /// </summary>
    /// <param name="value">The value produced by the binding source.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>
    /// A converted value. If the method returns null, the valid null value is used.
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value is Enum)
      {
        var fi = value.GetType().GetField(value.ToString());
        var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
        return ((attributes.Length > 0) && (!String.IsNullOrEmpty(attributes[0].Description))) ? attributes[0].Description : value.ToString();
      }
      // return (null) in case enum value is not assigned...       
      // this is mainly for dependency properties as DP property may contain undefined enum
      // native CLR properties will always assign it according to the first value
      if (value == null)
      {
        return NullValueString;
      }
      else
      {
        return value.ToString();
      }
    }

    /// <summary>
    /// Converts a value.
    /// </summary>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="targetType">The type to convert to.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>
    /// A converted value. If the method returns null, the valid null value is used.
    /// </returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotSupportedException();
    }

    #endregion
  }
}
