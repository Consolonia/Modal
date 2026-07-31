using Consolonia.Core.Drawing.PixelBufferImplementation;
using Consolonia.NUnit;

namespace Consolonia.Sandbox.Tests
{
    /// <summary>
    ///     Base class for Consolonia.Sandbox end-to-end (UI) tests.
    /// </summary>
    internal class SandboxTestsBase : ConsoloniaAppTestBase<App>
    {
        protected SandboxTestsBase() : base(new PixelBufferSize(80, 40))
        {
            Args = [];
        }
    }
}