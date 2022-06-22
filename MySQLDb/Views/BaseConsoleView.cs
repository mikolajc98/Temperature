using MySQLDb.Extensions;

namespace MySQLDb.Views
{
    public abstract class BaseConsoleView
    {
        protected BaseConsoleView _previousView;

        protected delegate void BaseConsoleViewEvent();

        protected BaseConsoleViewEvent OnOpened;

        public BaseConsoleView(BaseConsoleView previousView)
        {
            _previousView = previousView;
            OnOpened += SetConsoleContext;
        }

        public virtual void Open()
        {            
            OnOpened?.Invoke();
            //Console.Clear();
        }

        public void GoBack()
        {
            if (_previousView != null)
                _previousView.Open();
        }

        private void SetConsoleContext()
        {
            ConsoleExtensions.SetContext(this);
        }
    }
}
