using System.Windows;

namespace HeBianGu.Control.PropertyGrid.Design
{
  /// <summary>
  /// The default alphabetical view for properties.
  /// </summary>
  public class AlphabeticalLayout : System.Windows.Controls.Control
  {
    static AlphabeticalLayout()
    {      
      DefaultStyleKeyProperty.OverrideMetadata(typeof(AlphabeticalLayout), new FrameworkPropertyMetadata(typeof(AlphabeticalLayout)));
    }
  }
}
