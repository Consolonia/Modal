using System.Diagnostics;
using Avalonia.Input;
using Consolonia.NUnit;
using NUnit.Framework;

namespace Consolonia.Sandbox.Tests
{
    [TestFixture]
    internal class ModalTests : SandboxTestsBase
    {
        private const string ShowModalButton = "Show Modal";
        private const string ShowModal2Button = "Show Modal 2";

        // Text rendered on the background (main) window.
        private const string BackgroundWelcome = "Welcome to Consolonia Modal Sandbox!";

        private const string BackgroundParagraph =
            "Consolonia.Modal provides a mechanism for displaying modal dialogs in Consolonia";

        // Text shared by both modal dialogs.
        private const string ModalIcon = "ⓘ";
        private const string ModalCommonText = "This is a modal dialog in Consolonia!";
        private const string ModalCloseButton = "Close";

        // Text unique to each modal dialog (also used to prove size behaviour).
        private const string SmallModalText = "Size is determined by this StackPanel.";
        private const string FullScreenModalText = "Size is just full screen with padding";

        [Test]
        public async Task FirstModal_ContentSized_OpensClosesAndRestoresFocus()
        {
            // The background window is shown with its description and both buttons.
            await UITest.AssertHasText(BackgroundWelcome, ShowModalButton, ShowModal2Button);

            // Open the first modal by activating the focused "Show Modal" button.
            await UITest.KeyInput(Key.Tab);
            await UITest.KeyInput(Key.Enter);

            await UITest.WaitRendered();
            
            // The modal is shown with its title, icon and its own content.
            await UITest.AssertHasText("Sample Modal", ModalIcon, ModalCommonText, SmallModalText, ModalCloseButton);

            // Size check: this modal is only as big as its content, so the background text
            // behind it (the centered welcome line and the full first paragraph line) stays visible.
            await UITest.AssertHasText(BackgroundWelcome, BackgroundParagraph);
            // ...and it is NOT the full-screen modal.
            await UITest.AssertHasNoText(FullScreenModalText);

            // Close the modal.
            await UITest.KeyInput(Key.Tab, Key.Enter);
            await UITest.AssertHasNoText("Sample Modal", SmallModalText);

            // Focus must have been restored to the same "Show Modal" button.
            UITest.KeyInput(Key.Enter);
            await UITest.WaitRendered();
            
            await UITest.AssertHasText("Sample Modal", ModalIcon, ModalCommonText, SmallModalText, ModalCloseButton);
            
            // Close the modal.
            await UITest.KeyInput(Key.Tab, Key.Enter);
            await UITest.AssertHasNoText("Sample Modal", SmallModalText);

            await SecondModal_FullScreen_OpensClosesAndRestoresFocus();
        }

        [Ignore("Depends on the first test")]
        public async Task SecondModal_FullScreen_OpensClosesAndRestoresFocus()
        {
            // Open the second modal by activating the focused "Show Modal 2" button.
            await UITest.KeyInput(Key.Tab);
            await UITest.KeyInput(Key.Enter);

            // The modal is shown with its title, icon and its own content.
            await UITest.AssertHasText("Sample Modal 2", ModalIcon, ModalCommonText, FullScreenModalText, ModalCloseButton);

            // Size check: this modal is full screen (with padding), so it covers the centered
            // background text and breaks the background paragraph line - neither is visible now.
            await UITest.AssertHasNoText(BackgroundWelcome, BackgroundParagraph);
            // ...and it is NOT the small content-sized modal.
            await UITest.AssertHasNoText(SmallModalText);
            // checking exacty rendering
            await UITest.AssertHasText(
                "Conso▕ Size is just full screen with padding                              ▏lonia");

            // Close the modal.
            await UITest.KeyInput(Key.Tab, Key.Enter);
            await UITest.AssertHasNoText("Sample Modal 2", FullScreenModalText);

            // The background text is visible again once the full-screen modal is gone.
            await UITest.AssertHasText(BackgroundWelcome, BackgroundParagraph);

            // Focus must have been restored to the same "Show Modal 2" button.
            // Opening the focused button one more time must show the very same dialog.
            await UITest.KeyInput(Key.Enter);
            await UITest.AssertHasText("Sample Modal 2", FullScreenModalText);
            await UITest.AssertHasNoText(SmallModalText);
        }
    }
}
