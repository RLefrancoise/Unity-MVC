namespace Mvc.Screens.Tests
{
    /// <inheritdoc cref="IScreen" />
    /// <summary>
    /// Stub for IScreen interface. Used for unit testing
    /// </summary>
    public class ScreenStub : IScreen
    {
        /// <summary>
        /// Is created ?
        /// </summary>
        public bool Created { get; private set; }

        /// <summary>
        /// Is Destroyed ?
        /// </summary>
        public bool Destroyed { get; private set; }

        /// <summary>
        /// Is Visible ?
        /// </summary>
        public bool IsVisible { get; private set; }

        public void OnCreate(object data = null)
        {
            Created = true;
            Destroyed = false;
            IsVisible = false;
        }

        public void OnDestroyed()
        {
            Created = false;
            Destroyed = true;
            IsVisible = false;
        }

        public void OnShow()
        {
            IsVisible = true;
        }

        public void OnHide()
        {
            IsVisible = false;
        }
    }
}
