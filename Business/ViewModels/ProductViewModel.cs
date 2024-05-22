using Model.Context;
using Model.Entities;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ViewModel.Dtos;

namespace ViewModel.ViewModels
{
    public class ProductViewModel : ViewModelBase
    {
        private ObservableCollection<Product>? _products;
        private Product? _selectedProduct;
        private bool _isCreating = true;
        private bool _isProductSelected;

        public ObservableCollection<Product> Products
        {
            get => _products ??= new ObservableCollection<Product>();
            set
            {
                _products = value;
                OnPropertyChange(nameof(Products));
            }
        }

        public Product SelectedProduct
        {
            get => _selectedProduct ??= new Product();
            set
            {
                _selectedProduct = value;
                IsProductSelected = !(SelectedProduct.Id == default);
                OnPropertyChange(nameof(SelectedProduct));
            }
        }

        public bool IsCreating
        {
            get => _isCreating;
            set
            {
                _isCreating = value;
                OnPropertyChange(nameof(IsCreating));
            }
        }

        public bool IsProductSelected
        {
            get => _isProductSelected;
            set
            {
                _isProductSelected = value;
                OnPropertyChange(nameof(IsProductSelected));
            }
        }

        public ProductViewModel()
        {
            IsLoading = true;
            Reload();
            IsLoading = false;
        }

        private ICommand? _addCommand;

        public ICommand AddCommand => _addCommand ??= new CommandBase(async () =>
        {
            IsLoading = true;
            //TODO: Handle add product
            if (Validate())
            {
                using var context = new DatabaseContext();
                await context.Products.CreateAsync(SelectedProduct);
                await context.SaveChangesAsync();
                Reload();
                SelectedProduct = new();
            }
            IsLoading = false;
        });

        private ICommand? _updateCommand;

        public ICommand UpdateCommand => _updateCommand ??= new CommandBase(async () =>
        {
            //TODO: Handle update product
            IsLoading = true;
            if (Validate())
            {
                using var context = new DatabaseContext();
                await context.Products.UpdateAsync(SelectedProduct);
                await context.SaveChangesAsync();
                var currentProduct = SelectedProduct;
                Reload();
                SelectedProduct = currentProduct;
            }
            IsLoading = false;
        });

        private ICommand? _deleteCommand;

        public ICommand DeleteCommand => _deleteCommand ??= new CommandBase(async () =>
        {
            IsLoading = true;
            //TOTO: Handle delete product
            using var context = new DatabaseContext();
            await context.Products.DeleteAsync(SelectedProduct);
            await context.SaveChangesAsync();
            Reload();
            IsLoading = false;
        });

        private void Reload()
        {
            using var context = new DatabaseContext();
            var query = context.Products.NoTracking();
            Products.FromEnumerable(query);
        }

        private bool Validate()
        {
            Errors.Clear();
            return !IsInvalid;
        }
    }
}
