using PKHeX.Core;

namespace ItemPrinterDeGacha.WinForms;

internal static class WinFormsUtil
{
    public static void InitializeBinding(this ListControl control)
    {
        control.DisplayMember = nameof(ComboItem.Text);
        control.ValueMember = nameof(ComboItem.Value);
    }

    /// <summary>
    /// Gets the selected value of the input <see cref="cb"/>. If no value is selected, will return 0.
    /// </summary>
    /// <param name="cb">ComboBox to retrieve value for.</param>
    internal static int GetIndex(ListControl cb) => (int)(cb.SelectedValue ?? 0);
}
