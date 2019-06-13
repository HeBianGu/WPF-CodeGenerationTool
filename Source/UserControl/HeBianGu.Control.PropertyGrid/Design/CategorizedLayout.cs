using System.Windows;

namespace HeBianGu.Control.PropertyGrid.Design
{
  /// <summary>
  /// The default categorized view for properties.
  /// </summary>
  public class CategorizedLayout : System.Windows.Controls.Control
  {
    static CategorizedLayout()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(CategorizedLayout), new FrameworkPropertyMetadata(typeof(CategorizedLayout)));
    }
  }
}
