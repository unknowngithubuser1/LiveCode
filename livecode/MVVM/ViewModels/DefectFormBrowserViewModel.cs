using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Assisticant;
using Assisticant.Collections;
using Assisticant.Fields;

namespace livecode.wpf.MVVM.ViewModels
{
    public class DefectFormBrowserViewModel
    {
        private Observable<int> _selectedIndex = new Observable<int>();
        private ObservableList<DefectFormViewModel> _items = new ObservableList<DefectFormViewModel>();

        public IEnumerable<DefectFormViewModel> Items => _items;

        public int SelectedIndex => _selectedIndex;
        public string DisplayIndex => $"{_selectedIndex + 1} / {_items.Count}";

        public ICommand GoNext => MakeCommand.When(CanGoNext).Do(DoGoNext);
        public ICommand GoBack => MakeCommand.When(CanGoBack).Do(DoGoBack);
        public ICommand Submit => MakeCommand.Do(DoSubmit);

        private bool CanGoNext() => SelectedIndex < _items.Count - 1;
        private bool CanGoBack() => SelectedIndex > 0;

        private void DoGoNext() => _selectedIndex.Value++;
        private void DoGoBack() => _selectedIndex.Value--;

        private void DoSubmit()
        {
            if (CanGoNext())
            {
                DoGoNext();
                _items.RemoveAt(_selectedIndex - 1);
            }
            else if (CanGoBack())
            {
                DoGoBack();
                _items.RemoveAt(_selectedIndex + 1);
            }
            else
            {
                _items.RemoveAt(_selectedIndex);
                _selectedIndex.Value = -1;
            }

            if (_items.Count == 0)
                OnEmpty?.Invoke();
        }

        public event Action OnEmpty;

        public DefectFormViewModel SelectedItem => _selectedIndex < 0 ? null : _items[_selectedIndex];


        public DefectFormBrowserViewModel(IEnumerable<DefectFormViewModel> forms)
        {
            _items.AddRange(forms);
            _selectedIndex.Value = 0;
        }

        public void AddToItems(DefectFormViewModel[] newSet)
        {
            _items.AddRange(newSet);
            if (_selectedIndex < 0) _selectedIndex.Value = 0;
        }
    }
}
