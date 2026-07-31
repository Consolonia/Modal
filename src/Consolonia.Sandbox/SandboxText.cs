namespace Consolonia.Sandbox
{
    /// <summary>
    ///     Single source of truth for the user-facing text of the sandbox application.
    ///     These constants are consumed both by the XAML (via <c>{x:Static}</c>) and by the
    ///     end-to-end tests, so the same literals are never duplicated across the code base.
    /// </summary>
    public static class SandboxText
    {
        // Main window.
        public const string MainWindowTitle = "Sandbox Modal Demo";
        public const string BackgroundWelcome = "Welcome to Consolonia Modal Sandbox!";
        public const string ShowModalButton = "Show Modal";
        public const string ShowModal2Button = "Show Modal 2";

        /// <summary>
        ///     First (visible) line of the description paragraph. It is kept as its own constant so
        ///     tests can use it as a "background is visible" indicator without depending on wrapping.
        /// </summary>
        public const string DescriptionFirstLine =
            "Consolonia.Modal provides a mechanism for displaying modal dialogs in Consolonia";

        /// <summary>Full description paragraph shown on the main window.</summary>
        public const string Description =
            "\n" +
            DescriptionFirstLine + " applications.\n" +
            "Features:\n" +
            "- Modal Overlay: Shades the background to prevent interaction.\n" +
            "- Integration: Use ModalTheme to enable modal support for all windows.\n" +
            "- Customization: Control title, icon, close button visibility.\n" +
            "- Keyboard: Supports Escape key for closing (CancelOnEscape).\n" +
            "- Layout: Position modals with HorizontalAlignment and VerticalAlignment.\n" +
            "- Themes: Supports Modern and TurboVision themes.\n" +
            "\n" +
            "Detailed Layout:\n" +
            "- First Child: Its size determines the modal box size.\n" +
            "- Stretch: First child must have HorizontalAlignment/VerticalAlignment as Stretch.\n" +
            "- Positioning: Set Margin, Alignment, or size on ModalWindow to position the frame.\n" +
            "\n" +
            "Mechanics:\n" +
            "- ModalHost: Every Window gets a ModalHost when ModalTheme is active.\n" +
            "- ShowModalAsync: Use this method from code-behind to display the modal.\n";

        // Text shared by both modal dialogs.
        public const string ModalTitle = "Sample Modal";
        public const string Modal2Title = "Sample Modal 2";
        public const string ModalIcon = "ⓘ";
        public const string ModalCommonText = "This is a modal dialog in Consolonia!";
        public const string ModalCloseButton = "Close";

        // Text unique to each modal dialog (also proves the size behaviour).
        public const string SmallModalText = "Size is determined by this StackPanel.";
        public const string FullScreenModalText = "Size is just full screen with padding";
    }
}
