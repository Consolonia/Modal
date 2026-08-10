using Avalonia.Input;
using Consolonia.NUnit;
using NUnit.Framework;

namespace Consolonia.Sandbox.Tests
{
    [TestFixture]
    internal class ModalTests : SandboxTestsBase
    {
        [Test]
        public async Task FirstModal_ContentSized_OpensClosesAndRestoresFocus()
        {
            await UITest.AssertHasText(SandboxText.BackgroundWelcome, SandboxText.ShowModalButton,
                SandboxText.ShowModal2Button);

            // Open the first modal by activating the focused "Show Modal" button.
            await UITest.KeyInput(Key.Tab);
            await UITest.KeyInput(Key.Enter);

            await UITest.AssertHasText(SandboxText.ModalTitle, SandboxText.ModalIcon, SandboxText.ModalCommonText,
                SandboxText.SmallModalText, SandboxText.ModalCloseButton);

            // Size check: this modal is only as big as its content, so the background text
            // behind it (the centered welcome line and the full first paragraph line) stays visible.
            await UITest.AssertHasText(SandboxText.BackgroundWelcome, SandboxText.DescriptionFirstLine);
            // ...and it is NOT the full-screen modal.
            await UITest.AssertHasNoText(SandboxText.FullScreenModalText);

            // Close the modal.
            await UITest.KeyInput(Key.Tab, Key.Enter);
            await UITest.AssertHasNoText(SandboxText.ModalTitle, SandboxText.SmallModalText);

            // Focus must have been restored to the same "Show Modal" button.
            await UITest.KeyInput(Key.Enter);
            await UITest.AssertHasText(SandboxText.ModalTitle, SandboxText.ModalIcon, SandboxText.ModalCommonText,
                SandboxText.SmallModalText, SandboxText.ModalCloseButton);

            // Close the modal.
            await UITest.KeyInput(Key.Tab, Key.Enter);
            await UITest.AssertHasNoText(SandboxText.ModalTitle, SandboxText.SmallModalText);

            await SecondModal_FullScreen_OpensClosesAndRestoresFocus();
        }

        private async Task SecondModal_FullScreen_OpensClosesAndRestoresFocus()
        {
            // Open the second modal by activating the focused "Show Modal 2" button.
            await UITest.KeyInput(Key.Tab);
            await UITest.KeyInput(Key.Enter);

            await UITest.AssertHasText(SandboxText.Modal2Title, SandboxText.ModalIcon, SandboxText.ModalCommonText,
                SandboxText.FullScreenModalText, SandboxText.ModalCloseButton);

            // Size check: this modal is full screen (with padding), so it covers the centered
            // background text and breaks the background paragraph line - neither is visible now.
            await UITest.AssertHasNoText(SandboxText.BackgroundWelcome, SandboxText.DescriptionFirstLine);
            // ...and it is NOT the small content-sized modal.
            await UITest.AssertHasNoText(SandboxText.SmallModalText);
            // checking exactly rendering
            await UITest.AssertHasText(
                "Conso▕ " + SandboxText.FullScreenModalText + "                              ▏lonia");

            // Close the modal using ESC.
            await UITest.KeyInput(Key.Tab);
            await UITest.KeyInput(Key.Escape);
            await UITest.AssertHasNoText(SandboxText.Modal2Title, SandboxText.FullScreenModalText);

            // The background text is visible again once the full-screen modal is gone.
            await UITest.AssertHasText(SandboxText.BackgroundWelcome, SandboxText.DescriptionFirstLine);

            // Focus must have been restored to the same "Show Modal 2" button.
            // Opening the focused button one more time must show the very same dialog.
            await UITest.KeyInput(Key.Enter);
            await UITest.AssertHasText(SandboxText.Modal2Title, SandboxText.FullScreenModalText);
            await UITest.AssertHasNoText(SandboxText.SmallModalText);
        }
    }
}