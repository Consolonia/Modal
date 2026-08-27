using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Primitives.PopupPositioning;
using Avalonia.Controls.Templates;
using Avalonia.Input;
using Avalonia.VisualTree;
using Consolonia.Controls;

namespace Consolonia.Modal
{
    public class ModalHost
    {
        public static readonly AttachedProperty<bool> IsModalHostProperty =
            AvaloniaProperty.RegisterAttached<Control, bool>("IsModalHost", typeof(ModalHost));

        internal static readonly AttachedProperty<ModalHost> ModalHostProperty =
            AvaloniaProperty.RegisterAttached<Control, ModalHost>("ModalHost", typeof(ModalHost));

        private readonly Stack<Popup> _modals = new();

        private readonly Window _window;

        static ModalHost()
        {
            IsModalHostProperty.Changed.SubscribeAction(args =>
            {
                args.Sender.SetValue(ModalHostProperty,
                    args.NewValue.Value ? new ModalHost((Window)args.Sender) : null);
            });
        }

        private ModalHost(Window window)
        {
            _window = window;
        }

        public static bool GetIsModalHost(Control control)
        {
            return control.GetValue(IsModalHostProperty);
        }

        public static void SetIsModalHost(Control control, bool value)
        {
            control.SetValue(IsModalHostProperty, value);
        }

        public void OpenInternal(ModalWindow modalWindow)
        {
            IInputElement focusedElement = _window.FocusManager! /*todo: low: Why can be null?*/.GetFocusedElement();
            var overlayLayer = OverlayLayer.GetOverlayLayer(_window);

            var popup = new Popup
            {
                PlacementGravity = PopupGravity.BottomRight,
                PlacementAnchor = PopupAnchor.TopLeft,
                Placement = PlacementMode.AnchorAndGravity,
                ShouldUseOverlayLayer = true
            };

            overlayLayer.Children.Add(popup);


            var modalWrap = new ModalWrap();
            modalWrap.SetContent(modalWindow);
            popup.Child = modalWrap;
            GetFirstContentPresenter().IsEnabled = false;

            if (_modals.TryPeek(out Popup previousModal)) previousModal.IsEnabled = false;

            modalWrap.HadFocusOn = focusedElement;

            _modals.Push(popup);

            popup.Open();

            modalWindow.AttachedToVisualTree += ModalAttachedToVisualTree;
            return;

            static void ModalAttachedToVisualTree(object sender, EventArgs e)
            {
                var modalWindow = (ModalWindow)sender!;
                modalWindow.AttachedToVisualTree -= ModalAttachedToVisualTree;
                modalWindow.Focus();
            }
        }

        private ContentPresenter GetFirstContentPresenter()
        {
            ContentPresenter firstContentPresenter = _window.GetTemplateDescendants()
                .Select(control => control.FindDescendantOfType<ContentPresenter>())
                .First(d => d!.Name == "PART_ContentPresenter");
            return firstContentPresenter;
        }

        public void PopInternal(ModalWindow modalWindow)
        {
            Popup overlayPopupHost = _modals.Pop();
            var modalWrap = (ModalWrap)overlayPopupHost.Child!;
            if (!Equals(modalWrap!.FoundContentPresenter.Content, modalWindow!))
                throw new InvalidOperationException("Modal is not topmost. Close private modals first");
            overlayPopupHost.Close();

            if (_modals.TryPeek(out Popup previousModal))
            {
                previousModal.IsEnabled = true;
                previousModal.Focus();
            }

            if (_modals.Count == 0)
            {
                ContentPresenter firstContentPresenter = GetFirstContentPresenter();
                firstContentPresenter.IsEnabled = true;
                firstContentPresenter.Focus();
            }

            modalWrap.HadFocusOn?.Focus();
        }
    }
}