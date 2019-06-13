using System.Windows.Controls;
using System.Windows.Input;

namespace HeBianGu.Control.PropertyGrid.Design
{
  /// <summary>
  /// Specifies a property name presenter.
  /// </summary>
  public sealed class PropertyNameTextBlock : TextBlock
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="PropertyNameTextBlock"/> class.
    /// </summary>
    public PropertyNameTextBlock()
    {
     TextTrimming = System.Windows.TextTrimming.CharacterEllipsis;
     TextWrapping = System.Windows.TextWrapping.NoWrap;
     TextAlignment = System.Windows.TextAlignment.Right;
     VerticalAlignment = System.Windows.VerticalAlignment.Center;
     ClipToBounds = true;
      
      KeyboardNavigation.SetIsTabStop(this, false);
    }
  }
}
